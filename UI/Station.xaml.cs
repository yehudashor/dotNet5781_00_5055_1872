using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UI.StationShow;

namespace UI
{
    /// <summary>
    /// Interaction logic for Station.xaml
    /// </summary>
    public partial class Station : Window
    {
        IBL1 bl = BLFactory.GetBL("1");
        public static ObservableCollection<BO.BusStationBO> busLineBOs = new ObservableCollection<BO.BusStationBO>();
        public Station()
        {
            InitializeComponent();
            IEnumerable<BO.BusStationBO> busStationBOs = bl.ShowStation();

            StationList.ItemsSource = busStationBOs;
        }

        private void Station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Station1 station1 = new Station1((BO.BusStationBO)StationList.SelectedItem);
            _ = station1.ShowDialog();
        }

        private void Linespassingstation(object sender, RoutedEventArgs e)
        {
            BO.BusStationBO busStationBO = new BO.BusStationBO();
            FrameworkElement fxElt = sender as FrameworkElement;
            busStationBO = fxElt.DataContext as BO.BusStationBO;
            try
            {
                LineInStation lineInStation = new LineInStation(bl.LinePastInStationBOs(busStationBO.StationNumber));
                _ = lineInStation.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
            //Bus1 = fxElt.DataContext as Bus;

        }
    }
}
