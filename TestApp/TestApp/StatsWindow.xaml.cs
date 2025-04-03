using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TestApp;

namespace TestApp
{
    public partial class StatsWindow : Window
    {
        private readonly string _userKey;
        private JObject _userStats;
        private Dictionary<string, JObject> _sessions = new Dictionary<string, JObject>();

        public StatsWindow(string userKey)
        {
            InitializeComponent();
            _userKey = userKey;
            LoadStats();
        }

        private async void LoadStats()
        {
            var firebase = new FirebaseClient("https://test-qstem-default-rtdb.firebaseio.com/");
            var data = await firebase.Child("Score").Child(_userKey).OnceSingleAsync<object>();

            if (data == null)
            {
                MessageBox.Show("Статистика не найдена.");
                return;
            }

            _userStats = JObject.Parse(JsonConvert.SerializeObject(data));

            // Сохраняем все сессии
            foreach (var session in _userStats)
            {
                _sessions[session.Key] = (JObject)session.Value;
                SessionSelector.Items.Add(session.Key);
            }

            if (_sessions.Count > 0)
                SessionSelector.SelectedIndex = 0;
        }

        private void SessionSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SessionSelector.SelectedItem == null)
                return;

            string selectedSession = SessionSelector.SelectedItem.ToString();
            if (!_sessions.ContainsKey(selectedSession))
                return;

            JObject sessionData = _sessions[selectedSession];
            RenderSession(sessionData);
        }

        private void RenderSession(JObject sessionData)
        {
            int totalCorrect = 0;
            int totalWrong = 0;
            int totalScore = 0;
            int maxScore = 0;

            SubjectsProgressPanel.Children.Clear();
            SubjectsGradePanel.Children.Clear();

            foreach (var subject in sessionData)
            {
                TextBlock subjectLabel = new TextBlock { Text = subject.Key, FontWeight = FontWeights.Bold, Margin = new Thickness(0, 10, 0, 2) };
                SubjectsProgressPanel.Children.Add(subjectLabel);

                foreach (var test in (JObject)subject.Value)
                {
                    var testInfo = (JObject)test.Value;
                    string scoreStr = testInfo["Score"]?.ToString()?.Replace(" балл", "") ?? "0";
                    if (!int.TryParse(scoreStr, out int score)) score = 0;

                    int correct = 0, wrong = 0;
                    var answers = (JObject)testInfo["Вопросы"];
                    foreach (var answer in answers)
                    {
                        if (answer.Value.ToString().StartsWith("True"))
                            correct++;
                        else
                            wrong++;
                    }

                    totalCorrect += correct;
                    totalWrong += wrong;
                    totalScore += score;
                    maxScore += answers.Count;

                    // Прогрессбар
                    double percent = answers.Count > 0 ? (double)correct / answers.Count : 0;

                    StackPanel line = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 2, 0, 2) };
                    ProgressBar progress = new ProgressBar
                    {
                        Value = percent * 100,
                        Maximum = 100,
                        Width = 200,
                        Height = 10,
                        Margin = new Thickness(0, 0, 10, 0)
                    };

                    line.Children.Add(progress);
                    line.Children.Add(new TextBlock { Text = $"{correct} из {answers.Count}" });

                    SubjectsProgressPanel.Children.Add(line);

                    // Оценка (грубая по 5-балльной шкале)
                    int grade = CalculateGrade(correct, answers.Count);
                    SubjectsGradePanel.Children.Add(new TextBlock
                    {
                        Text = $"{subject.Key} ({test.Key}): {grade}",
                        Margin = new Thickness(0, 2, 0, 0)
                    });
                }
            }

            // Обновление карточек
            TotalScoreText.Text = totalScore.ToString();
            AchievementText.Text = maxScore > 0 ? $"{(int)((double)totalCorrect / (totalCorrect + totalWrong) * 100)}" : "0";
            CorrectAnswersText.Text = totalCorrect.ToString();
            IncorrectAnswersText.Text = totalWrong.ToString();

            // Общая информация (примитивная, можно доработать)
            var fullName = _userKey.Split(' ')[0] + " " + _userKey.Split(' ')[1];
            UserNameText.Text = $"Имя-фамилия: {fullName}";
            ExamNameText.Text = "Экзамен: EDUCON-2025";
            StartTimeText.Text = $"Дата начала: —";
            EndTimeText.Text = $"Дата завершения: —";
            DurationText.Text = $"Продолжительность(мин): —";
        }

        private int CalculateGrade(int correct, int total)
        {
            if (total == 0) return 0;
            double percent = (double)correct / total;
            if (percent >= 0.9) return 5;
            if (percent >= 0.75) return 4;
            if (percent >= 0.5) return 3;
            if (percent >= 0.3) return 2;
            return 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(Session.CurrentUser);
            mainWindow.Show();
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
