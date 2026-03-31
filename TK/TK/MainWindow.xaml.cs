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

namespace TK
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(DistanceTextBox.Text, out double distance) || distance <= 0)
            {
                MessageBox.Show("Введите корректное расстояние");
                return;
            }

            if (!int.TryParse(TicketsTextBox.Text, out int tickets) || tickets <= 0)
            {
                MessageBox.Show("Введите корректное количество билетов");
                return;
            }

            double coefficient = 1.0;

            if (Kupe.IsChecked == true)
                coefficient = 1.1;
            else if (Polylux.IsChecked == true)
                coefficient = 1.2;
            else if (Lux.IsChecked == true)
                coefficient = 1.3;

            double result = distance * 8 * coefficient * tickets;

            ResultBox.Text = result.ToString("F2") + " руб.";
        }
    }
}