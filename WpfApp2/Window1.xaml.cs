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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Bus _Bus;

        public Window1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Entering the data of the new line and adding them to the main list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string LicinsNumber = license_numberTextBox.Text;
            DateTime dateTime = startDateDatePicker.DisplayDate;
            DateTime dateTime1 = dayOfTreatmentDatePicker.DisplayDate;
            _Bus = new Bus(LicinsNumber, dateTime, dateTime1);
            MainWindow.listBus.Add(_Bus);
        }
    }
}
