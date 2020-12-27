using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineExitBo
    {
        public int BusLineID2 { get; set; }
        public string LineStartTime { get; set; }
        //זמן סיום מחושב ע"פ זמן יציאה + תחנות קו וזמנים ביניהן
        public int LineFinishTime { get; set; }
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
