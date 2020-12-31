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
    /// Interaction logic for Station1.xaml
    /// </summary>
    public partial class Station1 : Window
    {
        BO.BusStationBO Station = new BO.BusStationBO();
        public Station1(BO.BusStationBO BusStationBO1)
        {
            Station = BusStationBO1;
            InitializeComponent();
            StationList1.DataContext = Station;
        }
    }
}
