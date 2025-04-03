using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TestApp.Services;

namespace TestApp
{
    public partial class MainWindow : Window
    {
        private FirebaseService _firebaseService = new FirebaseService();
        private Dictionary<string, Dictionary<string, QuizData>> _quizzes;
        private Dictionary<string, string> _selectedTests = new Dictionary<string, string>();

        public MainWindow(User user)
        {
            InitializeComponent();
            LoadSubjects();

            lblFullName.Text = $"{user.firstName} {user.lastName}";
            lblEmail.Text = user.email;
            lblPhone.Text = user.phone;
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
