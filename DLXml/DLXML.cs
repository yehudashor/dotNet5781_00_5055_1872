//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Threading.Tasks;
//using System.Xml.Linq;
//using DLAPI;
//using DO;
////using DO;

//namespace DL
//{
//    sealed class DLXML : IDAL    //internal
//    {
//        #region singelton
//        static readonly DLXML instance = new DLXML();
//        static DLXML() { }// static ctor to ensure instance init is done just before first usage
//        DLXML() { } // default => private
//        public static DLXML Instance { get => instance; }// The public Instance property to use
//        #endregion singelton

//        #region  User
//        string userXml = @"UserXml.xml";
//        void IDAL.AddUser(User user)
//        {
//            XElement element = XMLTools.LoadListFromXMLElement(userXml);
//            XElement user1 = (from p in element.Elements()
//                              where p.Element("ID").Value == user.Username
//                              select p).FirstOrDefault();
//            if (user1 != null)
//            {
//                throw new ExceptionUser(user.Username, "the User alrdy exist in the compny!!!");
//            }
//            XElement personElem = new XElement("Username", new XElement("ID", user.Username),
//                                  new XElement("Password", user.Password),
//                                  new XElement("Permission1", user.Permission1),
//                                  new XElement("ManagementPermission", user.ManagementPermission),
//                                  new XElement("ChackDelete", user.ChackDelete));
//            element.Add(personElem);

//            XMLTools.SaveListToXMLElement(element, userXml);
//        }
//        void IDAL.DeleteUser(string Username1)
//        {
//            XElement element = XMLTools.LoadListFromXMLElement(userXml);
//            XElement user1 = (from p in element.Elements()
//                              where p.Element("ID").Value == Username1
//                              select p).FirstOrDefault();
//            if (user1 == null)
//            {
//                throw new ExceptionUser(Username1, "the user not exist in the list!!!");
//            }
//            else
//            {
//                user1.Remove();
//                XMLTools.SaveListToXMLElement(element, userXml);
//            }
//        }
//        void IDAL.UpdatingUser(User user)
//        {
//            XElement element = XMLTools.LoadListFromXMLElement(userXml);
//            XElement user1 = (from p in element.Elements()
//                              where p.Element("ID").Value == user.Username
//                              select p).FirstOrDefault();

//            if (user1 != null)
//            {
//                user1.Element("Password").Value = user.Password;
//                user1.Element("Permission1").Value = user.Permission1.ToString();
//                user1.Element("ManagementPermission").Value = user.ManagementPermission.ToString();
//                user1.Element("ChackDelete").Value = user.ChackDelete.ToString();
//                XMLTools.SaveListToXMLElement(element, userXml);
//            }
//            else
//            {
//                throw new ExceptionUser(user.Username, "The user not exist in the compny!!!");
//            }
//        }
//        User IDAL.ReturnUser(string Username1)
//        {
//            XElement element = XMLTools.LoadListFromXMLElement(userXml);

//            User user1 = (from user in element.Elements()
//                          where user.Element("ID").Value == Username1
//                          select new User()
//                          {
//                              Username = user.Element("ID").Value,
//                              Password = user.Element("Password").Value,
//                              Permission1 = (Permission)Enum.Parse(typeof(Permission), user.Element("Permission1").Value),
//                              ManagementPermission = bool.Parse(user.Element("ManagementPermission").Value),
//                              ChackDelete = bool.Parse(user.Element("ChackDelete").Value)
//                          }
//                        ).FirstOrDefault();

//            return user1 ?? throw new ExceptionUser(Username1, "the user not exist in the list!!!");
//        }
//        IEnumerable<User> IDAL.UseresList()
//        {
//            XElement element = XMLTools.LoadListFromXMLElement(userXml);

