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
using System.Windows.Shapes;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace TestApp
{
    /// <summary>
    /// Логика взаимодействия для Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        public Calculator()
        {
            InitializeComponent();
            AssignButtonEvents();
        }

        private void AssignButtonEvents()
        {
            if (this.Content is Border border && border.Child is Grid rootGrid)
            {
                AttachButtonHandlersRecursive(rootGrid);
            }
        }

        private void AttachButtonHandlersRecursive(Panel panel)
        {
            foreach (UIElement element in panel.Children)
            {
                if (element is Button button && button.Content != null)
                {
                    button.Click += Button_Click;
                }
                else if (element is Panel innerPanel)
                {
                    AttachButtonHandlersRecursive(innerPanel); // Рекурсивный вызов
                }
                else if (element is Border border && border.Child is Panel borderPanel)
                {
                    AttachButtonHandlersRecursive(borderPanel); // Если кнопки внутри Border
                }
                else if (element is ScrollViewer scrollViewer && scrollViewer.Content is Panel scrollPanel)
                {
                    AttachButtonHandlersRecursive(scrollPanel); // Если ScrollViewer с панелью
                }
            }
        }






        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string content = button.Content.ToString();

            switch (content)
            {
                case "C":
                    ExpressionTextBox.Text = "";
                    break;
                case "⌫":
                    if (ExpressionTextBox.Text.Length > 0)
                        ExpressionTextBox.Text = ExpressionTextBox.Text.Substring(0, ExpressionTextBox.Text.Length - 1);
                    break;
                case "=":
                    CalculateResult();
                    break;
                case "±":
                    ToggleSign();
                    break;
                default:
                    ExpressionTextBox.Text += content;
                    break;
            }
        }


        private void CalculateResult()
        {
            try
            {
                string expression = ExpressionTextBox.Text
                    .Replace("×", "*")
                    .Replace("÷", "/");

                expression = ConvertPercentages(expression);

                var result = new DataTable().Compute(expression, null);
                ExpressionTextBox.Text = result.ToString();
            }
            catch
            {
                ExpressionTextBox.Text = "Қателеу!";
            }
        }
        private string ConvertPercentages(string expression)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"(\d+(\.\d+)?)%");
            var matches = regex.Matches(expression);

            foreach (System.Text.RegularExpressions.Match match in matches.Cast<System.Text.RegularExpressions.Match>().Reverse())
            {
                string percentStr = match.Groups[1].Value;
                int percentIndex = match.Index;

                // Найдём базовое значение — число перед текущим оператором (%)
                double baseValue = FindBaseForPercentage(expression, percentIndex);

                // Заменяем 10% → (base * 10 / 100)
                string replacement = $"({baseValue}*{percentStr}/100)";
                expression = expression.Remove(percentIndex, match.Length).Insert(percentIndex, replacement);
            }

            return expression;
        }

        private double FindBaseForPercentage(string expression, int percentIndex)
        {
            // Ищем число перед оператором (например, перед +, -, *, /)
            int i = percentIndex - 1;

            // Пропустить саму цифру процента
            while (i >= 0 && (char.IsDigit(expression[i]) || expression[i] == '.')) i--;

            // Теперь идем назад до знака операции
            while (i >= 0 && expression[i] == ' ') i--;

            // Теперь ищем базовое значение — число перед оператором
            int operatorIndex = -1;

            for (int j = i; j >= 0; j--)
            {
                if ("+-*/".Contains(expression[j]))
                {
                    operatorIndex = j;
                    break;
                }
            }

            // Если не нашли оператор — значит всё выражение до %
            string baseNumberStr;

            if (operatorIndex >= 0)
            {
                // Ищем число справа от оператора
                int k = operatorIndex - 1;
                while (k >= 0 && expression[k] == ' ') k--;

                int end = k;
                while (k >= 0 && (char.IsDigit(expression[k]) || expression[k] == '.')) k--;

                baseNumberStr = expression.Substring(k + 1, end - k);
            }
            else
            {
                // Если не нашли, берём всё выражение до процента
                int end = percentIndex - 1;
                while (end >= 0 && (char.IsDigit(expression[end]) || expression[end] == '.')) end--;
                baseNumberStr = expression.Substring(end + 1, percentIndex - end - 1);
            }

            return double.TryParse(baseNumberStr, out double baseValue) ? baseValue : 1.0;
        }

      



        private void ToggleSign()
        {
            if (double.TryParse(ExpressionTextBox.Text, out double number))
            {
                number = -number;
                ExpressionTextBox.Text = number.ToString();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}

