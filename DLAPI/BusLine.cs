using System;

namespace DO
{
    /// <summary>
    /// Bus line department.
    /// </summary>
    public enum Area1 { ירושלים, גליל, השרון, שפלה, אילת, צפון, דרום, מרכז };
    public enum Urban { Urban, NotUrban };
    public enum Available { Available, Notavailable };
    public class BusLine
    {
        public int BusLineID1 { get; set; }
        public int LineNumber { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public Area1 AreaBusUrban { get; set; }
        public Urban GetUrban { get; set; }
        public Available GetAvailable { get; set; }
        public TimeSpan BeginningTime { get; set; }
        public TimeSpan EndTime { get; set; }
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
