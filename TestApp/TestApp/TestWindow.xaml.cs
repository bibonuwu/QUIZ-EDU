using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media; // обязательно для FontFamily
using System.Windows.Controls;
using System.Windows.Media;
using Firebase.Database;
using Firebase.Database.Query;
using System.Windows.Media.Animation;
namespace TestApp
{
    public partial class TestWindow : Window
    {
        private Dictionary<string, string> _selectedTests;
        private Dictionary<string, Dictionary<string, QuizData>> _quizzes;
        private Dictionary<string, int> _currentQuestionIndex = new Dictionary<string, int>();
        private Dictionary<string, List<int>> _selectedAnswers = new Dictionary<string, List<int>>();
        private string _currentSubject;
        private Calculator secondWindow; // Храним ссылку на окно
        private Periodic secondWindow1; // Храним ссылку на окно
        private bool _isClosingAnimationCompleted = false;

        public TestWindow(Dictionary<string, string> selectedTests, Dictionary<string, Dictionary<string, QuizData>> quizzes)
        {
            InitializeComponent();
            _selectedTests = selectedTests;
            _quizzes = quizzes;

            foreach (var subject in selectedTests.Keys)
            {
                _currentQuestionIndex[subject] = 0;
                _selectedAnswers[subject] = new List<int>();
                SubjectComboBox.Items.Add(subject);
            }

            // выбираем первый предмет по умолчанию
            _currentSubject = selectedTests.Keys.First();
            SubjectComboBox.SelectedItem = _currentSubject;

            LoadQuestion();
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



        private void LoadQuestion()
        {
            string testId = _selectedTests[_currentSubject];
            List<Question> questions = _quizzes[_currentSubject][testId].Questions;
            int index = _currentQuestionIndex[_currentSubject];
            Question question = questions[index];

            SubjectTitle.Text = $"{_currentSubject}. Сұрақ - {index + 1} / {questions.Count}";
            QuestionText.Text = question.Text;
            Foreground = (Brush)new BrushConverter().ConvertFromString("#454F5B");

            // Применим шрифт к заголовку
            SubjectTitle.FontFamily = (FontFamily)FindResource("MontserratMedium");
            QuestionText.FontFamily = (FontFamily)FindResource("MontserratMedium");

            // Отображение изображения
            if (string.IsNullOrEmpty(question.ImageUrl))
            {
                QuestionImage.Visibility = Visibility.Collapsed;
            }
            else
            {
                QuestionImage.Visibility = Visibility.Visible;
                QuestionImage.Source = new BitmapImage(new Uri(question.ImageUrl));
            }

            AnswersPanel.Children.Clear();

            for (int i = 0; i < question.Answers.Count; i++)
            {
                Answer answer = question.Answers[i];
                char optionLetter = (char)('A' + i);
                string optionPrefix = $"{optionLetter})";

                RadioButton radio = new RadioButton
                {
                    GroupName = "Answers",
                    Tag = i,
                    Background = (Brush)new BrushConverter().ConvertFromString("#454F5B"),
                    Style = (Style)FindResource("ModernRadioButtonStyle")

                };

                // Контейнер с буквой и контентом
                WrapPanel contentStack = new WrapPanel
                {
                    Orientation = Orientation.Horizontal,
                    VerticalAlignment = VerticalAlignment.Center,
                    MaxWidth = 250 // можно поэкспериментировать
                };


                TextBlock letterBlock = new TextBlock
                {
                    Text = optionPrefix,
                    Width = 30,
                    FontSize = 15,
                    Foreground = (Brush)new BrushConverter().ConvertFromString("#454F5B"),
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = (FontFamily)FindResource("MontserratMedium")
                };

                contentStack.Children.Add(letterBlock);

                if (!string.IsNullOrEmpty(answer.Text))
                {
                    TextBlock textBlock = new TextBlock
                    {
                        Text = answer.Text,
                        FontSize = 15,
                        Foreground = (Brush)new BrushConverter().ConvertFromString("#454F5B"),
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = (FontFamily)FindResource("MontserratMedium"),
                        TextWrapping = TextWrapping.Wrap,
                        MaxWidth = 180
                    };

                    contentStack.Children.Add(textBlock);
                }

                if (!string.IsNullOrEmpty(answer.ImageUrl))
                {
                    Image image = new Image
                    {
                        Source = new BitmapImage(new Uri(answer.ImageUrl)),
                        Margin = new Thickness(10, 0, 0, 0), // можно оставить, если хочешь отступ
                        Stretch = Stretch.Uniform // сохраняет пропорции изображения
                    };


                    contentStack.Children.Add(image);
                }


                radio.Content = contentStack;

                // Отметить выбранный
                if (_selectedAnswers[_currentSubject].Count > index &&
                    _selectedAnswers[_currentSubject][index] == i)
                {
                    radio.IsChecked = true;
                }

                radio.Checked += Answer_Checked;
                AnswersPanel.Children.Add(radio);
            }
            GenerateQuestionNav(questions.Count); // ← верни это сюда!

            UpdateAnsweredCounter();
        }



        private void Answer_Checked(object sender, RoutedEventArgs e)
        {
            int selected = (int)((RadioButton)sender).Tag;
            int index = _currentQuestionIndex[_currentSubject];

            // Убедиться, что список достаточно длинный
            while (_selectedAnswers[_currentSubject].Count <= index)
            {
                _selectedAnswers[_currentSubject].Add(-1);
            }

            _selectedAnswers[_currentSubject][index] = selected;

            UpdateAnsweredCounter();

            // Заново загрузить вопрос с актуальной подсветкой
            LoadQuestion();
        }



        private void GenerateQuestionNav(int total)
        {
            QuestionNavPanel.Children.Clear();

            int currentIndex = _currentQuestionIndex[_currentSubject];

            for (int i = 0; i < total; i++)
            {
                Button btn = new Button
                {
                    Content = (i + 1).ToString(),
                    Tag = i,
                    Width = 50,
                    Height = 45,
                    FontFamily = (FontFamily)FindResource("MontserratMedium"),
                    
                    Margin = new Thickness(4),
                    Background = (Brush)new BrushConverter().ConvertFrom("#EFF2F8"),
                    Foreground = (Brush)new BrushConverter().ConvertFrom("#A2A6B8"),
                    BorderBrush = Brushes.Transparent,
                    BorderThickness = new Thickness(1),
                    Cursor = System.Windows.Input.Cursors.Hand
                };

                // Цвет фона в зависимости от состояния
                if (currentIndex == i)
                {
                    btn.Background = (Brush)new BrushConverter().ConvertFrom("#3CCABC");
                    btn.Foreground = Brushes.White;
                    btn.FontWeight = FontWeights.Bold;
                }
                else if (_selectedAnswers[_currentSubject].Count > i && _selectedAnswers[_currentSubject][i] >= 0)
                {
                    btn.Background = (Brush)new BrushConverter().ConvertFrom("#5468A4");
                    btn.Foreground = Brushes.White;

                }

                // Шаблон с лёгким скруглением
                var borderFactory = new FrameworkElementFactory(typeof(Border));
                borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(6)); // квадратный стиль
                borderFactory.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Button.BackgroundProperty));
                borderFactory.SetValue(Border.BorderBrushProperty, new TemplateBindingExtension(Button.BorderBrushProperty));
                borderFactory.SetValue(Border.BorderThicknessProperty, new TemplateBindingExtension(Button.BorderThicknessProperty));

