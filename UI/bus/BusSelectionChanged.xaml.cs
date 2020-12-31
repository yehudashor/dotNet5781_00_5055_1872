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

namespace UI.bus
{
    /// <summary>
    /// Interaction logic for BusSelectionChanged.xaml
    /// </summary>
    public partial class BusSelectionChanged : Window
    {
        private readonly BO.BusBO bus;
        public BusSelectionChanged(BO.BusBO Bus1)
        {
            InitializeComponent();
            bus = Bus1;
            MainGrid.DataContext = bus;
        }

    }
}
