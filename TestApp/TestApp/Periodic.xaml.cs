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

        public Periodic()
        {
            InitializeComponent();
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


    }
}
