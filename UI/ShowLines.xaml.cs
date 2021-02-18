using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using BO;
using UI.lines;
using UI.PO;
using UI.StationLine;
using UI.StationShow;

namespace UI
{
    /// <summary>
    /// Interaction logic for ShowLine.xaml
    /// </summary>
    public partial class ShowLine : Window
    {
        public IBL1 bl;
        public ObservableCollection<StationLinePO> busLineBOs = new ObservableCollection<StationLinePO>();
        public ObservableCollection<LineExitPO> lineExitBos = new ObservableCollection<LineExitPO>();
        public int MyIndex { get; set; }
        public BusLine BusLine { get; set; }
        public StationLinePO StationLineBO1 { get; set; }
        public StationLinePO StationLineBO2 { get; set; }

        public Line line;
        public ShowLine(BusLine busLineBO, IBL1 bl1, Line line1)
        {
            InitializeComponent();
            BusLine = busLineBO;
            bl = bl1;
            line = line1;
            foreach (StationLinePO item in BusLine.StationLineBOs)
            {
                busLineBOs.Add(item);
            }
            foreach (LineExitPO item in BusLine.LineExitBos1)
            {
                lineExitBos.Add(item);
            }
            BusLine = busLineBO;
            busStationBOListView.ItemsSource = busLineBOs;
            shoeLine.DataContext = busLineBO;
            exit.ItemsSource = lineExitBos;
        }

        private void BusStationBOListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationLineBO1 = (StationLinePO)busStationBOListView.SelectedItem;
            if (StationLineBO1 != null)
            {
                TCS.ItemsSource = bl.TimeCamingToCurrnetStation(BusLine.BusLineID1, StationLineBO1.StationNumberOnLine);
                int index = busStationBOListView.SelectedIndex;
                if (index == busLineBOs.Count - 1)
                {
                    _ = MessageBox.Show("Error!!! It is not possible to change a terminal or first station to a line. Create a new line", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    addStation.IsEnabled = true;
                    addStation.Content = "הוסף תחנה לאחר תחנה: " + StationLineBO1.NameOfStation;
                    StationLineBO2 = busLineBOs[index + 1];
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddStationTowOldLine addStationTowOldLine = new AddStationTowOldLine(bl, BusLine.BusLineID1, StationLineBO1, StationLineBO2, this);
            addStationTowOldLine.Show();
            busStationBOListView.Items.Refresh();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = sender as FrameworkElement;
                StationLinePO stationLinePO = frameworkElement.DataContext as StationLinePO;
                StationLineBO stationLineBO = new StationLineBO();
                stationLinePO.DeepCopyTo(stationLineBO);
                ConsecutiveStationsBo consecutiveStations = bl.DeleteStationFromLine(stationLineBO.BusLineID2, stationLineBO.StationNumberOnLine);
                busLineBOs.Clear();
                foreach (StationLineBO item in bl.ReturnLineStationList(stationLineBO.BusLineID2))
                {
                    StationLinePO stationLinePO1 = new StationLinePO();
                    item.DeepCopyTo(stationLinePO1);
                    busLineBOs.Add(stationLinePO1);
                }
                busStationBOListView.Items.Refresh();
            }
            catch (BOExceptionLineStation ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                    MessageBoxImage.Error);
            }
        }

        private void UdaptingTd(object sender, RoutedEventArgs e)
        {
            FrameworkElement frameworkElement = sender as FrameworkElement;
            StationLinePO stationLinePO = frameworkElement.DataContext as StationLinePO;
            if (stationLinePO != busLineBOs[busLineBOs.Count - 1])
            {
                UdaptingDt udaptingDt = new UdaptingDt(stationLinePO, busLineBOs[stationLinePO.LocationNumberOnLine + 1].StationNumberOnLine, bl, line, this);
                udaptingDt.Show();
                busStationBOListView.Items.Refresh();
                //Close();
            }
            else
            {
                _ = MessageBox.Show("Last station has no time and distance to the next station", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LineExitPO lineExitPO = (LineExitPO)exit.SelectedItem;
            if (lineExitPO != null)
            {
                time.ItemsSource = lineExitPO.DepartureTimes;
                travl.ItemsSource = lineExitPO.TimeFinishTrval;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddLineExit addLineExit = new AddLineExit(BusLine.BusLineID1, bl, this);
            addLineExit.Show();
            exit.Items.Refresh();
            //Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FrameworkElement frameworkElement = sender as FrameworkElement;
            LineExitPO lineExitBo = frameworkElement.DataContext as LineExitPO;
            try
            {
                bl.DeleteLineExit(lineExitBo.BusLineID1, lineExitBo.LineStartTime);
                _ = lineExitBos.Remove(lineExitBo);
                time.ItemsSource = null;
                travl.ItemsSource = null;
                exit.Items.Refresh();
            }
            catch (BOExceptionLineExit ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }
    }
}
