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
        // תקינות נתונים להכל
        // הוספה ועדכון
        // מחיקה
        // בקשות 
        // להשתמש בגרופינג ולהסתכל על שימוש בלפחות ארבעה ביטויי למבדה
        // להציג את התחנות עוקבות בחלון של התחנות על ידי שאילתא מהתחנות עוקבות בDO
        // לעשות רשימה של כל התחנות
        // שאילתה של יציאות קו והוספת לוז לקו מסוים
        // #region LineExit
        // #endregion LineExit

        #region Bus
        public void AddBusBO(BusBO bus)
        {

            try
            {
                ChackBus(bus);
            }
            catch (Exception)
            {

                throw;
            }

            try
            {
                Bus bus1 = new Bus();
                bus.CopyPropertiesTo(bus1);
                dl.AddBus(bus1);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteBusBO(string License_number)
        {
            try
            {
                dl.DeleteBus(License_number);
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
            }
        }
        //public IEnumerable<BusBO> BusInformation()
        //{
        //    try
        //    {
        //        IEnumerable<BusBO> busBOs;
        //        IEnumerable<Bus> buses;
        //        buses = dl.BusLists();


        //        return (IEnumerable<BusBO>)dl.BusLists();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
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
            catch (Exception)
            {

                throw;
            }
        }


        // קווים שעוברים בתחנה
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
                        UrbanInterUrban = item.UrbanInterUrban,
                        FirstStation = item.FirstStation,
                        LastStation = item.LastStation,
                        IsAvailable1 = item.IsAvailable1
                    };
                    return busLineBO1;
                }

                return busLineBOs ?? throw new ArgumentException("there is no station that pased in this station!!!");
            }
            catch (Exception)
            {

                throw;
            }
        }

        // מידע על הקו שכולל את מעבר התחנות וכו
        BusLineBO IBL1.LineInformation(int numberLine)
        {
            BusLineBO busLineBO = new BusLineBO();
            try
            {
                BusLine busLine = dl.ReturnBusLine(numberLine);
                busLine.CopyPropertiesTo(busLineBO);

                IEnumerable<LineStation> lineStations = from line in dl.OneLineFromList(line1 => line1.ChackDelete2 && line1.BusLineID2 == numberLine)
                                                        orderby line.LocationNumberOnLine
                                                        select line;

                IEnumerable<BusStation> busStationBOs = from busLine5 in lineStations
                                                        let p = dl.ReturnStation(busLine5.StationNumberOnLine)
                                                        select p;

                busLineBO.BusStationBO1 = from busLine5 in busStationBOs
                                          let p = function1(busLine5)
                                          select p;

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

                //for (int i = 0; i < busLineBO.StationLineBOs.Count - 1; i++)
                //{
                //    busLineBO.DistanceBetweenTooStationsList.Add(dl.DistanceBetweenTooStations(busLineBO.StationLineBOs[i].StationNumberOnLine, busLineBO.StationLineBOs[i + 1].StationNumberOnLine));
                //}
                //for (int i = 0; i < busLineBO.StationLineBOs.Count - 1; i++)
                //{
                //    busLineBO.AverageTimeBetweenTooStationsList.Add(dl.AverageTimeBetweenTooStationsList(busLineBO.StationLineBOs[i].StationNumberOnLine, busLineBO.StationLineBOs[i + 1].StationNumberOnLine));
                //}
            }
            catch (Exception)
            {

                throw;
            }
            return busLineBO;
        }
        #endregion BusLineStation

        #region Stations
        public void ChackStation(ref BusStationBO busStationBO)
        {
            if (busStationBO.StationNumber < 0 || busStationBO.StationNumber > 1000000)
            {
                throw new ArgumentException("Incorrect station number!!!");
            }
            busStationBO.Latitude = (float)((float)(D.NextDouble() * (33.3 - 31)) + 31);
            busStationBO.Longitude = (float)((float)(D.NextDouble() * (35.5 - 34.3)) + 34.3);
        }
        // הוספה תחנה בודדת
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
                // public List<StationLineBO> StationLineBOs { get; set; }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // מחיקת תחנה בודדת 
        void IBL1.DeleteStationFromDo(int numberOfStation)
        {
            try
            {
                dl.DeleteStation(numberOfStation);
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {

                throw;
            }
        }

        // הוספת קו בודד 
        void IBL1.AddBusLineBO(BusLineBO busLineBO)
        {
            // לבדוק תקינות הנתון
            try
            {
                //&& busLineBO.BusStationBO1.FindAll(item => item.IsAvailable3 == true)
                if (busLineBO.StationLineBOs.Count > 2)
                {
                    for (int i = 0; i < busLineBO.StationLineBOs.Count - 1; i++)
                    {
                        busLineBO.ConsecutiveStationsBos.Add(new ConsecutiveStationsBl(busLineBO.StationLineBOs[i].StationNumberOnLine, busLineBO.StationLineBOs[i + 1].StationNumberOnLine));
                        ConsecutiveStations consecutiveStations = new ConsecutiveStations
                        {
                            StationNumber1 = busLineBO.ConsecutiveStationsBos[i].StationNumber1,
                            StationNumber2 = busLineBO.ConsecutiveStationsBos[i].StationNumber2,
                            DistanceBetweenTooStations = busLineBO.ConsecutiveStationsBos[i].DistanceBetweenTooStations,
                            AverageTime = busLineBO.ConsecutiveStationsBos[i].AverageTime
                        };
                        dl.AddConsecutiveStations(consecutiveStations);
                    }

                    BusLine busLine = new BusLine
                    {
                        LineNumber = busLineBO.LineNumber,
                        AreaBusUrban = (DO.Area1)(int)busLineBO.AreaBusUrban,
                        UrbanInterUrban = busLineBO.UrbanInterUrban,
                        FirstStation = busLineBO.StationLineBOs[0].StationNumberOnLine,
                        LastStation = busLineBO.StationLineBOs[busLineBO.StationLineBOs.Count].StationNumberOnLine,
                        IsAvailable1 = busLineBO.IsAvailable1
                    };

                    for (int i = 0; i < busLineBO.StationLineBOs.Count; i++)
                    {

                        busLineBO.StationLineBOs[i].ChackDelete2 = true;
                        busLineBO.StationLineBOs[i].LocationNumberOnLine = i;
                    }

                    busLineBO.BusLineID1 = dl.AddBusLine(busLine);

                    IEnumerable<LineStation> s = from AddLineStationToDL in busLineBO.StationLineBOs
                                                 let p = ReturnNewLineStationDoFromBo(AddLineStationToDL)
                                                 select p;
                    foreach (LineStation item in s)
                    {
                        dl.AddLineStation(item);
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
            catch (Exception)
            {

                throw;
            }
        }

        void IBL1.DeleteBusLineBO(int NumberLine)
        {
            try
            {
                dl.DeleteBusLine(NumberLine);
                dl.DeleteLineStation(NumberLine);
            }
            catch (Exception)
            {

                throw;
            }
        }

        void IBL1.AddStationToLine(StationLineBO stationLineBO)
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
                dl.AddLineStation(lineStation1);
                int number = dl.IndexOfLastLineStation(stationLineBO.BusLineID2);
                ConsecutiveStationsBl consecutiveStationsBl = new ConsecutiveStationsBl(number, stationLineBO.StationNumberOnLine);
                ConsecutiveStations consecutiveStations = new ConsecutiveStations
                {
                    StationNumber1 = consecutiveStationsBl.StationNumber1,
                    StationNumber2 = consecutiveStationsBl.StationNumber2,
                    DistanceBetweenTooStations = consecutiveStationsBl.DistanceBetweenTooStations,
                    AverageTime = consecutiveStationsBl.AverageTime
                };
                dl.AddConsecutiveStations(consecutiveStations);
                // הוספת פונקציה עם ערכי ברירת מחדל 
            }
            catch (Exception)
            {

                throw;
            }
        }

        void IBL1.DeleteStationFromLine(int LineNumber, int NumberStationToDelete, int DeleteFromConsecutiveStations)
        {
            try
            {
                dl.DeleteOneLineStation(LineNumber, NumberStationToDelete);
                // לבדוק את זה כי יכול להיות שהתחנות האלה עוקבות בכללי ובקו אחר
                dl.DeleteConsecutiveStations(NumberStationToDelete, DeleteFromConsecutiveStations);

            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteUserFromDo(string Username)
        {
            try
            {
                dl.DeleteUser(Username);
            }
            catch (Exception)
            {

                throw;
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
            bool v = true;
            //try
            //{
            //    v = dl.FindUser(pass, UserNam);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //    //   throw;
            //}
            return v;
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