using System;
namespace BO
{
    /// <summary>
    /// An object that represents consecutive stations - 
    /// (in principle we did not need it in bl because we already have the information here in line stations),
    /// is here to represent only consecutive stations in the display and therefore exists in the above layer.
    /// </summary>
    public class ConsecutiveStationsBo
    {
        public int StationNumber1 { get; set; }

        public int StationNumber2 { get; set; }
        public float DistanceBetweenTooStations { get; set; }
        public TimeSpan AverageTime { get; set; }
    }
}
