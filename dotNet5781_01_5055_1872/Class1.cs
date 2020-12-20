using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Windows;
using System.Windows;
using System.Windows.Media;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace dotNet_01_5055_1872
{
    /// <summary>
    /// TravelMode by
    /// </summary>
    /// 
    public enum TravelMode
    {
        ReadyToGo,
        InMiddleOfTrip,
        InTreatment,
        OnRefueling,
    }
    public class Bus : INotifyPropertyChanged
    {
        public Background background = new Background();

        public event PropertyChangedEventHandler PropertyChanged;
        public Bus(string _License_number, DateTime _StartDate, DateTime _DayOfTreatment, int _KmForRefueling = 0, int _KmForTreatment = 0, int _TotalMiles = 0)
        {
            DayOfTreatment = _DayOfTreatment;
            StartDate = _StartDate;
            License_number = _License_number;
            KmForRefueling = _KmForRefueling;
            KmForTreatment = _KmForTreatment;
            TotalMiles = _TotalMiles;
            Status = TravelMode.ReadyToGo;
        }

        private static readonly Random random = new Random(DateTime.Now.Millisecond);
        private readonly Background Brushes;
        private TravelMode status;

        /// <summary>
        /// 
        /// </summary>
        public TravelMode Status
        {
            get => status;
            set
            {
                int number = random.Next(3);
                value = (TravelMode)number;
                status = status == TravelMode.InMiddleOfTrip ? TravelMode.InMiddleOfTrip : value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
            }
        }

        public Bus() { }

        public DateTime DayOfTreatment { get; set; }

        public int KmOfTreatment { get; set; }

        /// <summary>
        /// kmForAllBuses = A personal addition that sums up the overall mileage of all buses For future optional use
        /// </summary>
        public static int kmForAllBuses = 0;

        /// <summary>
        /// A feature whose function is to measure the number of kilometers between treatments in order to prevent deviation above 20,000 km.
        /// In addition, it produces an abnormality if a year has passed since the last treatment
        /// </summary>
        private int kmForTreatment;
        public int KmForTreatment
        {

            get => kmForTreatment;
            set
            {
                DateTime yearAgo = DateTime.Today.AddYears(-1);
                if (value + kmForTreatment > 20000 || yearAgo > DayOfTreatment)
                {
                    _ = MessageBox.Show("Treatment");
                }

                else
                {
                    kmForTreatment += value;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("KmForTreatment"));
            }
        }

        /// <summary>
        /// A feature whose function is to prevent the vehicle from exceeding 1200 kilometers without refueling
        /// </summary>
        private int kmForRefueling;
        public int KmForRefueling
        {
            get => kmForRefueling;
            set
            {
                if (kmForRefueling + value <= 1200)
                {
                    kmForRefueling += value;
                }
                else
                {
                    if (value + kmForTreatment > 20000 || ((DateTime.Now - DayOfTreatment).TotalDays < 365))
                    {
                        RefuelingAndTreatment();
                        _ = MessageBox.Show("Refueling and handling The bus was ready to go");
                    }
                    else
                    {
                        _ = MessageBox.Show("Refueling");
                        Refueling();
                    }
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("KmForRefueling"));
            }
        }

        /// <summary>
        /// counter of km general
        /// </summary>
        private int totalMiles;

        public int TotalMiles
        {
            get => totalMiles;
            set
            {
                totalMiles += value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalMiles"));
            }
        }
        public DateTime StartDate { get; set; }

        public string Nicense_number { get; set; }

        /// <summary>
        /// A property that places a value in the license number and returns a value of the license number,
        /// in the form of the required format
        /// </summary>
        public string License_number
        {
            get
            {
                string start, middel, end;
                if (StartDate.Year < 2018)
                {
                    start = Nicense_number.Substring(0, 2);
                    middel = Nicense_number.Substring(2, 3);
                    end = Nicense_number.Substring(5, 2);
                }
                else
                {
                    start = Nicense_number.Substring(0, 3);
                    middel = Nicense_number.Substring(3, 2);
                    end = Nicense_number.Substring(5, 3);
                }
                return string.Format("{0}-{1}-{2}", start, middel, end);
            }
            set
            {
                if (StartDate.Year < 2018 && value.Length == 7)
                {
                    Nicense_number = value;
                }

                if (StartDate.Year >= 2018 && value.Length == 8)
                {
                    Nicense_number = value;
                }

                if (value.Length != 7 && value.Length != 8)
                {
                    Console.WriteLine("The license number must be 7 or 8 digits");
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("License_number"));
            }
        }

        /// <summary>
        ///     A treatment method that updates the required fields        
        /// </summary>
        /// 
        public void Treatment()
        {
            DayOfTreatment = DateTime.Now;
            KmOfTreatment = totalMiles;
            KmForTreatment *= -1;
        }

        /// <summary>
        /// Reset the fuel
        /// </summary>
        public void Refueling()
        {
            KmForRefueling *= -1;
        }

        public void RefuelingAndTreatment()
        {
            Treatment();
            Refueling();
            Status = TravelMode.ReadyToGo;
        }

        /// <summary>
        /// Execution of Command 4.
        /// Printing a license number and mileage from a recent handling for the vehicle
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("License_number = {0}\t {1}\t {2}\t  {3}\t  {4}\t  {5}  ", License_number, StartDate.Date.ToString(), kmForTreatment, KmForRefueling, TotalMiles, DayOfTreatment);
        }
    }
}
