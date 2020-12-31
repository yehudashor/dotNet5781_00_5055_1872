using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using DLAPI;
using DO;

namespace DL
{
    internal class LineDl : IDAL
    {
        #region singleton
        static LineDl() { }// static ctor to ensure instance init is done just before first usage
        LineDl() { } // default => private
        public static LineDl Instance { get; } = new LineDl(); // The public Instance property to use
        #endregion

        #region Bus
        void IDAL.AddBus(Bus bus)
        {
            if (DataSource.Bus1.Exists(bus1 => bus1.License_number == bus.License_number && bus1.IsAvailable == true))
            {
                throw new ExceptionDl("The buses alrady exist in the compny");
            }
            else
            {
                DataSource.Bus1.Add(bus.Clone());
            }
        }

        void IDAL.DeleteBus(string License_number)
        {
            int index = DataSource.Bus1.FindIndex(item => item.License_number == License_number && item.IsAvailable);
            DataSource.Bus1[index].IsAvailable = index == -1 ? throw new ExceptionDl("The buses not exist in the compny") : false;
        }
        void IDAL.UpdatingBus(Bus bus)
        {
            int index = DataSource.Bus1.FindIndex(bus1 => bus1.License_number == bus.License_number);
            DataSource.Bus1[index] = index == -1 ? throw new ExceptionDl("The buses not exist in the compny") : bus.Clone();
        }
        Bus IDAL.ReturnBusToBl(string License_number)
        {
            Bus bus = DataSource.Bus1.Find(bus1 => bus1.License_number == License_number);
            return bus.Clone() ?? throw new ExceptionDl("The buses not exist in the compny");
        }

        IEnumerable<Bus> IDAL.BusLists()
        {
            return from bus in DataSource.Bus1
                   where bus.IsAvailable
                   select bus;
        }
        #endregion Bus

        #region Station
        void IDAL.AddStation(BusStation station)
        {
            if (!DataSource.BusStations.Exists(station1 => station1.StationNumber == station.StationNumber && station1.IsAvailable3))
            {
                throw new ExceptionDl("The Station alrady exist in the compny");
            }
            else
            {
                DataSource.BusStations.Add(station.Clone());
            }
        }

        void IDAL.DeleteStation(int numberStation)
        {
            int index = DataSource.BusStations.FindIndex(station1 => station1.StationNumber == numberStation);
            if (index == -1)
            {
                throw new ExceptionDl("The Station not exist in the compny");
            }
            else
            {
                DataSource.BusStations[index].IsAvailable3 = true ? false : throw new ExceptionDl("The station exists but has already been deleted");
                _ = DataSource.LineStations.RemoveAll(station1 => station1.StationNumberOnLine == numberStation);
            }
        }

        void IDAL.UpdatingStation(BusStation station)
        {
            int index = DataSource.BusStations.FindIndex(station1 => station1.StationNumber == station.StationNumber);
            DataSource.BusStations[index] = index == -1 ? throw new ExceptionDl("The buses not exist in the compny") : station.Clone();
        }
        BusStation IDAL.ReturnStation(int numberStation)
        {
            BusStation station = DataSource.BusStations.Find(station1 => station1.StationNumber == numberStation);
            return station.Clone() ?? throw new ExceptionDl("The Station not exist in the compny");
        }

        IEnumerable<BusStation> IDAL.StationList()
        {
            return from station in DataSource.BusStations
                   where station.IsAvailable3 == true
                   select station.Clone();
        }
        #endregion Station

        #region BusLine
        int IDAL.BusLineId()
        {
            //IEnumerable<int> vs = from line in DataSource.BusLines
            //                      where line.IsAvailable1
            //                      select line.BusLineID1;
            return DataSource.BusLines.Count == 0 ? throw new ExceptionDl("there are no lines in the compny") : DataSource.BusLines.Count;
        }
        int IDAL.AddBusLine(BusLine line)
        {
            line.BusLineID1 = NumbersAreRunning.BusLineID;
            NumbersAreRunning.BusLineID++;
            DataSource.BusLines.Add(line.Clone());
            return line.BusLineID1;
        }

