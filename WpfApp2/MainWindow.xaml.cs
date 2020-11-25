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
using dotNet_01_5055_1872;
namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<Bus> listBus = new List<Bus>();
        private static readonly Random r = new Random(DateTime.Now.Millisecond);
        public MainWindow()
        {
            for (int i = 0; i < r.Next(10, 15); i++)
            {
                int number = r.Next(1200);
                DateTime _StartDate = new DateTime(r.Next(1900, 2020), r.Next(1, 13), r.Next(1, 32));
                double number1 = _StartDate.Year >= 2018 ? r.Next(10000000, 100000000) : r.Next(1000000, 10000000);
                DateTime _StartDate1 = new DateTime(_StartDate.Year + r.Next(1, 5), r.Next(1, 13), r.Next(1, 32));
                Bus bus = new Bus(number1.ToString(), _StartDate, number, number * r.Next(3, 15), number * r.Next(5, 15), _StartDate1);
                listBus.Add(bus);
            }
            for (int i = 0; i < 2; i++)
            {
                listBus[r.Next(10)].DayOfTreatment = DateTime.Now.AddDays(-r.Next(5));
                listBus[r.Next(10)].KmForTreatment = r.Next(19000, 20000);
            }
            InitializeComponent();
            Bus.ItemsSource = listBus;
            Bus.SelectedIndex = -1;
            Bus.SelectionChanged += NewBus1;
            Bus.SelectionChanged += Bus_SelectionChanged;
        }
        private void Bus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = MessageBox.Show((string)Bus.ItemsSource);
        }

        private void NewBus1(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
        }
    }

}
