using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for cameToStation.xaml
    /// </summary>
    public partial class cameToStation : Window
    {
        public IBL1 bl;
        public BO.StationLineBO StationLine { get; set; }
        public BO.BusLineBO BusLine { get; set; }
        public ObservableCollection<BO.BusLineBO> busLineBOs = new ObservableCollection<BO.BusLineBO>();
        private bool flag;
        public BackgroundWorker Worker;
        public BackgroundWorker Worker1;
        public cameToStation(IBL1 bl1)
        {
            InitializeComponent();
            bl = bl1;
            lines.ItemsSource = Lines(busLineBOs);
            lines.DisplayMemberPath = "LineNumber";
        }
        private ObservableCollection<BO.BusLineBO> Lines(ObservableCollection<BO.BusLineBO> busLineBOs)
        {
            try
            {
                int count = bl.ReturnBusLineIdFromDl();
                for (int i = 0; i < count; i++)
                {
                    if (bl.LineInformation(i) != null)
                    {
                        busLineBOs.Add(bl.LineInformation(i));
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
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BusLine = new BO.BusLineBO();
            BusLine = (BO.BusLineBO)lines.SelectedItem;
            Stations.ItemsSource = BusLine.StationLineBOs;
            Stations.DisplayMemberPath = "NameOfStation";
            //Worker1 = new BackgroundWorker();
            //Worker1.DoWork += Worker_DoWork0;
        }

        private void Stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationLine = new BO.StationLineBO();
            StationLine = Stations.SelectedItem as BO.StationLineBO;
            //Worker1.RunWorkerAsync();
            Worker = new BackgroundWorker();
            Worker.DoWork += Worker_DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged1;
            Worker.WorkerReportsProgress = true;
            Worker.RunWorkerAsync();
        }
        //private void Worker_DoWork0(object sender, DoWorkEventArgs e)
        //{
        //    Worker = new BackgroundWorker();
        //    Worker.DoWork += Worker_DoWork;
        //    Worker.ProgressChanged += Worker_ProgressChanged1;
        //    Worker.WorkerReportsProgress = true;
        //    Worker.RunWorkerAsync();
        //    Thread.Sleep(6000);
        //}
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            flag = true;
            while (flag)
            {
                Worker.ReportProgress(1);
                Thread.Sleep(60000);
            }
        }

        private void Worker_ProgressChanged1(object sender, ProgressChangedEventArgs e)
        {
            IEnumerable<string> timeSpans = bl.TimeCamingToCurrnetStation(BusLine.BusLineID1, StationLine.StationNumberOnLine);
            time.ItemsSource = timeSpans.ToList();
        }
    }
}
