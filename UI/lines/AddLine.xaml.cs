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
using UI.StationLine;

namespace UI.lines
{
    /// <summary>
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        public AddLine()
        {
            InitializeComponent();
            areaBusUrbanComboBox.ItemsSource = Enum.GetValues(typeof(BO.Area1));
            getUrbanComboBox.ItemsSource = Enum.GetValues(typeof(BO.Urban));
            getAvailableComboBox.ItemsSource = Enum.GetValues(typeof(BO.Available));
        }

        private void AddStation(object sender, RoutedEventArgs e)
        {
            //if ((BO.Area1)areaBusUrbanComboBox.SelectedItem == null)
            //{

            //}
            BO.BusLineBO BusLineBO = new BO.BusLineBO
            {
                LineNumber = int.Parse(lineNumberTextBox.Text),
                AreaBusUrban = (BO.Area1)areaBusUrbanComboBox.SelectedItem,
                GetUrban = (BO.Urban)getUrbanComboBox.SelectedItem,
                GetAvailable = (BO.Available)getAvailableComboBox.SelectedItem
            };
            BusLineBO.StationLineBOs = new List<BO.StationLineBO>();
            AddStationLine addStationLine = new AddStationLine(BusLineBO);
            _ = addStationLine.ShowDialog();
        }
        //PreviewTextInput= "NumberValidationTextBox" 
        /// <summary>
        /// A function that ensures that only numbers can be entered in the textBox field !!!
        /// (Part of the above bonus).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]$");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
