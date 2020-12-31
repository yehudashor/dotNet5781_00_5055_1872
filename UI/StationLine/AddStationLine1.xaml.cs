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
        public int Number { get; set; }
        public AddStationLine1(BO.BusLineBO BusLineBO, int i)
        {
            InitializeComponent();
            Number = i;
            IEnumerable<BO.BusStationBO> busStationBOs = bl.ShowStation();
            foreach (BO.BusStationBO item in busStationBOs)
            {
                ComboBoxItem newItem = new ComboBoxItem
                {
                    Content = item.StationNumber
                };
                _ = listOfNumberStation.Items.Add(newItem);
            }
            BusLine = BusLineBO;
        }
        public AddStationLine1()
        {
            InitializeComponent();
        }

        private void AddBus(object sender, RoutedEventArgs e)
        {
            bl.AddBusLineBO(BusLine);
            Line.busLineBOs.Add(BusLine);
        }

        private void AddStation(object sender, RoutedEventArgs e)
        {
            BO.StationLineBO busStationBO = new BO.StationLineBO
            {
                StationNumberOnLine = (int)listOfNumberStation.SelectedItem
            };
            BusLine.StationLineBOs.Add(busStationBO);
        }
    }
}
