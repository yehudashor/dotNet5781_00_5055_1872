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
        public IBL1 bl;
        BO.StationLineBO StationLineBO1;
        public int index { get; set; }
        // private readonly BO.ConsecutiveStationsBo consecutiveStationsBo;
        public UdaptingDt(BO.StationLineBO StationLineBO, int index1, IBL1 bl1)
        {
            InitializeComponent();
            bl = bl1;
            StationLineBO1 = StationLineBO;
            index = index1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.ConsecutiveStationsBo consecutiveStationsBo = new BO.ConsecutiveStationsBo { StationNumber1 = StationLineBO1.StationNumberOnLine, StationNumber2 = index };
                consecutiveStationsBo.DistanceBetweenTooStations = float.Parse(D.Text);
                consecutiveStationsBo.AverageTime = new TimeSpan(int.Parse(H.Text), int.Parse(M.Text), int.Parse(S.Text));
                bl.Udapting(consecutiveStationsBo);
                //ShowLine.busLineBOs[StationLineBO1.LocationNumberOnLine].AverageTime = new TimeSpan(int.Parse(H.Text), int.Parse(M.Text), int.Parse(S.Text));
                //ShowLine.busLineBOs[StationLineBO1.LocationNumberOnLine].DistanceBetweenTooStations = float.Parse(D.Text);
                //for (int i = 0; i < Line.busLineBOs.Count; i++)
                //{
                //    for (int j = 0; j < Line.busLineBOs[i].StationLineBOs.Count - 1; j++)
                //    {
                //        if (Line.busLineBOs[i].StationLineBOs[j].StationNumberOnLine == StationLineBO1.StationNumberOnLine && Line.busLineBOs[i].StationLineBOs[j + 1].StationNumberOnLine == index)
                //        {
                //            _ = Line.busLineBOs[i].StationLineBOs[j].AverageTime == consecutiveStationsBo.AverageTime;
                //            _ = Line.busLineBOs[i].StationLineBOs[j].DistanceBetweenTooStations = consecutiveStationsBo.DistanceBetweenTooStations;
                //        }
                //    }
                //}
                Close();
            }
            catch (BO.BOExceptionConsecutiveStations ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }
    }
}