//            return from user in element.Elements()
//                   let p = new User()
//                   {
//                       Username = user.Element("ID").Value,
//                       Password = user.Element("Password").Value,
//                       Permission1 = (Permission)Enum.Parse(typeof(Permission), user.Element("Permission1").Value),
//                       ManagementPermission = bool.Parse(user.Element("ManagementPermission").Value),
//                       ChackDelete = bool.Parse(user.Element("ChackDelete").Value)
//                   }
//                   where p.ChackDelete
//                   select p;
//        }
//        bool IDAL.FindUser(string pass, string UserNam)
//        {
//            XElement element = XMLTools.LoadListFromXMLElement(userXml);
//            User user1 = (from user in element.Elements()
//                          where user.Element("ID").Value == UserNam && user.Element("Password").Value == pass
//                          select new User()
//                          {
//                              Username = user.Element("ID").Value,
//                              Password = user.Element("Password").Value,
//                              Permission1 = (Permission)Enum.Parse(typeof(Permission), user.Element("Permission1").Value),
//                              ManagementPermission = bool.Parse(user.Element("ManagementPermission").Value),
//                              ChackDelete = bool.Parse(user.Element("ChackDelete").Value)
//                          }
//                        ).FirstOrDefault();
//            return user1 != null && user1.ChackDelete && user1.Permission1 == Permission.ManagementPermission
//                ? true
//                : throw new ExceptionUser(UserNam, "Rong user!!!");
//        }
//        #endregion User

//        #region Bus
//        string busXml = @"BusXml.xml";
//        void IDAL.AddBus(Bus bus)
//        {
//            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);

//            if (buses.Exists(bus1 => bus1.License_number == bus.License_number && bus1.IsAvailable == true))
//            {
//                throw new ExceptionBus(bus.License_number, $"bad bus id {bus.License_number}");
//            }
//            else
//            {
//                buses.Add(bus);
//                XMLTools.SaveListToXMLSerializer(buses, busXml);
//            }
//        }

//        void IDAL.DeleteBus(string License_number)
//        {
//            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);
//            int index = buses.FindIndex(item => item.License_number == License_number && item.IsAvailable);
//            buses[index].IsAvailable = index == -1 ? throw new ExceptionBus(License_number, $"bad bus id {License_number}") : false;
//            XMLTools.SaveListToXMLSerializer(buses, busXml);
//        }
//        void IDAL.UpdatingBus(Bus bus)
//        {
//            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);
//            int index = buses.FindIndex(bus1 => bus1.License_number == bus.License_number);
//            buses[index] = index == -1 ? throw new ExceptionBus(bus.License_number, $"bad bus id {bus.License_number}") : bus;
//            XMLTools.SaveListToXMLSerializer(buses, busXml);
//        }
//        Bus IDAL.ReturnBusToBl(string License_number)
//        {
//            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);
//            Bus bus = buses.Find(bus1 => bus1.License_number == License_number);
//            return bus ?? throw new ExceptionBus(License_number, $"bad bus id {License_number}");
//        }
//        IEnumerable<Bus> IDAL.BusLists()
//        {
//            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);
//            return from bus in buses
//                   where bus.IsAvailable
//                   select bus;
//        }
//        #endregion Bus

//        #region Station
//        string stationXml = @"StationXml.xml";
//        void IDAL.AddStation(BusStation station)
//        {
//            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
//            if (busStations.Exists(station1 => station1.StationNumber == station.StationNumber && station1.IsAvailable3))
//            {
//                throw new ExceptionStation(station.StationNumber, "bad id - The Station alrady exist in the compny: {station.StationNumber}");
//            }
//            else
//            {
//                busStations.Add(station);
//            }
//            XMLTools.SaveListToXMLSerializer(busStations, stationXml);
//        }
//        void IDAL.DeleteStation(int numberStation)
//        {
//            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
//            int index = busStations.FindIndex(station1 => station1.StationNumber == numberStation);
//            if (index == -1)
//            {
//                throw new ExceptionStation(numberStation, "bad id - The Station not exist in the compny: {numberStation}");
//            }
//            else
//            {
//                List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
//                List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
//                busStations[index].IsAvailable3 = true ? false : throw new ExceptionStation(numberStation, "The station exists but has already been deleted");
//                foreach (LineStation item in lineStations.Where(item => item.StationNumberOnLine == numberStation))
//                {
//                    item.ChackDelete2 = false;
//                }
//                XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
//                _ = consecutives.RemoveAll(item => item.StationNumber1 == numberStation || item.StationNumber2 == numberStation);
//                XMLTools.SaveListToXMLSerializer(consecutives, ConsecutiveStationsXml);
//            }
//            XMLTools.SaveListToXMLSerializer(busStations, stationXml);
//        }
//        void IDAL.UpdatingStation(BusStation station)
//        {
//            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
//            int index = busStations.FindIndex(station1 => station1.StationNumber == station.StationNumber);
//            busStations[index] = index == -1 ? throw new ExceptionStation(station.StationNumber, "bad id - The Station not exist in the compny: {station.StationNumber}") : station;
//            XMLTools.SaveListToXMLSerializer(busStations, stationXml);
//        }
//        BusStation IDAL.ReturnStation(int numberStation)
//        {
//            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
//            BusStation station = busStations.Find(station1 => station1.StationNumber == numberStation);
//            return station ?? throw new ExceptionStation(numberStation, "bad id - The Station not exist in the compny: {numberStation}");
//        }
//        IEnumerable<BusStation> IDAL.StationList()
//        {
//            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
//            return from station in busStations
//                   where station.IsAvailable3 == true
//                   select station;
//        }
//        #endregion Station

