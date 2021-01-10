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
    /// Interaction logic for AddStationLine1.xaml
    /// </summary>
    public partial class AddStationLine1 : Window
    {
        IBL1 bl = BLFactory.GetBL("1");
        BO.BusLineBO BusLine;
        public string Number { get; set; }
        public AddStationLine1(BO.BusLineBO BusLineBO)
        {
            InitializeComponent();

            IEnumerable<BO.BusStationBO> busStationBOs = bl.ShowStation();
            Stationline.DataContext = bl.ShowStation().ToList();
            foreach (BO.BusStationBO item in busStationBOs)
            {
                _ = Stationline.Items.Add(item.StationNumber);
                _ = Stationline1.Items.Add(item.NameOfStation);
            }
            BusLine = BusLineBO;
        }
        public AddStationLine1()
        {
            InitializeComponent();
        }

        private void AddBus(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult box = MessageBox.Show("האם אתה בטוח שברצונך להוסיף את הקו?", "ask", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                switch (box)
                {
                    case MessageBoxResult.OK:
                        bl.AddBusLineBO(BusLine);
                        Line.busLineBOs.Add(bl.LineInformation(BusLine.BusLineID1));
                        MessageBoxResult messageBoxResult = MessageBox.Show("הקו נוסף למערכת", "Good");
                        if (messageBoxResult == MessageBoxResult.OK)
                        {
                            Close();
                        }
                        break;
                    case MessageBoxResult.Cancel:
                        Close();
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AddStation(object sender, RoutedEventArgs e)
        {
            BO.StationLineBO busStationBO = new BO.StationLineBO
            {
                StationNumberOnLine = int.Parse(tb.Text)
            };
            _ = MessageBox.Show("Good Insert Station");
            BusLine.StationLineBOs.Add(busStationBO);
        }
    }
}
