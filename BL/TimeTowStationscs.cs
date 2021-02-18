using System;

namespace BL
{
    /// <summary>
    /// Time of arrival of a specific line to the station. 
    /// (Unlike a line ride whose name is the general arrival time of the lines to the station).
    /// </summary>
    public class TimeTowStations
    {
        public int LineNumber { get; set; }
        public TimeSpan TimeStations { get; set; }
    }
}
