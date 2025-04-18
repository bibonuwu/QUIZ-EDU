using Firebase.Database;
using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace TestApp
{
    public partial class LoginWindow : Window
    {
        private readonly FirebaseClient _firebase = new FirebaseClient("https://test-qstem-default-rtdb.firebaseio.com/");
        private bool _isClosingAnimationCompleted = false;

        public LoginWindow()
        {
            if (!IsInternetAvailable())
            {
                MessageBox.Show("Нет подключения к интернету. Пожалуйста, проверьте соединение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }
            InitializeComponent();
        }

        private bool IsInternetAvailable()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AnimateWindow(0, 1, 0.5);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_isClosingAnimationCompleted)
            {
                e.Cancel = true;
                AnimateWindow(1, 0, 0.5, () =>
                {
                    _isClosingAnimationCompleted = true;
                    Close();
                });
            }
        }

        private void AnimateWindow(double from, double to, double durationSeconds, Action onCompleted = null)
        {
            var animation = new DoubleAnimation(from, to, TimeSpan.FromSeconds(durationSeconds));
            if (onCompleted != null)
                animation.Completed += (s, e) => onCompleted();
            BeginAnimation(OpacityProperty, animation);
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var users = await _firebase.Child("users").OnceAsync<User>();

            var matchedUser = users.FirstOrDefault(u =>
                u.Object.email == txtLoginEmail.Text &&
                u.Object.password == txtLoginPassword.Password);

            if (matchedUser != null)
            {
                Session.CurrentUser = matchedUser.Object;
                WindowManager.ShowWindow(() => new MainWindow(matchedUser.Object));
                Close();
            }
            else
            {
                MessageBox.Show("Неверная почта или пароль");
            }
        }

        private void OpenRegistration_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow().Show();
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) => Close();

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
    }
}
