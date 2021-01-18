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
using UI.LineTrip;
using UI.Users;

namespace UI
{
    /// <summary>
    /// Interaction logic for LineDisplay.xaml
    /// </summary>
    public partial class LineDisplay : Window
    {
        public IBL1 bl;
        public LineDisplay(IBL1 bl1)
        {
            bl = bl1;
            InitializeComponent();
        }

        private void Bus(object sender, RoutedEventArgs e)
        {
            Bus bus = new Bus(bl);
            _ = bus.ShowDialog();
        }

        private void Line(object sender, RoutedEventArgs e)
        {
            Line line = new Line(bl);
            line.Show();
        }

        private void Station(object sender, RoutedEventArgs e)
        {
            Station station = new Station(bl);
            station.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(bl);
            user.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LineTripS lineTripS = new LineTripS(bl);
            lineTripS.Show();
        }
    }
}
