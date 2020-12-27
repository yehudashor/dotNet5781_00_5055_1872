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
    public enum Area { North, South, Center };
    public enum Area1 { Jerusalem, Galil, Hasharon, Shefela, Eilat };
    public class BusLine
    {
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
