/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Tizen;
using ElmSharp;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;

namespace ElottieSharp
{
    /// <summary>
    /// A Lottie animation view
    /// </summary>
    public class LottieAnimationView : EvasObject
    {
        public static string Tag { get; set; } = "Elottie";

        static bool s_UseRlottie = File.Exists("/usr/lib/libRlottie-player.so.0");

        IntPtr _animation = IntPtr.Zero;
        IntPtr _animator = IntPtr.Zero;
        EvasObjectEvent _showed;
        EvasObjectEvent _hid;
        double _speed = 1;
        float _minProgress = 0;
        float _maxProgress = 1;
        float _currentProgress;
        float _startProgress;

        float _fromProgress;
        float _toProgress;

        /// <summary>
        /// Initializes a new instance of the LottieAnimationView class.
        /// </summary>
        /// <param name="parent"></param>
        public LottieAnimationView(EvasObject parent) : base(parent)
        {
            Shown += OnShown;
            Hid += OnHid;
        }

        /// <summary>
        /// Occurs when the view is shown.
        /// </summary>
        public event EventHandler Shown
        {
            add { _showed.On += value; }
            remove { _showed.On -= value; }
        }

        /// <summary>
        /// /// Occurs when the view is hid.
        /// </summary>
        public event EventHandler Hid
        {
            add { _hid.On += value; }
            remove { _hid.On -= value; }
        }

        /// <summary>
        /// Occurs when the animation is started.
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Occurs when the animation is stopped.
        /// </summary>
        public event EventHandler Stopped;

        /// <summary>
        /// Occurs when the animation is paused.
        /// </summary>
        public event EventHandler Paused;

        /// <summary>
        /// Occurs when the animation is finished.
        /// </summary>
        public event EventHandler Finished;

        /// <summary>
        /// Occurs when the frame is updated.
        /// </summary>
        public event EventHandler<FrameEventArgs> FrameUpdated;

        /// <summary>
        /// Gets or sets a value indicating whether this animation is play automatically or not.
        /// </summary>
        [DefaultValue(false)]
        public bool AutoPlay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this animation is repeated automatically or not.
        /// </summary>
        [DefaultValue(false)]
        public bool AutoRepeat { get; set; }

