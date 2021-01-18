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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Threading;
using System.ComponentModel;
using dotNet_01_5055_1872;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static int Time { get; set; }
        private FrameworkElement fxElt;
        public Button S { get; set; }
        public static int Temp1 { get; set; }
        public static Bus Bus1 { set; get; }
        public static ObservableCollection<Bus> listBus = new ObservableCollection<Bus>();
        private static readonly Random r = new Random(DateTime.Now.Millisecond);
        public static BackgroundWorker Worker;
        public static BackgroundWorker Worker1;
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Calling the function of initializing and placing the list in listView.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            ListOfBuses(ref listBus);
            BusList.ItemsSource = listBus;
            Worker = new BackgroundWorker();
            Worker.DoWork += RefuelingToBus1;
            Worker1 = new BackgroundWorker();
            Worker1.DoWork += RefuelingToBus0;
            Worker1.ProgressChanged += RefuelingToBus3;
            Worker1.WorkerReportsProgress = true;
        }

        public FrameworkElement FxElt
        {
            get => fxElt;
            set
            {
                fxElt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(" FxElt"));
            }
        }

        /// <summary>
        /// A function that initializes between ten and fifteen buses according to the requirements of the exercise.
        /// </summary>
        /// <param name="listBus"></param>
        private void ListOfBuses(ref ObservableCollection<Bus> listBus)
        {
            for (int i = 0; i < r.Next(10, 15); i++)
            {
                int number = r.Next(1200);
                DateTime _StartDate = new DateTime(r.Next(1999, 2020), r.Next(1, 13), r.Next(1, 29));
                double number1 = _StartDate.Year >= 2018 ? r.Next(10000000, 100000000) : r.Next(1000000, 10000000);
                DateTime _StartDate1 = new DateTime(2020, r.Next(1, 13), r.Next(1, 29));
                Bus bus = new Bus(number1.ToString(), _StartDate, _StartDate1, number, number * r.Next(3, 10), number * r.Next(20, 25));
                listBus.Add(bus);
            }

            for (int i = r.Next(10), j = 0; j < 2; j++, i = r.Next(10))
            {
                listBus[i].DayOfTreatment = listBus[i].DayOfTreatment.AddYears(2);
                listBus[i].KmForTreatment *= -1;
                listBus[i].KmForTreatment = r.Next(19000, 20000);
                listBus[i].KmForRefueling *= -1;
                listBus[i].KmForRefueling = r.Next(1150, 1200);
            }
        }

        /// <summary>
        /// A push of a button event at the push of a button will open a new window
        /// for adding line data And then refresh the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewBus1(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            _ = window1.ShowDialog();
            BusList.Items.Refresh();
        }

        /// <summary>
        /// A button that opens a new window at the click of a button to Delete a bus
        /// for a bonus by enriching the graphical interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBus(object sender, RoutedEventArgs e)
        {
            Window4 window4 = new Window4();
            _ = window4.ShowDialog();
            BusList.Items.Refresh();
        }

        /// <summary>
        /// An event of a push of a button at the push of a button will open a data entry window for the number of kilometers of the trip and update the data
        /// if this is possible by pressing enter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoTrip(object sender, RoutedEventArgs e)
        {
            FrameworkElement fxElt = sender as FrameworkElement;
            Bus1 = fxElt.DataContext as Bus;
            Window2 window2 = new Window2();
            _ = window2.ShowDialog();
            BusList.Items.Refresh();
        }


        /// <summary>
        /// A function that sends the bus for refueling.
        ///  process start warking!!!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefuelingToBus(object sender, RoutedEventArgs e)
        {
            FxElt = sender as FrameworkElement;
            Bus1 = FxElt.DataContext as Bus;
            Worker.RunWorkerAsync();
            Worker1.RunWorkerAsync();
            BusList.Items.Refresh();
        }

        /// <summary>
        /// Refueling By process
        /// </summary>
        /// <param name="sender"></param>
        public static void RefuelingToBus1(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(12000);
            e.Result = false;
            Bus1.Refueling();
            _ = MessageBox.Show("The bus will be successfully refueled");
        }

        /// <summary>
        /// Event function of the listView of selecting an element from the list.
        /// Clicking on any element in the list will open a new window displaying the line information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bus_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Window3 window3 = new Window3((Bus)BusList.SelectedItem);
            _ = window3.ShowDialog();
            BusList.Items.Refresh();
        }

        private void RefuelingToBus0(object sender, DoWorkEventArgs e)
        {
            for (int i = 12; i >= 0; i--)
            {
                Worker1.ReportProgress(i);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefuelingToBus3(object sender, ProgressChangedEventArgs e)
        {
            temp.Text = e.ProgressPercentage.ToString();
            proggr.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeFoul_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //var p = FxElt.Parent as Grid;
            //    var s = p.Children[9] as ProgressBar;
            //    s.Value = 100;
            //temp2 = sender as FrameworkElement as ProgressBar;
            //temp2.Value = 30;
        }
    }
}











