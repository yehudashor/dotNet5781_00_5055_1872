using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BLAPI;
using UI.lines;
using UI.PO;

namespace UI
{
    /// <summary>
    /// Interaction logic for Line.xaml
    /// </summary>
    public partial class Line : Window
    {
        internal ObservableCollection<BusLine> busLineBOs = new ObservableCollection<BusLine>();

        public BO.BusLineBO BusLineBO { get; set; }

        public IBL1 bl;

        public Line(IBL1 bl1)
        {
            InitializeComponent();
            bl = bl1;
            busLineBOs = Lines(busLineBOs);
            _ = busLineBOs.OrderBy(l => (int)l.AreaBusUrban);
            lines.ItemsSource = busLineBOs;
        }

        private ObservableCollection<BusLine> Lines(ObservableCollection<BusLine> busLineBOs)
        {
            try
            {
                List<BusLine> busLineBOs1 = new List<BusLine>();
                int count = bl.ReturnBusLineIdFromDl();
                for (int i = 0; i < count; i++)
                {
                    BusLine busLine = new BusLine();
                    BO.BusLineBO busLineBO = new BO.BusLineBO();
                    busLineBO = bl.LineInformation(i);
                    if (busLineBO != null)
                    {
                        busLineBO.DeepCopyTo(busLine);

                        for (int j = 0; j < busLineBO.StationLineBOs.Count; j++)
                        {
                            StationLinePO stationLinePO = new StationLinePO();
                            busLineBO.StationLineBOs[j].DeepCopyTo(stationLinePO);
                            busLine.StationLineBOs.Add(stationLinePO);
                        }
                        for (int k = 0; k < busLineBO.LineExitBos1.Count; k++)
                        {
                            LineExitPO lineExitPO = new LineExitPO();
                            busLineBO.LineExitBos1[k].DeepCopyTo(lineExitPO);
                            lineExitPO.DepartureTimes = busLineBO.LineExitBos1[k].DepartureTimes;
                            lineExitPO.TimeFinishTrval = busLineBO.LineExitBos1[k].TimeFinishTrval;
                            busLine.LineExitBos1.Add(lineExitPO);
                        }
                        //busLineBOs1.Add(busLine);
                        busLineBOs.Add(busLine);
                    }
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
            catch (BO.BOExceptionConsecutiveStations ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
            return busLineBOs;
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            AddLine addLine = new AddLine(bl, this);
            addLine.Show();
        }
        private void Udapting(object sender, RoutedEventArgs e)
        {
            FrameworkElement frameworkElement = sender as FrameworkElement;
            BusLine busLinePO = frameworkElement.DataContext as BusLine;
            UdptingLine udptingLine = new UdptingLine(busLinePO, bl);
            _ = udptingLine.ShowDialog();
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = sender as FrameworkElement;
                BusLine busLinePO = frameworkElement.DataContext as BusLine;
                bl.DeleteBusLineBO(busLinePO.BusLineID1);
                _ = busLineBOs.Remove(busLinePO);
                lines.Items.Refresh();
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
        private void Lines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BusLine busLinePO = (BusLine)lines.SelectedItem;
            ShowLine showLine = new ShowLine(busLinePO, bl, this);
            _ = showLine.ShowDialog();
            lines.Items.Refresh();
        }
    }
}


//public partial class NumericUpDownControl : UserControl
//{
//    private float? num = null;
//    public float? Value
//    {
//        get { return num; }
//        set
//        {
//            if (value > MaxValue)
//                num = MaxValue;
//            else if (value < MinValue)
//                num = MinValue;
//            else
//                num = value;

//            textNumber.Text = num == null ? "" : num.ToString();
//        }
//    }

//    public int MinValue { get; set; }
//    //  public int MaxValue { get; set; }



//    public int MaxValue
//    {
//        get { return (int)GetValue(MaxValueProperty); }
//        set { SetValue(MaxValueProperty, value); }
//    }

//    // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
//    public static readonly DependencyProperty MaxValueProperty =
//        DependencyProperty.Register("MaxValue", typeof(int), typeof(NumericUpDownControl), new PropertyMetadata(100));





//    public NumericUpDownControl()
//    {
//        InitializeComponent();
//        MaxValue = 100;
//    }



//    private void cmdUp_Click(object sender, RoutedEventArgs e)
//    {
//        Value++;
//    }

//    private void cmdDown_Click(object sender, RoutedEventArgs e)
//    {
//        Value--;
//    }

//    private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
//    {
//        if (textNumber == null || textNumber.Text == "-")
//            return;

//        float val;
//        if (!float.TryParse(textNumber.Text, out val))
//            textNumber.Text = Value.ToString();
//        else
//            Value = val;
//    }
//}
//}
