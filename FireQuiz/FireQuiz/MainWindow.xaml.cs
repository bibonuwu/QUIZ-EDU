using System.Windows;
using System.Windows.Controls;

namespace FireQuiz
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            if (!IsInternetAvailable())
            {
                MessageBox.Show("Нет подключения к интернету. Пожалуйста, проверьте соединение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(); // Закрыть приложение, если нет интернета
                return;
            }

            InitializeComponent();
            MainFrame.Navigate(new LoginPage());
        }
        private bool IsInternetAvailable()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                using (client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private void MenuMain_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LoginPage());
        }

    }
}
