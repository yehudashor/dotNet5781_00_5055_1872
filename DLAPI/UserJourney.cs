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
    public class UserJourney
    {
        public string U_Username1 { get; set; }
        public string U_LineID { get; set; }
        public int U_UpStationID { get; set; }
        public int U_DownStationID { get; set; }
        public DateTime U_UpStationTimeID { get; set; }
        public DateTime U_DownStationTimeID { get; set; }
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
