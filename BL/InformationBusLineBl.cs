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

        #region Bus
        public void AddBusBO(BusBO bus)
        {
            ChackBus(bus);
            try
            {
                Bus bus1 = new Bus();
                bus.CopyPropertiesTo(bus1);
                dl.AddBus(bus1);
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("Rong bus", ex);
            }
        }
        public void DeleteBusBO(string License_number)
        {
            try
            {
                dl.DeleteBus(License_number);
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("Rong bus to Delete", ex);
            }
        }
        public BusBO ShowBus(string License_number)
        {
            try
            {
                Bus bus1 = new Bus();
                BusBO bus = new BusBO();
                bus1 = dl.ReturnBusToBl(License_number);
                bus1.CopyPropertiesTo(bus);
                return bus;
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("Rong bus", ex);
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
                IEnumerable<BusBO> buses1 = from bus in buses
                                            let p = func(bus)
                                            orderby p.License_number
                                            select p;
                return buses1;
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("no bus dl", ex);
            }
        }
        #endregion Bus

        #region BusLineStation
        public IEnumerable<BusStationBO> ShowStation()
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
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("No Stations", ex);
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
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("Bad Line", ex);
            }
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

                return busLineBOs ?? throw new ArgumentException("there is no station that pased in this station!!!");
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("No line in Stations", ex);
            }
        }

        BusLineBO IBL1.LineInformation(int numberLine)
        {
            BusLineBO busLineBO = new BusLineBO();
            try
            {

                BusLine busLine = dl.ReturnBusLine(numberLine);
                busLine.CopyPropertiesTo(busLineBO);

                IEnumerable<LineStation> lineStations = dl.OneLineFromList(line1 => line1.ChackDelete2 && line1.BusLineID2 == numberLine);

                IEnumerable<LineStation> lineStations10 = from line in lineStations
                                                          orderby line.LocationNumberOnLine
                                                          select line;

                IEnumerable<BusStation> busStationBOs = from busLine5 in lineStations
                                                        let p = dl.ReturnStation(busLine5.StationNumberOnLine)
                                                        select p;

                IEnumerable<BusStationBO> busStationBOs1 = from busLine5 in busStationBOs
                                                           let p = function1(busLine5)
                                                           select p;

                var w = from busLine5 in busStationBOs1
                        select new { NameOfStation1 = busLine5.NameOfStation };

                List<LineStation> lineStations1 = lineStations10.ToList();
                busLineBO.StationLineBOs = new List<StationLineBO>();

                foreach (var item in w)
                {
                    busLineBO.StationLineBOs.Add(new StationLineBO { NameOfStation = item.NameOfStation1 });
                }

                for (int i = 0; i < lineStations1.Count; i++)
                {
                    busLineBO.StationLineBOs[i].BusLineID2 = lineStations1[i].BusLineID2;
                    busLineBO.StationLineBOs[i].StationNumberOnLine = lineStations1[i].StationNumberOnLine;
                    busLineBO.StationLineBOs[i].ChackDelete2 = lineStations1[i].ChackDelete2;
                    busLineBO.StationLineBOs[i].LocationNumberOnLine = lineStations1[i].LocationNumberOnLine;
                }

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
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("No line in Stations", ex);
            }
            return busLineBO;
        }
        #endregion BusLineStation

        #region Stations
        public void ChackStation(ref BusStationBO busStationBO)
        {
            if (busStationBO.StationNumber < 0 || busStationBO.StationNumber > 1000000)
            {
                throw new ArgumentException("Incorrect station number!!!" );
            }
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
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("bad id", ex);
            }
        }

        void IBL1.DeleteStationFromDo(int numberOfStation)
        {
            try
            {
                dl.DeleteStation(numberOfStation);
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("bad id", ex);
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
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("bad id", ex);
            }
        }
        #endregion Stations

        #region BusLine
        int IBL1.ReturnBusLineIdFromDl()
        {
            try
            {
                int i = dl.BusLineId();
                return i;
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("no data", ex);
            }
        }
        // הוספת קו בודד 
        void IBL1.AddBusLineBO(BusLineBO busLineBO)
        {
            try
            {
                if (busLineBO.StationLineBOs.Count >= 2)
                {
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

                    BusLine busLine = new BusLine
                    {
                        LineNumber = busLineBO.LineNumber,
                        AreaBusUrban = (DO.Area1)(int)busLineBO.AreaBusUrban,
                        GetUrban = (DO.Urban)(int)busLineBO.GetUrban,
                        FirstStation = busLineBO.StationLineBOs[0].StationNumberOnLine,
                        LastStation = busLineBO.StationLineBOs[busLineBO.StationLineBOs.Count - 1].StationNumberOnLine,
                        GetAvailable = (DO.Available)(int)busLineBO.GetAvailable
                    };

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
                        dl.AddLineStationToNewLine(item);
                    }

                    //if (busLineBO.LineExitBos1.Count == 0)
                    //{
                    //    LineExit lineExit = new LineExit
                    //    {
                    //        BusLineID1 = busLineBO.LineExitBos1[0].BusLineID2,
                    //        LineStartTime = busLineBO.LineExitBos1[0].LineStartTime,
                    //        LineFrequency = busLineBO.LineExitBos1[0].LineFrequency
                    //    };
                    //    dl.AddLineExit(lineExit);
                    //}

                    //for (int i = 0; i < busLineBO.LineExitBos1.Count; i++)
                    //{
                    //    LineExit lineExit = new LineExit
                    //    {
                    //        BusLineID1 = busLineBO.LineExitBos1[i].BusLineID2,
                    //        LineStartTime = busLineBO.LineExitBos1[i].LineStartTime,
                    //        LineFrequency = busLineBO.LineExitBos1[i].LineFrequency
                    //    };
                    //    dl.AddLineExit(lineExit);
                    //}

                    //if (busLineBO.LineExitBos1.Count > 0)
                    //{
                    //    for (int i = 0; i < busLineBO.LineExitBos1.Count; i++)
                    //    {
                    //        busLineBO.LineExitBos1[i].LineFinishTime = busLineBO.UrbanInterUrban
                    //            ? int.Parse(busLineBO.LineExitBos1[i].LineStartTime)
                    //                + (int)busLineBO.ConsecutiveStationsBos.Sum(item => item.AverageTime) + D.Next(10, 30)
                    //            : int.Parse(busLineBO.LineExitBos1[i].LineStartTime)
                    //                + (int)busLineBO.ConsecutiveStationsBos.Sum(item => item.AverageTime);
                    //    }
                    //}
                }

                else
                {
                    //Exception inner תזכורת לבדוק איפה החריגה תתפס 
                    throw new ArgumentException("There must be at least two stations on the line!!!");
                }
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("bad id line", ex);
            }
        }

        void IBL1.DeleteBusLineBO(int NumberLine)
        {
            try
            {
                dl.DeleteBusLine(NumberLine);
                dl.DeleteLineStation(NumberLine);
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("bad id line delete", ex);
            }
        }

        void IBL1.AddStationToLine(StationLineBO stationLineBO, int Number)
        {
            try
            {
                LineStation lineStation1 = new LineStation
                {
                    BusLineID2 = stationLineBO.BusLineID2,
                    StationNumberOnLine = stationLineBO.StationNumberOnLine,
                    LocationNumberOnLine = stationLineBO.LocationNumberOnLine,
                    ChackDelete2 = stationLineBO.ChackDelete2
                };

                if (dl.ChackExistingConsecutiveStations(item => item.StationNumber1 == stationLineBO.StationNumberOnLine
                        && item.StationNumber2 == Number || item.StationNumber1 == Number && item.StationNumber2 == stationLineBO.StationNumberOnLine))
                {
                    ConsecutiveStations consecutiveStations = new ConsecutiveStations
                    {
                        StationNumber1 = Number,
                        StationNumber2 = stationLineBO.StationNumberOnLine,
                        DistanceBetweenTooStations = stationLineBO.DistanceBetweenTooStations,
                        AverageTime = stationLineBO.AverageTime
                    };

                    dl.AddConsecutiveStations(consecutiveStations);
                }

                int number = dl.AddLineStation(lineStation1);

                if (dl.ChackExistingConsecutiveStations(item => (item.StationNumber1 == stationLineBO.StationNumberOnLine
                        && item.StationNumber2 == number) || (item.StationNumber1 == number && item.StationNumber2 == stationLineBO.StationNumberOnLine)))
                {
                    ConsecutiveStations consecutiveStations = new ConsecutiveStations
                    {
                        StationNumber1 = stationLineBO.StationNumberOnLine,
                        StationNumber2 = number,
                        DistanceBetweenTooStations = stationLineBO.DistanceBetweenTooStations,
                        AverageTime = stationLineBO.AverageTime
                    };
                    dl.AddConsecutiveStations(consecutiveStations);
                }
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("bad  station line", ex);
            }
        }
        IEnumerable<StationLineBO> IBL1.ReturnLineStationList(int LineNumber)
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
        public StationLineBO ReturnStationLine(int LineNumber, int numberStation)
        {
            try
            {
                StationLineBO stationLineBO = new StationLineBO();
                BusStation busStation = dl.ReturnStation(numberStation);
                LineStation lineStation = dl.ReturnLineStation(LineNumber, numberStation);
                lineStation.CopyPropertiesTo(stationLineBO);
                //ConsecutiveStations consecutiveStations = dl.
                stationLineBO.NameOfStation = busStation.NameOfStation;
                return stationLineBO;
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("bad station line", ex);
            }
        }
        void IBL1.DeleteStationFromLine(int LineNumber, int NumberStationToDelete)
        {
            try
            {
                dl.DeleteOneLineStation(LineNumber, NumberStationToDelete);
                // לבדוק את זה כי יכול להיות שהתחנות האלה עוקבות בכללי ובקו אחר
                // dl.DeleteConsecutiveStations(NumberStationToDelete, DeleteFromConsecutiveStations);

            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("bad id station line", ex);
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
        #endregion BusLine

        #region LineExit
        public void AddExitToLine(LineExitBo lineExitBo, int numberLine)
        {
            try
            {
                lineExitBo.BusLineID2 = numberLine;
                LineExit lineExit = new LineExit
                {
                    BusLineID1 = lineExitBo.BusLineID2,
                    LineStartTime = lineExitBo.LineStartTime,
                    LineFrequency = lineExitBo.LineFrequency,
                    LineFinishTime = lineExitBo.LineFinishTime
                };
                dl.AddLineExit(lineExit);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteLineExit(int numberOfLine, string startLineExit)
        {
            try
            {
                dl.DeleteLineExit(numberOfLine, startLineExit);
            }
            catch (Exception)
            {

                throw;
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
                        LineFrequency = item.LineFrequency
                    };
                    return lineExitBo;
                };
                return from one in lineExitBos
                       let p = func(one)
                       select p;
            }
            catch (Exception)
            {

                throw;
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
            ConsecutiveStationsBo consecutiveStationsBO = new ConsecutiveStationsBo();
            ConsecutiveStations consecutiveStations = dl.ReturnConsecutiveStation(number1, number2);
            consecutiveStations.CopyPropertiesTo(consecutiveStationsBO);
            return consecutiveStationsBO;
        }
        public void Udapting(ConsecutiveStationsBo consecutiveStationsBo)
        {
            try
            {
                ConsecutiveStations consecutiveStations = new ConsecutiveStations();
                consecutiveStationsBo.CopyPropertiesTo(consecutiveStations);
                dl.UpdatingConsecutiveStations(consecutiveStations);
            }
            catch (Exception)
            {

                throw;
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
                    ManagementPermission = userBo.ManagementPermission
                };
                user.Permission1 = userBo.ManagementPermission ? DO.Permission.ManagementPermission
                   : DO.Permission.WithoutManagementPermission;
                dl.AddUser(user);
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("bad Username", ex);
            }
        }
        public void DeleteUserFromDo(string Username)
        {
            try
            {
                dl.DeleteUser(Username);
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("bad Username", ex);
            }
        }
        public void UdptingUser(UserBo userBo)
        {
            User user = new User
            {
                Username = userBo.Username,
                Password = userBo.Password,
                ManagementPermission = userBo.ManagementPermission
            };
            user.Permission1 = userBo.ManagementPermission ? DO.Permission.ManagementPermission
               : DO.Permission.WithoutManagementPermission;
            dl.AddUser(user);
        }

        public bool FindUser(string pass, string UserNam)
        {
            try
            {
                return dl.FindUser(pass, UserNam);
            }
            catch (ExceptionDl ex)
            {
                throw new ExceptionBl("rong Username", ex);
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