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
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        IBL1 bl = BLFactory.GetBL("1");
        public AddStation()
        {
            InitializeComponent();
            //
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.BusStationBO busStationBO = new BO.BusStationBO
                {
                    StationNumber = int.Parse(stationNumberTextBox.Text),
                    NameOfStation = nameOfStationTextBox.Text,
                    StationAddress = stationAddressTextBox.Text,
                    IsAvailable3 = bool.Parse(isAvailable3TextBox.Text),
                    AccessForDisabled = bool.Parse(accessForDisabledTextBox.Text),
                    RoofToTheStation = bool.Parse(roofToTheStationTextBox.Text),
                };
                bl.AddStationToDo(busStationBO);
                Station.busLineBOs.Add(bl.ReturnStationToPL(busStationBO.StationNumber));

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
