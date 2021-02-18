using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using DLAPI;
using DO;

namespace DL
{
    internal class DLXML : IDAL
    {
        public static string busXml = @"BusXml.xml";
        public static string stationXml = @"StationXml.xml";
        public static string LineXml = @"LineXml.xml";
        public static string ConsecutiveStationsXml = @"ConsecutiveStations.xml";
        public static string LineStationXml = @"LineStation.xml";
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { /*InitializationLineStation();*/ }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion singelton

        #region  User
        string userXml = @"UserXml.xml";
        public void AddUser(User user)
        {
            XElement element = XMLTools.LoadListFromXMLElement(userXml);
            XElement user1 = (from p in element.Elements()
                              where p.Element("Username").Value == user.Username
                              select p).FirstOrDefault();
            if (user1 != null)
            {
                throw new ExceptionUser(user.Username, "the User alrdy exist in the compny!!!");
            }
            XElement personElem = new XElement("User", new XElement("Username", user.Username),
                                  new XElement("Password", user.Password),
                                  new XElement("Permission1", user.Permission1),
                                  new XElement("ManagementPermission", user.ManagementPermission),
                                  new XElement("ChackDelete", user.ChackDelete));
            element.Add(personElem);

            XMLTools.SaveListToXMLElement(element, userXml);
        }
        public void DeleteUser(string Username1)
        {
            XElement element = XMLTools.LoadListFromXMLElement(userXml);
            XElement user1 = (from p in element.Elements()
                              where p.Element("Username").Value == Username1
                              select p).FirstOrDefault();
            if (user1 == null)
            {
                throw new ExceptionUser(Username1, "the user not exist in the list!!!");
            }
            else
            {
                user1.Remove();
                XMLTools.SaveListToXMLElement(element, userXml);
            }
        }
        public void UpdatingUser(User user)
        {
            XElement element = XMLTools.LoadListFromXMLElement(userXml);
            XElement user1 = (from p in element.Elements()
                              where p.Element("Username").Value == user.Username
                              select p).FirstOrDefault();

            if (user1 != null)
            {
                user1.Element("Password").Value = user.Password;
                user1.Element("Permission1").Value = user.Permission1.ToString();
                user1.Element("ManagementPermission").Value = user.ManagementPermission.ToString();
                user1.Element("ChackDelete").Value = user.ChackDelete.ToString();
                XMLTools.SaveListToXMLElement(element, userXml);
            }
            else
            {
                throw new ExceptionUser(user.Username, "The user not exist in the compny!!!");
            }
        }
        public User ReturnUser(string Username1)
        {
            XElement element = XMLTools.LoadListFromXMLElement(userXml);

            User user1 = (from user in element.Elements()
                          where user.Element("Username").Value == Username1
                          select new User()
                          {
                              Username = user.Element("Username").Value,
                              Password = user.Element("Password").Value,
                              Permission1 = (Permission)Enum.Parse(typeof(Permission), user.Element("Permission1").Value),
                              ManagementPermission = bool.Parse(user.Element("ManagementPermission").Value),
                              ChackDelete = bool.Parse(user.Element("ChackDelete").Value)
                          }
                        ).FirstOrDefault();

            return user1 ?? throw new ExceptionUser(Username1, "the user not exist in the list!!!");
        }
        public IEnumerable<User> UseresList()
        {
            XElement element = XMLTools.LoadListFromXMLElement(userXml);

            return from user in element.Elements()
                   let p = new User()
                   {
                       Username = user.Element("Username").Value,
                       Password = user.Element("Password").Value,
                       Permission1 = (Permission)Enum.Parse(typeof(Permission), user.Element("Permission1").Value),
                       ManagementPermission = bool.Parse(user.Element("ManagementPermission").Value),
                       ChackDelete = bool.Parse(user.Element("ChackDelete").Value)
                   }
                   where p.ChackDelete
                   select p;
        }
        public bool FindUser(string pass, string UserNam)
        {
            XElement element = XMLTools.LoadListFromXMLElement(userXml);
            User user1 = (from user in element.Elements()
                          where user.Element("Username").Value == UserNam && user.Element("Password").Value == pass
                          select new User()
                          {
                              Username = user.Element("Username").Value,
                              Password = user.Element("Password").Value,
                              Permission1 = (Permission)Enum.Parse(typeof(Permission), user.Element("Permission1").Value),
                              ManagementPermission = bool.Parse(user.Element("ManagementPermission").Value),
                              ChackDelete = bool.Parse(user.Element("ChackDelete").Value)
                          }
                        ).FirstOrDefault();
            return user1 != null && user1.ChackDelete && user1.Permission1 == Permission.ManagementPermission
                ? true : throw new ExceptionUser(UserNam, "שם משתמש שגוי!!!");
        }
        #endregion User

