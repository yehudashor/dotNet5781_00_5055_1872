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
using UI.lines;

namespace UI
{
    /// <summary>
    /// Interaction logic for Line.xaml
    /// </summary>
    public partial class Line : Window
    {
        public static ObservableCollection<BO.BusLineBO> busLineBOs = new ObservableCollection<BO.BusLineBO>();
        IBL1 bl = BLFactory.GetBL("1");
        public Line()
        {
            InitializeComponent();
            int count = bl.ReturnBusLineIdFromDl();
            for (int i = 0; i < count; i++)
            {
                busLineBOs.Add(bl.LineInformation(i));
            }
            lines.ItemsSource = busLineBOs;
        }


        private void Add(object sender, RoutedEventArgs e)
        {
            AddLine addLine = new AddLine();
            _ = addLine.ShowDialog();
            lines.Items.Refresh();
        }

        private void Udapting(object sender, RoutedEventArgs e)
        {
            FrameworkElement frameworkElement = sender as FrameworkElement;
            BO.BusLineBO busLineBO = frameworkElement.DataContext as BO.BusLineBO;
            UdptingLine udptingLine = new UdptingLine(busLineBO);
            _ = udptingLine.ShowDialog();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = sender as FrameworkElement;
                BO.BusLineBO busLineBO = frameworkElement.DataContext as BO.BusLineBO;
                bl.DeleteBusLineBO(busLineBO.BusLineID1);
                busLineBOs.RemoveAt(busLineBO.BusLineID1);
                busLineBOs.Insert(busLineBO.BusLineID1, bl.LineInformation(busLineBO.BusLineID1));
                lines.Items.Refresh();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Lines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowLine showLine = new ShowLine((BO.BusLineBO)lines.SelectedItem);
            _ = showLine.ShowDialog();
            lines.Items.Refresh();
        }
    }
}
