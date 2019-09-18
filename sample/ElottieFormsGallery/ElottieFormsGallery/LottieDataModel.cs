using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ElottieFormsGallery
{
    public class LottieDataModel
    {
        public string AnimationFile { get; set; }

        public static IList<LottieDataModel> All { set; get; }

        static LottieDataModel()
        {
            All = new ObservableCollection<LottieDataModel> {
                new LottieDataModel
                {
                    AnimationFile = "a_mountain.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "cooking.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "done.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "emoji_wink.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "fingerprint_success.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "heart.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "icon_animation.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "like.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "loading.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "maps.json"
                }
            };
        }
    }
}