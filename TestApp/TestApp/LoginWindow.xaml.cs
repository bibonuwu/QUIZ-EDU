using Firebase.Database;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace TestApp
{
    public partial class LoginWindow : Window
    {
        private readonly FirebaseClient firebase = new FirebaseClient("https://test-qstem-default-rtdb.firebaseio.com/");
        private bool _isClosingAnimationCompleted = false;

        public LoginWindow()
        {
            if (!IsInternetAvailable())
            {
                MessageBox.Show("Нет подключения к интернету. Пожалуйста, проверьте соединение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(); // Закрыть приложение, если нет интернета
                return;
            }
            InitializeComponent();
        }
        private bool IsInternetAvailable()
        {
            try
            {
                using (var client = new System.Net.WebClient())
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













        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var users = await firebase.Child("users").OnceAsync<User>();

            foreach (var u in users)
            {
                if (u.Object.email == txtLoginEmail.Text &&
    u.Object.password == txtLoginPassword.Password) // ← простое сравнение паролей
                {
                    Session.CurrentUser = u.Object;  // ← Сохраняем текущего пользователя

                    MainWindow infoWindow = new MainWindow(u.Object);
                    infoWindow.Show();
                    this.Close();
                    return;
                }
            }

            MessageBox.Show("Неверная почта или пароль");
        }

        private void OpenRegistration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registration = new RegistrationWindow();
            registration.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                this.DragMove();
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
    }
}
