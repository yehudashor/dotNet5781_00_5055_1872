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
using UI.PO;
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
        public StationPO stationPO1;
        public ObservableCollection<BO.BusStationBO> busLineBOs1 = new ObservableCollection<BO.BusStationBO>();
        public ObservableCollection<StationPO> busLineBOs = new ObservableCollection<StationPO>();
        public Station(IBL1 bl1)
        {
            InitializeComponent();

            try
            {
                bl = bl1;
                List<BO.BusStationBO> busStationBOs = bl.ShowStation().ToList();

                foreach (BO.BusStationBO item in busStationBOs)
                {
                    busLineBOs1.Add(item);
                }
                for (int i = 0; i < busLineBOs1.Count; i++)
                {
                    StationPO stationPO = new StationPO();
                    busLineBOs1[i].DeepCopyTo(stationPO);
                    busLineBOs.Add(stationPO);
                }

                StationList.ItemsSource = busLineBOs;
            }
            catch (BO.BOExceptionStation ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }

        //public Station(BO.BusLineBO BusLine)
        //{
        //    InitializeComponent();
        //    IEnumerable<BO.BusStationBO> busStationBOs = bl.ShowStation();
        //    busStationBOs.DeepCopyTo(busLineBOs);
        //    StationList.ItemsSource = busStationBOs;
        //    BusLine1 = BusLine;
        //}


        private void Station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Station1 station1 = new Station1((StationPO)StationList.SelectedItem);
            _ = station1.ShowDialog();
        }

        private void Linespassingstation(object sender, RoutedEventArgs e)
        {
            FrameworkElement fxElt = sender as FrameworkElement;
            StationPO busStationPO = fxElt.DataContext as StationPO;
            try
            {
                LineInStation lineInStation = new LineInStation(bl.LinePastInStationBOs(busStationPO.StationNumber));
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

            AddStation addStation = new AddStation(bl, this);
            addStation.Show();
        }


        private void Udapting(object sender, RoutedEventArgs e)
        {
            FrameworkElement fxElt = sender as FrameworkElement;
            StationPO busStationPO = fxElt.DataContext as StationPO;
            Udapting udapting = new Udapting(busStationPO, bl);
            udapting.Show();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement fxElt = sender as FrameworkElement;
                StationPO busStationPO = fxElt.DataContext as StationPO;
                bl.DeleteStationFromDo(busStationPO.StationNumber);
                _ = busLineBOs.Remove(busStationPO);
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
