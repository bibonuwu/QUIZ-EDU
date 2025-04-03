using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FireQuiz
{
    public partial class SubjectsPage : Page
    {
        private readonly List<(string Name, string Image)> allSubjects = new List<(string Name, string Image)>
{
    ("Оқу сауаттылығы", "pack://application:,,,/subjects/books 2.png"),
    ("Мат сауаттылық", "pack://application:,,,/subjects/calculator.png"),
    ("Тарих", "pack://application:,,,/subjects/notebook.png"),
    ("Физика", "pack://application:,,,/subjects/telescope.png"),
    ("Биология", "pack://application:,,,/subjects/microscope.png"),
    ("Матиматика", "pack://application:,,,/subjects/cubes 2.png"),
    ("Химия", "pack://application:,,,/subjects/flask.png"),
    ("Информатика", "pack://application:,,,/subjects/computer.png")
};


        public SubjectsPage(List<string> allowedSubjects = null)
        {
            InitializeComponent();
            GenerateSubjectCards(allowedSubjects);
        }

        private void GenerateSubjectCards(List<string> allowedSubjects)
        {
            SubjectsPanel.Children.Clear();

            foreach (var subject in allSubjects)
            {
                if (allowedSubjects == null || allowedSubjects.Contains(subject.Name))
                {
                    Border card = new Border
                    {
                        Width = 300,
                        Height = 90,
                        Background = new SolidColorBrush(Color.FromRgb(71, 178, 255)), // Синий фон
                        CornerRadius = new CornerRadius(10),
                        Margin = new Thickness(10),
                        Cursor = Cursors.Hand
                    };

                    Grid grid = new Grid();
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                    TextBlock subjectText = new TextBlock
                    {
                        Text = subject.Name,
                        FontSize = 18,
                        Foreground = Brushes.White,
                        FontWeight = FontWeights.Bold,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(30, 0, 0, 0)
                    };
                    Grid.SetColumn(subjectText, 0);

                    Image subjectImage = new Image
                    {
                        Source = new BitmapImage(new Uri(subject.Image, UriKind.Absolute)),
                        Width = 87,
                        Height = 87,
                        Stretch = Stretch.UniformToFill,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(10)
                    };
                    Grid.SetColumn(subjectImage, 1);

                    grid.Children.Add(subjectText);
                    grid.Children.Add(subjectImage);
                    card.Child = grid;

                    // Навигация по клику
                    card.MouseDown += (s, e) =>
                    {
                        NavigationService.Navigate(new QuizListPage(subject.Name));
                    };

                    // Эффект наведения
                    card.MouseEnter += (s, e) => card.Background = new SolidColorBrush(Color.FromRgb(30, 136, 229));
                    card.MouseLeave += (s, e) => card.Background = new SolidColorBrush(Color.FromRgb(71, 178, 255));

                    SubjectsPanel.Children.Add(card);
                }
            }
        }
    }
}
