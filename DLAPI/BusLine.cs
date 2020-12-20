using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;

namespace DO
{
    public enum Area { North, South, Center};
    public enum Area1 { Jerusalem, Galil, Hasharon, Shefela, Eilat };
    public class BusLine
    {
        public int BusLineID1 { get; set; }
        public int LineNumber { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public Area Area1 { get; set; }
        public bool UrbanInterUrban { get; set; }
        public bool IsAvailable1 { get; set; }
    }
}
