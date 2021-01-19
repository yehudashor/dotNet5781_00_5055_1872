using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    //public enum Area { North, South, Center };
    public enum Area1 { Jerusalem, Galil, Hasharon, Shefela, Eilat, North, South, Center };
    public enum Urban { Urban, NotUrban };
    public enum Available { Available, Notavailable };
    public class BusLineBO
    {
        public List<LineExitBo> LineExitBos1 { get; set; }
        public List<StationLineBO> StationLineBOs { get; set; }
        public int BusLineID1 { get; set; }
        public int LineNumber { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public Area1 AreaBusUrban { get; set; }
        public Urban GetUrban { get; set; }
        public Available GetAvailable { get; set; }
        public TimeSpan BeginningTime { get; set; }
        public TimeSpan EndTime { get; set; }
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
