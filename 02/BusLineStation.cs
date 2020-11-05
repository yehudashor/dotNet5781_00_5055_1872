using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_02_5055_1872 
{
    class BusLineStation : BusStop
    {
        // לבדוק האם ניתן לוותר על הבנאים במחלקה הזאת והיורשת ממנה
        //public BusLineStation(double distance, float travelTimeToNextStation, string stationNumber, float latitude, float longitude, string stationAddress) : base(stationNumber, longitude, longitude, stationAddress)
        //{
        //    this.Distance = distance;
        //    this.TravelTimeToNextStation = travelTimeToNextStation;
        //}
        public double Distance{ get; set; }
        public float TravelTimeToNextStation { get; set;}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
      public double DistanceFromPreviousStation(BusStop a)
      {
            double d;
            return d = Math.Sqrt((Latitude - a.Latitude) * (Latitude - a.Latitude) + (Longitude - a.Longitude) * (Longitude - a.Longitude));
      }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = base.ToString();
            Console.WriteLine();
            result += ("The distance from the previous station is: "  , Distance);
            Console.WriteLine();
            result += ("Travel time from the previous station is: " , TravelTimeToNextStation);
            Console.WriteLine();
            return result;
        }


        public static implicit operator List<object>(BusLineStation v)
        {
            throw new NotImplementedException();
        }
    }
}
