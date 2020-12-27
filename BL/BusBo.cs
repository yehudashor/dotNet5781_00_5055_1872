using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Windows;
using System.Windows;
using BLAPI;
using BO;
using DO;
using System.Windows.Forms;

namespace BL
{
    public enum TravelMode
    {
        ReadyToGo,
        InMiddleOfTrip,
        InTreatment,
        OnRefueling,
    }
    public class BusBL : BusBO
    {
        public BusBL(string _License_number, DateTime _StartDate, DateTime _DayOfTreatment, int _KmForRefueling = 0, int _KmForTreatment = 0, int _TotalMiles = 0)
        {
            DayOfTreatment = _DayOfTreatment;
            StartDate = _StartDate;
            License_number = _License_number;
            KmForRefueling = _KmForRefueling;
            KmForTreatment = _KmForTreatment;
            TotalMiles = _TotalMiles;
            Status = TravelMode.ReadyToGo;
        }
        public string Nicense_number { get; set; }
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
            }
        }
        public int KmForTreatment1
        {

            get => KmForTreatment;
            set
            {
                DateTime yearAgo = DateTime.Today.AddYears(-1);
                if (value + KmForTreatment > 20000 || yearAgo > DayOfTreatment)
                {
                    _ = MessageBox.Show("Treatment");
                }

                else
                {
                    KmForTreatment += value;
                }
            }
        }
    }
}
