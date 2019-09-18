using Xamarin.Forms;
using ElottieSharp.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace ElottieFormsGallery
{
    public class MainPageCS : IndexPage
    {
        public MainPageCS()
        {
            ItemTemplate = new DataTemplate(() =>
            {
                var animationView = new ElottieAnimationView
                {
                    AutoRepeat = true,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                animationView.SetBinding(ElottieAnimationView.AnimationFileProperty, "AnimationFile");

                var contentPage = new ContentPage
                {
                    Content = new StackLayout
                    {
                        Children = { animationView }
                    }
                };

                contentPage.Appearing += (s, e) =>
                {
                    animationView.Play();
                };

                contentPage.Disappearing += (s, e) =>
                {
                    if (animationView.IsPlaying)
                        animationView.Stop();
                };

                return contentPage;
            });
            ItemsSource = LottieDataModel.All;
        }
    }
}
