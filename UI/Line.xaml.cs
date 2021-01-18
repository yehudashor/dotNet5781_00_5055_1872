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
    ///  <local:NumericUpDownControl x:Name="gradeNumUpDown" />
    //<LinearGradientBrush EndPoint = "0.5,1" StartPoint="0.5,0">
    //                    <GradientStop Color = "#FFEAE0E0" Offset="0.004"/>
    //                    <GradientStop Color = "#FFD3CCCC" Offset="1"/>
    //                    <GradientStop Color = "#FFA69C9C" Offset="0.5"/>
    //                </LinearGradientBrush>
    // <StackPanel Orientation = "Horizontal" >
    //    < Button x:Name="cmdDown" Margin="1" Content="˅" Click="cmdDown_Click" />
    //    <TextBox x:Name="textNumber" Margin="1" Width="50"  TextChanged="txtNum_TextChanged" />
    //    <Button x:Name="cmdUp" Margin="1" Content="˄" Click="cmdUp_Click" />
    //</StackPanel>
    public partial class Line : Window
    {
        public ObservableCollection<BO.BusLineBO> busLineBOs = new ObservableCollection<BO.BusLineBO>();
        public IBL1 bl;
        public Line(IBL1 bl1)
        {
            InitializeComponent();
            bl = bl1;
            lines.ItemsSource = Lines(busLineBOs);
            //if (busLineBOs1 != null)
            //{
            //    busLineBOs.Add(bl.LineInformation(busLineBOs1.BusLineID1));
            //    _ = busLineBOs.OrderBy(item => item.BusLineID1);
            //    lines.Items.Refresh();
            //}
        }

        private ObservableCollection<BO.BusLineBO> Lines(ObservableCollection<BO.BusLineBO> busLineBOs)
        {
            try
            {
                int count = bl.ReturnBusLineIdFromDl();
                for (int i = 0; i < 1; i++)
                {
                    busLineBOs.Add(bl.LineInformation(i));
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
            AddLine addLine = new AddLine(bl);
            addLine.Show();
            Close();
        }

        private void Udapting(object sender, RoutedEventArgs e)
        {
            FrameworkElement frameworkElement = sender as FrameworkElement;
            BO.BusLineBO busLineBO = frameworkElement.DataContext as BO.BusLineBO;
            UdptingLine udptingLine = new UdptingLine(busLineBO, bl);
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
            BO.BusLineBO busLineBO = (BO.BusLineBO)lines.SelectedItem;
            ShowLine showLine = new ShowLine(busLineBO, bl);
            _ = showLine.ShowDialog();
            lines.Items.Refresh();
            Close();
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
