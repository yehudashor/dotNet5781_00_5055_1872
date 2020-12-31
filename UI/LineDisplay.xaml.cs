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
namespace UI
{
    /// <summary>
    /// Interaction logic for LineDisplay.xaml
    /// </summary>
    public partial class LineDisplay : Window
    {
        IBL1 bl2;
        public LineDisplay(IBL1 bl)
        {
            bl2 = bl;
            InitializeComponent();
        }

        private void Bus(object sender, RoutedEventArgs e)
        {
            Bus bus = new Bus(bl2);
            _ = bus.ShowDialog();
        }

        private void Line(object sender, RoutedEventArgs e)
        {
            Line line = new Line(bl2);
            _ = line.ShowDialog();
        }

        private void Station(object sender, RoutedEventArgs e)
        {
            Station station = new Station();
            _ = station.ShowDialog();
        }
    }
}
