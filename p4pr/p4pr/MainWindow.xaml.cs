using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace p4pr
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new Page1());
        }

        private void btnPage1_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Page1());
        }

        private void btnPage2_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Page2());
        }

        private void btnPage3_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Page3());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
            "Вы действительно хотите выйти из приложения?",
            "Подтверждение выхода",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}

