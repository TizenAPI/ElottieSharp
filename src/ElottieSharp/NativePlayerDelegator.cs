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
using System.IO;

namespace ElottieSharp
{
    static class NativePlayerDelegator
    {
        static bool UseRlottie = File.Exists("/usr/lib/librlottie.so.0");

        public static IntPtr InvokeSetAnimationFile(string file)
        {
            if (UseRlottie)
                return Interop.Rlottie.lottie_animation_from_file(file);
            else
                return Interop.LottiePlayer.lottie_animation_from_file(file);
        }

        public static IntPtr InvokeSetAnimationData(string data, string key)
        {
            if (UseRlottie)
                return Interop.Rlottie.lottie_animation_from_data(data, key);
            else
                return Interop.LottiePlayer.lottie_animation_from_data(data, key);
        }

        public static Action<IntPtr> InvokeAnimationDestroy
        {
            get
            {
                if (UseRlottie)
                    return Interop.Rlottie.lottie_animation_destroy;
                else
                    return Interop.LottiePlayer.lottie_animation_destroy;
            }
        }

        public static void InvokeAnimationGetSize(IntPtr animation, out int w, out int h)
        {
            if (UseRlottie)
                Interop.Rlottie.lottie_animation_get_size(animation, out w, out h);
            else
                Interop.LottiePlayer.lottie_animation_get_size(animation, out w, out h);
        }

        public static double InvokeAnimationGetDuration(IntPtr animation)
        {
            if (UseRlottie)
                return Interop.Rlottie.lottie_animation_get_duration(animation);
            else
                return Interop.LottiePlayer.lottie_animation_get_duration(animation);
        }

        public static int InvokeAnimationGetTotalFrame(IntPtr animation)
        {
            if (UseRlottie)
                return Interop.Rlottie.lottie_animation_get_totalframe(animation);
            else
                return Interop.LottiePlayer.lottie_animation_get_totalframe(animation);
        }

        public static int InvokeAnimationGetFrameRate(IntPtr animation)
        {
            if (UseRlottie)
                return Interop.Rlottie.lottie_animation_get_framerate(animation);
            else
                return Interop.LottiePlayer.lottie_animation_get_framerate(animation);
        }

        public static int InvokeAnimationGetFrameRate(IntPtr animation, float pos)
        {
            if (UseRlottie)
                return Interop.Rlottie.lottie_animation_get_frame_at_pos(animation, pos);
            else
                return Interop.LottiePlayer.lottie_animation_get_frame_at_pos(animation, pos);
        }

        public static Action<IntPtr, int, int, int> InvokeAnimationPrepareFrame
        {
            get
            {
                if (UseRlottie)
                    return Interop.Rlottie.lottie_animation_prepare_frame;
                else
                    return Interop.LottiePlayer.lottie_animation_prepare_frame;
            }
        }

        public static Action<IntPtr, int, IntPtr, int, int, int> InvokeAnimationRenderAsync
        {
            get
            {
                if (UseRlottie)
                    return Interop.Rlottie.lottie_animation_render_async;
                else
                    return Interop.LottiePlayer.lottie_animation_render_async;
            }
        }

        public static Action<IntPtr> InvokeAnimationRenderFlush
        {
            get
            {
                if (UseRlottie)
                    return Interop.Rlottie.lottie_animation_render_flush;
                else
                    return Interop.LottiePlayer.lottie_animation_render_flush;
            }
        }
    }
}