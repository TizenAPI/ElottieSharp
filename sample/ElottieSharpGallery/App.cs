using System.IO;
using Tizen.Applications;
using ElmSharp;
using ElmSharp.Wearable;
using ElottieSharp;

namespace ElottieSharpGallery
{
    class App : CoreUIApplication
    {
        int _currentIndex = 0;
        static string[] _files = { "a_mountain.json", "cooking.json", "done.json","emoji_wink.json", "fingerprint_success.json",
            "heart.json", "icon_animation.json", "like.json", "loading.json", "maps.json"};
        LottieAnimationView[] _views = new LottieAnimationView[_files.Length];

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window window = new Window("ElottieSharpGallery")
            {
                AvailableRotations = DisplayRotation.Degree_0 | DisplayRotation.Degree_180 | DisplayRotation.Degree_270 | DisplayRotation.Degree_90
            };
            window.BackButtonPressed += (s, e) =>
            {
                Exit();
            };
            window.Show();

            var conformant = new Conformant(window);
            conformant.Show();

            var surface = new CircleSurface(conformant);
            CircleScroller circleScroller = new CircleScroller(conformant, surface)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
                VerticalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Invisible,
                HorizontalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Auto,
                ScrollBlock = ScrollBlock.Vertical,
                HorizontalPageScrollLimit = 1,
            };
            ((IRotaryActionWidget)circleScroller).Activate();
            circleScroller.SetPageSize(1.0, 1.0);
            circleScroller.Show();
            conformant.SetContent(circleScroller);

            var box = new Box(window)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
                IsHorizontal = true,
            };
            box.Show();
            circleScroller.SetContent(box);

            for (int i = 0; i <10; i++)
            {
                _views[i] = new LottieAnimationView(window)
                {
                    AlignmentX = -1,
                    AlignmentY = -1,
                    WeightX = 1,
                    WeightY = 1,
                    AutoRepeat = true,
                    MinimumWidth = window.ScreenSize.Width,
                };
                _views[i].Show();

                var path = Path.Combine(DirectoryInfo.Resource, _files[i]);
                _views[i].SetAnimation(path);

                if (i==0)
                    _views[i].Play();

                box.PackEnd(_views[i]);
            }

            circleScroller.Scrolled += (s, e) =>
            {
                if (_currentIndex != circleScroller.HorizontalPageIndex)
                {
                    int oldIndex = _currentIndex;
                    _currentIndex = circleScroller.HorizontalPageIndex;
                    EcoreMainloop.Post(() =>
                    {
                        if (_views[oldIndex].IsPlaying)
                            _views[oldIndex].Stop();
                        _views[_currentIndex].Play();
                    });
                }
            };
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
