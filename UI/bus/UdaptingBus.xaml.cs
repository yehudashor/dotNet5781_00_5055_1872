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
using UI.bus;

namespace UI.bus
{
    /// <summary>
    /// Interaction logic for UdaptingBus.xaml
    /// </summary>
    public partial class UdaptingBus : Window
    {
        public IBL1 bl;
        public BO.BusBO Bus;
        public UdaptingBus(IBL1 bl1, BO.BusBO Bus1)
        {
            InitializeComponent();
            bl = bl1;
            Bus = Bus1;
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.BusBO.TravelMode));
            statusComboBox.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Bus bus = new Bus();
            try
            {
                Bus.DayOfTreatment = dayOfTreatmentDatePicker.DisplayDate;
                Bus.StartDate = startDateDatePicker.DisplayDate;
                Bus.IsAvailable = (bool)isAvailableCheckBox.IsChecked;
                Bus.KmForRefueling = int.Parse(kmForRefuelingTextBox.Text);
                Bus.KmForTreatment += Bus.KmForRefueling;
                Bus.Status = (BO.BusBO.TravelMode)statusComboBox.SelectedItem;

                bl.DeleteBusBO(Bus.License_number);
                bl.AddBusBO(Bus);
                Bus busBO = new Bus(bl);
                busBO.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
