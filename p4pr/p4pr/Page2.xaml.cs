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
using System.Globalization;

namespace p4pr
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private double GetFx(double x)
        {
            if (rbSh.IsChecked == true)
                return Math.Sinh(x);

            if (rbX2.IsChecked == true)
                return Math.Pow(x, 2);

            return Math.Exp(x);
        }

        private void btnCalc2_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtX2.Text) ||
                string.IsNullOrWhiteSpace(txtY2.Text))
            {
                MessageBox.Show("Заполните поля x и y.",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            bool okX = double.TryParse(
                txtX2.Text.Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double x);

            bool okY = double.TryParse(
                txtY2.Text.Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double y);

            if (!okX || !okY)
            {
                MessageBox.Show("Введите корректные числовые значения.",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            try
            {
                double fx = GetFx(x);
                double b;

                if (y == 0)
                {
                    b = 0;
                }
                else if (x == 0)
                {
                    b = Math.Pow(fx * fx + y, 3);
                }
                else if (x / y > 0)
                {
                    if (fx <= 0)
                    {
                        MessageBox.Show("Нельзя вычислить ln(f(x)), так как f(x) <= 0.",
                            "Ошибка области определения",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        return;
                    }

                    b = Math.Log(fx) + Math.Pow(fx * fx + y, 3);
                }
                else
                {
                    double expr = Math.Abs(fx / y);

                    if (expr <= 0)
                    {
                        MessageBox.Show("Нельзя вычислить ln|f(x)/y|.",
                            "Ошибка области определения",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        return;
                    }

                    b = Math.Log(expr) + Math.Pow(fx + y, 3);
                }

                txtResult2.Text = b.ToString("F6", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при вычислении: " + ex.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void btnClear2_Click(object sender, RoutedEventArgs e)
        {
            txtX2.Clear();
            txtY2.Clear();
            txtResult2.Clear();
            rbSh.IsChecked = true;
        }
    }
}