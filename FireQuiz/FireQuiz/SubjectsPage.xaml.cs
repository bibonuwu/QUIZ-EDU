using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FireQuiz
{
    public partial class SubjectsPage : Page
    {
        public List<SubjectModel> Subjects { get; set; }

        public SubjectsPage(List<string> allowedSubjects = null)
        {
            InitializeComponent();

            Subjects = new List<SubjectModel>
            {
                new SubjectModel("Оқу сауаттылығы", "pack://application:,,,/subjects/books 2.png"),
                new SubjectModel("Мат сауаттылық", "pack://application:,,,/subjects/calculator.png"),
                new SubjectModel("Тарих", "pack://application:,,,/subjects/notebook.png"),
                new SubjectModel("Физика", "pack://application:,,,/subjects/telescope.png"),
                new SubjectModel("Биология", "pack://application:,,,/subjects/microscope.png"),
                new SubjectModel("Матиматика", "pack://application:,,,/subjects/cubes 2.png"),
                new SubjectModel("Химия", "pack://application:,,,/subjects/flask.png"),
                new SubjectModel("Информатика", "pack://application:,,,/subjects/computer.png")
            };

            if (allowedSubjects != null)
                Subjects = Subjects.FindAll(s => allowedSubjects.Contains(s.Name));

            SubjectsControl.ItemsSource = Subjects;
        }

        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is SubjectModel subject)
            {
                NavigationService.Navigate(new StatisticsPage(subject.Name));
            }
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is SubjectModel subject)
            {
                NavigationService.Navigate(new QuizListPage(subject.Name));
            }
        }

        private void Card_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
                border.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(30, 136, 229));
        }

        private void Card_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
                border.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(71, 178, 255));
        }
    }

    public class SubjectModel
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public SubjectModel(string name, string image)
        {
            Name = name;
            Image = image;
        }
    }
}
