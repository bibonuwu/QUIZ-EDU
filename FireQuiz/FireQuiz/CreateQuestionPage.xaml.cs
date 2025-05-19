    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using Firebase.Storage;
    using FireQuiz.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using System.Windows.Media;
    using System.ComponentModel;


    namespace FireQuiz
    {
        public class MatchingPair
        {
            public BitmapImage LeftImage { get; set; }
            public string LeftText { get; set; }
            public ObservableCollection<string> Options { get; set; } = new ObservableCollection<string>();

            // Для отображения букв a), b), c) ... (используется в XAML)
            public string PairLetter { get; set; }
        }

        public partial class CreateQuestionPage : Page
        {
            private readonly Quiz quiz;
            private Question question;
            private readonly bool isEditMode;
            private Border activeBorderBox;
            private readonly FirebaseStorage storage = new FirebaseStorage("test-qstem.firebasestorage.app");

            private TextBox[] answerTextBoxes;
            private CheckBox[] answerCheckBoxes;
            private Border[] answerImageBorders;
            private Image[] answerImages;
            private Button[] deleteAnswerImageButtons;
        

            public ObservableCollection<MatchingPair> MatchingPairs { get; set; } = new ObservableCollection<MatchingPair>();
            public string MainMatchingQuestion { get; set; } = "";

            public CreateQuestionPage(Quiz quiz, Question question = null)
            {
                InitializeComponent();
                this.quiz = quiz;
                this.isEditMode = question != null;
                this.question = question ?? new Question();

                QuestionTypeComboBox.SelectedIndex = 0; // Обычный тест по умолчанию
                QuestionTypeComboBox_SelectionChanged(null, null); // Принудительно выставить видимость
                DataContext = this;

                answerTextBoxes = new[] { AnswerTextBox1, AnswerTextBox2, AnswerTextBox3, AnswerTextBox4, AnswerTextBox5 };
                answerCheckBoxes = new[] { AnswerCheckBox1, AnswerCheckBox2, AnswerCheckBox3, AnswerCheckBox4, AnswerCheckBox5 };
                answerImageBorders = new[] { AnswerImageBorder1, AnswerImageBorder2, AnswerImageBorder3, AnswerImageBorder4, AnswerImageBorder5 };
                answerImages = new[] { AnswerImage1, AnswerImage2, AnswerImage3, AnswerImage4, AnswerImage5 };
                deleteAnswerImageButtons = new[] { DeleteAnswerImageButton1, DeleteAnswerImageButton2, DeleteAnswerImageButton3, DeleteAnswerImageButton4, DeleteAnswerImageButton5 };

                foreach (var cb in answerCheckBoxes)
                    cb.Checked += CheckBox_Checked;

                if (isEditMode)
                    LoadQuestionData();

                this.PreviewKeyDown += new KeyEventHandler(OnKeyDownHandler);

                MatchingPairs.CollectionChanged += (s, e) => UpdatePairLetters();
                MatchingPairsControl.ItemsSource = MatchingPairs;
            }

            private void OnKeyDownHandler(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                {
                    PasteImageFromClipboard();
                }
            }

        private void QuestionImageBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Вставка фото из буфера по клику
            if (Clipboard.ContainsImage())
            {
                BitmapSource bitmapSource = Clipboard.GetImage();
                if (bitmapSource != null)
                {
                    string tempPath = Path.GetTempFileName() + ".png";
                    SaveBitmapSourceToFile(bitmapSource, tempPath);

                    QuestionImage.Source = bitmapSource;
                    QuestionImageBorder.Tag = tempPath;
                    DeleteQuestionImageButton.Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageBox.Show("Буфер обмена не содержит изображение!");
            }
        }

        private void AnswerImageBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            int idx = Array.IndexOf(answerImageBorders, border);
            if (idx < 0) return;

            // Вставка фото из буфера по клику
            if (Clipboard.ContainsImage())
            {
                BitmapSource bitmapSource = Clipboard.GetImage();
                if (bitmapSource != null)
                {
                    string tempPath = Path.GetTempFileName() + ".png";
                    SaveBitmapSourceToFile(bitmapSource, tempPath);

                    answerImages[idx].Source = bitmapSource;
                    answerImageBorders[idx].Tag = tempPath;
                    deleteAnswerImageButtons[idx].Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageBox.Show("Буфер обмена не содержит изображение!");
            }
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
                        QuestionImage.Source = bitmapSource;
                        QuestionImageBorder.Tag = tempPath;
                        DeleteQuestionImageButton.Visibility = Visibility.Visible;
                    }
                    else if (activeBorderBox != null)
                    {
                        int idx = Array.IndexOf(answerImageBorders, activeBorderBox);
                        if (idx >= 0)
                        {
                            answerImages[idx].Source = bitmapSource;
                            answerImageBorders[idx].Tag = tempPath;
                            deleteAnswerImageButtons[idx].Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите контейнер перед вставкой изображения.");
                    }
                }
            }

            private void DeleteQuestionImage_Click(object sender, RoutedEventArgs e)
            {
                QuestionImage.Source = null;
                QuestionImageBorder.Tag = null;
                question.ImageUrl = null;
                DeleteQuestionImageButton.Visibility = Visibility.Collapsed;
            }

            private void DeleteAnswerImage_Click(object sender, RoutedEventArgs e)
            {
                Button btn = sender as Button;
                int idx = Array.IndexOf(deleteAnswerImageButtons, btn);
                if (idx >= 0)
                {
                    answerImageBorders[idx].Tag = null;
                    answerImages[idx].Source = null;
                    deleteAnswerImageButtons[idx].Visibility = Visibility.Collapsed;
                    MessageBox.Show("Ссылка на фото удалена, но файл в Firebase сохранён!", "Удаление ссылки", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            private void CheckBox_Checked(object sender, RoutedEventArgs e)
            {
                var selected = answerCheckBoxes.Count(cb => cb.IsChecked == true);
                if (selected > 3)
                {
                    MessageBox.Show("Можно выбрать не более трех правильных ответов!", "Ограничение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    (sender as CheckBox).IsChecked = false;
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

                    return await uploadTask; // это и есть ссылка на storage
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки изображения: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }

            private void LoadQuestionData()
            {
                QuestionText.Text = question.Text;

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

                for (int i = 0; i < 5; i++)
                {
                    if (i < question.Answers.Count)
                    {
                        answerTextBoxes[i].Text = question.Answers[i].Text;
                        answerCheckBoxes[i].IsChecked = question.Answers[i].IsCorrect;

                        if (!string.IsNullOrEmpty(question.Answers[i].ImageUrl) && Uri.IsWellFormedUriString(question.Answers[i].ImageUrl, UriKind.Absolute))
                        {
                            answerImages[i].Source = new BitmapImage(new Uri(question.Answers[i].ImageUrl));
                            answerImageBorders[i].Tag = question.Answers[i].ImageUrl;
                            deleteAnswerImageButtons[i].Visibility = Visibility.Visible;
                        }
                        else
                        {
                            answerImages[i].Source = null;
                            answerImageBorders[i].Tag = null;
                            deleteAnswerImageButtons[i].Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        answerTextBoxes[i].Text = "";
                        answerCheckBoxes[i].IsChecked = false;
                        answerImages[i].Source = null;
                        answerImageBorders[i].Tag = null;
                        deleteAnswerImageButtons[i].Visibility = Visibility.Collapsed;
                    }
                }
            }

            private async void SaveQuestion_Click(object sender, RoutedEventArgs e)
            {
                if (string.IsNullOrWhiteSpace(QuestionText.Text))
                {
                    MessageBox.Show("Введите текст вопроса!");
                    return;
                }

                if (question == null)
                    question = new Question();

                string questionImagePath = QuestionImageBorder.Tag as string;
                string finalQuestionImageUrl = question.ImageUrl;

                if (string.IsNullOrEmpty(questionImagePath) && !string.IsNullOrEmpty(question.ImageUrl))
                    finalQuestionImageUrl = null;
                else if (!string.IsNullOrEmpty(questionImagePath) && File.Exists(questionImagePath))
                    finalQuestionImageUrl = await UploadImageToFirebase(questionImagePath);

                var answers = new List<Answer>();
                for (int i = 0; i < 5; i++)
                {
                    string tempImagePath = answerImageBorders[i].Tag as string;
                    string finalImageUrl = null;

                    if (!string.IsNullOrEmpty(tempImagePath) && Uri.IsWellFormedUriString(tempImagePath, UriKind.Absolute))
                        finalImageUrl = tempImagePath;
                    else if (!string.IsNullOrEmpty(tempImagePath) && File.Exists(tempImagePath))
                        finalImageUrl = await UploadImageToFirebase(tempImagePath);

                    answers.Add(new Answer
                    {
                        Text = answerTextBoxes[i].Text,
                        IsCorrect = answerCheckBoxes[i].IsChecked ?? false,
                        ImageUrl = finalImageUrl
                    });
                }

                if (answers.All(a => !a.IsCorrect))
                {
                    MessageBox.Show("Выберите хотя бы один правильный ответ!");
                    return;
                }

                question.Text = QuestionText.Text;
                question.ImageUrl = finalQuestionImageUrl;
                question.Answers = answers;

                if (!isEditMode)
                    quiz.Questions.Add(question);

                MessageBox.Show("Вопрос сохранён!");
                NavigationService.GoBack();
            }

            private void QuestionTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (StandardTestPanel == null || MatchingTestPanel == null)
                    return; // Элементы еще не инициализированы

                if (QuestionTypeComboBox.SelectedIndex == 0)
                {
                    StandardTestPanel.Visibility = Visibility.Visible;
                    MatchingTestPanel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    StandardTestPanel.Visibility = Visibility.Collapsed;
                    MatchingTestPanel.Visibility = Visibility.Visible;
                }
            }

            private void AddMatchingPair_Click(object sender, RoutedEventArgs e)
            {
                MatchingPairs.Add(new MatchingPair { Options = new ObservableCollection<string> { "текст", "текст" } });
                UpdatePairLetters();
            }

            private void AddOptionToPair_Click(object sender, RoutedEventArgs e)
            {
                var btn = sender as Button;
                var pair = btn.Tag as MatchingPair;
                if (pair != null)
                {
                    pair.Options.Add("текст");
                }
            }

            private void UpdatePairLetters()
            {
                for (int i = 0; i < MatchingPairs.Count; i++)
                {
                    MatchingPairs[i].PairLetter = $"{(char)('a' + i)})";
                }
            }

            private async void SaveMatchingQuestion_Click(object sender, RoutedEventArgs e)
            {
                if (string.IsNullOrWhiteSpace(MainMatchingQuestion))
                {
                    MessageBox.Show("Введите главный вопрос!");
                    return;
                }

                var pairs = new List<MatchingPairData>();
                foreach (var pair in MatchingPairs)
                {
                    string imageUrl = null;
                    if (pair.LeftImage != null)
                    {
                        // Сохраняем картинку во временный файл
                        string tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".png");
                        using (FileStream fs = new FileStream(tempImagePath, FileMode.Create))
                        {
                            PngBitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(pair.LeftImage));
                            encoder.Save(fs);
                        }

                        // Загружаем на Firebase Storage и получаем ссылку
                        imageUrl = await UploadImageToFirebase(tempImagePath);

                        // После загрузки можно удалить временный файл
                        try { File.Delete(tempImagePath); } catch { }
                    }

                    pairs.Add(new MatchingPairData
                    {
                        ImageUrl = imageUrl,
                        Text = pair.LeftText,
                        Options = pair.Options.ToList()
                    });
                }

                // Создаём вопрос типа matching
                var matchingQuestion = new Question
                {
                    Text = MainMatchingQuestion,
                    IsMatching = true,
                    MatchingPairs = pairs
                };

                quiz.Questions.Add(matchingQuestion);

                MessageBox.Show("Matching-вопрос добавлен!");

                NavigationService.GoBack();
            }
            // Преобразование изображения в base64 (PNG)
            private string ImageToBase64(BitmapImage image)
            {
                if (image == null) return null;
                using (MemoryStream ms = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(ms);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
            public class MatchingPair : INotifyPropertyChanged
            {
                private BitmapImage _leftImage;
                public BitmapImage LeftImage
                {
                    get => _leftImage;
                    set { _leftImage = value; OnPropertyChanged(nameof(LeftImage)); }
                }

                public string LeftText { get; set; }
                public ObservableCollection<string> Options { get; set; } = new ObservableCollection<string>();

                private Brush _borderBrush = Brushes.Gray;
                public Brush BorderBrush
                {
                    get => _borderBrush;
                    set { _borderBrush = value; OnPropertyChanged(nameof(BorderBrush)); }
                }

                public string PairLetter { get; set; }

                public event PropertyChangedEventHandler PropertyChanged;
                protected void OnPropertyChanged(string name) =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            private MatchingPair activeMatchingPair = null;

            private void MatchingImageBorder_MouseDown(object sender, MouseButtonEventArgs e)
            {
                var border = sender as Border;
                if (border == null) return;

                var pair = border.DataContext as MatchingPair;
                if (pair == null) return;

                // Снимаем выделение со всех MatchingPairs
                foreach (var p in MatchingPairs)
                    p.BorderBrush = Brushes.Gray;

                // Выделяем текущий
                pair.BorderBrush = Brushes.Blue;
                activeMatchingPair = pair;

                // Вставка картинки, если есть в буфере
                if (Clipboard.ContainsImage())
                {
                    BitmapSource bitmapSource = Clipboard.GetImage();
                    if (bitmapSource != null)
                    {
                        var bmp = new BitmapImage();
                        using (MemoryStream ms = new MemoryStream())
                        {
                            PngBitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                            encoder.Save(ms);
                            ms.Position = 0;
                            bmp.BeginInit();
                            bmp.CacheOption = BitmapCacheOption.OnLoad;
                            bmp.StreamSource = ms;
                            bmp.EndInit();
                            bmp.Freeze();
                        }
                        pair.LeftImage = bmp;
                    }
                }
            }
        }
    }