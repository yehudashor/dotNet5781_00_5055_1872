using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLAPI;
using UI.PO;

namespace UI.StationLine
{
    /// <summary>
    /// Interaction logic for AddTimeDISxaml.xaml
    /// </summary>
    /// 
    public partial class AddTimeDIS : Window
    {
        IBL1 bl;
        ShowLine showLine;
        public StationLinePO StationLineBO1 { get; set; }
        public StationLinePO StationLineBO2 { get; set; }
        public StationLinePO StationLineBO3 { get; set; }
        public AddTimeDIS(IBL1 bl1, StationLinePO stationLineBO1, StationLinePO stationLineBO2, StationLinePO stationLineBO3, string name, ShowLine showLine1)
        {
            InitializeComponent();
            bl = bl1;
            showLine = showLine1;
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
                BO.StationLineBO stationLineBO = new BO.StationLineBO();
                StationLineBO2.DeepCopyTo(stationLineBO);
                bl.AddStationToLine(stationLineBO, StationLineBO1.StationNumberOnLine, StationLineBO3.StationNumberOnLine, timeSpan, d);
                showLine.busLineBOs.Clear();
                foreach (BO.StationLineBO item in bl.ReturnLineStationList(stationLineBO.BusLineID2))
                {
                    StationLinePO stationLinePO = new StationLinePO();
                    item.DeepCopyTo(stationLinePO);
                    showLine.busLineBOs.Add(stationLinePO);
                }
                showLine.busStationBOListView.Items.Refresh();
                _ = MessageBox.Show("The station was successfully added");
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
