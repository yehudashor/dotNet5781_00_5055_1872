using System;

namespace BO
{
    /// <summary>
    /// A class representing the arrival times of the lines to the station.
    /// In addition includes the travel time of a line from the above station to any station at the request of the company / customer.
    /// </summary>
    public class LineTrip
    {
        public int LineNumber { get; set; }
        public string NameOfLastStation { get; set; }
        public TimeSpan StartTrip { get; set; }
        public TimeSpan TimeCameToStation { get; set; }
        public TimeSpan TimeFromStationToDestination { get; set; }
    }
}
