using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using DLAPI;
using DO;
using System.Xml.Linq;

namespace DL
{
    internal class LineDl : IDAL
    {
        #region singleton
        static LineDl() { }// static ctor to ensure instance init is done just before first usage
        LineDl() { } // default => private
        public static LineDl Instance { get; } = new LineDl(); // The public Instance property to use
        #endregion

        #region  User
        void IDAL.AddUser(User user)
        {
            User user1 = DataSource.Users.FirstOrDefault(user2 => user2.Username == user.Username);
            if (user1 != null)
            {
                throw new ExceptionUser(user.Username, "the User alrdy exist in the compny!!!");
            }
            else
            {
                DataSource.Users.Add(user.Clone());
            }
        }
        void IDAL.DeleteUser(string Username1)
        {
            int index = DataSource.Users.FindIndex(user1 => user1.Username == Username1 && user1.ChackDelete);
            _ = index == -1 ? throw new ExceptionUser(Username1, "the user not exist in the list!!!") : DataSource.Users[index].ChackDelete = false;
        }
        void IDAL.UpdatingUser(User user)
        {
            int index = DataSource.Users.FindIndex(user1 => user1.Username == user.Username);
            DataSource.Users[index] = index == -1 ? throw new ExceptionUser(user.Username, "The user not exist in the compny!!!") : user.Clone();
        }
        User IDAL.ReturnUser(string Username1)
        {
            int index = DataSource.Users.FindIndex(user1 => user1.Username == Username1);
            return index == -1 ? throw new ExceptionUser(Username1, "the user not exist in the list!!!") : DataSource.Users[index].Clone();
        }
        IEnumerable<User> IDAL.UseresList()
        {
            return from user in DataSource.Users
                   where user.ChackDelete
                   select user;
        }
        bool IDAL.FindUser(string pass, string UserNam)
        {
            return DataSource.Users.Exists(item => item.ChackDelete && item.Password == pass && item.Username == UserNam && item.ManagementPermission && item.Permission1 == Permission.ManagementPermission)
                ? true
                : throw new ExceptionUser(UserNam, "Rong user!!!");
        }
        #endregion User

        #region Bus
        void IDAL.AddBus(Bus bus)
        {
            if (DataSource.Bus1.Exists(bus1 => bus1.License_number == bus.License_number && bus1.IsAvailable == true))
            {
                throw new ExceptionBus(bus.License_number, $"bad bus id {bus.License_number}");
            }
            else
            {
                DataSource.Bus1.Add(bus.Clone());
            }
        }

        void IDAL.DeleteBus(string License_number)
        {
            int index = DataSource.Bus1.FindIndex(item => item.License_number == License_number && item.IsAvailable);
            DataSource.Bus1[index].IsAvailable = index == -1 ? throw new ExceptionBus(License_number, $"bad bus id {License_number}") : false;
        }
        void IDAL.UpdatingBus(Bus bus)
        {
            int index = DataSource.Bus1.FindIndex(bus1 => bus1.License_number == bus.License_number);
            DataSource.Bus1[index] = index == -1 ? throw new ExceptionBus(bus.License_number, $"bad bus id {bus.License_number}") : bus.Clone();
        }
        Bus IDAL.ReturnBusToBl(string License_number)
        {
            Bus bus = DataSource.Bus1.Find(bus1 => bus1.License_number == License_number);
            return bus.Clone() ?? throw new ExceptionBus(License_number, $"bad bus id {License_number}");
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
            if (DataSource.BusStations.Exists(station1 => station1.StationNumber == station.StationNumber && station1.IsAvailable3))
            {
                throw new ExceptionStation(station.StationNumber, "bad id - The Station alrady exist in the compny: {station.StationNumber}");
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
                throw new ExceptionStation(numberStation, "bad id - The Station not exist in the compny: {numberStation}");
            }
            else
            {
                DataSource.BusStations[index].IsAvailable3 = true ? false : throw new ExceptionStation(numberStation, "The station exists but has already been deleted");
                foreach (LineStation item in DataSource.LineStations.Where(item => item.StationNumberOnLine == numberStation))
                {
                    item.ChackDelete2 = false;
                }
                _ = DataSource.ConsecutiveStations.RemoveAll(item => item.StationNumber1 == numberStation || item.StationNumber2 == numberStation);
            }
        }
        void IDAL.UpdatingStation(BusStation station)
        {
            int index = DataSource.BusStations.FindIndex(station1 => station1.StationNumber == station.StationNumber);
            DataSource.BusStations[index] = index == -1 ? throw new ExceptionStation(station.StationNumber, "bad id - The Station not exist in the compny: {station.StationNumber}") : station.Clone();
        }
        BusStation IDAL.ReturnStation(int numberStation)
        {
            BusStation station = DataSource.BusStations.Find(station1 => station1.StationNumber == numberStation);
            return station.Clone() ?? throw new ExceptionStation(numberStation, "bad id - The Station not exist in the compny: {numberStation}");
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
            return DataSource.BusLines.Count;
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
            DataSource.BusLines[index].GetAvailable = index == -1
                ? throw new ExceptionLine(BusLineID, " bad id line - The line not exist in the compny!!!")
                : DataSource.BusLines[index].GetAvailable == Available.Notavailable
                    ? throw new ExceptionLine(BusLineID, " bad id line - The Line exists but has already been deleted!!!")
                    : Available.Notavailable;
        }
        void IDAL.UpdatingBusLine(BusLine line)
        {
            int index = DataSource.BusLines.FindIndex(line1 => line1.BusLineID1 == line.BusLineID1);
            DataSource.BusLines[index] = index == -1 ? throw new ExceptionLine(line.BusLineID1, " bad id line - The BusLine not exist in the compny!!!") : line.Clone();
        }
        BusLine IDAL.ReturnBusLine(int numberLineId)
        {
            BusLine busLine = DataSource.BusLines.Find(line => line.BusLineID1 == numberLineId);
            return busLine.Clone() ?? throw new ExceptionLine(numberLineId, "bad id line - The line not exist in the compny!!!");
        }
        IEnumerable<BusLine> IDAL.BusLinesList()
        {
            return from line in DataSource.BusLines
                   where line.GetAvailable == Available.Available
                   select line.Clone();
        }
        IEnumerable<BusLine> IDAL.BusLinesList(int numberLine)
        {
            return from line in DataSource.BusLines
                   where line.GetAvailable == Available.Available && line.BusLineID1 == numberLine
                   select line;
        }
        #endregion BusLine