//        #region BusLine
//        string LineXml = @"LineXml.xml";
//        int IDAL.BusLineId()
//        {
//            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
//            return busLines.Count;
//        }
//        int IDAL.AddBusLine(BusLine line)
//        {
//            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
//            line.BusLineID1 = BusLine.BusLineID;
//            BusLine.BusLineID++;
//            busLines.Add(line);
//            XMLTools.SaveListToXMLSerializer(busLines, LineXml);
//            return line.BusLineID1;
//        }
//        void IDAL.DeleteBusLine(int BusLineID)
//        {
//            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
//            int index = busLines.FindIndex(BusLine => BusLine.BusLineID1 == BusLineID);
//            busLines[index].GetAvailable = index == -1
//                ? throw new ExceptionLine(BusLineID, " bad id line - The line not exist in the compny!!!")
//                : busLines[index].GetAvailable == Available.Notavailable
//                    ? throw new ExceptionLine(BusLineID, " bad id line - The Line exists but has already been deleted!!!")
//                    : Available.Notavailable;
//            XMLTools.SaveListToXMLSerializer(busLines, LineXml);
//        }
//        void IDAL.UpdatingBusLine(BusLine line)
//        {
//            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
//            int index = busLines.FindIndex(line1 => line1.BusLineID1 == line.BusLineID1);
//            busLines[index] = index == -1 ? throw new ExceptionLine(line.BusLineID1, " bad id line - The BusLine not exist in the compny!!!") : line;
//            XMLTools.SaveListToXMLSerializer(busLines, LineXml);
//        }
//        BusLine IDAL.ReturnBusLine(int numberLineId)
//        {
//            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
//            BusLine busLine = busLines.Find(line => line.BusLineID1 == numberLineId);
//            return busLine ?? throw new ExceptionLine(numberLineId, "bad id line - The line not exist in the compny!!!");
//        }
//        IEnumerable<BusLine> IDAL.BusLinesList()
//        {
//            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
//            return from line in busLines
//                   where line.GetAvailable == Available.Available
//                   select line;
//        }
//        IEnumerable<BusLine> IDAL.BusLinesList(int numberLine)
//        {
//            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
//            return from line in busLines
//                   where line.GetAvailable == Available.Available && line.BusLineID1 == numberLine
//                   select line;
//        }
//        #endregion BusLine

//        #region LineStation
//        string LineStationXml = @"LineStation.xml";
//        void IDAL.AddLineStation(LineStation lineStation)
//        {
//            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
//            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
//            if (busStations.Exists(station1 => station1.StationNumber == lineStation.StationNumberOnLine && !station1.IsAvailable3))
//            {
//                throw new ExceptionLineStation(lineStation.BusLineID2, lineStation.StationNumberOnLine, "The Station not exist in the compny");
//            }

//            if (lineStations.Exists(lineStation1 => lineStation1.BusLineID2 == lineStation.BusLineID2 && lineStation1.StationNumberOnLine == lineStation.StationNumberOnLine && lineStation1.ChackDelete2))
//            {
//                throw new ExceptionLineStation(lineStation.BusLineID2, lineStation.StationNumberOnLine, "the Station line alrady exist in the this line!!!");
//            }

//            if (!lineStations.Exists(item => item.BusLineID2 == lineStation.BusLineID2))
//            {
//                lineStations.Add(lineStation);
//                XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
//            }
//            else
//            {