        void IDAL.DeleteBusLine(int BusLineID)
        {
            int index = DataSource.BusLines.FindIndex(BusLine => BusLine.BusLineID1 == BusLineID);
            DataSource.BusLines[index].IsAvailable1 = index == -1
                ? throw new ExceptionDl("The line not exist in the compny!!!")
                : true ? false : throw new ExceptionDl("The Line exists but has already been deleted!!!");
        }
        void IDAL.UpdatingBusLine(BusLine line)
        {
            int index = DataSource.BusLines.FindIndex(line1 => line1.BusLineID1 == line.BusLineID1);
            DataSource.BusLines[index] = index == -1 ? throw new ExceptionDl("The BusLine not exist in the compny!!!") : line.Clone();
        }

        BusLine IDAL.ReturnBusLine(int numberLineId)
        {
            BusLine busLine = DataSource.BusLines.Find(line => line.BusLineID1 == numberLineId);
            return busLine.Clone() ?? throw new ExceptionDl("The line not exist in the compny!!!");
        }

        IEnumerable<BusLine> IDAL.BusLinesList()
        {
            return from line in DataSource.BusLines
                   where line.IsAvailable1 == true
                   select line.Clone();
        }

        IEnumerable<BusLine> IDAL.BusLinesList(int numberLine)
        {
            return from line in DataSource.BusLines
                   where line.IsAvailable1 == true && line.BusLineID1 == numberLine
                   select line.Clone();
        }
        #endregion BusLine

        #region LineStation

        void IDAL.AddLineStation(LineStation lineStation)
        {
            if (!DataSource.BusLines.Exists(BusLine => BusLine.BusLineID1 == lineStation.BusLineID2 && BusLine.IsAvailable1))
            {
                throw new ExceptionDl("The line not exist in the compny");
            }
            if (!DataSource.BusStations.Exists(station1 => station1.StationNumber == lineStation.StationNumberOnLine && station1.IsAvailable3))
            {
                throw new ExceptionDl("The Station not exist in the compny");
            }

            if (DataSource.LineStations.Exists(lineStation1 => lineStation1.BusLineID2 == lineStation.BusLineID2 && lineStation1.StationNumberOnLine == lineStation.StationNumberOnLine && lineStation1.ChackDelete2))
            {
                throw new ExceptionDl("the Station alrady exist in the this line!!!");
            }
            else
            {
                DataSource.LineStations.Add(lineStation.Clone());
            }
        }

        void IDAL.DeleteOneLineStation(int NumberLine, int stationNumber)
        {
            int index = DataSource.LineStations.FindIndex(lineStation => lineStation.BusLineID2 == NumberLine && lineStation.StationNumberOnLine == stationNumber);
            if (index != -1 && DataSource.LineStations[index].ChackDelete2)
            {
                DataSource.LineStations[index].ChackDelete2 = false;
            }
            if (index != -1 && !DataSource.LineStations[index].ChackDelete2)
            {
                throw new ExceptionDl("the station found but she deleted!!!");
            }
            if (index == -1)
            {
                throw new ExceptionDl("the line isnt exist in the list!!!");
            }
        }
        void IDAL.DeleteLineStation(int NumberLine)
        {
            int index = DataSource.LineStations.FindIndex(lineStation => lineStation.BusLineID2 == NumberLine && lineStation.ChackDelete2);
            if (index != -1)
            {
                foreach (LineStation item in DataSource.LineStations)
                {
                    if (item.BusLineID2 == NumberLine && item.ChackDelete2)
                    {
                        item.ChackDelete2 = false;
                    }
                }
            }
            else
            {
                throw new ExceptionDl("the line isnt exist in the list!!!");
            }
        }
        void IDAL.UpdatingLineStation(LineStation lineStation)
        {
            int index = DataSource.LineStations.FindIndex(lineStation1 => lineStation1.BusLineID2 == lineStation.BusLineID2 && lineStation1.StationNumberOnLine == lineStation.StationNumberOnLine);
            DataSource.LineStations[index] = index == -1 ? throw new ExceptionDl("The lineStation not exist in the compny!!!") : lineStation.Clone();
        }
        LineStation IDAL.ReturnLineStation(int numberLine, int stationNumber)
        {
            LineStation lineStation1 = DataSource.LineStations.Find(lineStation => lineStation.BusLineID2 == numberLine && lineStation.StationNumberOnLine == stationNumber);
            return lineStation1.Clone() ?? throw new ExceptionDl("The lineStation not exist in the compny!!!");
        }

