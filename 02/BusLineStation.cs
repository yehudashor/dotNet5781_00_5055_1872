using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_02_5055_1872
{
    public class BusLineStation : BusStop
    {
        public BusLineStation() { }

        private static Random d = new Random();

        /// <summary>
        /// Line Station Class Builder: Sends parameters to the busstup 
        /// class and also initializes the class fields by lottery.
        /// </summary>
        /// <param name="stationNumber"></param>
        /// <param name="stationAddress"></param>
        public BusLineStation(int stationNumber, string stationAddress) : base(stationNumber, stationAddress)
        {
            Distance = d.Next(300);
            TravelTimeToNextStation = (float)Distance / 60;
        }

        /// <summary>
        /// Distance from previous station The discount is a lottery number of up to 300 km.
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// Travel time from last stop, it is assumed that the bus travels at a speed of 60 kilometers per hour.
        /// </summary>
        public float TravelTimeToNextStation { get; set; }

        /// <summary>
        /// Calculate another distance that we did not use later in the exercise.
        /// The calculation is made according to a formula for distance between the longitude and latitude
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public double DistanceFromPreviousStation(BusLineStation a)
        {
            double d = Math.Sqrt(((Latitude - a.Latitude) * (Latitude - a.Latitude)) + ((Longitude - a.Longitude) * (Longitude - a.Longitude)));
            return d;
        }

        /// <summary>
        /// Override method to string from object class.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = base.ToString();
            result += '\n';
            result += "The distance from the previous station is: " + Distance + '\n';
            result += "Travel time from the previous station is: " + TravelTimeToNextStation + '\n';
            return result;
        }


        public static implicit operator List<object>(BusLineStation v)
        {
            throw new NotImplementedException();
        }
    }
}