//                int index1 = DataSource.LineStations.FindIndex(item => item.BusLineID2 == lineStation.BusLineID2 && lineStation.LocationNumberOnLine == item.LocationNumberOnLine);
//                foreach (LineStation item1 in DataSource.LineStations.Where(item => item.BusLineID2 == lineStation.BusLineID2 && item.LocationNumberOnLine >= lineStation.LocationNumberOnLine))
//                {
//                    ++item1.LocationNumberOnLine;
//                }
//                DataSource.LineStations.Add(lineStation);
//            }
//        }
//  void IDAL.DeleteLineStation1(int NumberLine)
//{
//    List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
//    int index = lineStations.FindIndex(lineStation => lineStation.BusLineID2 == NumberLine && lineStation.ChackDelete2);
//    _ = index != -1
//        ? lineStations.RemoveAll(item => item.BusLineID2 == NumberLine)
//        : throw new ExceptionLineStation(NumberLine, NumberLine, "the line isnt exist in the list!!!");
//    XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
//}
//        void IDAL.DeleteOneLineStation(int NumberLine, int stationNumber)
//        {
//            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
//            int index = lineStations.FindIndex(lineStation => lineStation.BusLineID2 == NumberLine && lineStation.StationNumberOnLine == stationNumber);
//            if (index != -1 && !lineStations[index].ChackDelete2)
//            {
//                throw new ExceptionLineStation(NumberLine, stationNumber, "the station found but she deleted!!!");
//            }

//            if (index != -1 && lineStations[index].ChackDelete2)
//            {
//                lineStations[index].ChackDelete2 = false;
//            }

//            if (index == -1)
//            {
//                throw new ExceptionLineStation(NumberLine, stationNumber, "the line station isnt exist in the compny!!!");
//            }
//            XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
//        }
//        void IDAL.DeleteLineStation(int NumberLine)
//        {
//            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
//            int index = lineStations.FindIndex(lineStation => lineStation.BusLineID2 == NumberLine && lineStation.ChackDelete2);
//            if (index != -1)
//            {
//                foreach (LineStation item in lineStations)
//                {
//                    if (item.BusLineID2 == NumberLine && item.ChackDelete2)
//                    {
//                        item.ChackDelete2 = false;
//                    }
//                }
//                XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
//            }
//            else
//            {
//                throw new ExceptionLineStation(NumberLine, NumberLine, "the line isnt exist in the list!!!");
//            }
//        }
//        void IDAL.UpdatingLineStation(LineStation lineStation)
//        {
//            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
//            int index = lineStations.FindIndex(lineStation1 => lineStation1.BusLineID2 == lineStation.BusLineID2 && lineStation1.StationNumberOnLine == lineStation.StationNumberOnLine);
//            lineStations[index] = index == -1 ? throw new ExceptionLineStation(lineStation.BusLineID2, lineStation.StationNumberOnLine, "The lineStation not exist in the compny!!!") : lineStation;
//            XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
//        }
//        LineStation IDAL.ReturnLineStation(int numberLine, int stationNumber)
//        {
//            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
//            LineStation lineStation1 = lineStations.Find(lineStation => lineStation.BusLineID2 == numberLine && lineStation.StationNumberOnLine == stationNumber);
//            return lineStation1 ?? throw new ExceptionLineStation(numberLine, stationNumber, "The lineStation not exist in the compny!!!");
//        }
//        IEnumerable<LineStation> IDAL.LineStationList()
//        {
//            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
//            return from line in lineStations
//                   where line.ChackDelete2
//                   select line;
//        }
//        IEnumerable<LineStation> IDAL.OneLineFromList(Predicate<LineStation> predicate)
//        {
//            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
//            return from line in lineStations
//                   where predicate(line)
//                   select line ?? throw new ExceptionLineStation(line.BusLineID2, line.StationNumberOnLine, "the line dsnt exist in the compny");
//        }
//        IEnumerable<int> IDAL.LinesFromList(int numberStation)
//        {
//            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
//            IEnumerable<int> Lines = from line in lineStations
//                                     where line.StationNumberOnLine == numberStation && line.ChackDelete2
//                                     select line.BusLineID2;
//            return Lines ?? throw new ExceptionLineStation(numberStation, numberStation, "there is no lines that pass in this station!!!");
//        }
//        #endregion LineStation

