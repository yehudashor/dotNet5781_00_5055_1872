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
using System.Threading;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public int Number { get; set; }
        public float Number1 { get; set; }
        private static readonly Random r = new Random(DateTime.Now.Millisecond);
        public static BackgroundWorker _timeOfTrval;
        public static BackgroundWorker Worker1;
        public Window2()
        {
            //_timeOfTrval.ProgressChanged += MainWindow.RefuelingToBus2;
            InitializeComponent();
            _timeOfTrval = new BackgroundWorker();
            _timeOfTrval.DoWork += RefuelingToBus1;
            _timeOfTrval.WorkerReportsProgress = true;

            Worker1 = new BackgroundWorker();
            Worker1 = new BackgroundWorker();
            Worker1.DoWork += RefuelingToBus0;
            Worker1.ProgressChanged += RefuelingToBus3;
            Worker1.WorkerReportsProgress = true;
        }

        /// <summary>
        /// A function that has an event of pressing enter (bonus) in the window and when clicked will call the travel expense function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (kmForRefuelingTextBox.Text == null)
            {
                return;
            }

            if (e == null)
            {
                return;
            }

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                Number = int.Parse(kmForRefuelingTextBox.Text);
                _timeOfTrval.RunWorkerAsync();
                Worker1.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Function for taking the bus for a ride according to the requirements of the exercise.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefuelingToBus1(object sender, DoWorkEventArgs e)
        {
            if (MainWindow.Bus1.KmForRefueling + Number > 1200)
            {
                _ = MessageBox.Show("Refueling required!!!", "Error");
            }
            else
            {
                DateTime yearAgo = DateTime.Today.AddYears(-1);
                if (MainWindow.Bus1.KmForTreatment + Number > 20000 || yearAgo > MainWindow.Bus1.DayOfTreatment)
                {
                    _ = MessageBox.Show("Treatment is required!!!", "Error");
                }
                else
                {
                    if (MainWindow.Bus1.Status == TravelMode.InMiddleOfTrip)
                    {
                        _ = MessageBox.Show("InMiddleOfTrip!!!", "Error");
                    }
                    else
                    {
                        Number1 = Number / r.Next(20, 50) * 6000;
                        Thread.Sleep((int)Number1);
                        MainWindow.Bus1.TotalMiles = Number;
                        MainWindow.Bus1.KmForRefueling = Number;
                        MainWindow.Bus1.KmForTreatment = Number;
                        _ = MessageBox.Show("The trip is over");
                    }
                }
            }
        }

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

        private void RefuelingToBus0(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < Number1 / 1000; i++)
            {
                Worker1.ReportProgress(i);
                Thread.Sleep(1000);
            }
        }
        private void RefuelingToBus3(object sender, ProgressChangedEventArgs e)
        {
            proggr.Value = e.ProgressPercentage;
        }
    }
}
