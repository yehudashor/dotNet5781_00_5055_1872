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
    public enum TravelMode
    {
        ReadyToGo,
        InMiddleOfTrip,
        InTreatment,
        OnRefueling,
    }
    public class Bus
    {
        public string License_number { get; set; }
        public int KmForTreatment { get; set; }
        public DateTime StartDate { get; set; }
        public int KmOfTreatment { get; set; }

        public int KmForRefueling { get; set; }
        public TravelMode Status { get; set; }

        public int TotalMiles { get; set; }
        /// <summary>
        /// kmForAllBuses = A personal addition that sums up the overall mileage of all buses For future optional use
        /// </summary>
        public static int kmForAllBuses = 0;

        public DateTime DayOfTreatment { get; set; }

        public bool IsAvailable { get; set; }

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
