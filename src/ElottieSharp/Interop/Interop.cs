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
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static class Libraries
    {
        internal const string Ecore = "libecore.so.1";
        internal const string Evas = "libevas.so.1";
        internal const string LottiePlayer = "liblottie-player.so.0";
        internal const string Rlottie = "librlottie.so.0";
    }

    internal static partial class Ecore
    {
        internal delegate bool EcoreTimelineCallback(IntPtr data, double pos);

        [DllImport(Libraries.Ecore)]
        internal static extern IntPtr ecore_animator_timeline_add(double runtime, EcoreTimelineCallback func, IntPtr data);

        [DllImport(Libraries.Ecore)]
        internal static extern void ecore_animator_freeze(IntPtr animator);

        [DllImport(Libraries.Ecore)]
        internal static extern void ecore_animator_thaw(IntPtr animator);

        [DllImport(Libraries.Ecore)]
        internal static extern IntPtr ecore_animator_del(IntPtr animator);
    }

    internal static partial class Evas
    {
        [DllImport(Libraries.Evas)]
        internal static extern IntPtr evas_object_evas_get(IntPtr obj);

        [DllImport(Libraries.Evas)]
        internal static extern IntPtr evas_object_image_add(IntPtr obj);

        [DllImport(Libraries.Evas)]
        internal static extern IntPtr evas_object_image_filled_add(IntPtr obj);

        [DllImport(Libraries.Evas)]
        internal static extern void evas_object_image_filled_set(IntPtr obj, bool setting);

        [DllImport(Libraries.Evas)]
        internal static extern void evas_object_image_alpha_set(IntPtr obj, bool has_alpha);

        [DllImport(Libraries.Evas)]
        internal static extern void evas_object_image_size_get(IntPtr obj, out int w, out int h);

        [DllImport(Libraries.Evas)]
        internal static extern void evas_object_image_size_set(IntPtr obj, int w, int h);

        [DllImport(Libraries.Evas)]
        internal static extern IntPtr evas_object_image_data_get(IntPtr obj, bool forWriting);

        [DllImport(Libraries.Evas)]
        internal static extern void evas_object_image_data_set(IntPtr obj, IntPtr data);

        [DllImport(Libraries.Evas)]
        internal static extern void evas_object_image_data_update_add(IntPtr obj, int x, int y, int w, int h);

        [DllImport(Libraries.Evas)]
        internal static extern int evas_object_image_stride_get(IntPtr obj);
    }

    internal static partial class LottiePlayer
    {
        [DllImport(Libraries.LottiePlayer)]
        internal static extern IntPtr lottie_animation_from_file(string file);

        [DllImport(Libraries.LottiePlayer)]
        internal static extern IntPtr lottie_animation_from_data(string data, string key);

        [DllImport(Libraries.LottiePlayer)]
        internal static extern void lottie_animation_destroy(IntPtr animation);

        [DllImport(Libraries.LottiePlayer)]
        internal static extern void lottie_animation_get_size(IntPtr animation, out int w, out int h);

        [DllImport(Libraries.LottiePlayer)]
        internal static extern double lottie_animation_get_duration(IntPtr animation);

        [DllImport(Libraries.LottiePlayer)]
        internal static extern int lottie_animation_get_totalframe(IntPtr animation);

        [DllImport(Libraries.LottiePlayer)]
        internal static extern int lottie_animation_get_framerate(IntPtr animation);

        [DllImport(Libraries.LottiePlayer)]
        internal static extern int lottie_animation_get_frame_at_pos(IntPtr animation, float pos);

        [DllImport(Libraries.LottiePlayer)]
        internal static extern void lottie_animation_prepare_frame(IntPtr animation, int frameNo, int w, int h);

        [DllImport(Libraries.LottiePlayer)]
        internal static extern void lottie_animation_render_async(IntPtr animation, int frameNo, IntPtr buffer, int w, int h, int bytesPerLine);

        [DllImport(Libraries.LottiePlayer)]
        internal static extern void lottie_animation_render_flush(IntPtr animation);
    }

    internal static partial class Rlottie
    {
        [DllImport(Libraries.Rlottie)]
        internal static extern IntPtr lottie_animation_from_file(string file);

        [DllImport(Libraries.Rlottie)]
        internal static extern IntPtr lottie_animation_from_data(string data, string key);

        [DllImport(Libraries.Rlottie)]
        internal static extern void lottie_animation_destroy(IntPtr animation);

        [DllImport(Libraries.Rlottie)]
        internal static extern void lottie_animation_get_size(IntPtr animation, out int w, out int h);

        [DllImport(Libraries.Rlottie)]
        internal static extern double lottie_animation_get_duration(IntPtr animation);

        [DllImport(Libraries.Rlottie)]
        internal static extern int lottie_animation_get_totalframe(IntPtr animation);

        [DllImport(Libraries.Rlottie)]
        internal static extern int lottie_animation_get_framerate(IntPtr animation);

        [DllImport(Libraries.Rlottie)]
        internal static extern int lottie_animation_get_frame_at_pos(IntPtr animation, float pos);

        [DllImport(Libraries.Rlottie)]
        internal static extern void lottie_animation_prepare_frame(IntPtr animation, int frameNo, int w, int h);

        [DllImport(Libraries.Rlottie)]
        internal static extern void lottie_animation_render_async(IntPtr animation, int frameNo, IntPtr buffer, int w, int h, int bytesPerLine);

        [DllImport(Libraries.Rlottie)]
        internal static extern void lottie_animation_render_flush(IntPtr animation);
    }
}