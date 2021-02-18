using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;

namespace DO
{
    /// <summary>
    /// Department of Subsequent Stations. 
    /// Which holds information about the time and distance of two consecutive stations.
    /// </summary>
    public class ConsecutiveStations
    {
        public int StationNumber1 { get; set; }

        public int StationNumber2 { get; set; }

        public float DistanceBetweenTooStations { get; set; }
        public TimeSpan AverageTime { get; set; }
    }
}
