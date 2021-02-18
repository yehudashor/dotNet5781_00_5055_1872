using System;

namespace DO
{
    /// <summary>
    /// Line Station Department. Which connects a physical station to a line by - the line number + the station number.
    /// And in addition holds the location of the station on the line.
    /// </summary>
    public class LineStation
    {
        public int BusLineID2 { get; set; }
        public int StationNumberOnLine { get; set; }
        public int LocationNumberOnLine { get; set; }
        public bool ChackDelete2 { get; set; }
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
