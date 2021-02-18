using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UI.StationLine;

namespace UI.lines
{
    /// <summary>
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        public IBL1 bl;
        public Line Add;
        public ObservableCollection<StationPO> busLineBOs1 = new ObservableCollection<StationPO>();
        public AddLine(IBL1 bl1, Line Add1)
        {
            InitializeComponent();
            Add = Add1;
            bl = bl1;
            List<BO.BusStationBO> busStationBOs = bl.ShowStation().ToList();

            for (int i = 0; i < busStationBOs.Count; i++)
            {
                StationPO stationPO = new StationPO();
                busStationBOs[i].DeepCopyTo(stationPO);
                busLineBOs1.Add(stationPO);
            }

            firstStationComboBox.ItemsSource = busLineBOs1;
            firstStationComboBox.DisplayMemberPath = "StationNumber";

            lastStationComboBox.ItemsSource = busLineBOs1;
            lastStationComboBox.DisplayMemberPath = "StationNumber";

            Stations.ItemsSource = busLineBOs1;

            areaBusUrbanComboBox.ItemsSource = Enum.GetValues(typeof(BO.Area1));
            getUrbanComboBox.ItemsSource = Enum.GetValues(typeof(BO.Urban));
            getAvailableComboBox.ItemsSource = Enum.GetValues(typeof(BO.Available));
            getAvailableComboBox.SelectedIndex = 0;
            getUrbanComboBox.SelectedIndex = 0;
            areaBusUrbanComboBox.SelectedIndex = 0;
            firstStationComboBox.SelectedIndex = 0;
            lastStationComboBox.SelectedIndex = 0;
        }

        private void AddStation(object sender, RoutedEventArgs e)
        {
            BO.BusLineBO BusLineBO = new BO.BusLineBO
            {
                LineNumber = int.Parse(lineNumberTextBox.Text),
                AreaBusUrban = (BO.Area1)areaBusUrbanComboBox.SelectedItem,
                GetUrban = (BO.Urban)getUrbanComboBox.SelectedItem,
                GetAvailable = (BO.Available)getAvailableComboBox.SelectedItem
            };

            BusLineBO.StationLineBOs = new List<BO.StationLineBO>
            {
                new BO.StationLineBO { StationNumberOnLine = ((StationPO)firstStationComboBox.SelectedItem).StationNumber},
                new BO.StationLineBO { StationNumberOnLine = ((StationPO)lastStationComboBox.SelectedItem).StationNumber}
            };
            try
            {
                if (BusLineBO.StationLineBOs[0].StationNumberOnLine != BusLineBO.StationLineBOs[1].StationNumberOnLine)
                {
                    MessageBoxResult box = MessageBox.Show("האם אתה בטוח שברצונך להוסיף את הקו?", "ask", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    switch (box)
                    {
                        case MessageBoxResult.OK:
                            bl.AddBusLineBO(BusLineBO);
                            BusLine busLine = new BusLine();
                            BO.BusLineBO busLineBO = new BO.BusLineBO();
                            busLineBO = bl.LineInformation(BusLineBO.BusLineID1);
                            busLineBO.DeepCopyTo(busLine);
                            for (int j = 0; j < busLineBO.StationLineBOs.Count; j++)
                            {
                                StationLinePO stationLinePO = new StationLinePO();
                                busLineBO.StationLineBOs[j].DeepCopyTo(stationLinePO);
                                busLine.StationLineBOs.Add(stationLinePO);
                            }
                            Add.busLineBOs.Add(busLine);
                            MessageBoxResult messageBoxResult = MessageBox.Show("הקו נוסף למערכת", "Good");
                            Close();
                            break;
                        case MessageBoxResult.Cancel:
                            Close();
                            break;
                    }
                }
                else
                {
                    _ = MessageBox.Show("First station should be different from last", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (BO.BOExceptionLine ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
            catch (BO.BOExceptionLineStation ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }
        //PreviewTextInput= "NumberValidationTextBox" 
        /// <summary>
        /// A function that ensures that only numbers can be entered in the textBox field !!!
        /// (Part of the above bonus).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]$");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
