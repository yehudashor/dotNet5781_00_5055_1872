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
    public class LineStation
    {
        public int BusLineID2 { get; set; }
        public int StationNumberOnLine { get; set; }
        public int LocationNumberOnLine { get; set; }
        public bool ChackDelete2 { get; set; }
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