//        #region ConsecutiveStations
//        string ConsecutiveStationsXml = @"ConsecutiveStations.xml";
//        bool IDAL.ChackExistingConsecutiveStations(Predicate<ConsecutiveStations> predicate)
//        {
//            List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
//            return consecutives.Exists(predicate);
//        }
//        void IDAL.AddConsecutiveStations(ConsecutiveStations consecutiveStations)
//        {
//            List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
//            if (consecutives.Exists(consecutiveStations1 => consecutiveStations1.StationNumber1 == consecutiveStations.StationNumber1 && consecutiveStations1.StationNumber2 == consecutiveStations.StationNumber2))
//            {
//                throw new ExceptionConsecutiveStations(consecutiveStations.StationNumber1, consecutiveStations.StationNumber2, "There are already two such stations on the list!!!");
//            }
//            else
//            {
//                consecutives.Add(consecutiveStations);
//                XMLTools.SaveListToXMLSerializer(consecutives, ConsecutiveStationsXml);
//            }
//        }
//        void IDAL.DeleteConsecutiveStations(int stationNumber1, int stationNumber2)
//        {
//            List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
//            if (consecutives.Exists(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber2))
//            {
//                ConsecutiveStations item = consecutives.Find(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber2);
//                _ = consecutives.Remove(item);
//                XMLTools.SaveListToXMLSerializer(consecutives, ConsecutiveStationsXml);
//            }
//            else
//            {
//                throw new ExceptionConsecutiveStations(stationNumber1, stationNumber2, "There are no two such stations on the list!!!");
//            }
//        }
//        void IDAL.UpdatingConsecutiveStations(ConsecutiveStations consecutiveStations)
//        {
//            List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
//            int index = consecutives.FindIndex(consecutiveStations1 => consecutiveStations1.StationNumber1 == consecutiveStations.StationNumber1 && consecutiveStations1.StationNumber2 == consecutiveStations.StationNumber2);
//            consecutives[index] = index == -1 ? throw new ExceptionConsecutiveStations(consecutiveStations.StationNumber1, consecutiveStations.StationNumber2, "The consecutiveStations not exist in the compny!!!") : consecutiveStations;
//            XMLTools.SaveListToXMLSerializer(consecutives, ConsecutiveStationsXml);
//        }
//        float IDAL.DistanceBetweenTooStations(int numberStation1, int numberStation2)
//        {
//            List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
//            int index = consecutives.FindIndex(consecutiveStations1 => consecutiveStations1.StationNumber1 == numberStation1 && consecutiveStations1.StationNumber2 == numberStation2);
//            return index != -1
//                ? consecutives[index].DistanceBetweenTooStations
//                : throw new ExceptionConsecutiveStations(numberStation1, numberStation2, "There are no two such stations on the list!!!");
//        }
//        TimeSpan IDAL.AverageTimeBetweenTooStationsList(int numberStation1, int numberStation2)
//        {
//            List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
//            int index = consecutives.FindIndex(consecutiveStations1 => consecutiveStations1.StationNumber1 == numberStation1 && consecutiveStations1.StationNumber2 == numberStation2);
//            return index != -1
//                ? consecutives[index].AverageTime
//                : throw new ExceptionConsecutiveStations(numberStation1, numberStation2, "There are no two such stations on the list!!!");
//        }
//        ConsecutiveStations IDAL.ReturnConsecutiveStation(int stationNumber1, int stationNumber2)
//        {
//            List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
//            ConsecutiveStations item = null;
//            if (consecutives.Exists(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber2))
//            {
//                item = consecutives.Find(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber2);
//            }
//            return item ?? throw new ExceptionConsecutiveStations(stationNumber1, stationNumber2, "There are no two such stations on the list!!!"); ;
//        }
//        IEnumerable<ConsecutiveStations> IDAL.ConsecutiveStationsList()
//        {
//            List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
//            return from Consecutive in consecutives
//                   select Consecutive;
//        }
//        #endregion ConsecutiveStations

