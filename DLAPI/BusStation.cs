using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;

namespace DO
{
    /// <summary>
    /// 
    /// </summary>
    public class BusStation
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
