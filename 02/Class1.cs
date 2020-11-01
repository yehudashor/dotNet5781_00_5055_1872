using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_02_5055_1872
{
   
    class BusStop
    {
         public Random line = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// 
        /// </summary>
        private string stationNumber;
        public string StationNumber
        {
            get { return stationNumber; }

            set {
                if (stationNumber.Length <= 6)
                    stationNumber = value;
                else
                {
                    // חריגה
                }
               
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private float latitude;
        public float Latitude
        {
            get { return latitude; }

            set {
                value = line.Next(31, 33); // המשך 
                latitude = value; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private float longitude;
        public float Longitude
        {
            get { return longitude; }

            set {
                value = line.Next(34, 35); // המשך
                longitude = value;
            }
        } 
        public string StationAddress { get; set; }

        public override string ToString()
        {
            return string.Format("Bus Station Code: {0} , Latitude : {1:f}°N , longitude: {2:f}°E", StationNumber , Latitude , Longitude);
        }
    }
}
