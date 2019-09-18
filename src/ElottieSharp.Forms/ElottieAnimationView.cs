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
using System.ComponentModel;
using Xamarin.Forms;

namespace ElottieSharp.Forms
{
    public class ElottieAnimationView : View, IAnimationViewController
    {
        public static readonly BindableProperty AutoPlayProperty = BindableProperty.Create(nameof(AutoPlay), typeof(bool), typeof(ElottieAnimationView), default(bool));

        public static readonly BindableProperty AutoRepeatProperty = BindableProperty.Create(nameof(AutoRepeat), typeof(bool), typeof(ElottieAnimationView), default(bool));

        public static readonly BindableProperty SpeedProperty = BindableProperty.Create(nameof(Speed), typeof(double), typeof(ElottieAnimationView), 1d);

        public static readonly BindableProperty MinimumProgressProperty = BindableProperty.Create(nameof(MinimumProgress), typeof(float), typeof(ElottieAnimationView), 0f);

        public static readonly BindableProperty MaximumProgressProperty = BindableProperty.Create(nameof(MaximumProgress), typeof(float), typeof(ElottieAnimationView), 1f);

        public static readonly BindableProperty AnimationFileProperty = BindableProperty.Create(nameof(AnimationFile), typeof(string), typeof(ElottieAnimationView), default(string));

        public bool AutoPlay
        {
            get { return (bool)GetValue(AutoPlayProperty); }
            set { SetValue(AutoPlayProperty, value); }
        }

        public bool AutoRepeat
        {
            get { return (bool)GetValue(AutoRepeatProperty); }
            set { SetValue(AutoRepeatProperty, value); }
        }

        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        public float MinimumProgress
        {
            get { return (float)GetValue(MinimumProgressProperty); }
            set { SetValue(MinimumProgressProperty, value); }
        }

        public float MaximumProgress
        {
            get { return (float)GetValue(MaximumProgressProperty); }
            set { SetValue(MaximumProgressProperty, value); }
        }

        public string AnimationFile
        {
            get { return (string)GetValue(AnimationFileProperty); }
            set { SetValue(AnimationFileProperty, value); }
        }

        public event EventHandler Started;

        public event EventHandler Stopped;

        public event EventHandler Paused;

        public event EventHandler Finished;

        public event EventHandler<FrameEventArgs> FrameUpdated;

        public event EventHandler<AnimationInitializedEventArgs> AnimationInitialized;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler<PlayRequestedEventArgs> PlayRequested;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler StopRequested;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler PauseRequested;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler<SeekRequestedEventArgs> SeekRequested;

        public int CurrentFrame { get; private set; }

        public int TotalFrame { get; private set; }

        public double DurationTime { get; private set; }

        public bool IsPlaying { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendStarted() => Started?.Invoke(this, EventArgs.Empty);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendStopped() => Stopped?.Invoke(this, EventArgs.Empty);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendPaused() => Paused?.Invoke(this, EventArgs.Empty);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendFinished() => Finished?.Invoke(this, EventArgs.Empty);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendFrameUpdated(FrameEventArgs args)
        {
            CurrentFrame = args.CurrentFrame;
            IsPlaying = true;
            FrameUpdated?.Invoke(this, args);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendAnimationInitialized(AnimationInitializedEventArgs args)
        {
            CurrentFrame = 0;
            DurationTime = args.DurationTime;
            TotalFrame = args.TotalFrame;
            IsPlaying = args.IsPlaying;
            OnAnimationFileReady();
            AnimationInitialized?.Invoke(this, args);
        }

        public void Play() => PlayRequested?.Invoke(this, new PlayRequestedEventArgs());

        public void Play(int from, int to) => PlayRequested?.Invoke(this, new PlayRequestedEventArgs(from, to));

        public void Play(float from, float to) => PlayRequested?.Invoke(this, new PlayRequestedEventArgs(from, to));

        public void Stop()
        {
            IsPlaying = false;
            StopRequested?.Invoke(this, EventArgs.Empty);
        }

        public void Pause()
        {
            IsPlaying = false;
            PauseRequested?.Invoke(this, EventArgs.Empty);
        }

        public void SeekTo(float progress) => SeekRequested?.Invoke(this, new SeekRequestedEventArgs(progress));

        protected virtual void OnAnimationFileReady()
        {
        }
    }
}
