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
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void btnCalc3_Click(object sender, RoutedEventArgs e)
        {
            bool okX0 = double.TryParse(
                txtX0.Text.Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double x0);

            bool okXk = double.TryParse(
                txtXk.Text.Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double xk);

            bool okDx = double.TryParse(
                txtDx.Text.Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double dx);

            bool okA = double.TryParse(
                txtA.Text.Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double a);

            bool okB = double.TryParse(
                txtB.Text.Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double b);

            if (!okX0 || !okXk || !okDx || !okA || !okB)
            {
                MessageBox.Show("Введите корректные числовые значения во все поля.",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (dx == 0)
            {
                MessageBox.Show("Шаг dx не может быть равен 0.",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (x0 < xk && dx < 0)
            {
                MessageBox.Show("Для движения от x0 к xk шаг dx должен быть положительным.",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (x0 > xk && dx > 0)
            {
                MessageBox.Show("Для движения от x0 к xk шаг dx должен быть отрицательным.",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            txtOutput.Clear();
            graphCanvas.Children.Clear();

            try
            {
                List<PointData> points = new List<PointData>();

                if (dx > 0)
                {
                    for (double x = x0; x <= xk; x += dx)
                    {
                        double y = 1.2 * Math.Pow(a - b, 3) * Math.Exp(x * x) + x;
                        txtOutput.AppendText($"x = {x:F2}; y = {y:F6}{Environment.NewLine}");
                        points.Add(new PointData(x, y));
                    }
                }
                else
                {
                    for (double x = x0; x >= xk; x += dx)
                    {
                        double y = 1.2 * Math.Pow(a - b, 3) * Math.Exp(x * x) + x;
                        txtOutput.AppendText($"x = {x:F2}; y = {y:F6}{Environment.NewLine}");
                        points.Add(new PointData(x, y));
                    }
                }

                DrawGraph(points);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при вычислении: " + ex.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void DrawGraph(List<PointData> points)
        {
            if (points == null || points.Count < 2)
                return;

            double canvasWidth = graphCanvas.Width;
            double canvasHeight = graphCanvas.Height;
            double margin = 40;

            double minX = points[0].X;
            double maxX = points[0].X;
            double minY = points[0].Y;
            double maxY = points[0].Y;

            foreach (PointData p in points)
            {
                if (p.X < minX) minX = p.X;
                if (p.X > maxX) maxX = p.X;
                if (p.Y < minY) minY = p.Y;
                if (p.Y > maxY) maxY = p.Y;
            }

            if (Math.Abs(maxX - minX) < 0.000001)
                maxX = minX + 1;

            if (Math.Abs(maxY - minY) < 0.000001)
                maxY = minY + 1;

            Rectangle border = new Rectangle
            {
                Width = canvasWidth - 2 * margin,
                Height = canvasHeight - 2 * margin,
                Stroke = Brushes.Gray,
                StrokeThickness = 1
            };
            Canvas.SetLeft(border, margin);
            Canvas.SetTop(border, margin);
            graphCanvas.Children.Add(border);

            for (int i = 0; i <= 10; i++)
            {
                double x = margin + i * (canvasWidth - 2 * margin) / 10.0;
                Line vertical = new Line
                {
                    X1 = x,
                    Y1 = margin,
                    X2 = x,
                    Y2 = canvasHeight - margin,
                    Stroke = Brushes.LightGray,
                    StrokeThickness = 1
                };
                graphCanvas.Children.Add(vertical);

                double y = margin + i * (canvasHeight - 2 * margin) / 10.0;
                Line horizontal = new Line
                {
                    X1 = margin,
                    Y1 = y,
                    X2 = canvasWidth - margin,
                    Y2 = y,
                    Stroke = Brushes.LightGray,
                    StrokeThickness = 1
                };
                graphCanvas.Children.Add(horizontal);
            }

            Polyline line = new Polyline
            {
                Stroke = Brushes.Blue,
                StrokeThickness = 2
            };

            foreach (PointData p in points)
            {
                double px = margin + (p.X - minX) / (maxX - minX) * (canvasWidth - 2 * margin);
                double py = canvasHeight - margin - (p.Y - minY) / (maxY - minY) * (canvasHeight - 2 * margin);
                line.Points.Add(new System.Windows.Point(px, py));
            }

            graphCanvas.Children.Add(line);

            TextBlock xMinLabel = new TextBlock
            {
                Text = minX.ToString("F2", CultureInfo.InvariantCulture),
                Foreground = Brushes.Black
            };
            Canvas.SetLeft(xMinLabel, margin);
            Canvas.SetTop(xMinLabel, canvasHeight - margin + 5);
            graphCanvas.Children.Add(xMinLabel);

            TextBlock xMaxLabel = new TextBlock
            {
                Text = maxX.ToString("F2", CultureInfo.InvariantCulture),
                Foreground = Brushes.Black
            };
            Canvas.SetLeft(xMaxLabel, canvasWidth - margin - 35);
            Canvas.SetTop(xMaxLabel, canvasHeight - margin + 5);
            graphCanvas.Children.Add(xMaxLabel);

            TextBlock yMinLabel = new TextBlock
            {
                Text = minY.ToString("F2", CultureInfo.InvariantCulture),
                Foreground = Brushes.Black
            };
            Canvas.SetLeft(yMinLabel, 5);
            Canvas.SetTop(yMinLabel, canvasHeight - margin - 10);
            graphCanvas.Children.Add(yMinLabel);

            TextBlock yMaxLabel = new TextBlock
            {
                Text = maxY.ToString("F2", CultureInfo.InvariantCulture),
                Foreground = Brushes.Black
            };
            Canvas.SetLeft(yMaxLabel, 5);
            Canvas.SetTop(yMaxLabel, margin - 10);
            graphCanvas.Children.Add(yMaxLabel);
        }

        private void btnClear3_Click(object sender, RoutedEventArgs e)
        {
            txtX0.Clear();
            txtXk.Clear();
            txtDx.Clear();
            txtA.Clear();
            txtB.Clear();
            txtOutput.Clear();
            graphCanvas.Children.Clear();
        }

        private class PointData
        {
            public double X { get; set; }
            public double Y { get; set; }

            public PointData(double x, double y)
            {
                X = x;
                Y = y;
            }
        }
    }
}