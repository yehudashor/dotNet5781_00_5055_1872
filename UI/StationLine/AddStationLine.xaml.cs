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

namespace UI.StationLine
{
    /// <summary>
    /// Interaction logic for AddStationLine.xaml
    /// </summary>
    public partial class AddStationLine : Window
    {
        BO.BusLineBO BusLine1;
        public AddStationLine(BO.BusLineBO BusLine)
        {
            BusLine1 = BusLine;
            InitializeComponent();
        }
        public AddStationLine()
        {
            InitializeComponent();
        }
        public int Number { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(numberOfStationInTheLine.Text) >= 2)
            {
                AddStationLine1 addStationLine1 = new AddStationLine1(BusLine1, int.Parse(numberOfStationInTheLine.Text));
                _ = addStationLine1.ShowDialog();
            }
            else
            {
                _ = MessageBox.Show("You must select a minimum of two stations plase enter again");
                numberOfStationInTheLine.ClearValue(TextBox.TextProperty);
            }
        }
    }
}
