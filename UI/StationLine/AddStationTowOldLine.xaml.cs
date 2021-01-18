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
using BLAPI;

namespace UI.StationLine
{
    /// <summary>
    /// Interaction logic for AddStationTowOldLine.xaml
    /// </summary>
    public partial class AddStationTowOldLine : Window
    {
        public IBL1 bl;
        public BO.StationLineBO StationLineBO1 { get; set; }
        public BO.StationLineBO StationLineBO2 { get; set; }
        public int NumberLine { get; set; }
        public AddStationTowOldLine(IBL1 bl1, int number, BO.StationLineBO stationLineBO, BO.StationLineBO stationLineBO3)
        {
            InitializeComponent();
            bl = bl1;
            IEnumerable<BO.BusStationBO> busStationBOs = bl.ShowStation();
            StationList.ItemsSource = busStationBOs;
            NumberLine = number;
            StationLineBO1 = stationLineBO;
            StationLineBO2 = stationLineBO3;
        }
        private void AddStation(object sender, RoutedEventArgs e)
        {
            FrameworkElement fxElt = sender as FrameworkElement;
            BO.BusStationBO busStationBO = fxElt.DataContext as BO.BusStationBO;

            BO.StationLineBO stationLineBO = new BO.StationLineBO
            {
                BusLineID2 = NumberLine,
                StationNumberOnLine = busStationBO.StationNumber,
                ChackDelete2 = true,
                LocationNumberOnLine = StationLineBO2.LocationNumberOnLine
            };
            AddTimeDIS addTimeDIS = new AddTimeDIS(bl, StationLineBO1, stationLineBO, StationLineBO2, busStationBO.NameOfStation);
            addTimeDIS.Show();
            Close();
        }
    }
}