        #region LineStation

        void IDAL.AddLineStation(LineStation lineStation)
        {
            if (DataSource.BusStations.Exists(station1 => station1.StationNumber == lineStation.StationNumberOnLine && !station1.IsAvailable3))
            {
                throw new ExceptionLineStation(lineStation.BusLineID2, lineStation.StationNumberOnLine, "The Station not exist in the compny");
            }

            if (DataSource.LineStations.Exists(lineStation1 => lineStation1.BusLineID2 == lineStation.BusLineID2 && lineStation1.StationNumberOnLine == lineStation.StationNumberOnLine && lineStation1.ChackDelete2))
            {
                throw new ExceptionLineStation(lineStation.BusLineID2, lineStation.StationNumberOnLine, "the Station line alrady exist in the this line!!!");
            }
            else
            {
                DataSource.LineStations.Add(lineStation.Clone());
            }

            //if (!DataSource.LineStations.Exists(item => item.BusLineID2 == lineStation.BusLineID2))
            //{
            //}

            //else
            //{
            //    int index1 = DataSource.LineStations.FindIndex(item => item.BusLineID2 == lineStation.BusLineID2 && lineStation.LocationNumberOnLine == item.LocationNumberOnLine);
            //    foreach (LineStation item1 in DataSource.LineStations.Where(item => item.BusLineID2 == lineStation.BusLineID2 && item.LocationNumberOnLine >= lineStation.LocationNumberOnLine))
            //    {
            //        ++item1.LocationNumberOnLine;
            //    }
            //    DataSource.LineStations.Add(lineStation.Clone());
            //}
        }
        void IDAL.DeleteOneLineStation(int NumberLine, int stationNumber)
        {
            int index = DataSource.LineStations.FindIndex(lineStation => lineStation.BusLineID2 == NumberLine && lineStation.StationNumberOnLine == stationNumber);
            if (index != -1 && !DataSource.LineStations[index].ChackDelete2)
            {
                throw new ExceptionLineStation(NumberLine, stationNumber, "the station found but she deleted!!!");
            }

            if (index != -1 && DataSource.LineStations[index].ChackDelete2)
            {
                DataSource.LineStations[index].ChackDelete2 = false;
            }

            if (index == -1)
            {
                throw new ExceptionLineStation(NumberLine, stationNumber, "the line station isnt exist in the compny!!!");
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
                throw new ExceptionLineStation(NumberLine, NumberLine, "the line isnt exist in the list!!!");
            }
        }
        void IDAL.DeleteLineStation1(int NumberLine)
        {
            int index = DataSource.LineStations.FindIndex(lineStation => lineStation.BusLineID2 == NumberLine && lineStation.ChackDelete2);
            _ = index != -1
                ? DataSource.LineStations.RemoveAll(item => item.BusLineID2 == NumberLine)
                : throw new ExceptionLineStation(NumberLine, NumberLine, "the line isnt exist in the list!!!");
        }
        void IDAL.UpdatingLineStation(LineStation lineStation)
        {
            int index = DataSource.LineStations.FindIndex(lineStation1 => lineStation1.BusLineID2 == lineStation.BusLineID2 && lineStation1.StationNumberOnLine == lineStation.StationNumberOnLine);
            DataSource.LineStations[index] = index == -1 ? throw new ExceptionLineStation(lineStation.BusLineID2, lineStation.StationNumberOnLine, "The lineStation not exist in the compny!!!") : lineStation.Clone();
        }
        LineStation IDAL.ReturnLineStation(int numberLine, int stationNumber)
        {
            LineStation lineStation1 = DataSource.LineStations.Find(lineStation => lineStation.BusLineID2 == numberLine && lineStation.StationNumberOnLine == stationNumber);
            return lineStation1.Clone() ?? throw new ExceptionLineStation(numberLine, stationNumber, "The lineStation not exist in the compny!!!");
        }
        IEnumerable<LineStation> IDAL.LineStationList()
        {
            return from line in DataSource.LineStations
                   where line.ChackDelete2
                   select line;
        }
        IEnumerable<LineStation> IDAL.OneLineFromList(Predicate<LineStation> predicate)
        {
            return from line in DataSource.LineStations
                   where predicate(line)
                   select line ?? throw new ExceptionLineStation(line.BusLineID2, line.StationNumberOnLine, "the line dsnt exist in the compny");
        }
        IEnumerable<int> IDAL.LinesFromList(int numberStation)
        {
            IEnumerable<int> Lines = from line in DataSource.LineStations
                                     where line.StationNumberOnLine == numberStation && line.ChackDelete2
                                     select line.BusLineID2;
            return Lines ?? throw new ExceptionLineStation(numberStation, numberStation, "there is no lines that pass in this station!!!");
        }
        #endregion LineStation

