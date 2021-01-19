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

namespace UI.StationLine
{
    /// <summary>
    /// Interaction logic for AddTimeDISxaml.xaml
    /// </summary>
    /// 
    public partial class AddTimeDIS : Window
    {
        IBL1 bl;
        public BO.StationLineBO StationLineBO1 { get; set; }
        public BO.StationLineBO StationLineBO2 { get; set; }
        public BO.StationLineBO StationLineBO3 { get; set; }
        public AddTimeDIS(IBL1 bl1, BO.StationLineBO stationLineBO1, BO.StationLineBO stationLineBO2, BO.StationLineBO stationLineBO3, string name)
        {
            InitializeComponent();
            bl = bl1;
            StationLineBO1 = stationLineBO1;
            StationLineBO2 = stationLineBO2;
            StationLineBO3 = stationLineBO3;
            sa.Text = stationLineBO1.NameOfStation;
            sbe.Text = name;
            sc.Text = name;
            sd.Text = stationLineBO3.NameOfStation;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StationLineBO2.AverageTime = new TimeSpan(int.Parse(H.Text), int.Parse(M.Text), int.Parse(S.Text));
                StationLineBO2.DistanceBetweenTooStations = float.Parse(D.Text);

                float d = float.Parse(DA.Text);
                TimeSpan timeSpan = new TimeSpan(int.Parse(HA.Text), int.Parse(MA.Text), int.Parse(SA.Text));

                bl.AddStationToLine(StationLineBO2, StationLineBO1.StationNumberOnLine, StationLineBO3.StationNumberOnLine, timeSpan, d);
                _ = MessageBox.Show("The station was successfully added");
                Line line = new Line(bl);
                line.Show();
                Close();
            }
            catch (BO.BOExceptionLineStation ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]$");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberValidationTextBox1(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]$");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberValidationTextBox2(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]$");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
