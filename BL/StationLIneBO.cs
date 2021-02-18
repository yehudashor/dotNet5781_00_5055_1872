using System;
namespace BO
{
    /// <summary>
    /// A class representing a line station includes information about the time and distance to the next station.
    /// </summary>
    public class StationLineBO
    {
        public int BusLineID2 { get; set; }
        public string NameOfStation { get; set; }
        public int StationNumberOnLine { get; set; }
        public int LocationNumberOnLine { get; set; }
        public bool ChackDelete2 { get; set; }
        public float DistanceBetweenTooStations { get; set; }
        public TimeSpan AverageTime { get; set; }
    }
}