        #region ConsecutiveStations
        bool IDAL.ChackExistingConsecutiveStations(Predicate<ConsecutiveStations> predicate)
        {
            return !DataSource.ConsecutiveStations.Exists(predicate);
        }
        void IDAL.AddConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            if (DataSource.ConsecutiveStations.Exists(consecutiveStations1 => consecutiveStations1.StationNumber1 == consecutiveStations.StationNumber1 && consecutiveStations1.StationNumber2 == consecutiveStations.StationNumber2))
            {
                throw new ExceptionConsecutiveStations(consecutiveStations.StationNumber1, consecutiveStations.StationNumber2, "There are already two such stations on the list!!!");
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
                throw new ExceptionConsecutiveStations(stationNumber1, stationNumber2, "There are no two such stations on the list!!!");
            }
        }
        void IDAL.UpdatingConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            int index = DataSource.ConsecutiveStations.FindIndex(consecutiveStations1 => consecutiveStations1.StationNumber1 == consecutiveStations.StationNumber1 && consecutiveStations1.StationNumber2 == consecutiveStations.StationNumber2);
            DataSource.ConsecutiveStations[index] = index == -1 ? throw new ExceptionConsecutiveStations(consecutiveStations.StationNumber1, consecutiveStations.StationNumber2, "The consecutiveStations not exist in the compny!!!") : consecutiveStations.Clone();
        }
        float IDAL.DistanceBetweenTooStations(int numberStation1, int numberStation2)
        {
            int index = DataSource.ConsecutiveStations.FindIndex(consecutiveStations1 => consecutiveStations1.StationNumber1 == numberStation1 && consecutiveStations1.StationNumber2 == numberStation2);
            return index != -1
                ? DataSource.ConsecutiveStations[index].DistanceBetweenTooStations
                : throw new ExceptionConsecutiveStations(numberStation1, numberStation2, "There are no two such stations on the list!!!");
        }
        TimeSpan IDAL.AverageTimeBetweenTooStationsList(int numberStation1, int numberStation2)
        {
            int index = DataSource.ConsecutiveStations.FindIndex(consecutiveStations1 => consecutiveStations1.StationNumber1 == numberStation1 && consecutiveStations1.StationNumber2 == numberStation2);
            return index != -1
                ? DataSource.ConsecutiveStations[index].AverageTime
                : throw new ExceptionConsecutiveStations(numberStation1, numberStation2, "There are no two such stations on the list!!!");
        }
        ConsecutiveStations IDAL.ReturnConsecutiveStation(int stationNumber1, int stationNumber2)
        {
            ConsecutiveStations item = null;
            if (DataSource.ConsecutiveStations.Exists(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber2))
            {
                item = DataSource.ConsecutiveStations.Find(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber2);
            }
            return item.Clone() ?? throw new ExceptionConsecutiveStations(stationNumber1, stationNumber2, "There are no two such stations on the list!!!"); ;
        }
        IEnumerable<ConsecutiveStations> IDAL.ConsecutiveStationsList()
        {
            return from Consecutive in DataSource.ConsecutiveStations
                   select Consecutive;
        }
        #endregion ConsecutiveStations

        #region LineExit
        void IDAL.AddLineExit(LineExit lineExit)
        {
            if (DataSource.LineExits.Exists(lineExit1 => lineExit1.BusLineID1 == lineExit.BusLineID1 && lineExit1.LineStartTime == lineExit.LineStartTime))
            {
                throw new ExceptionLineExit(lineExit.BusLineID1, lineExit.LineStartTime, "the LineExit alrdy exist in the list in the same time");
            }
            else
            {
                DataSource.LineExits.Add(lineExit.Clone());
            }
        }
        void IDAL.DeleteLineExit(int lineNumber, TimeSpan StartTime)
        {
            int index = DataSource.LineExits.FindIndex(lineExit1 => lineExit1.BusLineID1 == lineNumber && lineExit1.LineStartTime == StartTime);
            if (index == -1)
            {
                throw new ExceptionLineExit(lineNumber, StartTime, "the LineExit not found!!!");
            }
            else
            {
                DataSource.LineExits.RemoveAt(index);
            }
        }
        void IDAL.UpdatingLineExit(LineExit lineExit)
        {
            int index = DataSource.LineExits.FindIndex(lineExit1 => lineExit1.BusLineID1 == lineExit.BusLineID1 && lineExit1.LineStartTime == lineExit.LineStartTime);
            DataSource.LineExits[index] = index == -1 ? throw new ExceptionLineExit(lineExit.BusLineID1, lineExit.LineStartTime, "The lineExit not exist in the compny") : lineExit.Clone();
        }
        LineExit IDAL.ReturnLineExit(int lineNumber, TimeSpan StartTime)
        {
            LineExit lineExit = DataSource.LineExits.FirstOrDefault(lineExit1 => lineExit1.BusLineID1 == lineNumber && lineExit1.LineStartTime == StartTime);
            return lineExit.Clone() ?? throw new ExceptionLineExit(lineNumber, StartTime, "the LineExit not exist in the list");
        }
        LineExit IDAL.OneLineExitFromList(int numberLine, TimeSpan StartTime)
        {
            LineExit b = DataSource.LineExits.FirstOrDefault(lineExit => lineExit.BusLineID1 == numberLine && lineExit.LineStartTime == StartTime);
            return b = default ? throw new ExceptionLineExit(numberLine, StartTime, "the line Exit dsnt Exist") : b;
        }
        IEnumerable<LineExit> IDAL.LineExitList(int numberLine)
        {
            return from lineExit in DataSource.LineExits
                   where lineExit.BusLineID1 == numberLine
                   select lineExit;
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
        //    //int index = DataSource.BusTravelings.FindIndex(BusTravelings1 => BusTravelings1.License_number1 == License_number && BusTravelings1.LineInExecution == LineExecution && BusTravelings1.LeavingTime == LeavingTime);
        //    //if (index == -1)
        //    //{
        //    //    throw new ExceptionDl("the BusTraveling not exist!!!");
        //    //}
        //    //else
        //    //{
        //    //    DataSource.LineExits.RemoveAt(index);
        //    //}
        //}
        //void IDAL.UpdatingBusTraveling(BusTraveling busTraveling)
        //{
        //    int index = DataSource.BusTravelings.FindIndex(BusTravelings1 => BusTravelings1.License_number1 == busTraveling.License_number1 && BusTravelings1.LineInExecution == busTraveling.LineInExecution && BusTravelings1.LeavingTime == busTraveling.LeavingTime);
        //    DataSource.BusTravelings[index] = index == -1 ? throw new ExceptionDl("The busTraveling not exist in the compny!!!") : busTraveling.Clone();
        //}
        //public BusTraveling ReturnBusTraveling(int LineExecution, string License_number, string LeavingTime)
        //{
        //    BusTraveling busTraveling = DataSource.BusTravelings.FirstOrDefault(BusTravelings1 => BusTravelings1.License_number1 == License_number && BusTravelings1.LineInExecution == LineExecution && BusTravelings1.LeavingTime == LeavingTime);
        //    return busTraveling.Clone();//?? throw new ExceptionDl("the busTraveling not exist in the list!!!");
        //}

        //public IEnumerable<BusTraveling> BusTravelingList()
        //{
        //    return from busTraveling in DataSource.BusTravelings
        //           select busTraveling;
        //}
        #endregion BusTraveling
    }
}





