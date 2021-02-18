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
using UI.PO;

namespace UI.StationShow
{
    /// <summary>
    /// Interaction logic for UdaptingDt.xaml
    /// </summary>
    public partial class UdaptingDt : Window
    {
        public IBL1 bl;
        public StationLinePO StationLineBO1;
        public int index { get; set; }
        public Line line;
        public ShowLine showLine;
        public UdaptingDt(StationLinePO StationLineBO, int index1, IBL1 bl1, Line line1, ShowLine showLine1)
        {
            InitializeComponent();
            bl = bl1;
            line = line1;
            showLine = showLine1;
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

                for (int i = 0; i < line.busLineBOs.Count; i++)
                {
                    for (int j = 0; j < line.busLineBOs[i].StationLineBOs.Count - 1; j++)
                    {
                        if (line.busLineBOs[i].StationLineBOs[j].StationNumberOnLine == StationLineBO1.StationNumberOnLine && line.busLineBOs[i].StationLineBOs[j + 1].StationNumberOnLine == index)
                        {
                            line.busLineBOs[i].StationLineBOs[j].AverageTime = consecutiveStationsBo.AverageTime;
                            line.busLineBOs[i].StationLineBOs[j].DistanceBetweenTooStations = consecutiveStationsBo.DistanceBetweenTooStations;
                        }
                    }
                }
                showLine.busStationBOListView.Items.Refresh();
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