        #region Bus

        public void AddBus(Bus bus)
        {
            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);

            if (buses.Exists(bus1 => bus1.License_number == bus.License_number && bus1.IsAvailable == true))
            {
                throw new ExceptionBus(bus.License_number, $"bad bus id {bus.License_number}");
            }
            else
            {
                buses.Add(bus);
                XMLTools.SaveListToXMLSerializer(buses, busXml);
            }
        }
        public void DeleteBus(string License_number)
        {
            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);
            int index = buses.FindIndex(item => item.License_number == License_number && item.IsAvailable);
            buses[index].IsAvailable = index == -1 ? throw new ExceptionBus(License_number, $"bad bus id {License_number}") : false;
            XMLTools.SaveListToXMLSerializer(buses, busXml);
        }
        public void UpdatingBus(Bus bus)
        {
            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);
            int index = buses.FindIndex(bus1 => bus1.License_number == bus.License_number);
            buses[index] = index == -1 ? throw new ExceptionBus(bus.License_number, $"bad bus id {bus.License_number}") : bus;
            XMLTools.SaveListToXMLSerializer(buses, busXml);
        }
        public Bus ReturnBusToBl(string License_number)
        {
            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);
            Bus bus = buses.Find(bus1 => bus1.License_number == License_number);
            return bus ?? throw new ExceptionBus(License_number, $"bad bus id {License_number}");
        }
        public IEnumerable<Bus> BusLists()
        {
            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);
            return from bus in buses
                   where bus.IsAvailable
                   select bus;
        }

        #endregion Bus

        #region Station
        public void AddStation(BusStation station)
        {
            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
            if (busStations.Exists(station1 => station1.StationNumber == station.StationNumber && station1.IsAvailable3))
            {
                throw new ExceptionStation(station.StationNumber, "bad id - The Station alrady exist in the compny: {station.StationNumber}");
            }
            else
            {
                busStations.Add(station);
            }
            XMLTools.SaveListToXMLSerializer(busStations, stationXml);
        }
        public void DeleteStation(int numberStation)
        {
            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
            int index = busStations.FindIndex(station1 => station1.StationNumber == numberStation && station1.IsAvailable3);
            if (index == -1)
            {
                throw new ExceptionStation(numberStation, "bad id - The Station not exist in the compny: {numberStation}");
            }
            else
            {
                busStations[index].IsAvailable3 = true ? false : throw new ExceptionStation(numberStation, "The station exists but has already been deleted");
                XMLTools.SaveListToXMLSerializer(busStations, stationXml);
                //XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);
                //foreach (XElement item in element.Elements().Where(p => p.Element("StationNumber1").Value == numberStation.ToString() || p.Element("StationNumber2").Value == numberStation.ToString()))
                //{
                //    item.Remove();
                //}
                //XMLTools.SaveListToXMLElement(element, ConsecutiveStationsXml);

                //busStations[index].IsAvailable3 = true ? false : throw new ExceptionStation(numberStation, "The station exists but has already been deleted");

                //List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
                //foreach (LineStation item in lineStations.Where(item => item.StationNumberOnLine == numberStation))
                //{
                //    item.ChackDelete2 = false;
                //}
                //XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
            }
        }
        public void UpdatingStation(BusStation station)
        {
            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
            int index = busStations.FindIndex(station1 => station1.StationNumber == station.StationNumber);
            busStations[index] = index == -1 ? throw new ExceptionStation(station.StationNumber, "bad id - The Station not exist in the compny: {station.StationNumber}") : station;
            XMLTools.SaveListToXMLSerializer(busStations, stationXml);
        }
        public BusStation ReturnStation(int numberStation)
        {
            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
            BusStation station = busStations.Find(station1 => station1.StationNumber == numberStation);
            return station ?? throw new ExceptionStation(numberStation, "bad id - The Station not exist in the compny: {numberStation}");
        }
        public IEnumerable<BusStation> StationList()
        {
            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
            return from station in busStations
                   where station.IsAvailable3 == true
                   select station;
        }
        #endregion Station

        #region BusLine
        public int BusLineId()
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            return busLines.Count;
        }

        public int AddBusLine(BusLine line)
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            RunNumbers.BusLineID = busLines[busLines.Count - 1].BusLineID1 + 1;
            line.BusLineID1 = RunNumbers.BusLineID;
            busLines.Add(line);
            XMLTools.SaveListToXMLSerializer(busLines, LineXml);
            return line.BusLineID1;
        }
        public void DeleteBusLine(int BusLineID)
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            int index = busLines.FindIndex(BusLine => BusLine.BusLineID1 == BusLineID);
            busLines[index].GetAvailable = index == -1
                ? throw new ExceptionLine(BusLineID, " bad id line - The line not exist in the compny!!!")
                : busLines[index].GetAvailable == Available.Notavailable
                    ? throw new ExceptionLine(BusLineID, " bad id line - The Line exists but has already been deleted!!!")
                    : Available.Notavailable;
            XMLTools.SaveListToXMLSerializer(busLines, LineXml);
        }
        public void UpdatingBusLine(BusLine line)
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            int index = busLines.FindIndex(line1 => line1.BusLineID1 == line.BusLineID1);
            busLines[index] = index == -1 ? throw new ExceptionLine(line.BusLineID1, " bad id line - The BusLine not exist in the compny!!!") : line;
            XMLTools.SaveListToXMLSerializer(busLines, LineXml);
        }
        public BusLine ReturnBusLine(int numberLineId)
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            BusLine busLine = busLines.Find(line => line.BusLineID1 == numberLineId);
            return busLine ?? throw new ExceptionLine(numberLineId, "bad id line - The line not exist in the compny!!!");
        }
        public IEnumerable<BusLine> BusLinesList()
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            return from line in busLines
                   where line.GetAvailable == Available.Available
                   select line;
        }
        public IEnumerable<BusLine> BusLinesList(int numberLine)
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            return from line in busLines
                   where line.GetAvailable == Available.Available && line.BusLineID1 == numberLine
                   select line;
        }
        #endregion BusLine

        #region LineStation
        public void AddLineStation(LineStation lineStation)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);

            if (lineStations.Exists(lineStation1 => lineStation1.BusLineID2 == lineStation.BusLineID2 && lineStation1.StationNumberOnLine == lineStation.StationNumberOnLine && lineStation1.ChackDelete2))
            {
                throw new ExceptionLineStation(lineStation.BusLineID2, lineStation.StationNumberOnLine, "the Station line alrady exist in the this line!!!");
            }
            else
            {
                lineStations.Add(lineStation);
                XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
            }
        }
        public void DeleteLineStation1(int NumberLine)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            int index = lineStations.FindIndex(lineStation => lineStation.BusLineID2 == NumberLine && lineStation.ChackDelete2);
            _ = index != -1
                ? lineStations.RemoveAll(item => item.BusLineID2 == NumberLine)
                : throw new ExceptionLineStation(NumberLine, NumberLine, "the line isnt exist in the list!!!");
            XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
        }
        public void DeleteOneLineStation(int NumberLine, int stationNumber)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            int index = lineStations.FindIndex(lineStation => lineStation.BusLineID2 == NumberLine && lineStation.StationNumberOnLine == stationNumber);
            if (index != -1 && !lineStations[index].ChackDelete2)
            {
                throw new ExceptionLineStation(NumberLine, stationNumber, "the station found but she deleted!!!");
            }

            if (index != -1 && lineStations[index].ChackDelete2)
            {
                lineStations[index].ChackDelete2 = false;
            }

            if (index == -1)
            {
                throw new ExceptionLineStation(NumberLine, stationNumber, "the line station isnt exist in the compny!!!");
            }
            XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
        }
        public void DeleteLineStation(int NumberLine)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            int index = lineStations.FindIndex(lineStation => lineStation.BusLineID2 == NumberLine && lineStation.ChackDelete2);
            if (index != -1)
            {
                foreach (LineStation item in lineStations)
                {
                    if (item.BusLineID2 == NumberLine && item.ChackDelete2)
                    {
                        item.ChackDelete2 = false;
                    }
                }
                XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
            }
            else
            {
                throw new ExceptionLineStation(NumberLine, NumberLine, "the line isnt exist in the list!!!");
            }
        }
        public void UpdatingLineStation(LineStation lineStation)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            int index = lineStations.FindIndex(lineStation1 => lineStation1.BusLineID2 == lineStation.BusLineID2 && lineStation1.StationNumberOnLine == lineStation.StationNumberOnLine);
            lineStations[index] = index == -1 ? throw new ExceptionLineStation(lineStation.BusLineID2, lineStation.StationNumberOnLine, "The lineStation not exist in the compny!!!") : lineStation;
            XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
        }
        public LineStation ReturnLineStation(int numberLine, int stationNumber)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            LineStation lineStation1 = lineStations.Find(lineStation => lineStation.BusLineID2 == numberLine && lineStation.StationNumberOnLine == stationNumber);
            return lineStation1 ?? throw new ExceptionLineStation(numberLine, stationNumber, "The lineStation not exist in the compny!!!");
        }
        public IEnumerable<LineStation> LineStationList()
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            return from line in lineStations
                   where line.ChackDelete2
                   select line;
        }
        public IEnumerable<LineStation> OneLineFromList(Predicate<LineStation> predicate)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            return from line in lineStations
                   where predicate(line)
                   select line ?? throw new ExceptionLineStation(line.BusLineID2, line.StationNumberOnLine, "the line dsnt exist in the compny");
        }
        public IEnumerable<int> LinesFromList(int numberStation)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            IEnumerable<int> Lines = from line in lineStations
                                     where line.StationNumberOnLine == numberStation && line.ChackDelete2
                                     select line.BusLineID2;
            return Lines ?? throw new ExceptionLineStation(numberStation, numberStation, "there is no lines that pass in this station!!!");
        }
        #endregion LineStation

        #region ConsecutiveStations

        public void AddConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);
            XElement consecutiveStations1 = (from p in element.Elements()
                                             where p.Element("StationNumber1").Value == consecutiveStations.StationNumber1.ToString() && p.Element("StationNumber2").Value == consecutiveStations.StationNumber2.ToString()
                                             select p).FirstOrDefault();
            if (consecutiveStations1 != null)
            {
                throw new ExceptionConsecutiveStations(consecutiveStations.StationNumber1, consecutiveStations.StationNumber2, "There are already two such stations on the list!!!");
            }

            XElement consecutive = new XElement("ConsecutiveStations", new XElement("StationNumber1", consecutiveStations.StationNumber1),
                                   new XElement("StationNumber2", consecutiveStations.StationNumber2),
                                   new XElement("DistanceBetweenTooStations", consecutiveStations.DistanceBetweenTooStations.ToString()),
                                   new XElement("AverageTime", consecutiveStations.AverageTime.ToString()));
            element.Add(consecutive);

            XMLTools.SaveListToXMLElement(element, ConsecutiveStationsXml);
        }
        public void DeleteConsecutiveStations(int stationNumber1, int stationNumber2)
        {
            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);
            XElement consecutiveStations1 = (from p in element.Elements()
                                             where p.Element("StationNumber1").Value == stationNumber1.ToString() && p.Element("StationNumber2").Value == stationNumber2.ToString()
                                             select p).FirstOrDefault();
            if (consecutiveStations1 != null)
            {
                //throw new ExceptionConsecutiveStations(stationNumber1, stationNumber2, "There are no two such stations on the list!!!");
                consecutiveStations1.Remove();
                XMLTools.SaveListToXMLElement(element, ConsecutiveStationsXml);
            }
        }
        public void UpdatingConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);
            XElement consecutiveStations1 = (from p in element.Elements()
                                             where p.Element("StationNumber1").Value == consecutiveStations.StationNumber1.ToString() && p.Element("StationNumber2").Value == consecutiveStations.StationNumber2.ToString()
                                             select p).FirstOrDefault();

            if (consecutiveStations1 != null)
            {
                consecutiveStations1.Element("StationNumber1").Value = consecutiveStations.StationNumber1.ToString();
                consecutiveStations1.Element("StationNumber2").Value = consecutiveStations.StationNumber2.ToString();
                consecutiveStations1.Element("DistanceBetweenTooStations").Value = consecutiveStations.DistanceBetweenTooStations.ToString();
                consecutiveStations1.Element("AverageTime").Value = consecutiveStations.AverageTime.ToString();
                XMLTools.SaveListToXMLElement(element, ConsecutiveStationsXml);
            }
            else
            {
                throw new ExceptionConsecutiveStations(consecutiveStations.StationNumber1, consecutiveStations.StationNumber2, "The consecutiveStations not exist in the compny!!!");
            }
        }
        public ConsecutiveStations ReturnConsecutiveStation(int stationNumber1, int stationNumber2)
        {
            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);
            ConsecutiveStations consecutiveStations = (from p in element.Elements()
                                                       where p.Element("StationNumber1").Value == stationNumber1.ToString() && p.Element("StationNumber2").Value == stationNumber2.ToString()
                                                       select new ConsecutiveStations()
                                                       {
                                                           StationNumber1 = int.Parse(p.Element("StationNumber1").Value),
                                                           StationNumber2 = int.Parse(p.Element("StationNumber2").Value),
                                                           DistanceBetweenTooStations = float.Parse(p.Element("DistanceBetweenTooStations").Value),
                                                           AverageTime = TimeSpan.ParseExact(p.Element("AverageTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                                                       }).FirstOrDefault();

            return consecutiveStations ?? throw new ExceptionConsecutiveStations(stationNumber1, stationNumber2, "There are no two such stations on the list!!!");
        }
        public IEnumerable<ConsecutiveStations> ConsecutiveStationsList()
        {
            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);

            return from p in element.Elements()
                   let s = new ConsecutiveStations()
                   {
                       StationNumber1 = int.Parse(p.Element("StationNumber1").Value),
                       StationNumber2 = int.Parse(p.Element("StationNumber2").Value),
                       DistanceBetweenTooStations = float.Parse(p.Element("DistanceBetweenTooStations").Value),
                       AverageTime = TimeSpan.ParseExact(p.Element("AverageTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                   }
                   select s;
        }
        public IEnumerable<ConsecutiveStations> ConsecutiveStationsListPredicate(Predicate<ConsecutiveStations> predicate)
        {
            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);

            return from p in element.Elements()
                   let s = new ConsecutiveStations()
                   {
                       StationNumber1 = int.Parse(p.Element("StationNumber1").Value),
                       StationNumber2 = int.Parse(p.Element("StationNumber2").Value),
                       DistanceBetweenTooStations = float.Parse(p.Element("DistanceBetweenTooStations").Value),
                       AverageTime = TimeSpan.ParseExact(p.Element("AverageTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                   }
                   where predicate(s)
                   select s;
        }
        public bool ChackExistingConsecutiveStations(Predicate<ConsecutiveStations> predicate)
        {
            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);
            List<ConsecutiveStations> consecutives = (from p in element.Elements()
                                                      let s = new ConsecutiveStations()
                                                      {
                                                          StationNumber1 = int.Parse(p.Element("StationNumber1").Value),
                                                          StationNumber2 = int.Parse(p.Element("StationNumber2").Value),
                                                          DistanceBetweenTooStations = float.Parse(p.Element("DistanceBetweenTooStations").Value),
                                                          AverageTime = TimeSpan.ParseExact(p.Element("AverageTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                                                      }
                                                      select s).ToList();
            return !consecutives.Exists(predicate);
        }
        public float DistanceBetweenTooStations(int numberStation1, int numberStation2)
        {
            List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
            int index = consecutives.FindIndex(consecutiveStations1 => consecutiveStations1.StationNumber1 == numberStation1 && consecutiveStations1.StationNumber2 == numberStation2);
            return index != -1
                ? consecutives[index].DistanceBetweenTooStations
                : throw new ExceptionConsecutiveStations(numberStation1, numberStation2, "There are no two such stations on the list!!!");
        }
        public TimeSpan AverageTimeBetweenTooStationsList(int numberStation1, int numberStation2)
        {
            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);
            ConsecutiveStations consecutiveStations = (from p in element.Elements()
                                                       where p.Element("StationNumber1").Value == numberStation1.ToString() && p.Element("StationNumber2").Value == numberStation2.ToString()
                                                       select new ConsecutiveStations()
                                                       {
                                                           StationNumber1 = int.Parse(p.Element("StationNumber1").Value),
                                                           StationNumber2 = int.Parse(p.Element("StationNumber2").Value),
                                                           DistanceBetweenTooStations = float.Parse(p.Element("DistanceBetweenTooStations").Value),
                                                           AverageTime = TimeSpan.ParseExact(p.Element("AverageTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                                                       }).FirstOrDefault();

            return consecutiveStations == null ? throw new ExceptionConsecutiveStations(numberStation1, numberStation2, "There are no two such stations on the list!!!") : consecutiveStations.AverageTime;
        }

        #endregion ConsecutiveStations

        #region LineExit
        string LineExitXml = @"LineExit.xml";
        public void AddLineExit(LineExit lineExit)
        {
            XElement element = XMLTools.LoadListFromXMLElement(LineExitXml);
            XElement lineExit1 = (from p in element.Elements()
                                  where p.Element("BusLineID1").Value == lineExit.BusLineID1.ToString() && p.Element("LineStartTime").Value == lineExit.LineStartTime.ToString()
                                  select p).FirstOrDefault();
            if (lineExit1 != null)
            {
                throw new ExceptionLineExit(lineExit.BusLineID1, lineExit.LineStartTime, "the Exit alrdy exist in the list in the same time");
            }

            XElement lineExit2 = new XElement("LineExit", new XElement("BusLineID1", lineExit.BusLineID1),
                                   new XElement("LineStartTime", lineExit.LineStartTime.ToString()),
                                   new XElement("LineFinishTime", lineExit.LineFinishTime.ToString()),
                                   new XElement("LineFrequencyTime", lineExit.LineFrequencyTime.ToString()),
                                   new XElement("LineFrequency", lineExit.LineFrequency));

            element.Add(lineExit2);
            XMLTools.SaveListToXMLElement(element, LineExitXml);

            //List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
            //if (lineExits.Exists(lineExit1 => lineExit1.BusLineID1 == lineExit.BusLineID1 && lineExit1.LineStartTime == lineExit.LineStartTime))
            //{
            //    throw new ExceptionLineExit(lineExit.BusLineID1, lineExit.LineStartTime, "the LineExit alrdy exist in the list in the same time");
            //}
            //else
            //{
            //    lineExits.Add(lineExit);
            //    XMLTools.SaveListToXMLSerializer(lineExits, LineExitXml);
            //}
        }
        public void DeleteLineExit(int lineNumber, TimeSpan StartTime)
        {
            XElement element = XMLTools.LoadListFromXMLElement(LineExitXml);
            XElement lineExit1 = (from p in element.Elements()
                                  where p.Element("BusLineID1").Value == lineNumber.ToString() && p.Element("LineStartTime").Value == StartTime.ToString()
                                  select p).FirstOrDefault();
            if (lineExit1 == null)
            {
                throw new ExceptionLineExit(lineNumber, StartTime, "the Exit not found!!!");
            }
            lineExit1.Remove();
            XMLTools.SaveListToXMLElement(element, LineExitXml);
        }
        public void UpdatingLineExit(LineExit lineExit)
        {
            XElement element = XMLTools.LoadListFromXMLElement(LineExitXml);
            XElement lineExit1 = (from p in element.Elements()
                                  where p.Element("BusLineID1").Value == lineExit.BusLineID1.ToString() && p.Element("LineStartTime").Value == lineExit.LineStartTime.ToString()
                                  select p).FirstOrDefault();

            if (lineExit1 != null)
            {
                lineExit1.Element("BusLineID1").Value = lineExit.BusLineID1.ToString();
                lineExit1.Element("LineStartTime").Value = lineExit.LineStartTime.ToString();
                lineExit1.Element("LineFinishTime").Value = lineExit.LineFinishTime.ToString();
                lineExit1.Element("LineFrequency").Value = lineExit.LineFrequency.ToString();
                lineExit1.Element("LineFrequencyTime").Value = lineExit.LineFrequencyTime.ToString();
                XMLTools.SaveListToXMLElement(element, LineExitXml);
            }

            else
            {
                throw new ExceptionLineExit(lineExit.BusLineID1, lineExit.LineStartTime, "The Exit not exist in the compny");
            }
        }
        public LineExit ReturnLineExit(int lineNumber, TimeSpan StartTime)
        {
            XElement element = XMLTools.LoadListFromXMLElement(LineExitXml);
            LineExit lineExit1 = (from p in element.Elements()
                                  where p.Element("BusLineID1").Value == lineNumber.ToString() && p.Element("LineStartTime").Value == StartTime.ToString()
                                  select new LineExit()
                                  {
                                      BusLineID1 = int.Parse(p.Element("BusLineID1").Value),
                                      LineStartTime = TimeSpan.ParseExact(p.Element("LineStartTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                      LineFinishTime = TimeSpan.ParseExact(p.Element("LineFinishTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                      LineFrequencyTime = TimeSpan.ParseExact(p.Element("LineFrequencyTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                      LineFrequency = int.Parse(p.Element("LineFrequency").Value)
                                  }).FirstOrDefault();
            return lineExit1 ?? throw new ExceptionLineExit(lineNumber, StartTime, "the Exit not exist in the list");
        }
        public LineExit OneLineExitFromList(int numberLine, TimeSpan StartTime)
        {
            XElement element = XMLTools.LoadListFromXMLElement(LineExitXml);
            LineExit lineExit1 = (from p in element.Elements()
                                  where p.Element("BusLineID1").Value == numberLine.ToString() && p.Element("LineStartTime").Value == StartTime.ToString()
                                  select new LineExit()
                                  {
                                      BusLineID1 = int.Parse(p.Element("BusLineID1").Value),
                                      LineStartTime = TimeSpan.ParseExact(p.Element("LineStartTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                      LineFinishTime = TimeSpan.ParseExact(p.Element("LineFinishTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                      LineFrequencyTime = TimeSpan.ParseExact(p.Element("LineFrequencyTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                      LineFrequency = int.Parse(p.Element("LineFrequency").Value)
                                  }).FirstOrDefault();
            return lineExit1 ?? throw new ExceptionLineExit(numberLine, StartTime, "the Exit not exist in the list");
        }
        public IEnumerable<LineExit> LineExitList(int numberLine)
        {
            XElement element = XMLTools.LoadListFromXMLElement(LineExitXml);
            return from p in element.Elements()
                   where p.Element("BusLineID1").Value == numberLine.ToString()
                   let s = new LineExit()
                   {
                       BusLineID1 = int.Parse(p.Element("BusLineID1").Value),
                       LineStartTime = TimeSpan.ParseExact(p.Element("LineStartTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                       LineFinishTime = TimeSpan.ParseExact(p.Element("LineFinishTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                       LineFrequencyTime = TimeSpan.ParseExact(p.Element("LineFrequencyTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                       LineFrequency = int.Parse(p.Element("LineFrequency").Value)
                   }
                   select s;
        }
        #endregion LineExit

        #region BusTraveling
        //void IDAL.AddBusTraveling(BusTraveling busTraveling)
        //{
        //    if (DataSource.BusTravelings.Exists(busTraveling1 => busTraveling.LineInExecution == busTraveling.LineInExecution && busTraveling.LeavingTime == busTraveling.LeavingTime))
        //    {
        //        throw new ExceptionDl("the BusTraveling alrdy exist in the list whis the same Data!!!");
        //    }
        //    else
        //    {
        //        busTraveling.IdBusTraveling = NumbersAreRunning.U_TravelID;
        //        NumbersAreRunning.U_TravelID++;
        //        DataSource.BusTravelings.Add(busTraveling.Clone());
        //    }
        //}

        //public void DeleteBusTraveling(int LineExecution, string License_number, string LeavingTime)
        //{
        //    int index = DataSource.BusTravelings.FindIndex(BusTravelings1 => BusTravelings1.License_number1 == License_number && BusTravelings1.LineInExecution == LineExecution && BusTravelings1.LeavingTime == LeavingTime);
        //    if (index == -1)
        //    {
        //        throw new ExceptionDl("the BusTraveling not exist!!!");
        //    }
        //    else
        //    {
        //        DataSource.LineExits.RemoveAt(index);
        //    }
        //}
        //void IDAL.UpdatingBusTraveling(BusTraveling busTraveling)
        //{
        //    int index = DataSource.BusTravelings.FindIndex(BusTravelings1 => BusTravelings1.License_number1 == busTraveling.License_number1 && BusTravelings1.LineInExecution == busTraveling.LineInExecution && BusTravelings1.LeavingTime == busTraveling.LeavingTime);
        //    DataSource.BusTravelings[index] = index == -1 ? throw new ExceptionDl("The busTraveling not exist in the compny!!!") : busTraveling.Clone();
        //}
        //public BusTraveling ReturnBusTraveling(int LineExecution, string License_number, string LeavingTime)
        //{
        //    BusTraveling busTraveling = DataSource.BusTravelings.FirstOrDefault(BusTravelings1 => BusTravelings1.License_number1 == License_number && BusTravelings1.LineInExecution == LineExecution && BusTravelings1.LeavingTime == LeavingTime);
        //    return busTraveling.Clone(); ?? throw new ExceptionDl("the busTraveling not exist in the list!!!");
        //}

        //public IEnumerable<BusTraveling> BusTravelingList()
        //{
        //    return from busTraveling in DataSource.BusTravelings
        //           select busTraveling;
        //}
        #endregion BusTraveling
    }
}