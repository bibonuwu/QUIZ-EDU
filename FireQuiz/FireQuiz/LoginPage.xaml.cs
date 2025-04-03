using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FireQuiz
{
    public partial class LoginPage : Page
    {
        private const string BaseGitHubUrl = "https://raw.githubusercontent.com/bibonuwu/TEST_QSTEM/refs/heads/main/";
        private readonly Dictionary<string, List<string>> subjectCodes = new Dictionary<string, List<string>>();

        public LoginPage()
        {
            InitializeComponent();
            LoadSubjectsFromGitHub();
        }

        private async void LoadSubjectsFromGitHub()
        {
            try
            {
                var subjects = new Dictionary<string, List<string>>
                {
                    { "Oqu_sauattyluq", new List<string> { "Оқу сауаттылығы" } },
                    { "Math_sauattyluq_and_Math", new List<string> { "Мат сауаттылық", "Матиматика" } },
                    { "Tarih", new List<string> { "Тарих" } },
                    { "Phiziks", new List<string> { "Физика" } },
                    { "Informatics", new List<string> { "Информатика" } },
                    { "Chimya", new List<string> { "Химия" } },
                    { "Biologya", new List<string> { "Биология" } },
                    { "all_subjects", null } // Показывать все предметы
                };

                using (HttpClient client = new HttpClient())
                {
                    foreach (var subject in subjects)
                    {
                        string fileUrl = BaseGitHubUrl + subject.Key;
                        string code = await client.GetStringAsync(fileUrl);

                        if (!string.IsNullOrWhiteSpace(code))
                        {
                            string trimmedCode = code.Trim();

                            if (!subjectCodes.ContainsKey(trimmedCode))
                            {
                                subjectCodes[trimmedCode] = new List<string>();
                            }

                            if (subject.Value != null)
                            {
                                subjectCodes[trimmedCode].AddRange(subject.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки предметов с GitHub: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Box_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем ввод только цифр
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox currentBox = sender as TextBox;

            if (currentBox.Text.Length == 1) // Если ввели цифру, переход на следующее поле
            {
                if (currentBox == Box1) Box2.Focus();
                else if (currentBox == Box2) Box3.Focus();
                else if (currentBox == Box3) Box4.Focus();
                else if (currentBox == Box4) Box4.Focus(); // Остаемся в последнем поле
            }
        }

        private void Box_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox currentBox = sender as TextBox;

            if (e.Key == Key.Back) // Если нажали Backspace
            {
                if (currentBox.Text == "") // Если поле уже пустое, перейти на предыдущее
                {
                    if (currentBox == Box2) { Box1.Focus(); Box1.Clear(); }
                    else if (currentBox == Box3) { Box2.Focus(); Box2.Clear(); }
                    else if (currentBox == Box4) { Box3.Focus(); Box3.Clear(); }
                }
                else
                {
                    currentBox.Clear(); // Очищаем текущее поле
                }

                e.Handled = true; // Запрещаем стандартное поведение Backspace
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string code = Box1.Text + Box2.Text + Box3.Text + Box4.Text;

            if (code == "0000")
            {
                // Открываем страницу со всеми предметами
                NavigationService.Navigate(new SubjectsPage());
                return;
            }

            if (subjectCodes.ContainsKey(code))
            {
                NavigationService.Navigate(new SubjectsPage(subjectCodes[code]));
            }
            else
            {
                MessageBox.Show("Неверный код или предмет не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
