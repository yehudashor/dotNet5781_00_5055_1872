using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// A class that represents a single line port and calculates the flats for display 
    /// to the user by querying what dl and calculating the data
    /// in addition calculates the arrival times to the destination of each line port.
    /// </summary>
    public class LineExitBo
    {
        public int Id { get; set; }
        public int BusLineID1 { get; set; }
        public int LineFrequency { get; set; }
        public string NameOfLastStation { get; set; }
        public TimeSpan LineStartTime { get; set; }
        public TimeSpan LineFinishTime { get; set; }
        public TimeSpan LineFrequencyTime { get; set; }
        /// <summary>
        /// Frequency of the line to the current port.
        /// </summary>
        public List<TimeSpan> DepartureTimes = new List<TimeSpan>();
        /// <summary>
        /// Arrivals at the current departure point.
        /// </summary>
        public List<TimeSpan> TimeFinishTrval = new List<TimeSpan>();
    }
}
