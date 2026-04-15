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
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void btnCalc1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtX.Text) ||
                string.IsNullOrWhiteSpace(txtY.Text) ||
                string.IsNullOrWhiteSpace(txtZ.Text))
            {
                MessageBox.Show("Заполните все поля: x, y, z.",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            bool okX = double.TryParse(
                txtX.Text.Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double x);

            bool okY = double.TryParse(
                txtY.Text.Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double y);

            bool okZ = double.TryParse(
                txtZ.Text.Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double z);

            if (!okX || !okY || !okZ)
            {
                MessageBox.Show("Введите корректные числовые значения.",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            try
            {
                double part1 = Math.Pow(8 + Math.Pow(Math.Abs(x - y), 2) + 1, 1.0 / 3.0);
                double part2 = x * x + y * y + 2;
                double part3 = Math.Exp(Math.Abs(x - y)) * Math.Pow(Math.Pow(Math.Tan(z), 2) + 1, x);

                double v = part1 / part2 - part3;

                txtResult.Text = v.ToString("F6", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при вычислении: " + ex.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void btnClear1_Click(object sender, RoutedEventArgs e)
        {
            txtX.Clear();
            txtY.Clear();
            txtZ.Clear();
            txtResult.Clear();
        }
    }
}