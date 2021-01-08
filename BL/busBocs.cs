using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusBO
    {
        /// <summary>
        /// TravelMode by
        /// </summary>
        /// 
        public enum TravelMode
        {
            ReadyToGo,
            InMiddleOfTrip,
            InTreatment,
            OnRefueling,
        }
        public string License_number { get; set; }

        public DateTime StartDate { get; set; }
        public int KmOfTreatment { get; set; }

        public int KmForRefueling { get; set; }
        public TravelMode Status { get; set; }
        public int KmForTreatment { get; set; }
        public int TotalMiles { get; set; }

        public static int kmForAllBuses = 0;
        public DateTime DayOfTreatment { get; set; }

        public bool IsAvailable { get; set; }
    }
}
