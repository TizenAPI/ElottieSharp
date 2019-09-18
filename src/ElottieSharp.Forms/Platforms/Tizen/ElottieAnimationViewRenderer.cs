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

using ElottieSharp.Forms;
using ElottieSharp.Forms.Tizen;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using TForms = Xamarin.Forms.Forms;

[assembly: ExportRenderer(typeof(ElottieAnimationView), typeof(ElottieAnimationViewRenderer))]
namespace ElottieSharp.Forms.Tizen
{
    public class ElottieAnimationViewRenderer : ViewRenderer<ElottieAnimationView, LottieAnimationView>
    {

        IAnimationViewController ElementController => Element;

        public static void Init()
        {
            // needed because of this linker issue: https://bugzilla.xamarin.com/show_bug.cgi?id=31076
#pragma warning disable 0219
            var dummy = new ElottieAnimationViewRenderer();
#pragma warning restore 0219
        }

        public ElottieAnimationViewRenderer()
        {
            RegisterPropertyHandler(ElottieAnimationView.AutoPlayProperty, UpdateAutoPlay);
            RegisterPropertyHandler(ElottieAnimationView.AutoRepeatProperty, UpdateAutoRepeat);
            RegisterPropertyHandler(ElottieAnimationView.SpeedProperty, UpdateSpeed);
            RegisterPropertyHandler(ElottieAnimationView.MinimumProgressProperty, UpdateMinimumProgress);
            RegisterPropertyHandler(ElottieAnimationView.MaximumProgressProperty, UpdateMaximumProgress);
            RegisterPropertyHandler(ElottieAnimationView.AnimationFileProperty, UpdateAnimationFile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Control != null)
                {
                    Control.Started -= OnStarted;
                    Control.Stopped -= OnStopped;
                    Control.Paused -= OnPaused;
                    Control.Finished -= OnFinished;
                    Control.FrameUpdated -= OnFrameUpdated;
                }

                if (Element != null)
                {
                    Element.PlayRequested -= OnPlayRequested;
                    Element.StopRequested -= OnStopRequested;
                    Element.PauseRequested -= OnPauseRequested;
                    Element.SeekRequested -= OnSeekRequested;
                }
            }
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ElottieAnimationView> e)
        {
            if (Control == null)
            {
                SetNativeControl(new LottieAnimationView(TForms.NativeParent));
                Control.Started += OnStarted;
                Control.Stopped += OnStopped;
                Control.Paused += OnPaused;
                Control.Finished += OnFinished;
                Control.FrameUpdated += OnFrameUpdated;
            }

            if (e.OldElement != null)
            {
                e.OldElement.PlayRequested -= OnPlayRequested;
                e.OldElement.StopRequested -= OnStopRequested;
                e.OldElement.PauseRequested -= OnPauseRequested;
                e.OldElement.SeekRequested -= OnSeekRequested;
            }

            if (e.NewElement != null)
            {
                e.NewElement.PlayRequested += OnPlayRequested;
                e.NewElement.StopRequested += OnStopRequested;
                e.NewElement.PauseRequested += OnPauseRequested;
                e.NewElement.SeekRequested += OnSeekRequested;
            }
            base.OnElementChanged(e);
        }

        void UpdateAutoPlay(bool initialize)
        {
            if (initialize && !Element.AutoPlay)
                return;

            Control.AutoPlay = Element.AutoPlay;
        }

        void UpdateAutoRepeat(bool initialize)
        {
            if (initialize && !Element.AutoRepeat)
                return;

            Control.AutoRepeat = Element.AutoRepeat;
        }

        void UpdateSpeed(bool initialize)
        {
            if (initialize && Element.Speed == 1d)
                return;

            Control.Speed = Element.Speed;
        }

        void UpdateMinimumProgress(bool initialize)
        {
            if (initialize && Element.MinimumProgress == 0f)
                return;

            Control.MinimumProgress = Element.MinimumProgress;
        }

        void UpdateMaximumProgress(bool initialize)
        {
            if (initialize && Element.MaximumProgress == 1f)
                return;

            Control.MaximumProgress = Element.MaximumProgress;
        }

        void UpdateAnimationFile()
        {
            Control.SetAnimation(ResourcePath.GetPath(Element.AnimationFile));
            ElementController.SendAnimationInitialized(new AnimationInitializedEventArgs(Control.TotalFrame, Control.DurationTime, Control.IsPlaying));
            if (Element.AutoPlay)
            {
                Control.Play();
            }
        }

        void OnStarted(object sender, EventArgs e)
        {
            ElementController.SendStarted();
        }

        void OnStopped(object sender, EventArgs e)
        {
            ElementController.SendStopped();
        }

        void OnPaused(object sender, EventArgs e)
        {
            ElementController.SendPaused();
        }

        void OnFinished(object sender, EventArgs e)
        {
            ElementController.SendFinished();
        }

        void OnFrameUpdated(object sender, ElottieSharp.FrameEventArgs e)
        {
            ElementController.SendFrameUpdated(new FrameEventArgs(e.CurrentFrame));
        }

        void OnPlayRequested(object sender, PlayRequestedEventArgs e)
        {
            if (e.RequestType == PlayRequestType.Frame)
            {
                Control.Play(e.FrameFrom, e.FrameTo);
            }
            else if (e.RequestType == PlayRequestType.Progress)
            {
                Control.Play(e.ProgressFrom, e.ProgressTo);
            }
            else
            {
                Control.Play();
            }
        }

        void OnStopRequested(object sender, EventArgs e)
        {
            Control.Stop();
        }

        void OnPauseRequested(object sender, EventArgs e)
        {
            Control.Pause();
        }

        void OnSeekRequested(object sender, SeekRequestedEventArgs e)
        {
            Control.SeekTo(e.Progress);
        }
    }
}
