using Firebase.Database;
using Firebase.Storage;
using FireQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FireQuiz
{
    /// <summary>
    /// Логика взаимодействия для QuizListPage.xaml
    /// </summary>
    public partial class QuizListPage : Page
    {
        private string subject;
        public QuizListPage(string subject)
        {
            InitializeComponent();
            this.subject = subject;
            SubjectName.Text = subject;
            LoadQuizzes();
        }

        private async void LoadQuizzes()
        {
            QuizList.ItemsSource = await FirebaseHelper.GetQuizzes(subject);
        }

        private void AddQuiz_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateQuizPage(subject));
        }

        private void QuizSelected(object sender, SelectionChangedEventArgs e)
        {
            if (QuizList.SelectedItem is Quiz quiz)
            {
                NavigationService.Navigate(new QuestionListPage(quiz));
            }
        }
    }

}
