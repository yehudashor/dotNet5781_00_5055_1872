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
            //(Enums.AREA)cbArea.SelectedItem
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            busLineBO.LineNumber = int.Parse(lineNumberTextBlock.Text);
            //busLineBO.IsAvailable1 = 
            //busLineBO.AreaBusInterUrban = 
            _ = Line.busLineBOs.Remove(busLineBO);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
