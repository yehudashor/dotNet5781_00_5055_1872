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
        IBL1 bl3;
        public Line(IBL1 bl)
        {
            InitializeComponent();
            bl3 = bl;
            int count = bl3.ReturnBusLineIdFromDl();
            for (int i = 0; i < count; i++)
            {
                busLineBOs.Add(bl3.LineInformation(i));
            }
            lines.ItemsSource = busLineBOs;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            AddLine addLine = new AddLine(bl3);
            _ = addLine.ShowDialog();
            lines.Items.Refresh();
        }

        private void Udpting(object sender, RoutedEventArgs e)
        {

        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }

        private void Lines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowLine showLine = new ShowLine((BO.BusLineBO)lines.SelectedItem);
            _ = showLine.ShowDialog();
        }

        //private void ShowBusLine(int index)
        //{
        //    currentDisplayBusLine = collectionOfbusLines[index];
        //    UpGrid.DataContext = currentDisplayBusLine;
        //    lbBusLineStations.DataContext = currentDisplayBusLine.RouteTheLine;
        //}

        //private void CbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ShowBusLine((cbBusLines.SelectedValue as BusLine).LineNumber);
        //}

    }
}
