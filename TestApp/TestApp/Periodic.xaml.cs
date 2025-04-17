using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TestApp
{
    public partial class Periodic : Window
    {
        private double scale = 1.0;
        private const double ZoomStep = 0.1;
        private const double MinScale = 0.5;
        private const double MaxScale = 3.0;
        private static readonly Duration AnimationDuration = TimeSpan.FromSeconds(0.2);
        private static readonly IEasingFunction Easing = new CubicEase { EasingMode = EasingMode.EaseOut };
        private bool _isClosingAnimationCompleted = false;

        public Periodic()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
            BeginAnimation(OpacityProperty, fadeInAnimation);
        }

        // Плавное закрытие окна
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_isClosingAnimationCompleted)
            {
                e.Cancel = true; // отменяем закрытие, пока не завершится анимация

                DoubleAnimation fadeOutAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));
                fadeOutAnimation.Completed += (s, a) =>
                {
                    _isClosingAnimationCompleted = true;
                    Close(); // вызываем закрытие окна повторно, после завершения анимации
                };

                BeginAnimation(OpacityProperty, fadeOutAnimation);
            }
        }


        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            if (scale < MaxScale)
            {
                scale = Math.Min(scale + ZoomStep, MaxScale);
                ApplyZoom();
            }
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            if (scale > MinScale)
            {
                scale = Math.Max(scale - ZoomStep, MinScale);
                ApplyZoom();
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void ApplyZoom()
        {
            AnimateScaleProperty(imageScale, ScaleTransform.ScaleXProperty, scale);
            AnimateScaleProperty(imageScale, ScaleTransform.ScaleYProperty, scale);
        }

        private void AnimateScaleProperty(ScaleTransform target, DependencyProperty property, double toValue)
        {
            var animation = new DoubleAnimation
            {
                To = toValue,
                Duration = AnimationDuration,
                EasingFunction = Easing
            };
            target.BeginAnimation(property, animation);
        }



        private bool isOriginalImage = true;

        private void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            if (isOriginalImage)
            {
                zoomImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Solubility.jpg"));
            }
            else
            {
                zoomImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Periodic.jpg"));
            }

            isOriginalImage = !isOriginalImage;
        }


    }
}
