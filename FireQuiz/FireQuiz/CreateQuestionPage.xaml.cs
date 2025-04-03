using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using Firebase.Storage;
using FireQuiz.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FireQuiz
{
    public partial class CreateQuestionPage : Page
    {
        private readonly Quiz quiz;
        private Question question; // ✅ Теперь поле можно изменять
        private readonly bool isEditMode;
        private Border activeBorderBox;
        private readonly FirebaseStorage storage = new FirebaseStorage("test-qstem.firebasestorage.app");

        public CreateQuestionPage(Quiz quiz, Question question = null)
        {
            InitializeComponent();
            this.quiz = quiz;
            this.isEditMode = question != null;
            this.question = question ?? new Question(); // ✅ Создаем новый вопрос, если его нет

            GenerateAnswers();

            if (isEditMode)
                LoadQuestionData();

            this.PreviewKeyDown += new KeyEventHandler(OnKeyDownHandler);
        }



        private void GenerateAnswers()
        {
            AnswerPanel.Children.Clear();

            for (int i = 0; i < 5; i++)
            {
                StackPanel stack = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };

                TextBox answerText = new TextBox { Width = 250, Margin = new Thickness(5) };
                CheckBox isCorrect = new CheckBox { Content = "Правильный", Margin = new Thickness(5) };

                // Контейнер для изображения
                Border imageBorder = new Border
                {
                    Width = 60,
                    Height = 60,
                    BorderThickness = new Thickness(2),
                    BorderBrush = Brushes.Gray, // Начальный цвет
                    Margin = new Thickness(5),
                    Tag = i
                };

                Image imageBox = new Image
                {
                    Width = 50,
                    Height = 50,
                    Stretch = Stretch.UniformToFill
                };

                imageBorder.Child = imageBox;
                imageBorder.MouseDown += ImageBorder_MouseDown; // Добавляем клик

                // Кнопка "Удалить фото"
                Button deleteImageButton = new Button
                {
                    Content = "❌",
                    Width = 30,
                    Height = 30,
                    Visibility = Visibility.Collapsed, // По умолчанию скрыта
                    Tag = i
                };
                deleteImageButton.Click += DeleteImage_Click;

                stack.Children.Add(answerText);
                stack.Children.Add(isCorrect);
                stack.Children.Add(imageBorder);
                stack.Children.Add(deleteImageButton);

                AnswerPanel.Children.Add(stack);
            }
        }

        private void QuestionImageBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (activeBorderBox != null)
            {
                // Сбрасываем выделение у предыдущего активного бокса
                activeBorderBox.BorderBrush = Brushes.Gray;
            }

            // Устанавливаем активный бокс
            activeBorderBox = sender as Border;
            activeBorderBox.BorderBrush = Brushes.Blue; // Выделяем активный бокс

        }


        private void ImageBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (activeBorderBox != null)
            {
                // Сбрасываем цвет старого активного бокса
                activeBorderBox.BorderBrush = Brushes.Gray;
            }

            // Устанавливаем новый активный бокс
            activeBorderBox = sender as Border;
            activeBorderBox.BorderBrush = Brushes.Blue; // Выделяем активный бокс

        }


        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                PasteImageFromClipboard();
            }
        }
        private void DeleteQuestionImage_Click(object sender, RoutedEventArgs e)
        {
            QuestionImage.Source = null;
            QuestionImageBorder.Tag = null; // Убираем временный путь
            question.ImageUrl = null; // Удаляем ссылку из базы
            DeleteQuestionImageButton.Visibility = Visibility.Collapsed;
        }


        private void PasteImageFromClipboard()
        {
            if (Clipboard.ContainsImage())
            {
                BitmapSource bitmapSource = Clipboard.GetImage();
                string tempPath = Path.GetTempFileName() + ".png";
                SaveBitmapSourceToFile(bitmapSource, tempPath);

                if (activeBorderBox == QuestionImageBorder)
                {
                    // Вставляем изображение в вопрос
                    QuestionImage.Source = bitmapSource;
                    QuestionImageBorder.Tag = tempPath;
                    DeleteQuestionImageButton.Visibility = Visibility.Visible;
                }
                else if (activeBorderBox != null)
                {
                    // Вставляем изображение в выбранный ответ
                    ((Image)activeBorderBox.Child).Source = bitmapSource;
                    activeBorderBox.Tag = tempPath;
                }
                else
                {
                    MessageBox.Show("Выберите контейнер перед вставкой изображения.");
                }
            }
        }





        private void SaveBitmapSourceToFile(BitmapSource bitmapSource, string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(fileStream);
            }
        }

        private async Task<string> UploadImageToFirebase(string filePath)
        {
            try
            {
                var stream = File.OpenRead(filePath);
                var uploadTask = storage
                    .Child("imageanswers")
                    .Child(Path.GetFileName(filePath))
                    .PutAsync(stream);

                return await uploadTask; // Возвращаем ссылку
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки изображения: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }








        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var selectedCheckboxes = AnswerPanel.Children
                .OfType<StackPanel>()
                .Select(sp => sp.Children[1] as CheckBox)
                .Where(cb => cb.IsChecked == true)
                .ToList();

            if (selectedCheckboxes.Count > 3)
            {
                MessageBox.Show("Можно выбрать не более трех правильных ответов!", "Ограничение", MessageBoxButton.OK, MessageBoxImage.Warning);
                (sender as CheckBox).IsChecked = false;
            }
        }

        private void LoadQuestionData()
        {
            QuestionText.Text = question.Text;

            // Загружаем фото вопроса, если оно есть
            if (!string.IsNullOrEmpty(question.ImageUrl) && Uri.IsWellFormedUriString(question.ImageUrl, UriKind.Absolute))
            {
                QuestionImage.Source = new BitmapImage(new Uri(question.ImageUrl));
                QuestionImageBorder.Tag = question.ImageUrl;
                DeleteQuestionImageButton.Visibility = Visibility.Visible;
            }
            else
            {
                QuestionImage.Source = null;
                QuestionImageBorder.Tag = null;
                DeleteQuestionImageButton.Visibility = Visibility.Collapsed;
            }

            for (int i = 0; i < question.Answers.Count; i++)
            {
                var stack = AnswerPanel.Children[i] as StackPanel;
                (stack.Children[0] as TextBox).Text = question.Answers[i].Text;
                (stack.Children[1] as CheckBox).IsChecked = question.Answers[i].IsCorrect;

                var imageBorder = stack.Children[2] as Border;
                var imageBox = imageBorder.Child as Image;
                var deleteButton = stack.Children[3] as Button;

                if (!string.IsNullOrEmpty(question.Answers[i].ImageUrl) && Uri.IsWellFormedUriString(question.Answers[i].ImageUrl, UriKind.Absolute))
                {
                    imageBox.Source = new BitmapImage(new Uri(question.Answers[i].ImageUrl));
                    imageBorder.Tag = question.Answers[i].ImageUrl;
                    deleteButton.Visibility = Visibility.Visible;
                }
                else
                {
                    imageBox.Source = null;
                    imageBorder.Tag = null;
                    deleteButton.Visibility = Visibility.Collapsed;
                }
            }
        }




        private void DeleteImage_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            int index = (int)deleteButton.Tag;

            var stack = AnswerPanel.Children[index] as StackPanel;
            var imageBorder = stack.Children[2] as Border;
            var imageBox = imageBorder.Child as Image;

            // Убираем ссылку на фото (но не удаляем сам файл)
            imageBorder.Tag = ""; // Теперь это пустая строка вместо null
            imageBox.Source = null;

            // Скрываем кнопку удаления
            deleteButton.Visibility = Visibility.Collapsed;

            MessageBox.Show("Ссылка на фото удалена, но файл в Firebase сохранён!", "Удаление ссылки", MessageBoxButton.OK, MessageBoxImage.Information);
        }




        private string GetFileNameFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            Uri uri = new Uri(url);
            string fileName = System.IO.Path.GetFileName(uri.LocalPath); // Берем имя файла из пути

            return fileName;
        }


        private async void SaveQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(QuestionText.Text))
            {
                MessageBox.Show("Введите текст вопроса!");
                return;
            }

            if (question == null)
            {
                question = new Question(); // ✅ Создаем новый вопрос, если он отсутствует
            }

            string questionImagePath = QuestionImageBorder.Tag as string;
            string finalQuestionImageUrl = question.ImageUrl; // Сохраняем текущую ссылку

            // Если фото было удалено
            if (string.IsNullOrEmpty(questionImagePath) && !string.IsNullOrEmpty(question.ImageUrl))
            {
                finalQuestionImageUrl = null; // Удаляем ссылку в базе
            }
            // Если загружено новое фото (локальный путь), загружаем в Firebase
            else if (!string.IsNullOrEmpty(questionImagePath) && File.Exists(questionImagePath))
            {
                finalQuestionImageUrl = await UploadImageToFirebase(questionImagePath);
            }

            var answers = new List<Answer>();

            foreach (var stack in AnswerPanel.Children.OfType<StackPanel>())
            {
                var imageBorder = stack.Children[2] as Border;
                string tempImagePath = imageBorder.Tag as string;
                string finalImageUrl = null;

                // Используем старый URL или загружаем новый
                if (!string.IsNullOrEmpty(tempImagePath) && Uri.IsWellFormedUriString(tempImagePath, UriKind.Absolute))
                {
                    finalImageUrl = tempImagePath;
                }
                else if (!string.IsNullOrEmpty(tempImagePath) && File.Exists(tempImagePath))
                {
                    finalImageUrl = await UploadImageToFirebase(tempImagePath);
                }

                answers.Add(new Answer
                {
                    Text = (stack.Children[0] as TextBox).Text,
                    IsCorrect = (bool)(stack.Children[1] as CheckBox).IsChecked,
                    ImageUrl = finalImageUrl
                });
            }

            if (answers.All(a => !a.IsCorrect))
            {
                MessageBox.Show("Выберите хотя бы один правильный ответ!");
                return;
            }

            question.Text = QuestionText.Text;
            question.ImageUrl = finalQuestionImageUrl; // ✅ Теперь вопрос точно существует
            question.Answers = answers;

            if (!isEditMode)
            {
                quiz.Questions.Add(question);
            }

            MessageBox.Show("Вопрос сохранён!");
            NavigationService.GoBack();
        }




     
    }
}
