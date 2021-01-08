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
        IBL1 bl2 = BLFactory.GetBL("1");
        public Bus()
        {
            IEnumerable<BO.BusBO> busBOs = bl2.ShowAllBus();
            foreach (BO.BusBO item in busBOs)
            {
                listBus.Add(item);
            }
            InitializeComponent();
            busBODataGrid.ItemsSource = listBus;
        }

        //private void Bus_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    BusSelectionChanged bus = new BusSelectionChanged((BO.BusBO)BusList.SelectedItem);
        //    _ = bus.ShowDialog();
        //    BusList.Items.Refresh();
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busBOViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busBOViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busBOViewSource.Source = [generic data source]
        }
    }
}
