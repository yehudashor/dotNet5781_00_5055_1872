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
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;
using dotNet_01_5055_1872;
using System.Collections.ObjectModel;
namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        private readonly Bus _Bus;
        private readonly BackgroundWorker _RefuelingToBus;
        private readonly BackgroundWorker _Treatment;
        private readonly BackgroundWorker _proggresBar;
        private readonly BackgroundWorker _proggresBar1;

        /// <summary>
        /// The window displays the bus details and the sending option for refueling and handling processes.
        /// </summary>
        /// <param name="Bus1"></param>
        public Window3(Bus Bus1)
        {
            InitializeComponent();
            _Bus = Bus1;
            MainGrid.DataContext = _Bus;
            _RefuelingToBus = new BackgroundWorker();
            _proggresBar = new BackgroundWorker();

            _RefuelingToBus.DoWork += RefuelingToBus1;
            _RefuelingToBus.ProgressChanged += RefuelingToBus0;
            _RefuelingToBus.RunWorkerCompleted += TimeOver;
            _proggresBar.DoWork += TimeRefueling1;
            _proggresBar.ProgressChanged += RefuelingToBus0;
            _proggresBar.WorkerReportsProgress = true;

            _Treatment = new BackgroundWorker();
            _proggresBar1 = new BackgroundWorker();
            _Treatment.DoWork += Treatment1;
            _Treatment.RunWorkerCompleted += TimeOver1;
            _proggresBar1.DoWork += TimeTreatment1;
            _proggresBar1.ProgressChanged += Treatment2;
            _proggresBar1.WorkerReportsProgress = true;
            onlyDate.Text = _Bus.StartDate.Date.ToString("d");
            onlyDate1.Text = _Bus.DayOfTreatment.Date.ToString("d");
        }

        /// <summary>
        /// Send button for refueling When the button is pressed, the function registered for the treadmill will be activated.
        /// In addition to the bonus the button will turn off.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Treatment(object sender, RoutedEventArgs e)
        {
            _Treatment.RunWorkerAsync();
            Trat.IsEnabled = false;
            _proggresBar1.RunWorkerAsync();
        }

        /// <summary>
        /// The treatment function performed in the process is
        /// to anesthetize the process to the desired time and treat the required line.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Treatment1(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(144000);
            e.Result = true;
            _Bus.Treatment();
            _ = MessageBox.Show("The bus will be successfully Treatment");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeTreatment1(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i < 145; i++)
            {
                _proggresBar1.ReportProgress(i);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeOver1(object sender, RunWorkerCompletedEventArgs e)
        {
            Trat.IsEnabled = (bool)e.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Treatment2(object sender, ProgressChangedEventArgs e)
        {
            TimeTreatment.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Send button for refueling When the button is pressed, the function registered for the treadmill will be activated.
        /// In addition to the bonus the button will turn off.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefuelingToBus(object sender, RoutedEventArgs e)
        {
            _RefuelingToBus.RunWorkerAsync();
            rful.IsEnabled = false;
            _proggresBar.RunWorkerAsync();
        }

        /// 
        /// <summary>
        /// The Refueling function performed in the process is
        /// to anesthetize the process to the desired time and treat the required line.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefuelingToBus1(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(12000);
            e.Result = true;
            _Bus.Refueling();
            _ = MessageBox.Show("The bus will be successfully refueled");
        }

        private void TimeRefueling1(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 15; i++)
            {
                _proggresBar.ReportProgress(100 / 12);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefuelingToBus0(object sender, ProgressChangedEventArgs e)
        {
            TimeRefueling.Value += e.ProgressPercentage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeOver(object sender, RunWorkerCompletedEventArgs e)
        {
            rful.IsEnabled = (bool)e.Result;
        }
    }
}
