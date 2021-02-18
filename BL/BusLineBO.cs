using System;
using System.Collections.Generic;

namespace BO
{
    /// <summary>
    /// A line department representing a bus line.
    /// And includes a list of stations of the line built by a query from line stations in DL, 
    /// and information about the times and distances between them that also comes from tracking stations in DL.
    /// In addition holds a list of daily schedule of line times.
    /// </summary>

    public enum Area1 { ירושלים, גליל, השרון, שפלה, אילת, צפון, דרום, מרכז };
    public enum Urban { Urban, NotUrban };
    public enum Available { Available, Notavailable };
    public class BusLineBO
    {
        /// <summary>
        /// The list of stations on the line includes times and distances between them.
        /// </summary>
        public List<LineExitBo> LineExitBos1 { get; set; }

        /// <summary>
        /// The schedule of the line is divided by frequencies.
        /// </summary>
        public List<StationLineBO> StationLineBOs { get; set; }
        public int BusLineID1 { get; set; }
        public int LineNumber { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public Area1 AreaBusUrban { get; set; }
        public Urban GetUrban { get; set; }
        public Available GetAvailable { get; set; }
        /// <summary>
        /// Time of departure of the first line of the day.
        /// </summary>
        public TimeSpan BeginningTime { get; set; }
        /// <summary>
        /// Last departure time for the last line of the day.
        /// </summary>
        public TimeSpan EndTime { get; set; }
    }
}