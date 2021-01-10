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

namespace UI.StationShow
{
    /// <summary>
    /// Interaction logic for UdaptingDt.xaml
    /// </summary>
    public partial class UdaptingDt : Window
    {
        IBL1 bl = BLFactory.GetBL("1");
        BO.ConsecutiveStationsBo consecutiveStationsBo;
        public UdaptingDt(BO.ConsecutiveStationsBo consecutiveStationsBo1)
        {
            consecutiveStationsBo = consecutiveStationsBo1;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                consecutiveStationsBo.AverageTime = TimeSpan.Parse("H.Text:, M.Text:, S.Text");
                consecutiveStationsBo.DistanceBetweenTooStations = float.Parse(D.Text);
                bl.Udapting(consecutiveStationsBo);
                _ = ConsecutiveStations.busLineBOs.Remove(consecutiveStationsBo);
                ConsecutiveStations.busLineBOs.Add(bl.ShowOneConsecutiveStations(consecutiveStationsBo.StationNumber1, consecutiveStationsBo.StationNumber2));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