        IEnumerable<LineStation> IDAL.LineStationList()
        {
            return from line in DataSource.LineStations
                   where line.ChackDelete2
                   select line;
        }

        int IDAL.IndexOfLastLineStation(int numberLine)
        {
            List<LineStation> lineStations = DataSource.LineStations.FindAll(item => item.BusLineID2 == numberLine);
            lineStations = (List<LineStation>)(from lineStation in lineStations
                                               orderby lineStation.LocationNumberOnLine
                                               select lineStation);
            return lineStations[lineStations.Count].StationNumberOnLine;
        }

        public IEnumerable<LineStation> OneLineFromList(Predicate<LineStation> predicate)
        {
            return from line in DataSource.LineStations
                   where predicate(line)
                   select line.Clone();
            //return from line in DataSource.LineStations
            //       where line.ChackDelete2 && line.BusLineID2 == numberLine
            //       select line.Clone();
            //return OneLineStation ?? throw new ExceptionDl("the line dsnt exist in the compny");
        }

        IEnumerable<int> IDAL.LinesFromList(int numberStation)
        {
            IEnumerable<int> Lines = from line in DataSource.LineStations
                                     where line.StationNumberOnLine == numberStation && line.ChackDelete2
                                     select line.BusLineID2;
            return Lines ?? throw new ExceptionDl("there is no lines that pass in this station!!!");
        }
        #endregion LineStation

        #region ConsecutiveStations

        void IDAL.AddConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            if (DataSource.ConsecutiveStations.Exists(consecutiveStations1 => consecutiveStations1.StationNumber1 == consecutiveStations.StationNumber1 && consecutiveStations1.StationNumber2 == consecutiveStations.StationNumber2))
            {
                throw new ExceptionDl("There are already two such stations on the list!!!");
            }
            else
            {
                DataSource.ConsecutiveStations.Add(consecutiveStations.Clone());
            }
        }

        void IDAL.DeleteConsecutiveStations(int stationNumber1, int stationNumber2)
        {
            if (DataSource.ConsecutiveStations.Exists(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber2))
            {
                ConsecutiveStations item = DataSource.ConsecutiveStations.Find(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber2);
                _ = DataSource.ConsecutiveStations.Remove(item);
            }
            else
            {
                throw new ExceptionDl("There are no two such stations on the list!!!");
            }
        }

