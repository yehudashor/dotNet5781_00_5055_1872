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

namespace UI.lines
{
    /// <summary>
    /// Interaction logic for UdptingLine.xaml
    /// </summary>
    public partial class UdptingLine : Window
    {
        IBL1 bl = BLFactory.GetBL("1");
        BO.BusLineBO busLineBO;
        public UdptingLine(BO.BusLineBO busLineBO1)
        {
            busLineBO = busLineBO1;
            InitializeComponent();
            areaBusUrbanComboBox.ItemsSource = Enum.GetValues(typeof(BO.Area1));
            getUrbanComboBox.ItemsSource = Enum.GetValues(typeof(BO.Urban));
            getAvailableComboBox.ItemsSource = Enum.GetValues(typeof(BO.Available));
            getAvailableComboBox.SelectedIndex = 0;
            getUrbanComboBox.SelectedIndex = 0;
            areaBusUrbanComboBox.SelectedIndex = 0;
            lineNumberTextBox.Text = busLineBO1.LineNumber.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                busLineBO.AreaBusUrban = (BO.Area1)areaBusUrbanComboBox.SelectedItem;
                busLineBO.GetUrban = (BO.Urban)getUrbanComboBox.SelectedItem;
                busLineBO.GetAvailable = (BO.Available)getAvailableComboBox.SelectedItem;
                bl.UdaptingLine(busLineBO);
                Line.busLineBOs.Insert(busLineBO.BusLineID1, busLineBO);
                _ = MessageBox.Show("The line has been updated successfully");
            }
            catch (Exception)
            {

                throw;
            }
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
