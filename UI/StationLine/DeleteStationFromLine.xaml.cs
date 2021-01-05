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

namespace UI.StationLine
{
    /// <summary>
    /// Interaction logic for DeleteStationFromLine.xaml
    /// </summary>
    public partial class DeleteStationFromLine : Window
    {
        IBL1 bl = BLFactory.GetBL("1");
        public int MyProperty { get; set; }
        public int DeleteStationFromLineId { get; set; }
        public DeleteStationFromLine(int number)
        {
            InitializeComponent();
            DeleteStationFromLineId = number;
            ListViewDelete.ItemsSource = ShowLine.busLineBOs;
            ListViewDelete.SelectionChanged += DeleteStationLine;
        }

        private void DeleteStationLine(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BO.StationLineBO stationLineBO = new BO.StationLineBO
                {
                    StationNumberOnLine = (ListViewDelete.SelectedValue as BO.BusStationBO).StationNumber
                };
                bl.DeleteStationFromLine(DeleteStationFromLineId, stationLineBO.StationNumberOnLine);
                BO.StationLineBO stationLineBO1 = ShowLine.busLineBOs.FirstOrDefault(item => item.StationNumberOnLine == stationLineBO.StationNumberOnLine);
                _ = ShowLine.busLineBOs.Remove(stationLineBO1);
                // ShowLine.busLineBOs.Add(bl.)
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DeleteStation(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.StationLineBO stationLineBO = new BO.StationLineBO();
                FrameworkElement fxElt = sender as FrameworkElement;
                stationLineBO = fxElt.DataContext as BO.StationLineBO;
                bl.DeleteStationFromLine(DeleteStationFromLineId, stationLineBO.StationNumberOnLine);
                _ = ShowLine.busLineBOs.Remove(stationLineBO);
                ShowLine.busLineBOs.Insert(stationLineBO.LocationNumberOnLine, bl.ReturnStationLine(DeleteStationFromLineId, stationLineBO.StationNumberOnLine));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
