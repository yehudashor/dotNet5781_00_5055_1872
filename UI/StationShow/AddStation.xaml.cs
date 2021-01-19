using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public IBL1 bl;
        public AddStation(IBL1 bl1)
        {
            InitializeComponent();
            bl = bl1;
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
                    IsAvailable3 = (bool)isAvailable3CheckBox.IsChecked,
                    AccessForDisabled = (bool)accessForDisabledComboBox.IsChecked,
                    RoofToTheStation = (bool)roofToTheStationComboBox1.IsChecked,
                };
                bl.AddStationToDo(busStationBO);
                Station station = new Station(bl);
                station.Show();
            }
            catch (BO.BOExceptionStation ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]$");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
