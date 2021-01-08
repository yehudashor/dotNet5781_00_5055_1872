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

namespace UI.StationShow
{
    /// <summary>
    /// Interaction logic for ConsecutiveStations.xaml
    /// </summary>
    public partial class ConsecutiveStations : Window
    {
        public static ObservableCollection<BO.ConsecutiveStationsBo> busLineBOs = new ObservableCollection<BO.ConsecutiveStationsBo>();
        IBL1 bl = BLFactory.GetBL("1");
        public ConsecutiveStations()
        {
            foreach (BO.ConsecutiveStationsBo item in bl.ShowConsecutiveStationsBo())
            {
                busLineBOs.Add(item);
            }
            InitializeComponent();
            consecutiveStationsBoListView.DataContext = busLineBOs;
        }

        private void Updating(object sender, RoutedEventArgs e)
        {
            FrameworkElement fxElt = sender as FrameworkElement;
            BO.ConsecutiveStationsBo consecutiveStationsBo = fxElt.DataContext as BO.ConsecutiveStationsBo;
            UdaptingDt udaptingDt = new UdaptingDt(consecutiveStationsBo);
            udaptingDt.Show();
            consecutiveStationsBoListView.Items.Refresh();
        }
    }
}
