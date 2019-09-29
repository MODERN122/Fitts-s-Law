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
    /// Логика взаимодействия для Page3Experiment.xaml
    /// </summary>
    public partial class Page3Experiment : Page
    {
        readonly Stopwatch timer = new Stopwatch();
        private static int S = 200, number=11, indexIndent;
        private StringBuilder result = new StringBuilder();
        public List<double>[] list_times = new List<double>[number];
        private int[] indents = {0, 1, 2, 3, 4, 5, 7, 10, 15, 20, 25};
        public Page3Experiment()
        {
            InitializeComponent();
            for (int i = 0; i < number; i++)
            {
                list_times[i] = new List<double>();
            }
        }

        private void Btn_main_OnClick(object sender, RoutedEventArgs e)
        {
            btn_main.IsEnabled = false;
            var rand = new Random();
            var hNext = rand.Next((int)Exp3.WindowHeight);
            indexIndent = rand.Next(number);
            var flag = rand.Next(2);
            if (flag == 1)
            {
                btn_tap.Margin = new Thickness(indents[indexIndent], hNext, 0,0);
                btn_tap.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                btn_tap.Margin=new Thickness( 0, hNext, indents[indexIndent], 0);
                btn_tap.HorizontalAlignment = HorizontalAlignment.Right;
            }
            btn_tap.Visibility = Visibility.Visible;
            timer.Restart();
        }

        private void Btn_tap_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            list_times[indexIndent].Add(timer.ElapsedMilliseconds);
            if (list_times[indexIndent].Count >= 8)
            {
                number--;
                if (number == 0)
                {
                    result.Append(indents[number].ToString() + ":" + list_times[number].Average() + "\n");
                    MessageBox.Show(result.ToString());
                    foreach (var listTime in list_times)
                    {
                            listTime.Clear();
                    }
                    number = 11;
                    btn_tap.Visibility = Visibility.Collapsed;
                    btn_main.IsEnabled = true;
                    result.Clear();
                    return;
                }
                double average = list_times[indexIndent].Average();
                if (indexIndent == number)
                {
                    list_times[indexIndent].Clear();
                    list_times[indexIndent].Add(average);
                }
                else
                {
                    List<double> tempList = list_times[number];
                    int tempIndent = indents[number];
                    indents[number] = indents[indexIndent];
                    indents[indexIndent] = tempIndent;
                    list_times[number].Clear();
                    list_times[number].Add(average);
                    list_times[indexIndent].Clear();
                    list_times[indexIndent] = tempList;
                }
                result.Append(indents[number].ToString() + ":" + list_times[number][0] + "\n");
            }
            btn_tap.Visibility = Visibility.Collapsed;
            btn_main.IsEnabled = true;
            
        }
    }
}
