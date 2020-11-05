using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_01_5055_1872
{ 
    class Bus
    {
        private DateTime dayOfTreatment;
        private int kmOfTreatment;

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
            
            get { return kmForTreatment; }
            set {
                if (value + kmForTreatment > 20000 || ((DateTime.Now - dayOfTreatment).TotalDays < 365))
                    Console.WriteLine("danger!!! To make this trip you must perform treatment first.");
                else
                       kmForTreatment += value;
            }
        }

        /// <summary>
        /// counter of km general
        /// </summary>
        private int totalMiles;
        public int TotalMiles
        {
            get { return totalMiles; }
            set { totalMiles += value;}
        }

        /// <summary>
        /// 
        /// </summary>
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        /// <summary>
        /// The license number in the form of int
        /// </summary>
        private string license_number;

        public string Nicense_number
        {
            get { return license_number; }
            set { license_number = value; }
        }

        /// <summary>
        /// A property that places a value in the license number and returns a value of the license number,
        /// in the form of the required format
        /// </summary>
        public string License_number
        {
            get
            {
                string start, middel, end;
                if (startDate.Year < 2018)
                {
                    start = license_number.Substring(0, 2);
                    middel = license_number.Substring(2, 3);
                    end = license_number.Substring(5, 2);
                }
                else
                {
                    start = license_number.Substring(0, 3);
                    middel = license_number.Substring(3, 2);
                    end = license_number.Substring(5, 3);
                }
               return string.Format("{0}-{1}-{2}", start, middel, end); 
            }
            set
            {
                    if (startDate.Year < 2018 && value.Length == 7)
                            license_number = value;
                    if (startDate.Year >= 2018 && value.Length == 8)
                            license_number = value;
                    if (value.Length  != 7 && value.Length != 8)
                          Console.WriteLine("The license number must be 7 or 8 digits");
            }
        }

        /// <summary>
        /// A feature whose function is to prevent the vehicle from exceeding 1200 kilometers without refueling
        /// </summary>
        private int kmForRefueling;
        public int KmForRefueling
        {
            get { return kmForRefueling; }
            set
            {
                if (kmForRefueling + value <= 1200)
                    kmForRefueling += value;
                else
                    Console.WriteLine("Refueling is required for this trip!!!");
            }
        }

        /// <summary>
        ///     A treatment method that updates the required fields        
        /// </summary>
        /// 
        public void Treatment()
        {
            this.dayOfTreatment = DateTime.Now;
            this.kmOfTreatment = this.totalMiles;
            this.kmForTreatment = 0;
        }

        /// <summary>
        /// Reset the fuel
        /// </summary>
        public void Refueling()
        {
            this.KmForRefueling *= -1;
        }

        /// <summary>
        /// Execution of Command 4.
        /// Printing a license number and mileage from a recent handling for the vehicle
        /// </summary>
        /// <returns></returns>
        public override string ToString()
       {
            return string.Format("License_number = {0} kmForTreatment = {1} ", License_number, kmForTreatment);
       }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="_license_number"></param>
        /// <param name="Start_Date"></param>
        //public Bus(string _license_number, DateTime Start_Date)
        //{
        //    License_number = _license_number;
        //    StartDate = Start_Date;
        //}
    }
}
