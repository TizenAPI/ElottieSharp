
using Tizen.Applications;
using ElmSharp;
using ElottieSharp;
using System.IO;

namespace HelloElottieSharp
{
    class App : CoreUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window window = new Window("ElottieSharp")
            {
                AvailableRotations = DisplayRotation.Degree_0 | DisplayRotation.Degree_180 | DisplayRotation.Degree_270 | DisplayRotation.Degree_90
            };
            window.BackButtonPressed += (s, e) =>
            {
                Exit();
            };
            window.Show();

            var box = new Box(window)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
            };
            box.Show();

            var bg = new Background(window)
            {
                Color = Color.Transparent
            };
            bg.SetContent(box);

            var conformant = new Conformant(window);
            conformant.Show();
            conformant.SetContent(bg);

            Button button = new Button(window)
            {
                Text = "Button",
                AlignmentX = -1,
                WeightX = 1,
            };
            button.Show();

            LottieAnimationView lottie = new LottieAnimationView(window)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
                AutoPlay = true,
                AutoRepeat = true,
                MinimumProgress = 0.2f,
                MaximumProgress = 0.8f,
            };

            var path = Path.Combine(DirectoryInfo.Resource, "a_mountain.json");
            lottie.SetAnimation(path);
            lottie.Play();
            lottie.Show();
            lottie.FrameUpdated += (s, e) =>
            {
                button.Text = e.CurrentFrame+"";
            };

            button.Clicked += (s, e) =>
            {
                button.Text = lottie.GetFrameFromProgress(0.7f)+"";
                lottie.Stop();
            };

            Button button2 = new Button(window)
            {
                Text = "Play Segment",
                AlignmentX = -1,
                WeightX = 1
            };
            button2.Show();

            button2.Clicked += (s, e) =>
            {
                lottie.Play(30, 50); // lottie.Play(0.5f, 0.7f);
            };

            Button button3 = new Button(window)
            {
                Text = "Pause",
                AlignmentX = -1,
                WeightX = 1
            };
            button3.Show();

            button3.Clicked += (s, e) =>
            {
                lottie.Pause();
            };

            box.PackEnd(button);
            box.PackEnd(lottie);
            box.PackEnd(button2);
            box.PackEnd(button3);
        }

        static void Main(string[] args)
        {
            Elementary.Initialize();
            Elementary.ThemeOverlay();
            App app = new App();
            app.Run(args);
        }
    }
}
