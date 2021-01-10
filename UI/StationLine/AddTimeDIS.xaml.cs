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
    /// Interaction logic for AddTimeDISxaml.xaml
    /// </summary>
    public partial class AddTimeDIS : Window
    {
        public int NumberLine { get; set; }
        public int NumberStation { get; set; }
        IBL1 bl = BLFactory.GetBL("1");
        public BO.StationLineBO stationLineBO1;
        public AddTimeDIS(BO.StationLineBO stationLineBO, int numberLine, int numberStation)
        {
            stationLineBO1 = stationLineBO;
            NumberLine = numberLine;
            NumberStation = numberStation;
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //stationLineBO1.AverageTime = TimeSpan.Parse("H.Text, M.Text, S.Text");
                stationLineBO1.DistanceBetweenTooStations = float.Parse(D.Text);
                bl.AddStationToLine(stationLineBO1, NumberStation);
                IEnumerable<BO.StationLineBO> stationLineBO2 = bl.ReturnLineStationList(NumberLine);
                foreach (BO.StationLineBO item in stationLineBO2)
                {
                    ShowLine.busLineBOs.Add(item);
                }
                _ = MessageBox.Show("The station was successfully added");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
