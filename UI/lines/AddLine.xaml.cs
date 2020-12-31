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
using UI.StationLine;

namespace UI.lines
{
    /// <summary>
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        IBL1 bl4;
        BO.BusLineBO BusLineBO;
        public AddLine(IBL1 bl)
        {
            BusLineBO = new BO.BusLineBO();
            bl4 = bl;
            InitializeComponent();
        }

        private void AddStation(object sender, RoutedEventArgs e)
        {
            //AddLineData
            // BusLineBO =
            BusLineBO = (BO.BusLineBO)AddLineData.DataContext;
            AddStationLine addStationLine = new AddStationLine();
            _ = addStationLine.ShowDialog();
        }
    }
}
