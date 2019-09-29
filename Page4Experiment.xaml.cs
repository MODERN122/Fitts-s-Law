using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Fitts_s_Law
{
    /// <summary>
    /// Логика взаимодействия для Page4Expirement.xaml
    /// </summary>
    public partial class Page4Experiment : Page
    {
        readonly Stopwatch timer = new Stopwatch();
        private static int number = 4, corner;
        private StringBuilder result = new StringBuilder();
        private Dictionary<int, string> corners = new Dictionary<int, string>(4);

        public List<double>[] list_times = new List<double>[number];
        public Page4Experiment()
        {
            InitializeComponent();
            for (int i = 0; i < number; i++)
            {
                list_times[i] = new List<double>();
            }
            corners.Add(0,"TopLeft: ");
            corners.Add(1, "TopRight: ");
            corners.Add(2, "BottomRight: ");
            corners.Add(3, "BottomLeft: ");
        }

        private void Btn_tap_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            list_times[corner].Add(timer.ElapsedMilliseconds);
            if (list_times[corner].Count >= 2)
            {
                number--;
                if (number == 0)
                {
                    result.Append(corners[number]+list_times[number].Average() + "\n");
                    MessageBox.Show(result.ToString());
                    foreach (var listTime in list_times)
                    {
                        listTime.Clear();
                    }
                    number = 4;
                    btn_tap.Visibility = Visibility.Collapsed;
                    btn_main.IsEnabled = true;
                    result.Clear();
                    return;
                }
                string temp;
                double average = list_times[corner].Average();
                if (corner == number)
                {
                    temp = corners[number];
                    list_times[corner].Clear();
                }
                else
                {
                    temp = corners[corner];
                    corners[corner] = corners[number];
                    list_times[corner].Clear();
                    list_times[corner] = list_times[number];
                }
                result.Append(temp+average + "\n");
            }
            btn_tap.Visibility = Visibility.Collapsed;
            btn_main.IsEnabled = true;
        }

        private void Btn_main_OnClick(object sender, RoutedEventArgs e)
        {
            btn_main.IsEnabled = false;
            var rand = new Random();
            corner = rand.Next(number);
            switch (corners[corner])
            {
                case "TopLeft: ":
                    btn_tap.HorizontalAlignment = HorizontalAlignment.Left;
                    btn_tap.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case "TopRight: ":
                    btn_tap.HorizontalAlignment = HorizontalAlignment.Right;
                    btn_tap.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case "BottomRight: ":
                    btn_tap.HorizontalAlignment = HorizontalAlignment.Right;
                    btn_tap.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                case "BottomLeft: ":
                    btn_tap.HorizontalAlignment = HorizontalAlignment.Left;
                    btn_tap.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
            }
            btn_tap.Visibility = Visibility.Visible;
            timer.Restart();
        }
    }
}
