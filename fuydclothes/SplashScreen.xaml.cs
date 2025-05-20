using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace fuydclothes
{
    /// <summary>
    /// SplashScreen.xaml etkileşim mantığı
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            this.Loaded += SplashScreen_Loaded;
        }

        private async void SplashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1200);

            var blurEffect = new BlurEffect { Radius = 0 };
            this.Effect = blurEffect;

            var transform = new TranslateTransform(0, 0);
            this.RenderTransform = transform;

            // Animasyonlar
            var slideDown = new DoubleAnimation(0, 100, TimeSpan.FromSeconds(1))
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1))
            {
                EasingFunction = new QuadraticEase()
            };

            var blurAnim = new DoubleAnimation(0, 12, TimeSpan.FromSeconds(1))
            {
                EasingFunction = new QuadraticEase()
            };

            fadeOut.Completed += (s, _) =>
            {
                var mainWindow = new MainWindow();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                this.Close();
            };

            transform.BeginAnimation(TranslateTransform.YProperty, slideDown);
            this.BeginAnimation(Window.OpacityProperty, fadeOut);
            blurEffect.BeginAnimation(BlurEffect.RadiusProperty, blurAnim);
        }
    }
}