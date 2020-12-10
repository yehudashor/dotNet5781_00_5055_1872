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
using dotNet_01_5055_1872;
using System.Collections.ObjectModel;
namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Deleting a bus for a bonus by enriching the graphical interface
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBus(object sender, RoutedEventArgs e)
        {
            string LicinsNumber = license_numberTextBox.Text;
            Bus bus = null;
            foreach (Bus item in MainWindow.listBus)
            {
                if (item.License_number == LicinsNumber)
                {
                    bus = item;
                }
            }
            if (bus != null)
            {
                _ = MainWindow.listBus.Remove(bus);
                _ = MessageBox.Show("Good the bus was deleted");
            }
            else
            {
                _ = MessageBox.Show("this buses not exist in the compny");
            }
        }
    }
}
