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
using UI.PO;

namespace UI.StationShow
{
    /// <summary>
    /// Interaction logic for Udapting.xaml
    /// </summary>
    public partial class Udapting : Window
    {
        public bool MyProperty { get; set; }
        public StationPO busStationPO;
        public IBL1 bl;
        public Udapting(StationPO busStationPO1, IBL1 bl1)
        {
            busStationPO = busStationPO1;
            InitializeComponent();
            stationNumberTextBox.Text = busStationPO1.StationNumber.ToString();
            bl = bl1;
        }

        private void Updating(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.BusStationBO busStationBO = new BO.BusStationBO();
                busStationPO.NameOfStation = nameOfStationTextBox.Text;
                busStationPO.StationAddress = stationAddressTextBox.Text;
                busStationPO.IsAvailable3 = (bool)isAvailable3CheckBox.IsChecked;
                busStationPO.AccessForDisabled = (bool)accessForDisabledComboBox.IsChecked;
                busStationPO.RoofToTheStation = (bool)roofToTheStationComboBox1.IsChecked;
                busStationPO.DeepCopyTo(busStationBO);
                bl.DeleteStationFromDo(busStationBO.StationNumber);
                bl.AddStationToDo(busStationBO);
                Close();
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
