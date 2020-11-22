using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_02_5055_1872
{
    public class BusStop
    {
        private static readonly Random line = new Random();

        public BusStop() { }

        /// <summary>
        /// A builder that initializes the fields of the set get in values.
        /// </summary>
        /// <param name="stationNumber"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="StationAddress"></param>
        public BusStop(int stationNumber, string stationAddress = "")
        {
            StationNumber = stationNumber;
            StationAddress = stationAddress;
            Latitude = (float)((float)(line.NextDouble() * (33.3 - 31)) + 31);
            Longitude = (float)((float)(line.NextDouble() * (35.5 - 34.3)) + 34.3);
        }

        private int stationNumber;
        /// <summary>
        /// The station number initialization is unusual when the number is greater than six digits.
        /// </summary>
        public int StationNumber
        {
            get => stationNumber;

            set => stationNumber = (value < 1000000 && value > 0) ? value : throw new ArgumentException("Incorrect station number: ");
        }

        /// <summary>
        /// longitude
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        /// longitude
        /// </summary>
        public float Longitude { get; set; }

        /// <summary>
        /// Station address.
        /// </summary>
        public string StationAddress { get; set; }

        /// <summary>
        /// Override method to string from object class
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Bus Station Code: {0}, Latitude : {1:f}°N , longitude: {2:f}°E , StationAddress: {3} ", StationNumber, Latitude, Longitude, StationAddress + '\n');
        }
    }
}
