using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TestApp.Services;
using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TestApp
{
    public partial class MainWindow : Window
    {
        private FirebaseService _firebaseService = new FirebaseService();
        private Dictionary<string, Dictionary<string, QuizData>> _quizzes;
        private Dictionary<string, string> _selectedTests = new Dictionary<string, string>();
        private const string FirebaseUrl = "https://test-qstem-default-rtdb.firebaseio.com/";
        private bool _isClosingAnimationCompleted = false;

        public MainWindow(User user)
        {
            InitializeComponent();
            LoadSubjects();

            lblFullName.Text = $"{user.firstName} {user.lastName}";
            CheckStatusAsync();
        }
        // Плавное появление окна
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

        private async void CheckStatusAsync()
        {
            var now = DateTime.Now.ToString("HH:mm");

            if (IsInternetAvailable())
            {
                InternetEllipse.Fill = Brushes.Green;

                bool firebaseOk = await CanAccessFirebaseAsync(FirebaseUrl);
                FirebaseEllipse.Fill = firebaseOk ? Brushes.Green : Brushes.Red;
            }
            else
            {
                InternetEllipse.Fill = Brushes.Red;
                FirebaseEllipse.Fill = Brushes.Gray;
            }

            CheckTimeText.Text = $" {now}";
        }




        private bool IsInternetAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        private async Task<bool> CanAccessFirebaseAsync(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    var response = await client.GetAsync(url + ".json");
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }

        private async void LoadSubjects()
        {
            _quizzes = await _firebaseService.GetAllQuizzesAsync();

            var mainOrder = new List<string> { "Оқу сауаттылығы", "Мат сауаттылық", "Тарих" };
            var additionalSubjects = new List<string>();

            foreach (var subject in _quizzes.Keys)
            {
                if (!mainOrder.Contains(subject))
                    additionalSubjects.Add(subject);
            }

            // Загрузка главных предметов
            foreach (var subject in mainOrder)
            {
                if (_quizzes.ContainsKey(subject))
                    AddSubjectControl(MainSubjectsList, subject);
            }

            // Загрузка дополнительных предметов
            foreach (var subject in additionalSubjects)
                AddSubjectControl(AdditionalSubjectsList, subject);
        }

        private void AddSubjectControl(ItemsControl control, string subjectName)
        {
            var label = new Label { Content = subjectName };
            var comboBox = new ComboBox { Tag = subjectName, Width = 250 };

            if (_quizzes[subjectName].Count == 0)
            {
                comboBox.Items.Add("Тестов нет");
                comboBox.IsEnabled = false;
            }
            else
            {
                foreach (var test in _quizzes[subjectName])
                    comboBox.Items.Add(test.Key);
            }

            comboBox.SelectionChanged += ComboBox_SelectionChanged;

            control.Items.Add(label);
            control.Items.Add(comboBox);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var subject = comboBox.Tag.ToString();
            var selectedTest = comboBox.SelectedItem?.ToString();
            if (selectedTest != null && selectedTest != "Тестов нет")
                _selectedTests[subject] = selectedTest;
        }

        private void StartTest_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTests.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы один тест");
                return;
            }

            var testWindow = new TestWindow(_selectedTests, _quizzes);
            testWindow.Show();
            this.Close();
        }

        private void OpenStats_Click(object sender, RoutedEventArgs e)
        {
            string key = $"{Session.CurrentUser.firstName} {Session.CurrentUser.lastName} {Session.CurrentUser.Id}";
            var statsWindow = new StatsWindow(key);
            statsWindow.Show(); // Используем Show, а не ShowDialog
            this.Close();       // Закрываем текущее окно
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow LoginWindow = new LoginWindow();
            LoginWindow.Show();
            this.Close();
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
    }
} 
