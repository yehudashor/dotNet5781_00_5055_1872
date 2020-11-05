using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_02_5055_1872
{
    enum Area { North, South, Center, Jerusalem, Galil, Hasharon, Shefela, Eilat };
    class BusLine :BusLineStation
    {
        private List<BusLineStation> routeTheLine = new List<BusLineStation>();
        Random r = new Random(DateTime.Now.Millisecond);
        /// <summary>
        /// 
        /// </summary>
        private Area area1;
        public Area Area1
        {
            get { return area1; }
            set {
                int number = r.Next(8);
                value = (Area)number;
                area1 = value; 
            }
        }


        public BusLine() {}

        /// <summary>
        /// /
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="travelTimeToNextStation"></param>
        /// <param name="stationNumber"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="stationAddress"></param>
        public BusLine(double distance, float travelTimeToNextStation, int stationNumber, float latitude, float longitude, string stationAddress) //: base(distance, travelTimeToNextStation, stationNumber, longitude, longitude, stationAddress)
        {
            BusLine busLine = new BusLine();
            busLine.StationNumber = stationNumber;
            busLine.Latitude = latitude;
            busLine.Longitude = longitude;
            busLine.Distance = distance;
            busLine.TravelTimeToNextStation = travelTimeToNextStation;
            busLine.area1 = Area1;
            routeTheLine.Add(busLine);
        }

        public BusLineStation FirstStation
        {
            get => routeTheLine[0];
        }
        public BusLineStation LastStation
        {
            get => routeTheLine[routeTheLine.Count - 1];
        }

        public BusLineStation this[int index] => routeTheLine[index];

        public int LineNumber { get; set; }
        public IEnumerable<BusLine> RouteTheLine { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = base.ToString();
            result += ("line number: ", LineNumber);
            Console.WriteLine();
            result += ("an area in the country: ", Area1);
            Console.WriteLine();
            result += "Station numbers on the round trip: ";
            foreach (BusLine item in RouteTheLine)
            {
                Console.WriteLine();
                result += item.StationNumber;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="stationNumber"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="stationAddress"></param>
        /// <param name="distance"></param>
        /// <param name="travelTimeToNextStation"></param>
        public void AddLineToTheRoundTrip(int index, int stationNumber, float latitude, float longitude, string stationAddress, double distance, float travelTimeToNextStation)
        {
            if (index == 0 || index == routeTheLine.Count)
                new BusLine(distance, travelTimeToNextStation, stationNumber, latitude, longitude, stationAddress);
            else
            {
                BusLine busLine = new BusLine();
                busLine.StationNumber = stationNumber;
                busLine.Latitude = latitude;
                busLine.Longitude = longitude;
                busLine.Distance = distance;
                busLine.TravelTimeToNextStation = travelTimeToNextStation;
                busLine.StationAddress = stationAddress;
                busLine.area1 = Area1;
                routeTheLine.Insert(index, busLine);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void DeletLineStationFromRoute(int index)
        {
            routeTheLine.RemoveAt(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="busLine"></param>
        /// <returns></returns>
        public bool inRoute(BusLine busLine)
        {
            bool f1 = RouteTheLine.Contains(busLine);
            return f1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="index1"></param>
        /// <returns></returns>
        public double DistanceBetweenTwoStations(int index, int index1)
        {
            double d = 0.0;
            for (int i = index; i < index1; i++)
                d += routeTheLine[index].DistanceFromPreviousStation(routeTheLine[index1]);
            return d;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="index1"></param>
        /// <returns></returns>
        public float TravelTimeBetweenStations(int index, int index1)
        {
            float time = 0;
            for (int i = index; i <= index1; i++)
                time += routeTheLine[index + 1].TravelTimeToNextStation;
            return time;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Statian1"></param>
        /// <param name="Statian2"></param>
        /// <returns></returns>
        List <BusLine> SubRouteOfTheLine(int Statian1, int Statian2)
        {
           List <BusLine> Temp = new List <BusLine>();
            for (int i = Statian1; i <= Statian2; i++)
                Temp.Add((BusLine)routeTheLine[Statian1]);
            return Temp;
        }






    }
}
