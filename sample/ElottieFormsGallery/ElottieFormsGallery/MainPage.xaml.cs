using System;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;
using ElottieSharp.Forms;

namespace ElottieFormsGallery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : IndexPage
    {
        public MainPage()
        {
            InitializeComponent();
            ItemsSource = LottieDataModel.All;
        }

        void OnPageAppearing(object sender, EventArgs args)
        {
            ContentPage page = (ContentPage)sender;
            StackLayout layout = (StackLayout)page.Content;
            ElottieAnimationView animationView = (ElottieAnimationView)layout.Children[0];
            animationView.Play();
        }

        void OnPageDisappearing(object sender, EventArgs args)
        {
            ContentPage page = (ContentPage)sender;
            StackLayout layout = (StackLayout)page.Content;
            ElottieAnimationView animationView = (ElottieAnimationView)layout.Children[0];
            if (animationView.IsPlaying)
                animationView.Stop();

        }
    }
}