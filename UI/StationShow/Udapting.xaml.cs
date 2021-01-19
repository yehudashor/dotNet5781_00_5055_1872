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
    /// Interaction logic for Udapting.xaml
    /// </summary>
    public partial class Udapting : Window
    {
        public bool MyProperty { get; set; }
        BO.BusStationBO busStationBO;
        public IBL1 bl;
        public Udapting(BO.BusStationBO busStationBO1, IBL1 bl1)
        {
            busStationBO = busStationBO1;
            InitializeComponent();
            stationNumberTextBox.Text = busStationBO1.StationNumber.ToString();
            bl = bl1;
        }

        private void Updating(object sender, RoutedEventArgs e)
        {
            try
            {
                busStationBO.NameOfStation = nameOfStationTextBox.Text;
                busStationBO.StationAddress = stationAddressTextBox.Text;
                busStationBO.IsAvailable3 = (bool)isAvailable3CheckBox.IsChecked;
                busStationBO.AccessForDisabled = (bool)accessForDisabledComboBox.IsChecked;
                busStationBO.RoofToTheStation = (bool)roofToTheStationComboBox1.IsChecked;
                bl.DeleteStationFromDo(busStationBO.StationNumber);
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

        //PreviewTextInput="GenericTextBox_PreviewTextInput"
        private void GenericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text, @"[^א-תA-Za-z/]");
        }
        private static bool IsTextAllowed(string Text, string AllowedRegex)
        {
            try
            {
                Regex regex = new Regex(AllowedRegex);
                return !regex.IsMatch(Text);
            }
            catch
            {
                return true;
            }
        }
    }
}
