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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class PageButton : Page
    {
        readonly Stopwatch timer = new Stopwatch();
        private int D = 3, S = 200;
        static int number = 15;
        private int randColor;
        public List<double>[] list_times = new List<double>[number];

        private int count;

        StringBuilder result = new StringBuilder();

        private Color[] colorses =
        {
            Colors.Aqua, Colors.Yellow, Colors.Fuchsia, Colors.Gray, Colors.Green, Colors.Lime,
            Colors.Maroon, Colors.Navy, Colors.Olive, Colors.Purple, Colors.Red, Colors.Silver, Colors.Teal,
            Colors.White, Colors.Black
        };
        public PageButton()
        {
            InitializeComponent();
            btn_tap.Height = D;
            btn_tap.Width = D * 3;
            for (int i = 0; i < number; i++)
            {
                list_times[i] = new List<double>();
            }
        }
        public void TapButton()
        {
            timer.Stop();
            var milliSeconds = timer.ElapsedMilliseconds;
            list_times[randColor].Add(milliSeconds);
            count++;
            if (list_times[randColor].Count >= 10)
            {
                number--;
                if (number == 0)
                {
                    result.Append(colorses[number].ToString() + ":" + list_times[number].Average() + "\n");
                    MessageBox.Show(result.ToString());
                    for(int i=0; i<15; i++)
                        list_times[i].Clear();
                    number = 15;
                    btn_tap.Visibility = Visibility.Collapsed;
                    btn_main.IsEnabled = true;
                    result.Clear();
                    return;
                }
                double average = list_times[randColor].Average();
                if (randColor == number)
                {
                    list_times[randColor].Clear();
                    list_times[randColor].Add(average);
                }
                else
                {
                    List<double> tempList = list_times[number];
                    Color tempColor = colorses[number];
                    colorses[number] = colorses[randColor];
                    colorses[randColor] = tempColor;
                    list_times[number].Clear();
                    list_times[number].Add(average);
                    list_times[randColor].Clear();
                    list_times[randColor] = tempList;
                }
                result.Append(colorses[number].ToString() + ":" + list_times[number][0] + "\n");
            }
            btn_tap.Visibility = Visibility.Collapsed;
            btn_main.IsEnabled = true;
        }

        public void Btn_tap_Click(object sender, RoutedEventArgs e)
        {
            TapButton();
        }
        private void Btn_main_OnClick(object sender, RoutedEventArgs e)
        {
            btn_main.IsEnabled = false;
            var rand = new Random();
            var angle = rand.Next(360);
            var x = Math.Cos(Math.PI * angle / 180) * S;
            var y = Math.Sin(Math.PI * angle / 180) * S;
            randColor = rand.Next(number - 1);
            btn_tap.Margin = new Thickness(x, y, 0, 0);
            btn_tap.Background = new SolidColorBrush(colorses[randColor]);
            Window.Background = new SolidColorBrush(Colors.Blue);
            btn_tap.Visibility = Visibility.Visible;
            timer.Restart();
        }

    }
}
