using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
//using System.Text;
//using System.Threading.Tasks;
using DLAPI;
using BLAPI;
using BO;
using DO;

namespace BL
{
    internal class InformationBusLineBl : IBL1
    {
        IDAL dl = DLFactory.GetDL();
        private static readonly Random D = new Random(DateTime.Now.Millisecond);
        public event EventHandler ValueChanged;
        #region LineTrip
        void IBL1.StartSimulator(TimeSpan startTime, int Rate, Action<TimeSpan> updateTime)
        {

        }
        void IBL1.StopSimulator()
        {

        }
        TimeSpan IBL1.TravelTimeBetweenTwoStations(int LineNumber, int NumberStation1, int NumberStation2)
        {
            TimeSpan timeSpan = new TimeSpan();
            List<LineStation> lineStations = dl.OneLineFromList(item => item.BusLineID2 == LineNumber).ToList();
            int index = lineStations.FindIndex(item => item.StationNumberOnLine == NumberStation1);
            int index1 = lineStations.FindIndex(item => item.StationNumberOnLine == NumberStation2);
            if (index > index1)
            {
                for (int i = index; i < index1; i++)
                {
                    timeSpan += dl.AverageTimeBetweenTooStationsList(lineStations[i].StationNumberOnLine, lineStations[i + 1].StationNumberOnLine);
                }
            }
            return timeSpan;
        }
        IEnumerable<LineTrip> IBL1.LineTrips(int NumberStation, TimeSpan Time, int numberStationDestination = 0)
        {
            List<int> lineStations = dl.LinesFromList(NumberStation).ToList();
            List<LineExitBo> lineExits1 = new List<LineExitBo>();
            List<LineExit> lineExits = new List<LineExit>();
            List<LineStation> lineStations1 = new List<LineStation>();
            List<LineTrip> lineTrip = new List<LineTrip>();
            Dictionary<int, string> valuePairs = new Dictionary<int, string>();

            var busLines = from line in lineStations
                           let p = dl.ReturnBusLine(line)
                           let k = dl.ReturnStation(p.LastStation)
                           select new { nubmerLine = p.LineNumber, lastStation = k.NameOfStation };
            TimeSpan time = new TimeSpan();
            foreach (var item in busLines)
            {
                lineExits1.Add(new LineExitBo { BusLineID1 = item.nubmerLine, NameOfLastStation = item.lastStation });
            }

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

            int temp, temp1 = 0;
            TimeSpan timeSpan = new TimeSpan();
            TimeSpan timeSpan1 = new TimeSpan();
            TimeSpan timeSpan2 = new TimeSpan();
            for (int i = 0; i < lineStations.Count; i++)
            {
                lineStations1 = dl.OneLineFromList(item => item.BusLineID2 == lineStations[i]).OrderBy(item => item.LocationNumberOnLine).ToList();
                temp = lineStations1.FindIndex(item => item.StationNumberOnLine == NumberStation);
                temp1 = lineStations1.FindIndex(item => item.StationNumberOnLine == numberStationDestination);
                for (int j = 0; j < temp; j++)
                {
                    timeSpan += dl.AverageTimeBetweenTooStationsList(lineStations1[j].StationNumberOnLine, lineStations1[j + 1].StationNumberOnLine);
                }

                if (numberStationDestination != 0 && temp1 != -1 && temp1 > temp)
                {
                    for (int t = temp; t < temp1; t++)
                    {
                        timeSpan1 += dl.AverageTimeBetweenTooStationsList(lineStations1[t].StationNumberOnLine, lineStations1[t + 1].StationNumberOnLine);
                    }
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
                timeSpan1 = TimeSpan.Zero;
                timeSpan2 = TimeSpan.Zero;
            }
            // groping
            //IEnumerable<LineTrip> lineTrips = from trip in lineTrip
            //                                  group trip by trip.StartTrip into trip


            IEnumerable<LineTrip> lineTrips = lineTrip;
            return lineTrips;
        }
        #endregion LineTrip

        #region Bus
        void IBL1.AddBusBO(BusBO bus)
        {
            ChackBus(bus);
            try
            {
                Bus bus1 = new Bus();
                bus.CopyPropertiesTo(bus1);
                dl.AddBus(bus1);
            }
            catch (ExceptionBus ex)
            {
                throw new BOExceptionBus("Rong bus", ex);
            }
        }
        void IBL1.DeleteBusBO(string License_number)
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
        BusBO IBL1.ShowBus(string License_number)
        {
            try
            {
                Bus bus1 = new Bus();
                BusBO bus = new BusBO();
                bus1 = dl.ReturnBusToBl(License_number);
                bus1.CopyPropertiesTo(bus);
                return bus;
            }
            catch (ExceptionBus ex)
            {
                throw new BOExceptionBus("Rong bus", ex);
            }
        }
        IEnumerable<BusBO> IBL1.ShowAllBus()
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
                IEnumerable<BusBO> buses1 = from bus in buses
                                            let p = func(bus)
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

        #region BusLineStation
        void IBL1.AddStationToLine(StationLineBO stationLineBO, int NumberStation1, int NumberStation2, TimeSpan timeSpan, float distance)
        {
            try
            {
                List<LineStation> lineStations = dl.OneLineFromList(item => item.BusLineID2 == stationLineBO.BusLineID2).ToList();
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

                dl.DeleteLineStation1(stationLineBO.BusLineID2);
                foreach (LineStation item2 in lineStations1)
                {
                    dl.AddLineStation(item2);
                }

                LineStation lineStation1 = new LineStation
                {
                    BusLineID2 = stationLineBO.BusLineID2,
                    StationNumberOnLine = stationLineBO.StationNumberOnLine,
                    LocationNumberOnLine = stationLineBO.LocationNumberOnLine,
                    ChackDelete2 = stationLineBO.ChackDelete2
                };
                dl.AddLineStation(lineStation1);

                if (dl.ChackExistingConsecutiveStations(item => (item.StationNumber1 == stationLineBO.StationNumberOnLine
                        && item.StationNumber2 == NumberStation1) || (item.StationNumber1 == NumberStation1 && item.StationNumber2 == stationLineBO.StationNumberOnLine)))
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

                if (dl.ChackExistingConsecutiveStations(item => (item.StationNumber1 == stationLineBO.StationNumberOnLine
                        && item.StationNumber2 == NumberStation2) || (item.StationNumber1 == NumberStation2 && item.StationNumber2 == stationLineBO.StationNumberOnLine)))
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
        IEnumerable<StationLineBO> IBL1.ReturnLineStationList(int LineNumber)
        {
            try
            {
                IEnumerable<LineStation> stationLines = dl.OneLineFromList(item => item.BusLineID2 == LineNumber);
                IEnumerable<StationLineBO> stationLineBOs = from line in stationLines
                                                            orderby line.LocationNumberOnLine
                                                            let p = ReturnStationLine(LineNumber, line.StationNumberOnLine)
                                                            select p;
                List<StationLineBO> stationLineBOs1 = new List<StationLineBO>();
                foreach (StationLineBO item in stationLineBOs)
                {
                    stationLineBOs1.Add(item);
                }
                for (int i = 0; i < stationLineBOs1.Count - 1; i++)
                {
                    ConsecutiveStations consecutiveStations1 = dl.ReturnConsecutiveStation(stationLineBOs1[i].StationNumberOnLine, stationLineBOs1[i + 1].StationNumberOnLine);
                    stationLineBOs1[i].DistanceBetweenTooStations = consecutiveStations1.DistanceBetweenTooStations;
                    stationLineBOs1[i].AverageTime = consecutiveStations1.AverageTime;
                }
                stationLineBOs = stationLineBOs1;
                return stationLineBOs;
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
        void IBL1.DeleteStationFromLine(int LineNumber, int NumberStationToDelete)
        {
            try
            {
                dl.DeleteOneLineStation(LineNumber, NumberStationToDelete);
            }
            catch (ExceptionLineStation ex)
            {
                throw new BOExceptionLineStation("Error!!!", ex);
            }
        }
        public StationLineBO ReturnStationLine(int LineNumber, int numberStation)
        {
            try
            {
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
        IEnumerable<BusLineBO> IBL1.LinePastInStationBOs(int NumberStation)
        {
            try
            {
                IEnumerable<int> busLine = dl.LinesFromList(NumberStation);
                IEnumerable<BusLine> busLine1 = from line in busLine
                                                let p = dl.ReturnBusLine(line)
                                                select p;

                IEnumerable<BusLineBO> busLineBOs = from line in busLine1
                                                    let p = func(line)
                                                    orderby line.LineNumber
                                                    select p;

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

                return busLineBOs;
            }
            catch (ExceptionLine ex)
            {
                throw new BOExceptionLine("Error", ex);
            }
        }
        BusLineBO IBL1.LineInformation(int numberLine)
        {
            BusLineBO busLineBO = new BusLineBO();
            try
            {

                BusLine busLine = dl.ReturnBusLine(numberLine);
                busLine.CopyPropertiesTo(busLineBO);
                busLineBO.StationLineBOs = new List<StationLineBO>();

                IEnumerable<LineStation> lineStations = dl.OneLineFromList(line1 => line1.ChackDelete2 && line1.BusLineID2 == numberLine);

                IEnumerable<LineStation> lineStations10 = from line in lineStations
                                                          orderby line.LocationNumberOnLine
                                                          select line;

                IEnumerable<BusStation> busStationBOs = from busLine5 in lineStations10
                                                        let p = dl.ReturnStation(busLine5.StationNumberOnLine)
                                                        select p;

                List<BusStationBO> busStationBOs1 = (from busLine5 in busStationBOs
                                                     let p = function1(busLine5)
                                                     select p).ToList();


                busLineBO.StationLineBOs = (from line in lineStations10
                                            let p = function2(line)
                                            select p).ToList();

                for (int i = 0; i < busLineBO.StationLineBOs.Count; i++)
                {
                    busLineBO.StationLineBOs[i].NameOfStation = busStationBOs1[i].NameOfStation;
                }

                //List<LineStation> lineStations1 = lineStations10.ToList();
                //var w = from busLine5 in busStationBOs1
                //        select new { NameOfStation1 = busLine5.NameOfStation };
                //foreach (var item in w)
                //{
                //    busLineBO.StationLineBOs.Add(new StationLineBO { NameOfStation = item.NameOfStation1 });
                //}

                //foreach (LineStation item in lineStations10)
                //{
                //    busLineBO.StationLineBOs[i].BusLineID2 = lineStations1[i].BusLineID2;
                //    busLineBO.StationLineBOs[i].StationNumberOnLine = lineStations1[i].StationNumberOnLine;
                //    busLineBO.StationLineBOs[i].ChackDelete2 = lineStations1[i].ChackDelete2;
                //    busLineBO.StationLineBOs[i].LocationNumberOnLine = lineStations1[i].LocationNumberOnLine;
                //}
                //for (int i = 0; i < lineStations1.Count; i++)
                //{
                //    busLineBO.StationLineBOs[i].BusLineID2 = lineStations1[i].BusLineID2;
                //    busLineBO.StationLineBOs[i].StationNumberOnLine = lineStations1[i].StationNumberOnLine;
                //    busLineBO.StationLineBOs[i].ChackDelete2 = lineStations1[i].ChackDelete2;
                //    busLineBO.StationLineBOs[i].LocationNumberOnLine = lineStations1[i].LocationNumberOnLine;
                //}

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

                busLineBO.LineExitBos1 = ShowlineExit(busLineBO.BusLineID1).ToList();

                // groping 
                // TimeFinishTrval
                TimeSpan timeSpan = new TimeSpan(busLineBO.StationLineBOs.Sum(item => item.AverageTime.Hours),
                    busLineBO.StationLineBOs.Sum(item => item.AverageTime.Minutes),
                    busLineBO.StationLineBOs.Sum(item => item.AverageTime.Seconds));

                for (int i = 0; i < busLineBO.LineExitBos1.Count; i++)
                {
                    for (int j = 0; j < busLineBO.LineExitBos1[i].LineFrequency; j++)
                    {
                        busLineBO.LineExitBos1[i].DepartureTimes.Add(busLineBO.LineExitBos1[i].LineStartTime);
                        busLineBO.LineExitBos1[i].TimeFinishTrval.Add(busLineBO.LineExitBos1[i].LineStartTime + timeSpan);
                        for (int k = 0; k < j; k++)
                        {
                            busLineBO.LineExitBos1[i].DepartureTimes[j] += busLineBO.LineExitBos1[i].LineFrequencyTime;
                            busLineBO.LineExitBos1[i].TimeFinishTrval[j] += busLineBO.LineExitBos1[i].LineFrequencyTime;
                        }
                    }
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
        IEnumerable<BusStationBO> IBL1.ShowStation()
        {
            try
            {
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
            //if (busStationBO.StationNumber < 0 || busStationBO.StationNumber > 1000000)
            //{
            //    throw new ArgumentException("Incorrect station number!!!");
            //}
            busStationBO.Latitude = (float)((float)(D.NextDouble() * (33.3 - 31)) + 31);
            busStationBO.Longitude = (float)((float)(D.NextDouble() * (35.5 - 34.3)) + 34.3);
        }
        void IBL1.AddStationToDo(BusStationBO busStationBO)
        {
            try
            {
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
        void IBL1.DeleteStationFromDo(int numberOfStation)
        {
            try
            {
                dl.DeleteStation(numberOfStation);
            }
            catch (ExceptionStation ex)
            {
                throw new BOExceptionStation("Error", ex);
            }
        }
        BusStationBO IBL1.ReturnStationToPL(int numberOfStation)
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
        void IBL1.AddBusLineBO(BusLineBO busLineBO)
        {
            try
            {
                if (busLineBO.StationLineBOs.Count >= 2)
                {
                    BusLine busLine = new BusLine();
                    busLineBO.CopyPropertiesTo(busLine);
                    busLineBO.BusLineID1 = dl.AddBusLine(busLine);

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
        void IBL1.DeleteBusLineBO(int NumberLine)
        {
            try
            {
                dl.DeleteBusLine(NumberLine);
                dl.DeleteLineStation(NumberLine);
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
        int IBL1.ReturnBusLineIdFromDl()
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
        void IBL1.UdaptingLine(BusLineBO busLineBO)
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
                    TimeSpan timeSpan = lineExitBo.LineFinishTime - lineExitBo.LineStartTime;
                    if (timeSpan > lineExitBo.LineFrequencyTime)
                    {
                        for (TimeSpan i = lineExitBo.LineStartTime; i < lineExitBo.LineFinishTime; i += lineExitBo.LineFrequencyTime)
                        {
                            lineExitBo.LineFrequency++;
                        }
                    }
                    else
                    {
                        //throw new ("Error!!! bad Frequency Time")
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
        void IBL1.DeleteLineExit(int numberOfLine, TimeSpan StartTime)
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
                LineExitBo lineExitBo = new LineExitBo();
                LineExit lineExit = dl.ReturnLineExit(numberOfLine, StartTime);
                lineExit.CopyPropertiesTo(lineExitBo);
                //lineExitBo.DepartureTimes = new List<TimeSpan>();
                List<LineStation> lineStations = dl.OneLineFromList(item => item.BusLineID2 == numberOfLine).ToList();
                TimeSpan timeSpan = new TimeSpan();
                for (int i = 0; i < lineStations.Count - 1; i++)
                {
                    timeSpan += dl.AverageTimeBetweenTooStationsList(lineStations[i].StationNumberOnLine, lineStations[i + 1].StationNumberOnLine);
                }
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
                IEnumerable<LineExit> lineExitBos = dl.LineExitList(numberLine);
                Func<LineExit, LineExitBo> func = item =>
                {
                    LineExitBo lineExitBo = new LineExitBo
                    {
                        LineStartTime = item.LineStartTime,
                        LineFinishTime = item.LineFinishTime,
                        LineFrequency = item.LineFrequency,
                        LineFrequencyTime = item.LineFrequencyTime
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
                throw new BOExceptionUser("Error ", ex);
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
        IEnumerable<UserBo> IBL1.GetListUsers()
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
        public void ChackBus(BusBO bus)
        {
            string start, middel, end;
            if (bus.StartDate.Year < 2018)
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
            DateTime yearAgo = DateTime.Today.AddYears(-1);
            if (bus.KmForTreatment > 20000 || yearAgo > bus.DayOfTreatment)
            {
                throw new ArgumentException("DayOfTreatment > 20000!!!");
            }
            if (bus.KmForRefueling > 1200)
            {
                throw new ArgumentException("Refueling > 1200!!!");
            }
        }
    }
}