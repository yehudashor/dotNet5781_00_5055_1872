using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI.PO
{
    public class LineExitPO : DependencyObject
    {
        public int Id { get; set; }
        public int BusLineID1 { get; set; }
        public TimeSpan LineStartTime { get; set; }
        public TimeSpan LineFinishTime { get; set; }
        public TimeSpan LineFrequencyTime { get; set; }
        public int LineFrequency { get; set; }
        public List<TimeSpan> DepartureTimes = new List<TimeSpan>();
        public string NameOfLastStation { get; set; }
        public List<TimeSpan> TimeFinishTrval = new List<TimeSpan>();
    }
}
