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
    /// Interaction logic for Udapting.xaml
    /// </summary>
    public partial class Udapting : Window
    {
        public bool MyProperty { get; set; }
        BO.BusStationBO busStationBO;
        IBL1 bl = BLFactory.GetBL("1");
        public Udapting(BO.BusStationBO busStationBO1)
        {
            busStationBO = busStationBO1;
            InitializeComponent();
        }

        private void Updating(object sender, RoutedEventArgs e)
        {
            try
            {
                _ = Station.busLineBOs.Remove(busStationBO);
                busStationBO.NameOfStation = nameOfStationTextBox.Text;
                busStationBO.StationAddress = stationAddressTextBox.Text;
                busStationBO.Longitude = float.Parse(longitudeTextBox.Text);
                busStationBO.Latitude = float.Parse(latitudeTextBox.Text);
                busStationBO.IsAvailable3 = MyProperty;//bool.Parse((string)isAvailable3ComboBox.SelectedItem);
                //BO.BusStationBO busStationBO = new BO.BusStationBO
                //{
                //    StationNumber = int.Parse(stationNumberTextBox.Text),
                //    NameOfStation = nameOfStationTextBox.Text,
                //    StationAddress = stationAddressTextBox.Text,
                //    // IsAvailable3 = bool.Parse(isAvailable3TextBox.Text),
                //    // AccessForDisabled = bool.Parse(accessForDisabledTextBox.Text),
                //    // RoofToTheStation = bool.Parse(roofToTheStationTextBox.Text),
                //};
                //bl.(busStationBO);
                bl.AddStationToDo(busStationBO);
                Station.busLineBOs.Add(bl.ReturnStationToPL(busStationBO.StationNumber));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void roofToTheStationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void isAvailable3ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // MyProperty = bool.Parse(isAvailable3ComboBox.SelectedItem);
        }
    }
}
