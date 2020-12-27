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
    public class BusTraveling
    {
        public int IdBusTraveling { get; set; }
        public string License_number1 { get; set; }
        public int LineInExecution { get; set; }
        public string LeavingTime { get; set; }
        public DateTime ActualDepartureTime { get; set; }

        public int LastStopNumber { get; set; }
        public DateTime TransitTimeAt_TheLastStop { get; set; }
        public DateTime ArrivalAtTheNextStation { get; set; }

        public string DriverName { get; set; }
        public int DriverID { get; set; }
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
