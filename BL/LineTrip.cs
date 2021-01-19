using System;
using System.Collections.Generic;

namespace BO
{
    public class LineTrip
    {
        public int LineNumber { get; set; }
        public string NameOfLastStation { get; set; }
        //public List<TimeSpan> StartTrip = new List<TimeSpan>();
        //public List<TimeSpan> TimeCameToStation = new List<TimeSpan>();
        //public List<TimeSpan> TimeFromStationToDestination = new List<TimeSpan>()
        public TimeSpan StartTrip { get; set; }
        public TimeSpan TimeCameToStation { get; set; }
        public TimeSpan TimeFromStationToDestination { get; set; }
    }
}
