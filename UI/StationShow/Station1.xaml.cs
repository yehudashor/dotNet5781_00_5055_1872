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
using UI.PO;

namespace UI.StationShow
{
    /// <summary>
    /// Interaction logic for Station1.xaml
    /// </summary>
    public partial class Station1 : Window
    {
        public StationPO Station = new StationPO();
        public Station1(StationPO Station1)
        {
            Station = Station1;
            InitializeComponent();
            StationList1.DataContext = Station;
        }
    }
}
