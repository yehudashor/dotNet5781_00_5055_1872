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
using UI.StationLine;

namespace UI
{
    /// <summary>
    /// Interaction logic for ShowLine.xaml
    /// </summary>
    public partial class ShowLine : Window
    {
        public static ObservableCollection<BO.StationLineBO> busLineBOs = new ObservableCollection<BO.StationLineBO>();
        IBL1 bl = BLFactory.GetBL("1");
        public int MyIndex { get; set; }
        public BO.BusLineBO BusLine { get; set; }
        public BO.StationLineBO StationLineBO1 { get; set; }
        public ShowLine(BO.BusLineBO busLineBO)
        {

            foreach (BO.StationLineBO item in busLineBO.StationLineBOs)
            {
                busLineBOs.Add(item);
            }
            BusLine = busLineBO;
            InitializeComponent();
            busStationBOListView.ItemsSource = busLineBOs;
            shoeLine.DataContext = busLineBO;
            busStationBOListView.Items.Refresh();
        }

        private void BusStationBOListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationLineBO1 = (BO.StationLineBO)busStationBOListView.SelectedItem;
            addStation.Content = "הוסף תחנה לאחר תחנה: " + StationLineBO1.NameOfStation;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddStationTowOldLine addStationTowOldLine = new AddStationTowOldLine(BusLine.BusLineID1, StationLineBO1);
                _ = addStationTowOldLine.ShowDialog();
                //busLineBOs.Clear();
            }
            catch (BO.ExceptionBl ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                    MessageBoxImage.Error);
            }
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
            catch (BO.ExceptionBl ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                    MessageBoxImage.Error);
            }
        }

        private void UdaptingTd(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (BO.ExceptionBl ex)
            {
            }
        }
    }
}
