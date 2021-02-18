
namespace BO
{
    /// <summary>
    /// A class representing a physical station.
    /// </summary>
    public class BusStationBO
    {
        public int StationNumber { get; set; }
        public int StationId1 { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string NameOfStation { get; set; }
        public string StationAddress { get; set; }
        public bool AccessForDisabled { get; set; }
        public bool RoofToTheStation { get; set; }
        public bool IsAvailable3 { get; set; }
    }
}
