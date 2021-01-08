using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationLineBO
    {
        public int BusLineID2 { get; set; }
        public string NameOfStation { get; set; }
        public int StationNumberOnLine { get; set; }
        public int LocationNumberOnLine { get; set; }
        public bool ChackDelete2 { get; set; }
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
