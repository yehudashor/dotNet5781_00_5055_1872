using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;

namespace DO
{
    /// <summary>
    /// Line exit department. A class that holds the line times, 
    /// departure time departure time, exit end time, and line frequency at exit.
    /// </summary>
    public class LineExit
    {
        public int Id { get; set; }
        public int BusLineID1 { get; set; }
        public TimeSpan LineStartTime { get; set; }
        public TimeSpan LineFinishTime { get; set; }
        public TimeSpan LineFrequencyTime { get; set; }
        public int LineFrequency { get; set; }
        public override string ToString()
        {
            return ToStringProperty();
        }
        private string ToStringProperty()
        {
            throw new NotImplementedException();
        }
        //public DateTime Frequency { get; set; }
    }
}
