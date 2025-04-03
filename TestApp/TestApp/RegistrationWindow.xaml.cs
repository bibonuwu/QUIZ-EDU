using System;
using System.Linq;
using System.Windows;
using Firebase.Database;
using Firebase.Database.Query;

namespace TestApp
{
    public partial class RegistrationWindow : Window
    {
        private readonly FirebaseClient firebase = new FirebaseClient("https://test-qstem-default-rtdb.firebaseio.com/");

        public RegistrationWindow()
        {
            InitializeComponent();
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
                password = txtPassword.Password // ← теперь пароль будет в открытом виде!
            };


            user.Id = Guid.NewGuid().ToString(); // ← Генерация уникального ID

            // теперь пользователь добавится по его имени (firstName)
            await firebase.Child("users").Child(user.Id).PutAsync(user);

            MessageBox.Show("Регистрация успешна!");
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void OpenLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}
