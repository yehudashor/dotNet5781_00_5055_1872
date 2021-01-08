using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ConsecutiveStationsBo
    {
        public int StationNumber1 { get; set; }

        public int StationNumber2 { get; set; }
        public float DistanceBetweenTooStations { get; set; }
        public TimeSpan AverageTime { get; set; }
        //public override string ToString()
        //{
        //    return ToStringProperty();
        //}
        //private string ToStringProperty()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
