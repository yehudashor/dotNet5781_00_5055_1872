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
        public IBL1 bl;
        BO.BusLineBO BusLine1;
        public ObservableCollection<BO.BusStationBO> busLineBOs = new ObservableCollection<BO.BusStationBO>();
        public Station(IBL1 bl1)
        {
            InitializeComponent();
            try
            {
                bl = bl1;
                IEnumerable<BO.BusStationBO> busStationBOs = bl.ShowStation();
                foreach (BO.BusStationBO item in busStationBOs)
                {
                    busLineBOs.Add(item);
                }
                StationList.ItemsSource = busLineBOs;
            }
            catch (BO.BOExceptionStation ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
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
            catch (BO.BOExceptionLine ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                      MessageBoxImage.Error);
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            AddStation addStation = new AddStation(bl);
            addStation.Show();
            Close();
        }


        private void Udapting(object sender, RoutedEventArgs e)
        {
            BO.BusStationBO busStationBO = new BO.BusStationBO();
            FrameworkElement fxElt = sender as FrameworkElement;
            busStationBO = fxElt.DataContext as BO.BusStationBO;
            Udapting udapting = new Udapting(busStationBO, bl);
            udapting.Show();
            Close();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement fxElt = sender as FrameworkElement;
                BO.BusStationBO busStationBO = fxElt.DataContext as BO.BusStationBO;
                bl.DeleteStationFromDo(busStationBO.StationNumber);
                _ = busLineBOs.Remove(busStationBO);
                busLineBOs.Add(bl.ReturnStationToPL(busStationBO.StationNumber));
                StationList.Items.Refresh();
            }
            catch (BO.BOExceptionStation ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConsecutiveStations consecutiveStations = new ConsecutiveStations(bl);
            consecutiveStations.Show();
        }
    }
}
