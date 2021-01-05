using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusStationBO
    {
        public IEnumerable<BusLineBO> LinePastInStation { get; set; }
        public int StationNumber { get; set; }
        public int StationId1 { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string NameOfStation { get; set; }
        public string StationAddress { get; set; }

        public bool AccessForDisabled { get; set; }
        public bool RoofToTheStation { get; set; }
        public bool IsAvailable3 { get; set; }
        public override string ToString()
        {
            return string.Format("StationNumber: {0}, NameOfStation: {1}", StationNumber, NameOfStation);
        }

        private string ToStringProperty()
        {
            throw new NotImplementedException();
        }
    }
}
