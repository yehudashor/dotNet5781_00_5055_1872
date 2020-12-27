using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using DLAPI;
using BLAPI;
using BO;
using DO;

namespace BL
{

    internal class InformationBusLineBl : BusLineBO, IBL1
    {
        internal IDAL dl = DLFactory.GetDL();
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

        #region BusLineStation
        // קווים שעוברים בתחנה
        IEnumerable<BusLineBO> IBL1.LinePastInStationBOs(int NumberStation)
        {
            try
            {
                IEnumerable<int> busLine = dl.LinesFromList(NumberStation);
                IEnumerable<BusLineBO> busLineBOs;
                IEnumerable<BusLine> busLine1 = from line in busLine
                                                let p = dl.ReturnBusLine(line)
                                                select p;
                busLineBOs = (IEnumerable<BusLineBO>)busLine1;
                // busLine1.CopyPropertiesTo(busLineBOs);

                if (busLineBOs != null)
                {
                    var v = from line in busLineBOs
                            orderby line.LineNumber
                            select new
                            { id = line.BusLineID1, NumberLine = line.LineNumber, LastStation1 = line.BusStationBO1[BusStationBO1.Count].StationNumber, ur = line.UrbanInterUrban };
                    return (IEnumerable<BusLineBO>)v;
                }
                else
                {
                    throw new ArgumentException("there is no station that pased in this station!!!");
                }
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
                IEnumerable<LineStation> lineStations = dl.OneLineFromList(numberLine);
                IEnumerable<LineStation> lineStations1 = from SortlineStation in lineStations
                                                         orderby SortlineStation.LocationNumberOnLine
                                                         select SortlineStation;
                lineStations.CopyPropertiesTo(busLineBO.StationLineBOs);
                busLineBO.BusStationBO1 = (List<BusStationBO>)(from busLine5 in busLineBO.StationLineBOs
                                                               let p = dl.ReturnStation(busLine5.StationNumberOnLine)
                                                               select p);
                busLineBO.BusStationBO1 = (List<BusStationBO>)(from busLine5 in busLineBO.BusStationBO1
                                                               select new { StationNumber1 = busLine5.StationNumber, NameOfStation1 = busLine5.NameOfStation, RoofToTheStation1 = busLine5.RoofToTheStation, AccessForDisabled1 = busLine5.AccessForDisabled });

                for (int i = 0; i < busLineBO.StationLineBOs.Count - 1; i++)
                {
                    busLineBO.DistanceBetweenTooStationsList.Add(dl.DistanceBetweenTooStations(busLineBO.StationLineBOs[i].StationNumberOnLine, busLineBO.StationLineBOs[i + 1].StationNumberOnLine));
                }
                for (int i = 0; i < busLineBO.StationLineBOs.Count - 1; i++)
                {
                    busLineBO.AverageTimeBetweenTooStationsList.Add(dl.AverageTimeBetweenTooStationsList(busLineBO.StationLineBOs[i].StationNumberOnLine, busLineBO.StationLineBOs[i + 1].StationNumberOnLine));
                }
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
        // הוספת קו בודד 
        void IBL1.AddBusLineBO(BusLineBO busLineBO)
        {
            // לבדוק תקינות הנתון
            try
            {
                // && busLineBO.BusStationBO1.FindAll(item => item.IsAvailable3 == true)
                if (busLineBO.StationLineBOs.Count > 2)
                {
                    for (int i = 0; i < BusStationBO1.Count - 1; i++)
                    {
                        busLineBO.ConsecutiveStationsBos.Add(new ConsecutiveStationsBl(BusStationBO1[i].StationNumber, BusStationBO1[i + 1].StationNumber));
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
                        FirstStation = busLineBO.BusStationBO1[0].StationNumber,
                        LastStation = busLineBO.BusStationBO1[busLineBO.BusStationBO1.Count].StationNumber,
                        IsAvailable1 = busLineBO.IsAvailable1
                    };
                    busLineBO.BusLineID1 = dl.AddBusLine(busLine);

                    IEnumerable<LineStation> s = from AddLineStationToDL in busLineBO.StationLineBOs
                                                 let p = ReturnNewLineStationDoFromBo(AddLineStationToDL)
                                                 select p;
                    foreach (LineStation item in s)
                    {
                        dl.AddLineStation(item);
                    }

                    for (int i = 0; i < busLineBO.LineExitBos1.Count; i++)
                    {
                        LineExit lineExit = new LineExit
                        {
                            BusLineID1 = busLineBO.LineExitBos1[i].BusLineID2,
                            LineStartTime = busLineBO.LineExitBos1[i].LineStartTime,
                            LineFrequency = busLineBO.LineExitBos1[i].LineFrequency
                        };
                        dl.AddLineExit(lineExit);
                    }
                    if (busLineBO.LineExitBos1.Count > 0)
                    {
                        for (int i = 0; i < busLineBO.LineExitBos1.Count; i++)
                        {
                            busLineBO.LineExitBos1[i].LineFinishTime = busLineBO.UrbanInterUrban
                                ? int.Parse(busLineBO.LineExitBos1[i].LineStartTime)
                                    + (int)busLineBO.ConsecutiveStationsBos.Sum(item => item.AverageTime) + D.Next(10, 30)
                                : int.Parse(busLineBO.LineExitBos1[i].LineStartTime)
                                    + (int)busLineBO.ConsecutiveStationsBos.Sum(item => item.AverageTime);
                        }
                    }
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

        public void AddStationToLine(StationLineBO stationLineBO)
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
                int number = dl.IndexOfLastLineStation(LineNumber);
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

        public void DeleteStationFromLine(int LineNumber, int NumberStationToDelete, int DeleteFromConsecutiveStations)
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
            };
            lineStation.ChackDelete2 = func1.ChackDelete2;
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
                dl.DeleteLineExit(LineNumber, startLineExit);
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
            return dl.FindUser(pass, UserNam);
        }
        #endregion User
    }
}


















/// <summary>
/// proprty to list.
/// </summary>
//public List<BusStationBO> RouteTheLine
//{
//    get => routeTheLine;
//    set => routeTheLine = value;
//}
//public bool UrbanInterUrban1
//{
//    get { return UrbanInterUrban; }
//    set { myVar = value; }
//}

//public Area Area1
//{
//    get { return myVar; }
//    //UrbanInterUrban
//    set {
//        if ()
//        {

//        }
//        //myVar = value; 
//    }
//}


//void IBL1.AddBusStationBO(BusStationBO busStationBO)
//{
//    // לבדוק תקינות הנתון

//    BusStation busStation = new BusStation();
//    busStationBO.CopyPropertiesTo(busStation);
//    try
//    {
//        dl.AddStation(busStation);
//    }
//    catch (Exception)
//    {

//        throw;
//    }
//}
