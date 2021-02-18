using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using BO;
using DO;

namespace BL
{
    internal class InformationBusLineBl : IBL1
    {
        /// <summary>
        /// Factory SINGLETON Design Templates.
        /// </summary>
        private readonly IDAL dl = DLFactory.GetDL();

        #region LineTrip
        public IEnumerable<string> TimeCamingToCurrnetStation(int LineNumber, int NumberStation)
        {
            try
            {
                /// A list of line ports that includes multiple ports. 
                /// Each object in the list contains a start time, and an end time of a particular exit. 
                /// And the frequency of the line at the exit.
                List<LineExit> lineExits = dl.LineExitList(LineNumber).ToList();

                /// A list that contains all the line ports throughout the day with no division into ports.
                List<TimeSpan> timeSpans = new List<TimeSpan>();

                ///Arranging the times at the exit.
                for (int j = 0; j < lineExits.Count; j++)
                {
                    for (TimeSpan time = lineExits[j].LineStartTime; time < lineExits[j].LineFinishTime; time += lineExits[j].LineFrequencyTime)
                    {
                        timeSpans.Add(time);
                    }
                }

                /// A list of all line stations is arranged in order on the route
                List<LineStation> lineStations = dl.OneLineFromList(t => t.BusLineID2 == LineNumber && t.ChackDelete2).OrderBy(p => p.LocationNumberOnLine).ToList();
                ///Finding the index of the requested station to which we want to know the time of arrival of the line.
                int temp = lineStations.FindIndex(item => item.StationNumberOnLine == NumberStation);
                ///Time of arrival of the line to the above station from the moment of departure from the departure station.
                TimeSpan timeSpan = new TimeSpan();
                for (int j = 0; j < temp; j++)
                {
                    timeSpan += dl.AverageTimeBetweenTooStationsList(lineStations[j].StationNumberOnLine, lineStations[j + 1].StationNumberOnLine);
                }

                ///List of arrival times of the above line to the above station. This is a list because of course there can be several buses from the above line on the way to the station.
                List<TimeSpan> timeS = new List<TimeSpan>();

                ///Current time
                TimeSpan timeSpan1 = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                ///Final calculation of the time of arrival at the station of all the above lines, and only the relevant ones that have already left the route and have not yet reached the above station.
                for (int i = 0; i < timeSpans.Count; i++)
                {
                    if (timeSpans[i] + timeSpan > timeSpan1 && timeSpan1 > timeSpans[i])
                    {
                        timeS.Add(timeSpans[i] + timeSpan - timeSpan1);
                    }
                }

                /// Final calculation of the time of arrival at the station of all the above lines, and only the relevant ones that have already left the route and have not yet reached the above station.
                List<string> timeS1 = new List<string>();
                timeS1 = timeS.ConvertAll(f4);
                return timeS1;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string f4(TimeSpan timeSpan)
        {
            double i = Math.Round(timeSpan.TotalMinutes, MidpointRounding.AwayFromZero);
            return i.ToString() + "\t" + "דקות";
        }

        public TimeSpan TravelTimeBetweenTwoStations(int LineNumber, int NumberStation1, int NumberStation2)
        {
            TimeSpan timeSpan = new TimeSpan();
            /// A list of all line stations is arranged in order on the route
            List<LineStation> lineStations = dl.OneLineFromList(item => item.BusLineID2 == LineNumber).ToList();
            //Location of the first stop on the route.
            int index = lineStations.FindIndex(item => item.StationNumberOnLine == NumberStation1);
            ///Location of the second station on the route.
            int index1 = lineStations.FindIndex(item => item.StationNumberOnLine == NumberStation2);

            ///Travel time between stations.
            if (index > index1)
            {
                for (int i = index; i < index1; i++)
                {
                    timeSpan += dl.AverageTimeBetweenTooStationsList(lineStations[i].StationNumberOnLine, lineStations[i + 1].StationNumberOnLine);
                }
            }
            return timeSpan;
        }

        public IEnumerable<LineTrip> LineTrips(int NumberStation, TimeSpan Time, int numberStationDestination = 0)
        {
            ///Identifying numbers of all the lines that pass through the above station.
            List<int> lineStations = dl.LinesFromList(NumberStation).ToList();

            /// Bring all the line numbers and name the last stop on the lines according to a query from the identifying numbers.
            List<LineExitBo> lineExits1 = new List<LineExitBo>();
            var busLines = from line in lineStations
                           let p = dl.ReturnBusLine(line)
                           let k = dl.ReturnStation(p.LastStation)
                           select new { nubmerLine = p.LineNumber, lastStation = k.NameOfStation };
            foreach (var item in busLines)
            {
                lineExits1.Add(new LineExitBo { BusLineID1 = item.nubmerLine, NameOfLastStation = item.lastStation });
            }

            ///Arranging the departures of the above line according to frequencies.
            List<LineExit> lineExits = new List<LineExit>();
            TimeSpan time = new TimeSpan();
            for (int i = 0; i < lineStations.Count; i++)
            {
                lineExits = dl.LineExitList(lineStations[i]).ToList();
                for (int j = 0; j < lineExits.Count; j++)
                {
                    for (time = lineExits[j].LineStartTime; time < lineExits[j].LineFinishTime; time += lineExits[j].LineFrequencyTime)
                    {
                        lineExits1[i].DepartureTimes.Add(time);
                    }
                }
            }

            ///List of above line stations.
            List<LineStation> lineStations1 = new List<LineStation>();

            ///Travel time from the first station to the destination station.
            TimeSpan timeSpan = new TimeSpan();

            ///Location of the station on the route.
            int temp = 0;
            ///Travel time to final station from destination station.
            TimeSpan timeSpan2 = new TimeSpan();

            ///The list that returns from the function with the numbers of lines that pass through the station,
            ///the names of their last stations,
            ///times of arrival at the station, 
            ///times of departure, and times of arrival at the destination.
            List<LineTrip> lineTrip = new List<LineTrip>();

            for (int i = 0; i < lineStations.Count; i++)
            {
                lineStations1 = dl.OneLineFromList(item => item.BusLineID2 == lineStations[i] && item.ChackDelete2).OrderBy(item => item.LocationNumberOnLine).ToList();
                temp = lineStations1.FindIndex(item => item.StationNumberOnLine == NumberStation);

                for (int j = 0; j < temp; j++)
                {
                    timeSpan += dl.AverageTimeBetweenTooStationsList(lineStations1[j].StationNumberOnLine, lineStations1[j + 1].StationNumberOnLine);
                }

                for (int t = temp; t < lineStations1.Count - 1; t++)
                {
                    timeSpan2 += dl.AverageTimeBetweenTooStationsList(lineStations1[t].StationNumberOnLine, lineStations1[t + 1].StationNumberOnLine);
                }

                for (int p = 0; p < lineExits1[i].DepartureTimes.Count; p++)
                {
                    if (lineExits1[i].DepartureTimes[p] + timeSpan > Time && Time > lineExits1[i].DepartureTimes[p])
                    {
                        lineTrip.Add(new LineTrip { StartTrip = lineExits1[i].DepartureTimes[p], LineNumber = lineExits1[i].BusLineID1, NameOfLastStation = lineExits1[i].NameOfLastStation, TimeCameToStation = lineExits1[i].DepartureTimes[p] + timeSpan - Time, TimeFromStationToDestination = lineExits1[i].DepartureTimes[p] + timeSpan + timeSpan2 });
                    }
                }
                timeSpan = TimeSpan.Zero;
                timeSpan2 = TimeSpan.Zero;
            }
            IEnumerable<LineTrip> lineTrips = lineTrip;
            return lineTrips;

            //   timeSpan1 = TimeSpan.Zero;
            // TimeSpan timeSpan1 = new TimeSpan();
            //  temp1 = lineStations1.FindIndex(item => item.StationNumberOnLine == numberStationDestination);
            //if (numberStationDestination != 0 && temp1 != -1 && temp1 > temp)
            //{
            //    for (int t = temp; t < temp1; t++)
            //    {
            //        timeSpan1 += dl.AverageTimeBetweenTooStationsList(lineStations1[t].StationNumberOnLine, lineStations1[t + 1].StationNumberOnLine);
            //    }
            //}
        }

        //IEnumerable<TimeTowStations> IBL1.Timetow(int numberStation1, int numberStation2, TimeSpan time)
        //{
        //    try
        //    {
        //        List<int> lineStations = dl.LinesFromList(numberStation1).ToList();
        //        List<int> lineStations1 = dl.LinesFromList(numberStation2).ToList();
        //        List<int> vs = new List<int>();

        //        for (int i = 0; i < lineStations.Count; i++)
        //        {
        //            for (int j = 0; j < lineStations1.Count; i++)
        //            {
        //                if (lineStations[i] == lineStations1[j])
        //                {
        //                    vs.Add(lineStations[i]);
        //                }
        //            }
        //        }

        //        List<LineStation> lineStations2 = new List<LineStation>();
        //        for (int i = 0; i < vs.Count; i++)
        //        {
        //            lineStations2 = dl.OneLineFromList(item => item.BusLineID2 == vs[i]).ToList();
        //            int index = lineStations2.FindIndex(item => item.StationNumberOnLine == numberStation1);
        //            int index1 = lineStations2.FindIndex(item => item.StationNumberOnLine == numberStation2);
        //            if (index != -1 && index1 != -1)
        //            {
        //                for (int j = index; j < index1; i++)
        //                {

        //                }
        //            }
        //        }


        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        #endregion LineTrip

        #region BusLineStation
        public void AddStationToLine(StationLineBO stationLineBO, int NumberStation1, int NumberStation2, TimeSpan timeSpan, float distance)
        {
            try
            {
                ///Calling all the stations' operating line of the line and arranging them by location.
                List<LineStation> lineStations = dl.OneLineFromList(item => item.BusLineID2 == stationLineBO.BusLineID2).ToList();

                ///Go over all the existing stations,
                ///and promote the location of all the stations that are in the location of the station that should be added one ahead. 
                List<LineStation> lineStations1 = new List<LineStation>();
                LineStation item = new LineStation();
                foreach (LineStation item1 in lineStations)
                {
                    item = item1;
                    if (item.LocationNumberOnLine >= stationLineBO.LocationNumberOnLine)
                    {
                        ++item.LocationNumberOnLine;
                    }
                    lineStations1.Add(item);
                }

                ///Deleting the existing stations from the DL line and reinserting them in the correct order.
                ///And it is designed to allow insertion of the new station to the correct location on the line.
                dl.DeleteLineStation1(stationLineBO.BusLineID2);
                foreach (LineStation item2 in lineStations1)
                {
                    dl.AddLineStation(item2);
                }

                ///Adding the new station
                LineStation lineStation1 = new LineStation
                {
                    BusLineID2 = stationLineBO.BusLineID2,
                    StationNumberOnLine = stationLineBO.StationNumberOnLine,
                    LocationNumberOnLine = stationLineBO.LocationNumberOnLine,
                    ChackDelete2 = stationLineBO.ChackDelete2
                };
                dl.AddLineStation(lineStation1);

                ///Check if there are no such consecutive stations already 
                ///[referring to the station before the station that entered with the new station and the new station with the next station after it] 
                ///if they do not exist then they are added.
                if (dl.ChackExistingConsecutiveStations(item => item.StationNumber1 == NumberStation1 && item.StationNumber2 == stationLineBO.StationNumberOnLine))
                {
                    ConsecutiveStations consecutiveStations = new ConsecutiveStations
                    {
                        StationNumber1 = NumberStation1,
                        StationNumber2 = stationLineBO.StationNumberOnLine,
                        DistanceBetweenTooStations = stationLineBO.DistanceBetweenTooStations,
                        AverageTime = stationLineBO.AverageTime
                    };
                    dl.AddConsecutiveStations(consecutiveStations);
                }

                if (dl.ChackExistingConsecutiveStations(item => item.StationNumber1 == stationLineBO.StationNumberOnLine && item.StationNumber2 == NumberStation2))
                {
                    ConsecutiveStations consecutiveStations = new ConsecutiveStations
                    {
                        StationNumber1 = stationLineBO.StationNumberOnLine,
                        StationNumber2 = NumberStation2,
                        DistanceBetweenTooStations = distance,
                        AverageTime = timeSpan
                    };
                    dl.AddConsecutiveStations(consecutiveStations);
                }
            }
            catch (ExceptionLineStation ex)
            {
                throw new BOExceptionLineStation("Error", ex);
            }
        }
        public ConsecutiveStationsBo DeleteStationFromLine(int IdLine, int NumberStation)
        {
            try
            {
                ///Receipt of all the above line stations in order on the route.
                List<LineStation> lineStations = dl.OneLineFromList(S => S.BusLineID2 == IdLine && S.ChackDelete2).ToList();
                ///Deleting all stations on the DL line for re-insertion after adding 
                ///a new object of consecutive stations and arranging their locations.
                dl.DeleteLineStation1(IdLine);

                ///Add the station we want to delete and then delete it in the form of a field that marks it as deleted.
                LineStation lineStation = lineStations.Find(i => i.BusLineID2 == IdLine && i.StationNumberOnLine == NumberStation);
                if (lineStation != null)
                {
                    dl.AddLineStation(lineStation);
                    dl.DeleteOneLineStation(lineStation.BusLineID2, lineStation.StationNumberOnLine);
                    /// Add a new object of consecutive stations that were on the route before the deleted station and after the deleted station.
                    ConsecutiveStationsBo consecutiveStations = new ConsecutiveStationsBo();

                    /// ///Arrange the indexes and add them back to DL with the object of the following stations.
                    foreach (LineStation item in lineStations.Where(i => i.StationNumberOnLine != NumberStation))
                    {
                        if (lineStation.LocationNumberOnLine != 0 && lineStation.LocationNumberOnLine != lineStations[lineStations.Count - 1].LocationNumberOnLine)
                        {
                            if (item.LocationNumberOnLine == lineStation.LocationNumberOnLine - 1)
                            {
                                consecutiveStations.StationNumber1 = item.StationNumberOnLine;
                            }
                            if (item.LocationNumberOnLine == lineStation.LocationNumberOnLine + 1)
                            {
                                consecutiveStations.StationNumber2 = item.StationNumberOnLine;
                            }
                        }
                        if (item.LocationNumberOnLine > lineStation.LocationNumberOnLine)
                        {
                            --item.LocationNumberOnLine;
                        }
                        dl.AddLineStation(item);
                    }

                    if (consecutiveStations.StationNumber1 != 0 && consecutiveStations.StationNumber2 != 0)
                    {
                        if (dl.ChackExistingConsecutiveStations(item => item.StationNumber1 == consecutiveStations.StationNumber1
                            && item.StationNumber2 == consecutiveStations.StationNumber2))
                        {
                            consecutiveStations.DistanceBetweenTooStations = (float)((float)(D.NextDouble() * (4.6 - 1)) + 1);
                            consecutiveStations.AverageTime = new TimeSpan(0, D.Next(1, 7), D.Next(0, 60));
                            ConsecutiveStations consecutiveStations1 = new ConsecutiveStations();
                            consecutiveStations.CopyPropertiesTo(consecutiveStations1);
                            dl.AddConsecutiveStations(consecutiveStations1);
                        }
                        return consecutiveStations;
                    }
                }
                return null;
            }
            catch (ExceptionLineStation ex)
            {
                throw new BOExceptionLineStation("Error!!!", ex);
            }
        }
        public IEnumerable<StationLineBO> ReturnLineStationList(int LineNumber)
        {
            try
            {
                ///Return all stations of the above line.
                IEnumerable<LineStation> stationLines = dl.OneLineFromList(item => item.BusLineID2 == LineNumber && item.ChackDelete2);
                List<StationLineBO> stationLineBOs1 = new List<StationLineBO>();
                stationLineBOs1 = (from line in stationLines
                                   orderby line.LocationNumberOnLine
                                   let p = ReturnStationLine(LineNumber, line.StationNumberOnLine)
                                   select p).ToList();

                ///Adding times and distances.
                for (int i = 0; i < stationLineBOs1.Count - 1; i++)
                {
                    ConsecutiveStations consecutiveStations1 = dl.ReturnConsecutiveStation(stationLineBOs1[i].StationNumberOnLine, stationLineBOs1[i + 1].StationNumberOnLine);
                    stationLineBOs1[i].DistanceBetweenTooStations = consecutiveStations1.DistanceBetweenTooStations;
                    stationLineBOs1[i].AverageTime = consecutiveStations1.AverageTime;
                }

                ///Adding station names
                for (int i = 0; i < stationLineBOs1.Count; i++)
                {
                    BusStation busStation = new BusStation();
                    busStation = dl.ReturnStation(stationLineBOs1[i].StationNumberOnLine);
                    stationLineBOs1[i].NameOfStation = busStation.NameOfStation;
                }
                return stationLineBOs1;
            }
            catch (ExceptionLineStation ex)
            {
                throw new BOExceptionLineStation("Error", ex);
            }
            catch (ExceptionConsecutiveStations ex)
            {
                throw new BOExceptionConsecutiveStations("Error", ex);
            }
        }
        public StationLineBO ReturnStationLine(int LineNumber, int numberStation)
        {
            try
            {
                ///Returns a single object from a line station.
                StationLineBO stationLineBO = new StationLineBO();
                BusStation busStation = dl.ReturnStation(numberStation);
                LineStation lineStation = dl.ReturnLineStation(LineNumber, numberStation);
                lineStation.CopyPropertiesTo(stationLineBO);
                stationLineBO.NameOfStation = busStation.NameOfStation;
                return stationLineBO;
            }
            catch (ExceptionLineStation ex)
            {
                throw new BOExceptionLineStation("Error!!!", ex);
            }
        }
        public static LineStation ReturnNewLineStationDoFromBo(StationLineBO func1)
        {
            LineStation lineStation = new LineStation
            {
                BusLineID2 = func1.BusLineID2,
                StationNumberOnLine = func1.StationNumberOnLine,
                LocationNumberOnLine = func1.LocationNumberOnLine,
                ChackDelete2 = func1.ChackDelete2
            };
            return lineStation;
        }
        public IEnumerable<BusLineBO> LinePastInStationBOs(int NumberStation)
        {
            try
            {
                /// List of identifying numbers of lines passing through the above station.
                IEnumerable<int> busLine = dl.LinesFromList(NumberStation);

                ///Data for these lines.
                IEnumerable<BusLine> busLine1 = from line in busLine
                                                let p = dl.ReturnBusLine(line)
                                                select p;
                ///Copy their data to BO lines.
                IEnumerable<BusLineBO> busLineBOs = from line in busLine1
                                                    let p = func(line)
                                                    orderby line.LineNumber
                                                    select p;

                ///Arrange the lines that the station passes into groups with the key identifier being the line area.
                var o = from line in busLineBOs
                        group line by line.AreaBusUrban into NewGroup
                        select new { g = NewGroup };
                IEnumerable<BusLineBO> busLineBOs1 = from l in o
                                                     from m in l.g
                                                     select m;

                ///A function that accepts a DO object and converts it to BO.
                static BusLineBO func(BusLine item)
                {
                    BusLineBO busLineBO1 = new BusLineBO
                    {
                        BusLineID1 = item.BusLineID1,
                        LineNumber = item.LineNumber,
                        AreaBusUrban = (BO.Area1)(DO.Area1)(int)item.AreaBusUrban,
                        GetAvailable = (BO.Available)(DO.Available)(int)item.GetAvailable,
                        FirstStation = item.FirstStation,
                        LastStation = item.LastStation,
                        GetUrban = (BO.Urban)(DO.Urban)(int)item.GetUrban
                    };
                    return busLineBO1;
                }

                return busLineBOs1;
            }
            catch (ExceptionLine ex)
            {
                throw new BOExceptionLine("Error", ex);
            }
        }
        public BusLineBO LineInformation(int numberLine)
        {
            BusLineBO busLineBO = new BusLineBO();
            try
            {
                /// Bring the line information from the DL according to the line ID. And copying its fields to the BO line object.
                BusLine busLine = dl.ReturnBusLine(numberLine);
                busLine.CopyPropertiesTo(busLineBO);

                /// List of stations of the line.
                busLineBO.StationLineBOs = new List<StationLineBO>();
                if (busLineBO.GetAvailable == BO.Available.Available)
                {
                    ///Query to the stations of the available line and their arrangement by location.
                    IEnumerable<LineStation> lineStations = dl.OneLineFromList(line1 => line1.ChackDelete2 && line1.BusLineID2 == numberLine);
                    IEnumerable<LineStation> lineStations10 = from line in lineStations
                                                              orderby line.LocationNumberOnLine
                                                              select line;

                    ///Bringing the physical stations of the line for the purpose of copying the names of the stations to the above line stations. 
                    ///And copying their fields to the physical station object in BO.
                    IEnumerable<BusStation> busStationBOs = from busLine5 in lineStations10
                                                            let p = dl.ReturnStation(busLine5.StationNumberOnLine)
                                                            select p;
                    List<BusStationBO> busStationBOs1 = (from busLine5 in busStationBOs
                                                         let p = function1(busLine5)
                                                         select p).ToList();

                    ///List of stations of BO line station object.
                    busLineBO.StationLineBOs = (from line in lineStations10
                                                let p = function2(line)
                                                select p).ToList();

                    ///Adding the names of their stations.
                    for (int i = 0; i < busLineBO.StationLineBOs.Count; i++)
                    {
                        busLineBO.StationLineBOs[i].NameOfStation = busStationBOs1[i].NameOfStation;
                    }

                    ///Adding times and distances between stations.
                    for (int i = 0; i < busLineBO.StationLineBOs.Count - 1; i++)
                    {
                        ConsecutiveStations consecutiveStations = new ConsecutiveStations();
                        consecutiveStations = dl.ReturnConsecutiveStation(busLineBO.StationLineBOs[i].StationNumberOnLine, busLineBO.StationLineBOs[i + 1].StationNumberOnLine);
                        busLineBO.StationLineBOs[i].AverageTime = consecutiveStations.AverageTime;
                        busLineBO.StationLineBOs[i].DistanceBetweenTooStations = consecutiveStations.DistanceBetweenTooStations;
                    }

                    static BusStationBO function1(BusStation item)
                    {
                        BusStationBO busStationBO = new BusStationBO
                        {
                            StationNumber = item.StationNumber,
                            StationAddress = item.StationAddress,
                            NameOfStation = item.NameOfStation,
                            RoofToTheStation = item.RoofToTheStation,
                            Longitude = item.Longitude,
                            Latitude = item.Latitude,
                            IsAvailable3 = item.IsAvailable3,
                            AccessForDisabled = item.AccessForDisabled
                        };
                        return busStationBO;
                    }

                    static StationLineBO function2(LineStation item)
                    {
                        StationLineBO stationLineBO = new StationLineBO
                        {
                            BusLineID2 = item.BusLineID2,
                            StationNumberOnLine = item.StationNumberOnLine,
                            ChackDelete2 = item.ChackDelete2,
                            LocationNumberOnLine = item.LocationNumberOnLine
                        };
                        return stationLineBO;
                    }

                    ///Line Schedule Each object in the list contains travel start time, end time and frequency.
                    busLineBO.LineExitBos1 = ShowlineExit(busLineBO.BusLineID1).ToList();

                    ///Travel time of the line route.
                    TimeSpan timeSpan = new TimeSpan(busLineBO.StationLineBOs.Sum(item => item.AverageTime.Hours),
                        busLineBO.StationLineBOs.Sum(item => item.AverageTime.Minutes),
                        busLineBO.StationLineBOs.Sum(item => item.AverageTime.Seconds));

                    ///Arranging the frequencies for the user display and for each journey Departure time and time of arrival of the line at the destination at the above port.
                    for (int i = 0; i < busLineBO.LineExitBos1.Count; i++)
                    {
                        for (int j = 0; j <= busLineBO.LineExitBos1[i].LineFrequency; j++)
                        {
                            busLineBO.LineExitBos1[i].DepartureTimes.Add(busLineBO.LineExitBos1[i].LineStartTime);
                            busLineBO.LineExitBos1[i].TimeFinishTrval.Add(busLineBO.LineExitBos1[i].LineStartTime + timeSpan);
                            for (int k = 0; k < j; k++)
                            {
                                if (busLineBO.LineExitBos1[i].DepartureTimes[j] + busLineBO.LineExitBos1[i].LineFrequencyTime > busLineBO.LineExitBos1[i].LineFinishTime)
                                {
                                    break;
                                }
                                busLineBO.LineExitBos1[i].DepartureTimes[j] += busLineBO.LineExitBos1[i].LineFrequencyTime;
                                busLineBO.LineExitBos1[i].TimeFinishTrval[j] += busLineBO.LineExitBos1[i].LineFrequencyTime;
                            }
                        }
                    }

                    ///Departure and end time of the first and last line.
                    if (busLineBO.LineExitBos1.Count > 0)
                    {
                        busLineBO.BeginningTime = busLineBO.LineExitBos1[0].DepartureTimes[0];
                        busLineBO.EndTime = busLineBO.LineExitBos1[busLineBO.LineExitBos1.Count - 1].DepartureTimes[busLineBO.LineExitBos1[busLineBO.LineExitBos1.Count - 1].DepartureTimes.Count - 1];
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (ExceptionLineStation ex)
            {
                throw new BOExceptionLineStation("Error", ex);
            }
            catch (ExceptionLine ex)
            {
                throw new BOExceptionLine("Error", ex);
            }
            catch (ExceptionConsecutiveStations ex)
            {
                throw new BOExceptionConsecutiveStations("Error", ex);
            }
            return busLineBO;
        }
        #endregion BusLineStation

        #region Stations
        public IEnumerable<BusStationBO> ShowStation()
        {
            try
            {
                ///Display of the physical stations.
                IEnumerable<BusStation> busStations = dl.StationList();
                static BusStationBO function1(BusStation item)
                {
                    BusStationBO busStationBO = new BusStationBO
                    {
                        StationNumber = item.StationNumber,
                        StationAddress = item.StationAddress,
                        NameOfStation = item.NameOfStation,
                        RoofToTheStation = item.RoofToTheStation,
                        Longitude = item.Longitude,
                        Latitude = item.Latitude,
                        IsAvailable3 = item.IsAvailable3,
                        AccessForDisabled = item.AccessForDisabled
                    };
                    return busStationBO;
                }
                IEnumerable<BusStationBO> busStationBOs = from station in busStations
                                                          let p = function1(station)
                                                          select p;
                return busStationBOs;
            }
            catch (ExceptionStation ex)
            {
                throw new BOExceptionStation("Error", ex);
            }
        }
        public void ChackStation(ref BusStationBO busStationBO)
        {
            ///Determine longitude and latitude at random.
            busStationBO.Latitude = (float)((float)(D.NextDouble() * (33.3 - 31)) + 31);
            busStationBO.Longitude = (float)((float)(D.NextDouble() * (35.5 - 34.3)) + 34.3);
        }
        public void AddStationToDo(BusStationBO busStationBO)
        {
            try
            {
                /// Adding a new station.
                ChackStation(ref busStationBO);
                BusStation busStation = new BusStation
                {
                    StationNumber = busStationBO.StationNumber,
                    NameOfStation = busStationBO.NameOfStation,
                    StationAddress = busStationBO.StationAddress,
                    Latitude = busStationBO.Latitude,
                    Longitude = busStationBO.Longitude,
                    AccessForDisabled = busStationBO.AccessForDisabled,
                    RoofToTheStation = busStationBO.RoofToTheStation,
                    IsAvailable3 = busStationBO.IsAvailable3
                };
                dl.AddStation(busStation);
            }
            catch (ExceptionStation ex)
            {
                throw new BOExceptionStation("Rong Station!!!", ex);
            }
        }
        public void DeleteStationFromDo(int numberOfStation)
        {
            try
            {
                dl.DeleteStation(numberOfStation);
                ///Deleting the station from all line stations.
                ///Of course all the functionality of deleting a station from a line is done here.
                IEnumerable<int> busLine = dl.LinesFromList(numberOfStation);
                foreach (int item in busLine)
                {
                    _ = DeleteStationFromLine(item, numberOfStation);
                }
                ///Deleting the station from the following stations.
                foreach (ConsecutiveStations item in dl.ConsecutiveStationsListPredicate(item => item.StationNumber1 == numberOfStation || item.StationNumber2 == numberOfStation))
                {
                    dl.DeleteConsecutiveStations(item.StationNumber1, item.StationNumber2);
                }
            }
            catch (ExceptionStation ex)
            {
                throw new BOExceptionStation("Error", ex);
            }
            catch (ExceptionConsecutiveStations ex)
            {
                throw new BOExceptionConsecutiveStations("Error", ex);
            }
            catch (ExceptionLineStation ex)
            {
                throw new BOExceptionLineStation("Error", ex);
            }
        }
        public BusStationBO ReturnStationToPL(int numberOfStation)
        {
            try
            {
                BusStationBO busStationBO = new BusStationBO();
                BusStation busStation = dl.ReturnStation(numberOfStation);
                busStation.CopyPropertiesTo(busStationBO);
                return busStationBO;
            }
            catch (ExceptionStation ex)
            {
                throw new BOExceptionStation("rong station", ex);
            }
        }
        #endregion Stations

        #region BusLine
        public void AddBusLineBO(BusLineBO busLineBO)
        {
            try
            {
                /// Checking that we chose for the new line two stations.
                if (busLineBO.StationLineBOs.Count >= 2)
                {
                    BusLine busLine = new BusLine();
                    busLineBO.FirstStation = busLineBO.StationLineBOs[0].StationNumberOnLine;
                    busLineBO.LastStation = busLineBO.StationLineBOs[busLineBO.StationLineBOs.Count - 1].StationNumberOnLine;

                    ///Copy the fields of the line information only to the DO object and add to DL.
                    ///The insert function returns a runner number that will be used as the line ID number.
                    busLineBO.CopyPropertiesTo(busLine);
                    busLineBO.BusLineID1 = dl.AddBusLine(busLine);

                    ///Create line station objects with the line ID number and insert them into the DL.
                    for (int i = 0; i < busLineBO.StationLineBOs.Count; i++)
                    {
                        busLineBO.StationLineBOs[i].ChackDelete2 = true;
                        busLineBO.StationLineBOs[i].LocationNumberOnLine = i;
                        busLineBO.StationLineBOs[i].BusLineID2 = busLineBO.BusLineID1;
                    }
                    IEnumerable<LineStation> s = from AddLineStationToDL in busLineBO.StationLineBOs
                                                 let p = ReturnNewLineStationDoFromBo(AddLineStationToDL)
                                                 select p;
                    foreach (LineStation item in s)
                    {
                        item.BusLineID2 = busLineBO.BusLineID1;
                        dl.AddLineStation(item);
                    }

                    ///Create objects of successive stations.
                    for (int i = 0; i < busLineBO.StationLineBOs.Count - 1; i++)
                    {
                        if (dl.ChackExistingConsecutiveStations(item => item.StationNumber1 == busLineBO.StationLineBOs[i].StationNumberOnLine
                        && item.StationNumber2 == busLineBO.StationLineBOs[i + 1].StationNumberOnLine))
                        {
                            busLineBO.StationLineBOs[i].DistanceBetweenTooStations = (float)((float)(D.NextDouble() * (4.6 - 1)) + 1);
                            busLineBO.StationLineBOs[i].AverageTime = new TimeSpan(0, D.Next(1, 7), D.Next(0, 60)); ;
                            ConsecutiveStations consecutiveStations = new ConsecutiveStations
                            {
                                StationNumber1 = busLineBO.StationLineBOs[i].StationNumberOnLine,
                                StationNumber2 = busLineBO.StationLineBOs[i + 1].StationNumberOnLine,
                                DistanceBetweenTooStations = busLineBO.StationLineBOs[i].DistanceBetweenTooStations,
                                AverageTime = busLineBO.StationLineBOs[i].AverageTime
                            };
                            dl.AddConsecutiveStations(consecutiveStations);
                        }
                    }
                }

            }
            catch (ExceptionLine ex)
            {
                throw new BOExceptionLine("Error!!!", ex);
            }
            catch (ExceptionLineStation ex)
            {
                throw new BOExceptionLineStation("Error!!!", ex);
            }
        }
        public void DeleteBusLineBO(int NumberLine)
        {
            try
            {
                ///Line outputs for deleting them.
                IEnumerable<LineExit> lineExits = dl.LineExitList(NumberLine);
                ///Delete line information.
                dl.DeleteBusLine(NumberLine);
                ///Deleting the line station.
                dl.DeleteLineStation(NumberLine);
                ///Delete all line outputs (frequencies).
                foreach (LineExit item in lineExits)
                {
                    dl.DeleteLineExit(item.BusLineID1, item.LineStartTime);
                }
            }
            catch (ExceptionLine ex)
            {
                throw new BOExceptionLine("Error!!!", ex);
            }
            catch (ExceptionLineStation ex)
            {
                throw new BOExceptionLineStation("Error!!!", ex);
            }
        }
        public int ReturnBusLineIdFromDl()
        {
            try
            {
                int i = dl.BusLineId();
                return i;
            }
            catch (ExceptionLine ex)
            {
                throw new BOExceptionLine("Error!!!", ex);
            }
        }
        public void UdaptingLine(BusLineBO busLineBO)
        {
            try
            {
                BusLine busLine = new BusLine();
                busLineBO.CopyPropertiesTo(busLine);
                dl.UpdatingBusLine(busLine);
            }
            catch (ExceptionLine ex)
            {
                throw new BOExceptionLine("Error", ex);
            }
        }
        #endregion BusLine

        #region LineExit
        public void AddExitToLine(LineExitBo lineExitBo)
        {
            try
            {
                if (lineExitBo.LineFinishTime > lineExitBo.LineStartTime)
                {
                    ///Time which is the end time of departure less the start time for the purpose of checking that
                    ///the frequency of the line in the current port is contained inside the port.
                    TimeSpan timeSpan = lineExitBo.LineFinishTime - lineExitBo.LineStartTime;
                    if (timeSpan > lineExitBo.LineFrequencyTime)
                    {
                        for (TimeSpan i = lineExitBo.LineStartTime; i <= lineExitBo.LineFinishTime; i += lineExitBo.LineFrequencyTime)
                        {
                            lineExitBo.LineFrequency++;
                        }
                    }
                    else
                    {
                        //  throw new BOExceptionLineExit (lineExitBo.LineFrequencyTime.ToString(), "Error!!! bad Frequency Time")
                    }
                }
                else
                {
                    //throw new (Error!!! bad start time);
                }
                LineExit lineExit = new LineExit();
                lineExitBo.CopyPropertiesTo(lineExit);
                dl.AddLineExit(lineExit);
            }
            catch (ExceptionLineExit ex)
            {
                throw new BOExceptionLineExit("Error!!!", ex);
            }
        }
        public void DeleteLineExit(int numberOfLine, TimeSpan StartTime)
        {
            try
            {
                dl.DeleteLineExit(numberOfLine, StartTime);
            }
            catch (ExceptionLineExit ex)
            {
                throw new BOExceptionLineExit("Error!!!", ex);
            }
        }
        LineExitBo IBL1.GetLineExit(int numberOfLine, TimeSpan StartTime)
        {
            try
            {
                ///Request a single frequency of a line according to ID number and departure time.
                LineExitBo lineExitBo = new LineExitBo();
                LineExit lineExit = dl.ReturnLineExit(numberOfLine, StartTime);
                lineExit.CopyPropertiesTo(lineExitBo);
                ///List of line stations for the passage time of the line in each route.
                List<LineStation> lineStations = dl.OneLineFromList(item => item.BusLineID2 == numberOfLine).OrderBy(l => l.LocationNumberOnLine).ToList();
                ///The transit time of the line along the entire route.
                TimeSpan timeSpan = new TimeSpan();
                for (int i = 0; i < lineStations.Count - 1; i++)
                {
                    timeSpan += dl.AverageTimeBetweenTooStationsList(lineStations[i].StationNumberOnLine, lineStations[i + 1].StationNumberOnLine);
                }

                ///Arranging all the ports at the above port and for each port the time of arrival at the destination.
                if (lineExitBo.LineFrequency > 0)
                {
                    for (int j = 0; j < lineExitBo.LineFrequency; j++)
                    {
                        lineExitBo.DepartureTimes.Add(lineExitBo.LineStartTime);
                        lineExitBo.TimeFinishTrval.Add(lineExitBo.LineStartTime + timeSpan);
                        for (int k = 0; k < j; k++)
                        {
                            lineExitBo.DepartureTimes[j] += lineExitBo.LineFrequencyTime;
                            lineExitBo.TimeFinishTrval[j] += lineExitBo.LineFrequencyTime;
                        }
                    }
                }
                return lineExitBo;
            }
            catch (ExceptionLineExit ex)
            {
                throw new BOExceptionLineExit("Error!!!", ex);
            }
        }
        public IEnumerable<LineExitBo> ShowlineExit(int numberLine)
        {
            try
            {
                ///Collects all line outputs.
                IEnumerable<LineExit> lineExitBos = dl.LineExitList(numberLine);
                Func<LineExit, LineExitBo> func = item =>
                {
                    LineExitBo lineExitBo = new LineExitBo
                    {
                        LineStartTime = item.LineStartTime,
                        LineFinishTime = item.LineFinishTime,
                        LineFrequency = item.LineFrequency,
                        LineFrequencyTime = item.LineFrequencyTime,
                        BusLineID1 = item.BusLineID1
                    };
                    return lineExitBo;
                };
                return from one in lineExitBos
                       let p = func(one)
                       select p;
            }
            catch (ExceptionLineExit ex)
            {
                throw new BOExceptionLineExit("Error!!!", ex);
            }
        }
        public LineExitBo GetOneLineExit(int numberLine, TimeSpan timeSpan)
        {
            try
            {
                LineExit lineExit = dl.ReturnLineExit(numberLine, timeSpan);
                LineExitBo lineExitBo = new LineExitBo();
                lineExit.CopyPropertiesTo(lineExitBo);
                return lineExitBo;
            }
            catch (ExceptionLineExit ex)
            {
                throw new BOExceptionLineExit("Error!!!", ex);
            }
        }
        #endregion LineExit

        #region ConsecutiveStations
        public IEnumerable<ConsecutiveStationsBo> ShowConsecutiveStationsBo()
        {
            IEnumerable<ConsecutiveStations> consecutiveStationsBos = dl.ConsecutiveStationsList();
            Func<ConsecutiveStations, ConsecutiveStationsBo> func1 = item =>
            {
                ConsecutiveStationsBo consecutiveStationsBo = new ConsecutiveStationsBo
                {
                    StationNumber1 = item.StationNumber1,
                    StationNumber2 = item.StationNumber2,
                    DistanceBetweenTooStations = item.DistanceBetweenTooStations,
                    AverageTime = item.AverageTime
                };
                return consecutiveStationsBo;
            };
            return from con in consecutiveStationsBos
                   let p = func1(con)
                   select p;
        }
        public ConsecutiveStationsBo ShowOneConsecutiveStations(int number1, int number2)
        {
            try
            {
                ConsecutiveStationsBo consecutiveStationsBO = new ConsecutiveStationsBo();
                ConsecutiveStations consecutiveStations = dl.ReturnConsecutiveStation(number1, number2);
                consecutiveStations.CopyPropertiesTo(consecutiveStationsBO);
                return consecutiveStationsBO;
            }
            catch (ExceptionConsecutiveStations ex)
            {
                throw new BOExceptionConsecutiveStations("Error", ex);
            }
        }
        public void Udapting(ConsecutiveStationsBo consecutiveStationsBo)
        {
            try
            {
                ConsecutiveStations consecutiveStations = new ConsecutiveStations();
                consecutiveStationsBo.CopyPropertiesTo(consecutiveStations);
                dl.UpdatingConsecutiveStations(consecutiveStations);
            }
            catch (ExceptionConsecutiveStations ex)
            {
                throw new BOExceptionConsecutiveStations("Error", ex);
            }
        }
        #endregion ConsecutiveStations

        #region User
        public void AddUserToDo(UserBo userBo)
        {
            try
            {
                User user = new User
                {
                    Username = userBo.Username,
                    Password = userBo.Password,
                    ManagementPermission = userBo.ManagementPermission,
                    ChackDelete = userBo.ChackDelete
                };
                user.Permission1 = userBo.ManagementPermission ? DO.Permission.ManagementPermission
                   : DO.Permission.WithoutManagementPermission;
                dl.AddUser(user);
            }
            catch (ExceptionUser ex)
            {
                throw new BOExceptionUser("Error", ex);
            }
        }

        public void DeleteUserFromDo(string Username)
        {
            try
            {
                dl.DeleteUser(Username);
            }
            catch (ExceptionUser ex)
            {
                throw new BOExceptionUser("Error", ex);
            }
        }

        public void UdptingUser(UserBo userBo)
        {
            try
            {
                User user = new User();
                userBo.CopyPropertiesTo(user);
                dl.UpdatingUser(user);
            }
            catch (ExceptionUser ex)
            {
                throw new BOExceptionUser("Error", ex);
            }
        }

        public bool FindUser(string pass, string UserNam)
        {
            try
            {
                return dl.FindUser(pass, UserNam);
            }
            catch (ExceptionUser ex)
            {
                throw new BOExceptionUser("שגיאה ", ex);
            }
        }

        public UserBo GetUser(string UserName)
        {
            try
            {
                User user = dl.ReturnUser(UserName);
                UserBo userBo = new UserBo();
                user.CopyPropertiesTo(userBo);
                return userBo;
            }
            catch (ExceptionUser ex)
            {
                throw new BOExceptionUser("Error ", ex);
            }
        }
        public IEnumerable<UserBo> GetListUsers()
        {
            try
            {
                IEnumerable<User> users = dl.UseresList();
                IEnumerable<UserBo> userBos = from user in users
                                              let p = GetUser(user.Username)
                                              select p;
                return userBos;
            }
            catch (ExceptionUser ex)
            {
                throw new BOExceptionUser("Error ", ex);
            }
        }
        #endregion User

        #region Bus
        public void ChackBus(BusBO bus)
        {
            _ = DateTime.Today.AddYears(-1);
            if (bus.KmForTreatment > 20000)//|| yearAgo > bus.DayOfTreatment)
            {
                throw new ArgumentException("KmForTreatment  > 20000!!!");
            }
            if (bus.KmForRefueling > 1200)
            {
                throw new ArgumentException("Refueling > 1200!!!");
            }
        }
        public void AddBusBO(BusBO bus)
        {
            ChackBus(bus);
            try
            {
                if (bus.StartDate.Year >= 2018 && bus.License_number.Length != 8)
                {
                    //throw new FormatException
                }
                if (bus.StartDate.Year < 2018 && bus.License_number.Length != 7)
                {
                    //throw new FormatException
                }
                Bus bus1 = new Bus();
                bus.CopyPropertiesTo(bus1);
                dl.AddBus(bus1);
            }
            catch (ExceptionBus ex)
            {
                throw new BOExceptionBus("Rong bus", ex);
            }
        }
        public void DeleteBusBO(string License_number)
        {
            try
            {
                dl.DeleteBus(License_number);
            }
            catch (ExceptionBus ex)
            {
                throw new BOExceptionBus("Rong bus", ex);
            }
        }
        public BusBO ShowBus(string License_number)
        {
            try
            {
                Bus bus1 = new Bus();
                BusBO bus = new BusBO();
                bus1 = dl.ReturnBusToBl(License_number);
                string start, middel, end;
                if (bus1.StartDate.Year < 2018)
                {
                    start = bus.License_number.Substring(0, 2);
                    middel = bus.License_number.Substring(2, 3);
                    end = bus.License_number.Substring(5, 2);
                }
                else
                {
                    start = bus.License_number.Substring(0, 3);
                    middel = bus.License_number.Substring(3, 2);
                    end = bus.License_number.Substring(5, 3);
                }
                bus.License_number = string.Format("{0}-{1}-{2}", start, middel, end);
                bus1.CopyPropertiesTo(bus);
                return bus;
            }
            catch (ExceptionBus ex)
            {
                throw new BOExceptionBus("Rong bus", ex);
            }
        }
        public IEnumerable<BusBO> ShowAllBus()
        {
            try
            {
                Func<Bus, BusBO> func = item =>
                {
                    BusBO busBO = new BusBO
                    {
                        License_number = item.License_number,
                        StartDate = item.StartDate,
                        KmForRefueling = item.KmForRefueling,
                        KmForTreatment = item.KmForTreatment,
                        TotalMiles = item.TotalMiles,
                        Status = (BusBO.TravelMode)(int)item.Status,
                        IsAvailable = true
                    };
                    return busBO;
                };
                IEnumerable<Bus> buses = dl.BusLists();

                IEnumerable<BusBO> buses1 = from line in buses
                                            let p = func(line)
                                            orderby p.License_number
                                            select p;


                return buses1;
            }
            catch (ExceptionBus ex)
            {
                throw new BOExceptionBus("Error", ex);
            }
        }
        #endregion Bus
        private static readonly Random D = new Random(DateTime.Now.Millisecond);
    }
}