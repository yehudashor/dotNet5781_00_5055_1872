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
        BO.BusLineBO BusLine1;
        public static ObservableCollection<BO.BusStationBO> busLineBOs = new ObservableCollection<BO.BusStationBO>();
        public Station()
        {
            IEnumerable<BO.BusStationBO> busStationBOs = bl.ShowStation();
            foreach (BO.BusStationBO item in busStationBOs)
            {
                busLineBOs.Add(item);
            }
            InitializeComponent();
            StationList.ItemsSource = busLineBOs;
        }

        public Station(BO.BusLineBO BusLine)
        {
            InitializeComponent();
            IEnumerable<BO.BusStationBO> busStationBOs = bl.ShowStation();
            StationList.ItemsSource = busStationBOs;
            BusLine1 = BusLine;
        }


        private void Station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Station1 station1 = new Station1((BO.BusStationBO)StationList.SelectedItem);
            _ = station1.ShowDialog();
        }

        private void Linespassingstation(object sender, RoutedEventArgs e)
        {
            _ = new BO.BusStationBO();
            FrameworkElement fxElt = sender as FrameworkElement;
            BO.BusStationBO busStationBO = fxElt.DataContext as BO.BusStationBO;
            try
            {
                LineInStation lineInStation = new LineInStation(bl.LinePastInStationBOs(busStationBO.StationNumber));
                _ = lineInStation.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            AddStation addStation = new AddStation();
            addStation.Show();
        }


        private void Udapting(object sender, RoutedEventArgs e)
        {
            BO.BusStationBO busStationBO = new BO.BusStationBO();
            FrameworkElement fxElt = sender as FrameworkElement;
            busStationBO = fxElt.DataContext as BO.BusStationBO;
            Udapting udapting = new Udapting(busStationBO);
            udapting.Show();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            FrameworkElement fxElt = sender as FrameworkElement;
            BO.BusStationBO busStationBO = fxElt.DataContext as BO.BusStationBO;
            bl.DeleteStationFromDo(busStationBO.StationNumber);
            _ = busLineBOs.Remove(busStationBO);
            busLineBOs.Add(bl.ReturnStationToPL(busStationBO.StationNumber));
            // _ = ShowLine.busLineBOs.Remove(busStationBO);
            StationList.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConsecutiveStations consecutiveStations = new ConsecutiveStations();
            consecutiveStations.Show();
        }
    }
}
