using Xamarin.Forms;

namespace ElottieFormsGallery
{
    public class App : Application
    {
        public App()
        {
            //XAML
            MainPage = new MainPage();
            //C#
            //MainPage = new MainPageCS();


        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
