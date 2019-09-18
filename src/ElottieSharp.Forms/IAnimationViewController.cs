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
using Xamarin.Forms;

namespace ElottieSharp.Forms
{
    public interface IAnimationViewController : IViewController
    {
        event EventHandler<PlayRequestedEventArgs> PlayRequested;
        event EventHandler StopRequested;
        event EventHandler PauseRequested;
        event EventHandler<SeekRequestedEventArgs> SeekRequested;

        void SendStarted();
        void SendStopped();
        void SendPaused();
        void SendFinished();
        void SendFrameUpdated(FrameEventArgs args);
        void SendAnimationInitialized(AnimationInitializedEventArgs args);
    }
}