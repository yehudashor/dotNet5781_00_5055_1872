using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum Area { North, South, Center };
    public enum Area1 { Jerusalem, Galil, Hasharon, Shefela, Eilat };
    public class BusLineBO
    {
        public List<BusStationBO> BusStationBO1 { get; set; }
        public List<StationLineBO> StationLineBOs { get; set; }
        public List<int> DistanceBetweenTooStationsList { get; set; }
        public List<double> AverageTimeBetweenTooStationsList { get; set; }
        public int BusLineID1 { get; set; }
        public int LineNumber { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public Area AreaBusInterUrban { get; set; }
        public Area1 AreaBusUrban { get; set; }
        public bool UrbanInterUrban { get; set; }
        public bool IsAvailable1 { get; set; }
        public override string ToString()
        {
            return ToStringProperty();
        }

        private string ToStringProperty()
        {
            throw new NotImplementedException();
        }
    }
}
