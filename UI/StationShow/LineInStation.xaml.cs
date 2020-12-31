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
        }
    }
}