                var contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
                contentPresenterFactory.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                contentPresenterFactory.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
                borderFactory.AppendChild(contentPresenterFactory);

                btn.Template = new ControlTemplate(typeof(Button)) { VisualTree = borderFactory };

                btn.Click += (s, e) =>
                {
                    _currentQuestionIndex[_currentSubject] = (int)((Button)s).Tag;
                    LoadQuestion();
                };

                QuestionNavPanel.Children.Add(btn);
            }
        }


        private void UpdateAnsweredCounter()
        {
            int count = _selectedAnswers[_currentSubject].Count(answer => answer >= 0);
            AnsweredCounter.Text = count.ToString();
        }



        private void SubjectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SubjectComboBox.SelectedItem != null)
            {
                _currentSubject = SubjectComboBox.SelectedItem.ToString();
                LoadQuestion();
            }
        }

        private void PrevQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (_currentQuestionIndex[_currentSubject] > 0)
            {
                _currentQuestionIndex[_currentSubject]--;
                LoadQuestion();
            }
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            string testId = _selectedTests[_currentSubject];
            int total = _quizzes[_currentSubject][testId].Questions.Count;

            if (_currentQuestionIndex[_currentSubject] < total - 1)
            {
                _currentQuestionIndex[_currentSubject]++;
                LoadQuestion();
            }
        }

        private async void FinishTest_Click(object sender, RoutedEventArgs e)
        {
            if (Session.CurrentUser == null)
            {
                MessageBox.Show("Пользователь не авторизован!");
                return;
            }

            var subjectsResults = new Dictionary<string, object>();

            foreach (string subject in _selectedTests.Keys)
            {
                string testId = _selectedTests[subject];
                List<Question> questions = _quizzes[subject][testId].Questions;

                int totalScore = 0;
                var questionResults = new Dictionary<string, string>();

                for (int i = 0; i < questions.Count; i++)
                {
                    List<int> selectedIndices = new List<int>();

                    if (_selectedAnswers.ContainsKey(subject) && i < _selectedAnswers[subject].Count)
                    {
                        int selected = _selectedAnswers[subject][i];
                        if (selected != -1)
                            selectedIndices.Add(selected);
                    }

                    int score = ScoreCalculator.CalculateQuestionScore(questions[i], selectedIndices);
                    totalScore += score;

                    questionResults[$"Вопрос {i + 1}"] = score > 0 ? $"True ({score} балл)" : "False";
                }

                subjectsResults[subject] = new Dictionary<string, object>
                {
                    [testId] = new Dictionary<string, object>
                    {
                        ["Вопросы"] = questionResults,
                        ["Score"] = $"{totalScore} балл"
                    }
                };
            }

            string fullNameWithId = $"{Session.CurrentUser.firstName} {Session.CurrentUser.lastName} {Session.CurrentUser.Id}";
            string baseDate = DateTime.Now.ToString("dd-MM-yyyy");

            var firebase = new FirebaseClient("https://test-qstem-default-rtdb.firebaseio.com/");

            // Получение текущих тестов за сегодня
            var existingTests = await firebase
                .Child("Score")
                .Child(fullNameWithId)
                .OnceAsync<object>();

            // Считаем, сколько раз уже пройден тест сегодня
            int testNumber = existingTests.Count(x => x.Key.StartsWith(baseDate)) + 1;

            string currentDateWithNumber = $"{baseDate} {testNumber}";

            await firebase
                .Child("Score")
                .Child(fullNameWithId)
                .Child(currentDateWithNumber)
                .PutAsync(subjectsResults);

            MessageBox.Show("Результаты теста успешно сохранены!");

            string key = $"{Session.CurrentUser.firstName} {Session.CurrentUser.lastName} {Session.CurrentUser.Id}";
            var statsWindow = new StatsWindow(key);
            statsWindow.Show(); // Используем Show, а не ShowDialog
            this.Close();       // Закрываем текущее окно
        }

        private void EnlargeText_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionText.FontSize < 30)
            {
                QuestionText.FontSize += 5;
            }
            else
            {
                QuestionText.FontSize = 15; // Сброс обратно
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            if (secondWindow == null || !secondWindow.IsLoaded)
            {
                secondWindow = new Calculator();
                secondWindow.Show();
            }
            else
            {
                if (secondWindow.WindowState == WindowState.Minimized)
                {
                    secondWindow.WindowState = WindowState.Normal; // Разворачиваем, если свернуто
                }

                secondWindow.Activate(); // Показываем окно на передний план
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
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (secondWindow1 == null || !secondWindow1.IsLoaded)
            {
                secondWindow1 = new Periodic();
                secondWindow1.Show();
            }
            else
            {
                if (secondWindow1.WindowState == WindowState.Minimized)
                {
                    secondWindow1.WindowState = WindowState.Normal; // Разворачиваем, если свернуто
                }

                secondWindow1.Activate(); // Показываем окно на передний план
            }
        }
        private int GetQuestionIndexFromUI()
        {
            string testId = _selectedTests[_currentSubject];
            List<Question> questions = _quizzes[_currentSubject][testId].Questions;

            // сравниваем вопрос на экране с текущим из списка
            for (int i = 0; i < questions.Count; i++)
            {
                if (QuestionText.Text == questions[i].Text)
                    return i;
            }

            return _currentQuestionIndex[_currentSubject]; // fallback
        }

    }
}
