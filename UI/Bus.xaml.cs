using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BLAPI;
using UI.bus;
using System.Threading;
namespace UI
{
    /// <summary>
    /// Interaction logic for Bus.xaml
    /// </summary>
    public partial class Bus : Window
    {
        public ObservableCollection<BO.BusBO> listBus = new ObservableCollection<BO.BusBO>();
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
            busBOListView.ItemsSource = listBus;
        }

        private void GoTrip(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = sender as FrameworkElement;
                BO.BusBO busBO = frameworkElement.DataContext as BO.BusBO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void DeleteBus(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = sender as FrameworkElement;
                BO.BusBO busBO = frameworkElement.DataContext as BO.BusBO;
                bl.DeleteBusBO(busBO.License_number);
                _ = listBus.Remove(busBO);
                listBus.Add(bl.ShowBus(busBO.License_number));
                busBOListView.Items.Refresh();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void RefuelingToBus(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = sender as FrameworkElement;
                BO.BusBO busBO = frameworkElement.DataContext as BO.BusBO;
                List<BO.BusBO> busBOs = listBus.ToList();
                int index = busBOs.FindIndex(item => item.License_number == busBO.License_number);
                busBO.KmForRefueling = 0;
                bl.DeleteBusBO(busBO.License_number);
                Thread.Sleep(12000);
                bl.AddBusBO(busBO);
                listBus.Insert(index, bl.ShowBus(busBO.License_number));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void UdaptingBus(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = sender as FrameworkElement;
                BO.BusBO busBO = frameworkElement.DataContext as BO.BusBO;
                UdaptingBus udaptingBus = new UdaptingBus(bl, busBO);
                _ = udaptingBus.ShowDialog();
                Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //private void Bus_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    BusSelectionChanged bus = new BusSelectionChanged((BO.BusBO)BusList.SelectedItem);
        //    _ = bus.ShowDialog();
        //    BusList.Items.Refresh();
        //}
    }
}
