using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace UI.LineTrip
{
    /// <summary>
    /// Interaction logic for LineTripS.xaml
    /// </summary>
    public partial class LineTripS : Window
    {
        //Trip
        public ObservableCollection<BO.BusStationBO> busStationBOs = new ObservableCollection<BO.BusStationBO>();
        public ObservableCollection<BO.LineTrip> lineTrips = new ObservableCollection<BO.LineTrip>();
        public BackgroundWorker Timerworker;
        public BackgroundWorker Worker1;
        public int SimulationTime { get; set; }
        public int StationNumber { get; set; }
        public TimeSpan SystemClock { get; set; }
        public TimeSpan SystemClockForBl { get; set; }
        private Stopwatch stopWatch;
        private bool isTimerRun;

        IBL1 bl;
        public LineTripS(IBL1 bl1)
        {
            InitializeComponent();
            bl = bl1;
            foreach (BO.BusStationBO item in bl.ShowStation())
            {
                busStationBOs.Add(item);
            }
            Trip.ItemsSource = busStationBOs;
            stopWatch = new Stopwatch();

            Timerworker = new BackgroundWorker();
            Timerworker.DoWork += Worker_DoWork;
            Timerworker.ProgressChanged += Worker_ProgressChanged;
            Timerworker.WorkerReportsProgress = true;
        }

        private void Trip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                StationNumber = (Trip.SelectedItem as BO.BusStationBO).StationNumber;
                Signage.ItemsSource = bl.LinePastInStationBOs(StationNumber);
                if (SystemClockForBl == TimeSpan.Zero)
                {
                    return;
                }
                Worker1 = new BackgroundWorker();
                Worker1.DoWork += Worker_DoWork1;
                Worker1.ProgressChanged += Worker_ProgressChanged1;
                Worker1.WorkerReportsProgress = true;
                Worker1.RunWorkerAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string chack = timer.Content.ToString();

            if (chack == "הפעל" && TS.Text != "")
            {
                timer.Content = "עצור";
                SimulationTime = int.Parse(TS.Text);
                TS.IsReadOnly = true;
                if (!isTimerRun)
                {
                    stopWatch.Restart();
                    isTimerRun = true;
                    Timerworker.RunWorkerAsync();
                }
            }
            else
            {
                stopWatch.Stop();
                isTimerRun = false;
                timer.Content = "הפעל";
                h.IsReadOnly = false;
                m.IsReadOnly = false;
                s.IsReadOnly = false;
                h.ClearValue(TextBox.TextProperty);
                m.ClearValue(TextBox.TextProperty);
                s.ClearValue(TextBox.TextProperty);

            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            float a = (float)SimulationTime / 60;
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            SystemClockForBl = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second + TimeSpan.Parse(timerText).Seconds);
            // SystemClockForBl = timeSpan + timeSpan; //+ new TimeSpan(stopWatch.ElapsedTicks * SimulationTime);
            h.Text = SystemClockForBl.Hours.ToString();
            m.Text = SystemClockForBl.Minutes.ToString();
            s.Text = SystemClockForBl.Seconds.ToString();
        }


        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                Thread.Sleep(1000);
                Timerworker.ReportProgress(1);
            }
        }
        private void Worker_DoWork1(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                Worker1.ReportProgress(1);
                Thread.Sleep(6000);
            }
        }

        private void Worker_ProgressChanged1(object sender, ProgressChangedEventArgs e)
        {
            panl.ItemsSource = bl.LineTrips(StationNumber, SystemClockForBl);
        }
    }
}
