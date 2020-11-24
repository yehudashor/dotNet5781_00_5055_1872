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
using dotNet_01_5055_1872;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly List<Bus> listBus = new List<Bus>();
        private static readonly Random r = new Random(DateTime.Now.Millisecond);

        public Window1()
        {
            for (int i = 0; i < r.Next(10, 13); i++)
            {
                int number = r.Next(1200);
                DateTime _StartDate = new DateTime(r.Next(1990, 2020), r.Next(1, 13), r.Next(1, 32));
                listBus.Add(new Bus() { License_number = r.Next(1000000, 10000000).ToString(), StartDate = _StartDate, KmForRefueling = number, KmForTreatment = number * r.Next(3, 15), TotalMiles = number * r.Next(5, 15), DayOfTreatment = new DateTime(_StartDate.Year + r.Next(1, 5), r.Next(1, 12), r.Next(1, 32)) });
            }
            for (int i = 0; i < 2; i++)
            {
                listBus[r.Next(10)].DayOfTreatment = DateTime.Now.AddDays(-r.Next(5));
                listBus[r.Next(10)].KmForTreatment = r.Next(19000, 20000);
            }
            InitializeComponent();
            Bus.ItemsSource = listBus;
            Bus.DisplayMemberPath = "License_number";
            Bus.SelectedIndex = -1;
            Bus.SelectionChanged += Bus_SelectionChanged;
        }

        
    }
}
