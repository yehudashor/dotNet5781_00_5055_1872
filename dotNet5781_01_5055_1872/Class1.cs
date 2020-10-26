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
        DateTime dayOfTreatment;
        private int kmOfTreatment;
 
        private int kmForTreatment;
        public int KmForTreatment
        {
            get { return kmForTreatment; }
            set { 
                if(kmForTreatment + value <= 20000)
                {
                    kmForTreatment += value;
                }
                else
                {
                    //חריגה
                }
            }
        }


        private int totalMiles;
        public int TotalMiles
        {
            get { return totalMiles; }
            set { totalMiles += value;}
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private string license_number;
        public string License_number
        {
            get
            {
                string start, middel, end;
                if (license_number.Length == 7)
                {
                    start = license_number.Substring(0, 2);
                    middel = license_number.Substring(2, 3);
                    end = license_number.Substring(4, 2);
                }
                else
                {
                    start = license_number.Substring(0, 3);
                    middel = license_number.Substring(3, 2);
                    end = license_number.Substring(5, 3);
                }
                license_number = string.Format("{0}-{1}-{2}", start, middel, end);
                return license_number;
            }
            set
            {
                if (startDate.Year < 2018 && license_number.Length == 7)
                    license_number = value;
                else if (startDate.Year >= 2018 && license_number.Length == 8)
                    license_number = value;
                // חריגה
            }
        }
        
        public Bus(string _license_number, DateTime Start_Date)
        {    
          this.license_number = _license_number;
          this.startDate = Start_Date;
        }
        public void Treatment()
        {
            this.dayOfTreatment = DateTime.Now;
            this.kmOfTreatment = this.totalMiles;
        }
        
    }
}
