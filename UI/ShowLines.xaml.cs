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
        public BO.BusLineBO BusLine { get; set; }
        public ShowLine(BO.BusLineBO busLineBO)
        {
            InitializeComponent();
            BusLine = busLineBO;

            foreach (BO.StationLineBO item in busLineBO.StationLineBOs)
            {
                busLineBOs.Add(item);
            }

            busStationBOListView.ItemsSource = busLineBOs;
            shoeLine.DataContext = busLineBO;
            busStationBOListView.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddStationTowOldLine addStationTowOldLine = new AddStationTowOldLine(BusLine.BusLineID1);
            _ = addStationTowOldLine.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DeleteStationFromLine deleteStationFromLine = new DeleteStationFromLine(BusLine.BusLineID1);
            deleteStationFromLine.Show();
        }
    }
}
