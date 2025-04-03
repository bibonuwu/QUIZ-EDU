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
    /// Логика взаимодействия для CreateQuizPage.xaml
    /// </summary>
    public partial class CreateQuizPage : Page
    {
        private string subject;
        public CreateQuizPage(string subject)
        {
            InitializeComponent();
            this.subject = subject;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Quiz quiz = new Quiz
            {
                Id = Guid.NewGuid().ToString(),
                Name = QuizNameBox.Text,
                Subject = subject
            };
            NavigationService.Navigate(new QuestionListPage(quiz));
        }
    }

}
