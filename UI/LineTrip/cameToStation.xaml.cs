using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace UI.LineTrip
{
    /// <summary>
    /// Interaction logic for cameToStation.xaml
    /// </summary>
    public partial class cameToStation : Window
    {
        public IBL1 bl;
        internal ObservableCollection<BO.BusLineBO> busLineBOs = new ObservableCollection<BO.BusLineBO>();
        public BackgroundWorker Worker;
        public cameToStation(IBL1 bl1)
        {

            bl = bl1;
            lines.ItemsSource = Lines(busLineBOs);
            InitializeComponent();
        }
        private ObservableCollection<BO.BusLineBO> Lines(ObservableCollection<BO.BusLineBO> busLineBOs)
        {
            try
            {
                int count = bl.ReturnBusLineIdFromDl();
                for (int i = 0; i < count; i++)
                {
                    busLineBOs.Add(bl.LineInformation(i));
                }
            }
            catch (BO.BOExceptionLine ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                    MessageBoxImage.Error);
            }
            catch (BO.BOExceptionLineStation ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                    MessageBoxImage.Error);
            }
            catch (BO.BOExceptionConsecutiveStations ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
            return busLineBOs;
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.BusLineBO busLineBO = (BO.BusLineBO)lines.SelectedItem;
            Stations.ItemsSource = busLineBO.StationLineBOs;
        }

        private void Stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
