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
using UI.lines;
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
        public ObservableCollection<BO.StationLineBO> busLineBOs = new ObservableCollection<BO.StationLineBO>();
        public ObservableCollection<BO.LineExitBo> lineExitBos = new ObservableCollection<BO.LineExitBo>();
        public int MyIndex { get; set; }
        public BO.BusLineBO BusLine { get; set; }
        public BO.StationLineBO StationLineBO1 { get; set; }
        public BO.StationLineBO StationLineBO2 { get; set; }
        public ShowLine(BO.BusLineBO busLineBO, IBL1 bl1)
        {
            InitializeComponent();
            BusLine = busLineBO;
            bl = bl1;
            foreach (BO.StationLineBO item in busLineBO.StationLineBOs)
            {
                busLineBOs.Add(item);
            }
            foreach (BO.LineExitBo item in busLineBO.LineExitBos1)
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
            StationLineBO1 = (BO.StationLineBO)busStationBOListView.SelectedItem;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddStationTowOldLine addStationTowOldLine = new AddStationTowOldLine(bl, BusLine.BusLineID1, StationLineBO1, StationLineBO2);
            addStationTowOldLine.Show();
            busStationBOListView.Items.Refresh();
            Close();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = sender as FrameworkElement;
                BO.StationLineBO stationLineBO = frameworkElement.DataContext as BO.StationLineBO;
                bl.DeleteStationFromLine(BusLine.BusLineID1, stationLineBO.StationNumberOnLine);
                _ = busLineBOs.Remove(stationLineBO);
                busLineBOs.Insert(stationLineBO.LocationNumberOnLine, bl.ReturnStationLine(BusLine.BusLineID1, stationLineBO.StationNumberOnLine));
            }
            catch (BO.BOExceptionLineStation ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                    MessageBoxImage.Error);
            }
        }

        private void UdaptingTd(object sender, RoutedEventArgs e)
        {
            FrameworkElement frameworkElement = sender as FrameworkElement;
            BO.StationLineBO busStationBO = frameworkElement.DataContext as BO.StationLineBO;
            if (busStationBO != busLineBOs[busLineBOs.Count - 1])
            {
                UdaptingDt udaptingDt = new UdaptingDt(busStationBO, busLineBOs[busStationBO.LocationNumberOnLine + 1].StationNumberOnLine, bl);
                udaptingDt.Show();
                busStationBOListView.Items.Refresh();
                Close();
            }
            else
            {
                _ = MessageBox.Show("Last station has no time and distance to the next station", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.LineExitBo lineExitBo = (BO.LineExitBo)exit.SelectedItem;
            if (lineExitBo != null)
            {
                time.ItemsSource = lineExitBo.DepartureTimes;
                travl.ItemsSource = lineExitBo.TimeFinishTrval;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddLineExit addLineExit = new AddLineExit(BusLine.BusLineID1, bl);
            addLineExit.Show();
            exit.Items.Refresh();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FrameworkElement frameworkElement = sender as FrameworkElement;
            BO.LineExitBo lineExitBo = frameworkElement.DataContext as BO.LineExitBo;
            try
            {
                bl.DeleteLineExit(lineExitBo.BusLineID1, lineExitBo.LineStartTime);
                _ = lineExitBos.Remove(lineExitBo);
                exit.Items.Refresh();
            }
            catch (BO.BOExceptionLineExit ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }
    }
}
