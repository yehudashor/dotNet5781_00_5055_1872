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
        public ObservableCollection<BO.ConsecutiveStationsBo> busLineBOs = new ObservableCollection<BO.ConsecutiveStationsBo>();
        public IBL1 bl;
        public ConsecutiveStations(IBL1 bl1)
        {
            InitializeComponent();
            bl = bl1;
            try
            {
                foreach (BO.ConsecutiveStationsBo item in bl.ShowConsecutiveStationsBo())
                {
                    busLineBOs.Add(item);
                }
                consecutiveStationsBoListView.DataContext = busLineBOs;
            }
            catch (BO.BOExceptionStation ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }
    }
}
