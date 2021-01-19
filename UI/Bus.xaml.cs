using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UI.bus;

namespace UI
{
    /// <summary>
    /// Interaction logic for Bus.xaml
    /// </summary>
    public partial class Bus : Window
    {
        public static ObservableCollection<BO.BusBO> listBus = new ObservableCollection<BO.BusBO>();
        public IBL1 bl;
        public Bus(IBL1 bl1)
        {
            InitializeComponent();
            bl = bl1;
            IEnumerable<BO.BusBO> busBOs = bl.ShowAllBus();
            foreach (BO.BusBO item in busBOs)
            {
                listBus.Add(item);
            }
            busBODataGrid.ItemsSource = listBus;
        }

        //private void Bus_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    BusSelectionChanged bus = new BusSelectionChanged((BO.BusBO)BusList.SelectedItem);
        //    _ = bus.ShowDialog();
        //    BusList.Items.Refresh();
        //}
    }
}
