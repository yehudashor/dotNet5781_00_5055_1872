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
    public class ConsecutiveStations
    {
        public int StationNumber1 { get; set; }

        public int StationNumber2 { get; set; }

        public float DistanceBetweenTooStations { get; set; }
        public TimeSpan AverageTime { get; set; }
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
