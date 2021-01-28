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

namespace UI.StationShow
{
    /// <summary>
    /// Interaction logic for LineInStation.xaml
    /// </summary>
    public partial class LineInStation : Window
    {
        public LineInStation(IEnumerable<BO.BusLineBO> busLineBOs)
        {
            InitializeComponent();
            linesPastInStationListView.ItemsSource = busLineBOs;
            //var a = new int[] { 1, 2, 3, 4, 7 };
            //var b = new string[] { "19", "3", "24", "a" };
            //var ans = (
            // from x in a
            // from y in b
            // where y.StartsWith(x.ToString())
            // select x + 0.1
            //).OrderByDescending(q => q).FirstOrDefault();
            //foreach (var item in ans)
            //{
            //    Console.WriteLine(item + " " + item.GetType());
            //}

        }
    }
}
