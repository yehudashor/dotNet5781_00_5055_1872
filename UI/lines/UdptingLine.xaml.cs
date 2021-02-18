using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using UI.PO;

namespace UI.lines
{
    /// <summary>
    /// Interaction logic for UdptingLine.xaml
    /// </summary>
    public partial class UdptingLine : Window
    {
        public IBL1 bl;
        public BO.BusLineBO busLineBO;
        private BusLine line;
        public Line Add;
        public UdptingLine(BusLine busLineBO1, IBL1 bl1)
        {
            InitializeComponent();
            bl = bl1;
            line = busLineBO1;

            areaBusUrbanComboBox.ItemsSource = Enum.GetValues(typeof(BO.Area1));
            getUrbanComboBox.ItemsSource = Enum.GetValues(typeof(BO.Urban));
            getAvailableComboBox.ItemsSource = Enum.GetValues(typeof(BO.Available));
            getAvailableComboBox.SelectedIndex = 0;
            getUrbanComboBox.SelectedIndex = 0;
            areaBusUrbanComboBox.SelectedIndex = 0;
            lineNumberTextBox.Text = busLineBO1.LineNumber.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MessageBoxResult box = MessageBox.Show("האם אתה בטוח שברצונך לעדכן את פרטי הקו?", "ask", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                switch (box)
                {
                    case MessageBoxResult.OK:
                        busLineBO = new BO.BusLineBO();
                        line.AreaBusUrban = (Area1)areaBusUrbanComboBox.SelectedItem;
                        line.GetUrban = (Urban)getUrbanComboBox.SelectedItem;
                        line.GetAvailable = (Available)getAvailableComboBox.SelectedItem;
                        line.DeepCopyTo(busLineBO);
                        bl.UdaptingLine(busLineBO);
                        MessageBoxResult messageBoxResult = MessageBox.Show("הפרטים עודכנו במערכת", "Good");
                        Close();
                        break;
                    case MessageBoxResult.Cancel:
                        Close();
                        break;
                }
            }
            catch (BO.BOExceptionLine ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]$");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
