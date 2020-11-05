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
        /// <param name="stationNumber"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="StationAddress"></param>
        //public BusStop(string stationNumber, float latitude, float longitude, string stationAddress)
        //{
        //    this.StationNumber = stationNumber;
        //    this.Latitude = latitude;
        //    this.Longitude = longitude;
        //    this.StationAddress = StationAddress;
        //}

        /// <summary>
        /// 
        /// </summary>
        private int stationNumber;
        public int StationNumber
        {
            get => stationNumber; 
    
            
            set {
                try
                {
                    if (stationNumber < 1000000 && stationNumber > 0)
                        stationNumber = value;
                    //else
                    //{
                    //    throw;
                    //}
                }
                catch 
                {
                    Console.WriteLine();
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
            return string.Format("Bus Station Code: {0}\t , Latitude : {1:f}°N\t , longitude: {2:f}°E \t", StationNumber , Latitude , Longitude);
        }
    }
}
