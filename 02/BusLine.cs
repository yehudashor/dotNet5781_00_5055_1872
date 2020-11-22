using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace dotNet_02_5055_1872
{

    public class BusLine : IComparable
    {

        public BusLine() { }

        /// <summary>
        /// A list containing objects of line stations (which also includes physical stations).
        /// </summary>
        protected List<BusLineStation> routeTheLine = new List<BusLineStation>();

        /// <summary>
        /// proprty to list.
        /// </summary>
        public List<BusLineStation> RouteTheLine
        {
            get => routeTheLine;
            set => routeTheLine = value;
        }

        /// <summary>
        /// An object of the rendom class designed to grill a random number to select the line area from the enum.
        /// </summary>
        private static readonly Random r = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// A constructor that initializes the line number and area.
        /// </summary>
        /// <param name="line_number"></param>
        public BusLine(int line_number)
        {
            LineNumber = line_number;
            Area1 = Area1;
        }

        /// <summary>
        /// proprty to number of line.
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// proprty to enum Which selects an area for the line at random.
        /// </summary>
        private Area area1;
        public Area Area1
        {
            get => area1;
            set
            {
                int number = r.Next(8);
                value = (Area)number;
                area1 = value;
            }
        }

        /// <summary>
        /// proprty to the first station.
        /// </summary>
        public BusLineStation FirstStation
        {
            get => RouteTheLine[0];
            set => RouteTheLine[0] = value;
        }

        /// <summary>
        /// proprty to the last station. 
        /// </summary>
        public BusLineStation LastStation
        {
            get => RouteTheLine[RouteTheLine.Count - 1];
            set => RouteTheLine[RouteTheLine.Count - 1] = value;
        }

        /// <summary>
        /// indexer Who gets an index and returns his show to the list.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BusLineStation this[int index] => routeTheLine[index];

        /// <summary>
        /// Override method to string from object class.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "";
            result += "line number: " + LineNumber + '\n';
            result += "an area in the country: " + Area1 + '\n';
            result += "Station numbers on the round trip: ";

            foreach (BusLineStation item in RouteTheLine)
            {
                result += "\n";
                result += "Station number:";
                result += item.StationNumber;
            }
            foreach (BusLineStation item in RouteTheLine)
            {
                result += '\n';
                result += item;
            }
            return result;
        }

        /// <summary>
        /// Method for adding a line station object to the list of stations, the method gets an index for placement and an object of a line station. 
        /// There are options to add a station to the beginning of the list to the end of the list and to the middle of the list in case of a
        /// large index from the count the list will throw an exception.
        /// </summary>
        /// <param name="index = Location of the station on the list"></param>
        /// <param name="Stations = Line station object"></param>
        public void AddLineToTheRoundTrip(int index, BusLineStation Stations)
        {
            if (index == 0)
            {
                AddFirst(Stations);
            }

            else
            {
                if (index > RouteTheLine.Count)
                {
                    throw new ArgumentException("Error!!! Index larger than the range");
                }

                if (index == RouteTheLine.Count)
                {
                    AddLast(Stations);
                }
                else
                {
                    RouteTheLine.Insert(index, Stations);
                }
            }
        }

        /// <summary>
        /// Add a station to the beginning of the list.
        /// </summary>
        /// <param name="Stations"></param>
        public void AddFirst(BusLineStation Stations)
        {
            RouteTheLine.Insert(0, Stations);
            FirstStation = routeTheLine[0];
        }

        /// <summary>
        /// Add a station to the end of the list.
        /// </summary>
        /// <param name="Stations"></param>
        public void AddLast(BusLineStation Stations)
        {
            RouteTheLine.Insert(RouteTheLine.Count, Stations);
            LastStation = routeTheLine[RouteTheLine.Count - 1];
        }

        /// <summary>
        /// A station delete method gets a station number and deletes its instance from the list.
        /// </summary>
        /// <param name="NumberOfStatian"></param>
        public void DeleteLineStationFromRoute(int NumberOfStatian)
        {
            int index = 0;
            for (int i = 0; i < RouteTheLine.Count; i++)
            {
                if (RouteTheLine[i].StationNumber == NumberOfStatian)
                {
                    index = i;
                }
            }
            RouteTheLine.RemoveAt(index);
        }

        /// <summary>
        /// A Boolean method that checks if a particular station is on a route.
        /// </summary>
        /// <param name="Stationnum = number of station"></param>
        /// <returns></returns>
        public bool InRoute(int Stationnum)
        {
            foreach (BusLineStation item in RouteTheLine)
            {
                if (item.StationNumber == Stationnum)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// /// A method that accepts two station numbers and returns the distance between them.
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <returns></returns>
        public double DistanceBetweenTwoStations(int station1, int station2)
        {
            int s1 = 0, s2 = 0;
            double d = 0.0;

            for (int i = 0; i < RouteTheLine.Count; i++)
            {
                if (station1 == RouteTheLine[i].StationNumber)
                {
                    s1 = i;
                }
                if (station2 == RouteTheLine[i].StationNumber)
                {
                    s2 = i;
                }
            }

            for (int i = s1; i <= s2; i++)
            {
                d += RouteTheLine[s1 + 1].Distance;
            }
            return d;
        }

        /// <summary>
        /// A method that receives two station numbers and returns the travel time between them.
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <returns></returns>
        public float TravelTimeBetweenStations(int station1, int station2)
        {
            int s1 = 0, s2 = 0;
            float time = 0;

            for (int i = 0; i < RouteTheLine.Count; i++)
            {
                if (station1 == RouteTheLine[i].StationNumber)
                {
                    s1 = i;
                }
                if (station2 == RouteTheLine[i].StationNumber)
                {
                    s2 = i;
                }
            }

            for (int i = s1; i <= s2; i++)
            {
                time += RouteTheLine[s1 + 1].TravelTimeToNextStation;
            }

            return time;
        }

        /// <summary>
        ///A method that receives two station numbers and returns a sub-route of the line from the first station received to the second station received.
        /// </summary>
        /// <param name="Station1"></param>
        /// <param name="Station2"></param>
        /// <returns></returns>
        public BusLine SubRouteOfTheLine(int Station1, int Station2)
        {
            BusLine Temp = new BusLine();
            int s1 = 0, s2 = 0;
            for (int i = 0; i < RouteTheLine.Count; i++)
            {
                if (Station1 == RouteTheLine[i].StationNumber)
                {
                    s1 = i;
                }
                if (Station2 == RouteTheLine[i].StationNumber)
                {
                    s2 = i;
                }
            }
            for (int i = s1; i <= s2; i++)
            {
                Temp.RouteTheLine.Add(RouteTheLine[i]);
            }
            return Temp;
        }

        /// <summary>
        /// Add a proprty to the computer class for the total travel time of the line
        /// </summary>
        public double AllTime1 { get; set; }

        /// <summary>
        /// Add a proprty to the computer class for the total travel time of the line
        /// </summary>
        public double AllTime2 { get; set; }

        /// <summary>
        /// Implementing the Icomparable interface that compares the total travel time of two objects and returns the result in a large, small and equal ratio. Which makes the department comparable.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            BusLine other = (BusLine)obj;
            foreach (BusLineStation item in RouteTheLine)
            {
                AllTime1 += item.TravelTimeToNextStation;
            }

            foreach (BusLineStation item in other.RouteTheLine)
            {
                other.AllTime2 += item.TravelTimeToNextStation;
            }

            return AllTime1.CompareTo(other.AllTime2);
        }
    }
}
