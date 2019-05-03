using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Toolkit.Uwp.UI.Animations;


namespace Fluent365.Controls
{
    public sealed class OfficeTile : ContentControl
    {
        public static readonly DependencyProperty AppNameProperty = DependencyProperty.Register(nameof(AppName), typeof(string), typeof(OfficeTile), new PropertyMetadata(""));

        public string AppName
        {
            get { return (string)GetValue(AppNameProperty); }
            set { SetValue(AppNameProperty, value); }
        }

        public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(nameof(IconPath), typeof(string), typeof(OfficeTile), new PropertyMetadata(""));

        public string IconPath
        {
            get { return (string)GetValue(IconPathProperty); }
            set { SetValue(IconPathProperty, value); }
        }

        public static readonly DependencyProperty TileColorProperty = DependencyProperty.Register(nameof(TileColor), typeof(Color), typeof(OfficeTile), new PropertyMetadata(""));

        public Color TileColor
        {
            get { return (Color)GetValue(TileColorProperty); }
            set { SetValue(TileColorProperty, value); }
        }


        OfficeTile _officeTile;
        ThemeShadow _backgroundShadow;
        ThemeShadow _iconShadow;
        TextBlock _appTitle;
        Grid _backgroundShadowReceiverGrid;
        Grid _iconShadowReceiverGrid;
        Grid _tileGrid;
        Grid _rootGrid;
        Image _appIcon;

        public OfficeTile()
        {
            DefaultStyleKey = typeof(OfficeTile); 
        }

        protected override void OnApplyTemplate()
        {
            
               _officeTile = (OfficeTile)this;
            _backgroundShadowReceiverGrid = (Grid)_officeTile.GetTemplateChild("BackgroundShadowReceiverGrid");
            _iconShadowReceiverGrid = (Grid)_officeTile.GetTemplateChild("IconShadowReceiverGrid");
            _rootGrid = (Grid)_officeTile.GetTemplateChild("RootGrid");
            _backgroundShadow = (ThemeShadow)_rootGrid.Resources["BackgroundShadow"];
            _iconShadow = (ThemeShadow)_rootGrid.Resources["IconShadow"];

            _appTitle = (TextBlock)_officeTile.GetTemplateChild("AppTitle");
            _appIcon = (Image)_officeTile.GetTemplateChild("AppIcon");
            _tileGrid = (Grid)_officeTile.GetTemplateChild("TileGrid");


            _backgroundShadow.Receivers.Add(_backgroundShadowReceiverGrid);
            _iconShadow.Receivers.Add(_iconShadowReceiverGrid);
            _tileGrid.Background = new AcrylicBrush() { BackgroundSource = AcrylicBackgroundSource.Backdrop, FallbackColor = TileColor, TintColor = TileColor, TintOpacity = 0.8, TintTransitionDuration = new TimeSpan(0,0,0,0, 400) };

            _appIcon.Source = new BitmapImage() { UriSource = new Uri(IconPath) };
            base.OnApplyTemplate();

            Loaded -= OfficeTile_Loaded;
            Loaded += OfficeTile_Loaded;

            _tileGrid.PointerEntered += OfficeTile_PointerEntered;
            _tileGrid.PointerExited += OfficeTile_PointerExited;
        }

        private void OfficeTile_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            _appIcon.Translation = new Vector3(0, 0, 100);
            _tileGrid.Translation = new Vector3(0, 0, 16);


            var lightAnimationSet = _appIcon.Light(distance: 1500, duration: 350, delay: 0, color: Colors.White, easingMode: Windows.UI.Xaml.Media.Animation.EasingMode.EaseInOut);
            lightAnimationSet.Start();

            _appIcon.Offset(offsetX: 0f,
              offsetY: 0,
              duration: 350, delay: 0, easingMode: Windows.UI.Xaml.Media.Animation.EasingMode.EaseInOut).Start();

            _appTitle.Offset(offsetX: 0f,
                        offsetY: 20f,
                        duration: 250, delay: 75, easingMode: Windows.UI.Xaml.Media.Animation.EasingMode.EaseInOut).Start();

            _appTitle.Fade(value: 0f, duration: 350, delay: 0, easingMode: Windows.UI.Xaml.Media.Animation.EasingMode.EaseInOut).Start();
        }

        private void OfficeTile_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            _appIcon.Translation = new Vector3(0, 0, 120);
            _tileGrid.Translation = new Vector3(0, 0, 40);



            var lightAnimationSet = _appIcon.Light(distance: 50, duration: 350, delay: 0, color: Colors.White, easingMode: Windows.UI.Xaml.Media.Animation.EasingMode.EaseInOut);
            lightAnimationSet.Start();

            _appIcon.Offset(offsetX: 0f,
                      offsetY: -20f,
                      duration: 350, delay: 75, easingMode: Windows.UI.Xaml.Media.Animation.EasingMode.EaseInOut).Start();

            _appTitle.Offset(offsetX: 0f,
                         offsetY: -30,
                         duration: 250, delay: 0, easingMode: Windows.UI.Xaml.Media.Animation.EasingMode.EaseInOut).Start();

            _appTitle.Fade(value: 1f, duration: 350, delay: 0, easingMode: Windows.UI.Xaml.Media.Animation.EasingMode.EaseInOut).Start();


            


        }

        private void OfficeTile_Loaded(object sender, RoutedEventArgs e)
        {
           // throw new NotImplementedException();
        }
    }
}
