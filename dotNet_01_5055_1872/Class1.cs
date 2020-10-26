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
        private DateTime StartDate;
        public int MyProperty
        {
            get { return StartDate; }
            set { StartDate = value; }
        }

        private string license_number;
        public string License_number
        {
            get
            {
                string start, middel, end, Newstring;
                if (license_number.Length() == 7)
                {
                    start = license_number.Substring(0, 2);
                    middel = license_number.Substring(2, 3);
                    end = license_number.Substring(4, 2);
                }
                else if (license_number.Length() == 8)
                {
                    start = license_number.Substring(0, 3);
                    middel = license_number.Substring(3, 2);
                    end = license_number.Substring(5, 3);
                }
                Newstring = string.Format("{0}-{1}-{2}", start, middel, end);
                return Newstring;
            }
            set
            {
                if (StartDate.Year() < 2018 && license_number.Length() == 7)
                    license_number = value;
                else if (StartDate.Year() >= 2018 && license_number.Length() == 8)
                    license_number = value;
                // חריגה
            }
        }

        public Bus(string _license_number, DateTime Start_Date)
        {
            this.license_number = _license_number;
            this.StartDate = Start_Date;
        }
    }
}
