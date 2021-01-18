using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineExitBo
    {
        public int Id { get; set; }
        public int BusLineID1 { get; set; }
        public TimeSpan LineStartTime { get; set; }
        //זמן סיום מחושב ע"פ זמן יציאה + תחנות קו וזמנים ביניהן
        public TimeSpan LineFinishTime { get; set; }
        public TimeSpan LineFrequencyTime { get; set; }
        public int LineFrequency { get; set; }
        public List<TimeSpan> DepartureTimes = new List<TimeSpan>();
        public string NameOfLastStation { get; set; }
        public List<TimeSpan> TimeFinishTrval = new List<TimeSpan>();



        //public override string ToString()
        //{
        //    return ToStringProperty();
        //}

        //private string ToStringProperty()
        //{
        //    throw new NotImplementedException();
        //}
        //public DateTime Frequency { get; set; }
    }
}
