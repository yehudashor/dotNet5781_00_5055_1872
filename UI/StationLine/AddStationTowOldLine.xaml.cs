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
        IBL1 bl = BLFactory.GetBL("1");
        public BO.StationLineBO StationLineBO1 { get; set; }
        public int NumberLine { get; set; }
        public AddStationTowOldLine(int number, BO.StationLineBO stationLineBO)
        {
            InitializeComponent();
            IEnumerable<BO.BusStationBO> busStationBOs = bl.ShowStation();
            StationList.ItemsSource = busStationBOs;
            NumberLine = number;
            StationLineBO1 = stationLineBO;
        }
        private void AddStation(object sender, RoutedEventArgs e)
        {
            FrameworkElement fxElt = sender as FrameworkElement;
            BO.BusStationBO busStationBO = fxElt.DataContext as BO.BusStationBO;
            try
            {
                // int index = 
                BO.StationLineBO stationLineBO = new BO.StationLineBO
                {
                    BusLineID2 = NumberLine,
                    StationNumberOnLine = busStationBO.StationNumber,
                    ChackDelete2 = true,
                    LocationNumberOnLine = ++StationLineBO1.LocationNumberOnLine
                };
                AddTimeDIS addTimeDIS = new AddTimeDIS(stationLineBO, NumberLine, busStationBO.StationNumber);
                _ = addTimeDIS.ShowDialog();
                Close();
            }
            catch (Exception)
            {

                throw;
            }
            //Bus1 = fxElt.DataContext as Bus;

            //AddStation
        }
    }
}