        void IDAL.UpdatingConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            int index = DataSource.ConsecutiveStations.FindIndex(consecutiveStations1 => consecutiveStations1.StationNumber1 == consecutiveStations.StationNumber1 && consecutiveStations1.StationNumber2 == consecutiveStations.StationNumber2);
            DataSource.ConsecutiveStations[index] = index == -1 ? throw new ExceptionDl("The consecutiveStations not exist in the compny!!!") : consecutiveStations.Clone();
        }
        public int DistanceBetweenTooStations(int numberStation1, int numberStation2)
        {
            int index = DataSource.ConsecutiveStations.FindIndex(consecutiveStations1 => consecutiveStations1.StationNumber1 == numberStation1 && consecutiveStations1.StationNumber2 == numberStation2);
            return index != -1
                ? DataSource.ConsecutiveStations[index].DistanceBetweenTooStations
                : throw new ExceptionDl("There are no two such stations on the list!!!");
        }
        public double AverageTimeBetweenTooStationsList(int numberStation1, int numberStation2)
        {
            int index = DataSource.ConsecutiveStations.FindIndex(consecutiveStations1 => consecutiveStations1.StationNumber1 == numberStation1 && consecutiveStations1.StationNumber2 == numberStation2);
            return index != -1
                ? DataSource.ConsecutiveStations[index].AverageTime
                : throw new ExceptionDl("There are no two such stations on the list!!!");
        }
        ConsecutiveStations IDAL.ReturnConsecutiveStation(int stationNumber1, int stationNumber2)
        {
            ConsecutiveStations item = null;
            if (DataSource.ConsecutiveStations.Exists(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber1))
            {
                item = DataSource.ConsecutiveStations.Find(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber1);
            }
            return item.Clone() ?? throw new ExceptionDl("There are no two such stations on the list!!!"); ;
        }

        IEnumerable<ConsecutiveStations> IDAL.ConsecutiveStationsList()
        {
            return from Consecutive in DataSource.ConsecutiveStations
                   select Consecutive;
        }
        double IDAL.SumOfTime(int NumberOfLine)
        {
            return DataSource.ConsecutiveStations.Sum(item => item.AverageTime);
        }
        #endregion ConsecutiveStations

        #region LineExit
        void IDAL.AddLineExit(LineExit lineExit)
        {
            if (DataSource.LineExits.Exists(lineExit1 => lineExit1.BusLineID1 == lineExit.BusLineID1 && lineExit1.LineStartTime == lineExit.LineStartTime))
            {
                throw new ExceptionDl("the LineExit alrdy exist in the list in the same time");
            }
            else
            {
                DataSource.LineExits.Add(lineExit.Clone());
            }
        }
        void IDAL.DeleteLineExit(int lineNumber, string StartTime)
        {
            int index = DataSource.LineExits.FindIndex(lineExit1 => lineExit1.BusLineID1 == lineNumber && lineExit1.LineStartTime == StartTime);
            if (index == -1)
            {
                throw new ExceptionDl("the LineExit not found!!!");
            }
            else
            {
                DataSource.LineExits.RemoveAt(index);
            }
        }
        void IDAL.UpdatingLineExit(LineExit lineExit)
        {
            int index = DataSource.LineExits.FindIndex(lineExit1 => lineExit1.BusLineID1 == lineExit.BusLineID1 && lineExit1.LineStartTime == lineExit.LineStartTime);
            DataSource.LineExits[index] = index == -1 ? throw new ExceptionDl("The lineExit not exist in the compny") : lineExit.Clone();
        }
        LineExit IDAL.ReturnLineExit(int lineNumber, string StartTime)
        {
            LineExit lineExit = DataSource.LineExits.FirstOrDefault(lineExit1 => lineExit1.BusLineID1 == lineNumber && lineExit1.LineStartTime == StartTime);
            return lineExit.Clone() ?? throw new ExceptionDl("the LineExit not exist in the list");
        }

        LineExit IDAL.OneLineExitFromList(int numberLine, string start)
        {
            LineExit b = DataSource.LineExits.FirstOrDefault(lineExit => lineExit.BusLineID1 == numberLine && lineExit.LineStartTime == start);
            return b = default ? throw new ExceptionDl("the line Exit dsnt Exist") : b;
        }

        IEnumerable<LineExit> IDAL.LineExitList(int numberLine)
        {
            return from lineExit in DataSource.LineExits
                   where lineExit.BusLineID1 == numberLine
                   select lineExit;
        }
        #endregion LineExit

        #region BusTraveling
        void IDAL.AddBusTraveling(BusTraveling busTraveling)
        {
            if (DataSource.BusTravelings.Exists(busTraveling1 => busTraveling1.License_number1 == busTraveling.License_number1 && busTraveling.LineInExecution == busTraveling.LineInExecution && busTraveling.LeavingTime == busTraveling.LeavingTime))
            {
                throw new ExceptionDl("the BusTraveling alrdy exist in the list whis the same Data!!!");
            }
            else
            {
                busTraveling.IdBusTraveling = NumbersAreRunning.U_TravelID;
                NumbersAreRunning.U_TravelID++;
                DataSource.BusTravelings.Add(busTraveling.Clone());
            }
        }

        public void DeleteBusTraveling(int LineExecution, string License_number, string LeavingTime)
        {
            int index = DataSource.BusTravelings.FindIndex(BusTravelings1 => BusTravelings1.License_number1 == License_number && BusTravelings1.LineInExecution == LineExecution && BusTravelings1.LeavingTime == LeavingTime);
            if (index == -1)
            {
                throw new ExceptionDl("the BusTraveling not exist!!!");
            }
            else
            {
                DataSource.LineExits.RemoveAt(index);
            }
        }
        void IDAL.UpdatingBusTraveling(BusTraveling busTraveling)
        {
            int index = DataSource.BusTravelings.FindIndex(BusTravelings1 => BusTravelings1.License_number1 == busTraveling.License_number1 && BusTravelings1.LineInExecution == busTraveling.LineInExecution && BusTravelings1.LeavingTime == busTraveling.LeavingTime);
            DataSource.BusTravelings[index] = index == -1 ? throw new ExceptionDl("The busTraveling not exist in the compny!!!") : busTraveling.Clone();
        }
        public BusTraveling ReturnBusTraveling(int LineExecution, string License_number, string LeavingTime)
        {
            BusTraveling busTraveling = DataSource.BusTravelings.FirstOrDefault(BusTravelings1 => BusTravelings1.License_number1 == License_number && BusTravelings1.LineInExecution == LineExecution && BusTravelings1.LeavingTime == LeavingTime);
            return busTraveling.Clone() ?? throw new ExceptionDl("the busTraveling not exist in the list!!!");
        }

        public IEnumerable<BusTraveling> BusTravelingList()
        {
            return from busTraveling in DataSource.BusTravelings
                   select busTraveling;
        }
        #endregion BusTraveling

        #region  User
        void IDAL.AddUser(User user)
        {
            User user1 = DataSource.Users.FirstOrDefault(user2 => user2.Username == user.Username);
            if (user1 != null)
            {
                throw new ExceptionDl("the User alrdy exist in the compny!!!");
            }
            else
            {
                DataSource.Users.Add(user.Clone());
            }
        }

        void IDAL.DeleteUser(string Username1)
        {
            int index = DataSource.Users.FindIndex(user1 => user1.Username == Username1 && user1.ChackDelete);
            _ = index == -1 ? throw new ExceptionDl("the user not exist in the list!!!") : DataSource.Users[index].ChackDelete = false;
        }

        void IDAL.UpdatingUser(User user)
        {
            int index = DataSource.Users.FindIndex(user1 => user1.Username == user.Username);
            DataSource.Users[index] = index == -1 ? throw new ExceptionDl("The user not exist in the compny!!!") : user.Clone();
        }

        User IDAL.ReturnUser(string Username1)
        {
            int index = DataSource.Users.FindIndex(user1 => user1.Username == Username1);
            return index == -1 ? throw new ExceptionDl("the user not exist in the list!!!") : DataSource.Users[index].Clone();
        }

        IEnumerable<User> IDAL.UseresList()
        {
            return from user in DataSource.Users
                   where user.ChackDelete
                   select user;
        }

        public bool FindUser(string pass, string UserNam)
        {
            return DataSource.Users.Exists(item => item.ChackDelete && item.Password == pass && item.Username == UserNam && item.ManagementPermission && item.Permission1 == Permission.ManagementPermission)
                ? true
                : throw new ExceptionDl("rong User!!!");
        }
        #endregion User
    }
}