        /// <summary>
        /// Gets or sets the playback speed.
        /// </summary>
        [DefaultValue(1)]
        public double Speed
        {
            get => _speed;
            set
            {
                _speed = value < 0 ? 0 : value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum progress of animation.
        /// </summary>
        [DefaultValue(0)]
        public float MinimumProgress
        {
            get => _minProgress;
            set
            {
                _minProgress = Clamp(value, 0, 1);
            }
        }

        /// <summary>
        /// /// Gets or sets the maximum progress of animation.
        /// </summary>
        [DefaultValue(1)]
        public float MaximumProgress
        {
            get => _maxProgress;
            set
            {
                _maxProgress = Clamp(value, 0, 1);
            }
        }

        /// <summary>
        /// Gets the current frame count.
        /// </summary>
        public int CurrentFrame { get; private set; }

        /// <summary>
        /// Gets the total frame count.
        /// </summary>
        public int TotalFrame { get; private set; }

        /// <summary>
        /// Gets the duraion time.
        /// </summary>
        public double DurationTime { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this animation is played currently.
        /// </summary>
        public bool IsPlaying { get; private set; }

        /// <summary>
        /// Gets or sets the desired size of animation.
        /// </summary>
        public Size Size
        {
            get
            {
                int w, h;
                Interop.Evas.evas_object_image_size_get(RealHandle, out w, out h);
                return new Size(w, h);
            }
            set
            {
                Interop.Evas.evas_object_image_size_set(RealHandle, value.Width, value.Height);
            }
        }

        /// <summary>
        /// Sets the animation file.
        /// </summary>
        /// <param name="file">The animation file to play.</param>
        public void SetAnimation(string file)
        {
            if (_animation != IntPtr.Zero)
            {
                NativePlayerDelegator.InvokeAnimationDestroy(_animation);
                _animation = IntPtr.Zero;
            }

            _animation = NativePlayerDelegator.InvokeSetAnimationFile(file);
            if (_animation != IntPtr.Zero)
            {
                CurrentFrame = -1;
                TotalFrame = NativePlayerDelegator.InvokeAnimationGetTotalFrame(_animation);
                DurationTime = NativePlayerDelegator.InvokeAnimationGetDuration(_animation);
                Log.Error(Tag, "SetAnimation - file :" + file + ", DurationTime:" + DurationTime + " , TotalFrame:" + TotalFrame);
            }
            else
            {
                throw new InvalidOperationException("Failed to create animation from file");
            }
        }

        /// <summary>
        /// Play the animation.
        /// </summary>
        public void Play()
        {
            Play(MinimumProgress, MaximumProgress);
        }

        /// <summary>
        /// Play the animation for the given frame segment.
        /// </summary>
        /// <param name="from">The frame number to start</param>
        /// <param name="to">The frame number to end</param>
        public void Play(int from, int to)
        {
            if (from < 0 || to > TotalFrame || from > to)
            {
                throw new ArgumentException($"Parameters are not valid. {nameof(from)}:" + from + ", {nameof(to)}:" + to);
            }
            Play((float)from/TotalFrame, (float)to/TotalFrame);
        }

        /// <summary>
        /// Play the animation for the given progress segment.
        /// </summary>
        /// <param name="from">The progress to start</param>
        /// <param name="to">The progress to end</param>
        public void Play(float from, float to)
        {
            if (_animation == IntPtr.Zero)
            {
                throw new InvalidOperationException("Should set the animation before invoking Play().");
            }

            if (_animator != IntPtr.Zero)
            {
                Log.Warn(Tag, "It is already playing.");
                return;
            }

            if (from >= to)
            {
                throw new ArgumentException($"Parameter {nameof(to)} should be larger than {nameof(from)}.", nameof(to));
            }

            if (from < MinimumProgress)
            {
                throw new ArgumentException($"Parameter {nameof(from)} should be larger than {nameof(MinimumProgress)}.", nameof(from));
            }

            _fromProgress = Clamp(from, MinimumProgress, MaximumProgress);
            _toProgress = Clamp(to, MinimumProgress, MaximumProgress);


            _startProgress = IsPlaying ? _fromProgress : _currentProgress;
            _animator = Interop.Ecore.ecore_animator_timeline_add(DurationTime / Speed, AnimatorCallback, IntPtr.Zero);
            Debug.Assert(_animator != IntPtr.Zero, "Failed to create animator");

            IsPlaying = true;
            Started?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Stop the animation.
        /// </summary>
        public void Stop()
        {
            if (_animation == IntPtr.Zero)
                throw new InvalidOperationException("Should set the animation before invoking Pause().");

            EnsureAnimatorDeleted();
            IsPlaying = false;
            Stopped?.Invoke(this, EventArgs.Empty);
            SeekTo(MinimumProgress);
        }

        /// <summary>
        /// Pause the animation.
        /// </summary>
        public void Pause()
        {
            if (_animation == IntPtr.Zero)
                throw new InvalidOperationException("Should set the animation before invoking Pause().");

            if (!IsPlaying)
            {
                Log.Warn(Tag, "Should play the animation before invoking Pause().");
                return;
            }

            EnsureAnimatorDeleted();
            IsPlaying = false;
            Paused?.Invoke(this, EventArgs.Empty);
            SeekTo(_currentProgress);
        }

        /// <summary>
        /// Get the frame count from progress.
        /// </summary>
        /// <param name="progress">The progress (0~1.0) to get frame count.</param>
        /// <returns></returns>
        public int GetFrameFromProgress(float progress)
        {
            if (_animation == IntPtr.Zero)
                throw new InvalidOperationException("Should set the animation before invoking GetFrameFromProgress().");

            return (int)(progress * TotalFrame);
        }

        protected override IntPtr CreateHandle(EvasObject parent)
        {
            IntPtr evas = Interop.Evas.evas_object_evas_get(parent.Handle);
            var evasImage = Interop.Evas.evas_object_image_add(evas);
            Interop.Evas.evas_object_image_filled_set(evasImage, true);
            Interop.Evas.evas_object_image_alpha_set(evasImage, true);
            _showed = new EvasObjectEvent(this, evasImage, EvasObjectCallbackType.Show);
            _hid = new EvasObjectEvent(this, evasImage, EvasObjectCallbackType.Hide);
            //FIXME
            Interop.Evas.evas_object_image_size_set(evasImage, 360, 360);
            return evasImage;
        }

        protected override void OnUnrealize()
        {
            EnsureAnimatorDeleted();
            if (_animation != IntPtr.Zero)
            {
                NativePlayerDelegator.InvokeAnimationDestroy(_animation);
                _animation = IntPtr.Zero;
            }
            base.OnUnrealize();
        }

        /// <summary>
        /// Seeks to specified progress.
        /// </summary>
        /// <param name="progress">The progress (0 ~ 1.0) from the start to seek to</param>
        public void SeekTo(float progress)
        {
            if (_animation == IntPtr.Zero)
                throw new InvalidOperationException("Should set the animation before invoking Seek().");

            progress = Clamp(progress, MinimumProgress, MaximumProgress);
            _currentProgress = progress;

            int frame = (int)(progress * TotalFrame);

            if (CurrentFrame == frame)
                return;

            CurrentFrame = frame;
            FrameUpdated?.Invoke(this, new FrameEventArgs(frame));

            IntPtr buffer = Interop.Evas.evas_object_image_data_get(RealHandle, true);
            int imageRowStride = Interop.Evas.evas_object_image_stride_get(RealHandle);

            int w = Size.Width;
            int h = Size.Height;
            NativePlayerDelegator.InvokeAnimationRenderAsync(_animation, CurrentFrame, buffer, w, h, imageRowStride);
            NativePlayerDelegator.InvokeAnimationRenderFlush(_animation);

            Interop.Evas.evas_object_image_data_set(RealHandle, buffer);
            Interop.Evas.evas_object_image_data_update_add(RealHandle, 0, 0, w, h);
        }

        bool AnimatorCallback(IntPtr data, double progress)
        {
            float nextPos = _startProgress + (float)progress;
            nextPos = Clamp(nextPos, _fromProgress, _toProgress);

            SeekTo(nextPos);

            if ((float)progress >= _toProgress)
            {
                EnsureAnimatorDeleted();
                _startProgress = _fromProgress;

                Log.Info(Tag, "Finished");
                Finished?.Invoke(this, EventArgs.Empty);
                if (AutoRepeat)
                {
                    Play(_fromProgress,_toProgress);
                }
                return false;
            }
            return true;
        }

        void OnShown(object sender, EventArgs e)
        {
            Log.Info(Tag, "OnShown");
            if (AutoPlay && _animation != IntPtr.Zero)
                Play();

        }

        void OnHid(object sender, EventArgs e)
        {
            Log.Info(Tag, "OnHid");
            Stop();
        }

        void EnsureAnimatorDeleted()
        {
            if (_animator != IntPtr.Zero)
            {
                Interop.Ecore.ecore_animator_del(_animator);
                _animator = IntPtr.Zero;
            }
        }

        float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}