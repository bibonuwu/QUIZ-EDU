using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FireQuiz
{
    public partial class StatisticsPage : Page
    {
        private readonly string currentSubject;

        public StatisticsPage(string subject)
        {
            InitializeComponent();
            currentSubject = subject;
        }

        private async void ShowStats_Click(object sender, RoutedEventArgs e)
        {
            var scoresData = await FirebaseHelper.GetStudentScores();
            var usersData = await FirebaseHelper.GetUsers();



            DateTime fromDate = FromDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime toDate = ToDatePicker.SelectedDate ?? DateTime.MaxValue;

            var filteredData = new List<dynamic>();

            foreach (var student in scoresData)
            {
                string studentKey = student.Key;
                string studentName = studentKey;
                string studentGrade = "Неизвестно";

                // Поиск имени и класса ученика
                var user = usersData.FirstOrDefault(u => studentKey.Contains(u.Key));
                if (!user.Equals(default(KeyValuePair<string, User>)))
                {
                    studentName = $"{user.Value.firstName} {user.Value.lastName}";
                    studentGrade = $"{user.Value.grade} {user.Value.literal}";
                }

                foreach (var dateAttempt in student.Value)
                {
                    var dateParts = dateAttempt.Key.Split(' ')[0].Split('-');
                    var date = new DateTime(int.Parse(dateParts[2]), int.Parse(dateParts[1]), int.Parse(dateParts[0]));

                    if (date >= fromDate && date <= toDate && dateAttempt.Value.ContainsKey(currentSubject))
                    {
                        var quizzesDict = dateAttempt.Value[currentSubject];

                        foreach (var quizPair in quizzesDict)
                        {
                            var quizName = quizPair.Key;
                            var quizResult = quizPair.Value;

                            filteredData.Add(new
                            {
                                Ученик = studentName,
                                Класс = studentGrade,
                                Дата = date.ToShortDateString(),
                                Тест = quizName,
                                Баллы = quizResult.Score
                            });
                        }
                    }
                }
            }

            StatsDataGrid.ItemsSource = filteredData;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack(); // или: NavigationService.Navigate(new SubjectsPage());
        }


    }
}