//        #region LineExit
//        string LineExitXml = @"LineExit.xml";
//        void IDAL.AddLineExit(LineExit lineExit)
//        {
//            List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
//            if (lineExits.Exists(lineExit1 => lineExit1.BusLineID1 == lineExit.BusLineID1 && lineExit1.LineStartTime == lineExit.LineStartTime))
//            {
//                throw new ExceptionLineExit(lineExit.BusLineID1, lineExit.LineStartTime, "the LineExit alrdy exist in the list in the same time");
//            }
//            else
//            {
//                lineExits.Add(lineExit);
//                XMLTools.SaveListToXMLSerializer(lineExits, LineExitXml);
//            }
//        }
//        void IDAL.DeleteLineExit(int lineNumber, TimeSpan StartTime)
//        {
//            List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
//            int index = lineExits.FindIndex(lineExit1 => lineExit1.BusLineID1 == lineNumber && lineExit1.LineStartTime == StartTime);
//            if (index == -1)
//            {
//                throw new ExceptionLineExit(lineNumber, StartTime, "the LineExit not found!!!");
//            }
//            else
//            {
//                lineExits.RemoveAt(index);
//                XMLTools.SaveListToXMLSerializer(lineExits, LineExitXml);
//            }
//        }
//        void IDAL.UpdatingLineExit(LineExit lineExit)
//        {
//            List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
//            int index = lineExits.FindIndex(lineExit1 => lineExit1.BusLineID1 == lineExit.BusLineID1 && lineExit1.LineStartTime == lineExit.LineStartTime);
//            lineExits[index] = index == -1 ? throw new ExceptionLineExit(lineExit.BusLineID1, lineExit.LineStartTime, "The lineExit not exist in the compny") : lineExit;
//            XMLTools.SaveListToXMLSerializer(lineExits, LineExitXml);
//        }
//        LineExit IDAL.ReturnLineExit(int lineNumber, TimeSpan StartTime)
//        {
//            List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
//            LineExit lineExit = lineExits.FirstOrDefault(lineExit1 => lineExit1.BusLineID1 == lineNumber && lineExit1.LineStartTime == StartTime);
//            return lineExit ?? throw new ExceptionLineExit(lineNumber, StartTime, "the LineExit not exist in the list");
//        }
//        LineExit IDAL.OneLineExitFromList(int numberLine, TimeSpan StartTime)
//        {
//            List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
//            LineExit b = lineExits.FirstOrDefault(lineExit => lineExit.BusLineID1 == numberLine && lineExit.LineStartTime == StartTime);
//            return b = default ? throw new ExceptionLineExit(numberLine, StartTime, "the line Exit dsnt Exist") : b;
//        }
//        IEnumerable<LineExit> IDAL.LineExitList(int numberLine)
//        {
//            List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
//            return from lineExit in lineExits
//                   where lineExit.BusLineID1 == numberLine
//                   select lineExit;
//        }
//        #endregion LineExit

//        #region BusTraveling
//        //void IDAL.AddBusTraveling(BusTraveling busTraveling)
//        //{
//        //    if (DataSource.BusTravelings.Exists(busTraveling1 => busTraveling.LineInExecution == busTraveling.LineInExecution && busTraveling.LeavingTime == busTraveling.LeavingTime))
//        //    {
//        //        throw new ExceptionDl("the BusTraveling alrdy exist in the list whis the same Data!!!");
//        //    }
//        //    else
//        //    {
//        //        busTraveling.IdBusTraveling = NumbersAreRunning.U_TravelID;
//        //        NumbersAreRunning.U_TravelID++;
//        //        DataSource.BusTravelings.Add(busTraveling.Clone());
//        //    }
//        //}

//        //public void DeleteBusTraveling(int LineExecution, string License_number, string LeavingTime)
//        //{
//        //    int index = DataSource.BusTravelings.FindIndex(BusTravelings1 => BusTravelings1.License_number1 == License_number && BusTravelings1.LineInExecution == LineExecution && BusTravelings1.LeavingTime == LeavingTime);
//        //    if (index == -1)
//        //    {
//        //        throw new ExceptionDl("the BusTraveling not exist!!!");
//        //    }
//        //    else
//        //    {
//        //        DataSource.LineExits.RemoveAt(index);
//        //    }
//        //}
//        //void IDAL.UpdatingBusTraveling(BusTraveling busTraveling)
//        //{
//        //    int index = DataSource.BusTravelings.FindIndex(BusTravelings1 => BusTravelings1.License_number1 == busTraveling.License_number1 && BusTravelings1.LineInExecution == busTraveling.LineInExecution && BusTravelings1.LeavingTime == busTraveling.LeavingTime);
//        //    DataSource.BusTravelings[index] = index == -1 ? throw new ExceptionDl("The busTraveling not exist in the compny!!!") : busTraveling.Clone();
//        //}
//        //public BusTraveling ReturnBusTraveling(int LineExecution, string License_number, string LeavingTime)
//        //{
//        //    BusTraveling busTraveling = DataSource.BusTravelings.FirstOrDefault(BusTravelings1 => BusTravelings1.License_number1 == License_number && BusTravelings1.LineInExecution == LineExecution && BusTravelings1.LeavingTime == LeavingTime);
//        //    return busTraveling.Clone(); ?? throw new ExceptionDl("the busTraveling not exist in the list!!!");
//        //}

//        //public IEnumerable<BusTraveling> BusTravelingList()
//        //{
//        //    return from busTraveling in DataSource.BusTravelings
//        //           select busTraveling;
//        //}
//        #endregion BusTraveling
//    }
//}