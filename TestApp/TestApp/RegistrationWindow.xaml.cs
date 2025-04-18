using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Firebase.Database;
using Firebase.Database.Query;

namespace TestApp
{
    public partial class RegistrationWindow : Window
    {
        private readonly FirebaseClient firebase = new FirebaseClient("https://test-qstem-default-rtdb.firebaseio.com/");
        private bool _isClosingAnimationCompleted = false;

        public RegistrationWindow()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполненность
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            // Проверка на уникальность email
            var users = await firebase.Child("users").OnceAsync<User>();
            if (users.Any(u => u.Object.email == txtEmail.Text))
            {
                MessageBox.Show("Пользователь с таким email уже существует.");
                return;
            }

            var user = new User
            {
                firstName = txtFirstName.Text,
                lastName = txtLastName.Text,
                email = txtEmail.Text,
                phone = txtPhone.Text,
                region = cmbRegion.Text,
                district = cmbDistrict.Text,
                school = cmbSchool.Text,
                grade = cmbGrade.Text,
                literal = cmbLiteral.Text,
                gender = cmbGender.Text,
                password = txtPassword.Password
            };

            user.Id = Guid.NewGuid().ToString();

            await firebase.Child("users").Child(user.Id).PutAsync(user);

            MessageBox.Show("Регистрация успешна!");

            WindowManager.ShowWindow(() => new LoginWindow());
            this.Close();
        }


        private void OpenLogin_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ShowWindow(() => new LoginWindow());
            this.Close();
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
