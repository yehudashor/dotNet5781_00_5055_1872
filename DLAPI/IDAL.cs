using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DLAPI
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDAL
    {
        #region Bus
        public void AddBus(Bus bus);
        public void DeleteBus(string License_number);

        public void UpdatingBus(Bus bus);
        public Bus ReturnBusToBl(string License_number);

        public IEnumerable<Bus> BusLists();

        // public IEnumerable<object> FilteringBusList(Predicate<Bus> predicate);
        #endregion Bus

        #region station
        public void AddStation(BusStation station);
        public void DeleteStation(int numberStation);

        public void UpdatingStation(BusStation station);
        public BusStation ReturnStation(int numberStation);

        public IEnumerable<BusStation> StationList();
        #endregion station

        #region BusLine
        public int AddBusLine(BusLine Line);
        public void DeleteBusLine(int BusLineID);
        public void UpdatingBusLine(BusLine Line);
        public BusLine ReturnBusLine(int numberLineId);
        public IEnumerable<BusLine> BusLinesList();
        public int BusLineId();
        public IEnumerable<BusLine> BusLinesList(int numberLine);
        #endregion BusLine

        #region LineStation
        public void AddLineStation(LineStation lineStation);
        public void DeleteOneLineStation(int NumberLine, int stationNumber);
        public void DeleteLineStation(int NumberLine);
        public void DeleteLineStation1(int NumberLine);
        public LineStation ReturnLineStation(int numberLine, int stationNumber);
        public IEnumerable<LineStation> LineStationList();
        public void UpdatingLineStation(LineStation lineStation);
        public IEnumerable<LineStation> OneLineFromList(Predicate<LineStation> predicate);
        public IEnumerable<int> LinesFromList(int numberStation);

        // public IEnumerable<LineStation> OneLineFromList(Predicate<LineStation> predicate);
        #endregion LineStation

        #region ConsecutiveStations
        public bool ChackExistingConsecutiveStations(Predicate<ConsecutiveStations> predicate);
        public void AddConsecutiveStations(ConsecutiveStations consecutiveStations);
        public void DeleteConsecutiveStations(int stationNumber1, int stationNumber2);
        public void UpdatingConsecutiveStations(ConsecutiveStations consecutiveStations);
        public ConsecutiveStations ReturnConsecutiveStation(int stationNumber1, int stationNumber2);
        public IEnumerable<ConsecutiveStations> ConsecutiveStationsList();
        public float DistanceBetweenTooStations(int numberStation1, int numberStation2);
        public TimeSpan AverageTimeBetweenTooStationsList(int numberStation1, int numberStation2);
        // public IEnumerable<ConsecutiveStations> OneLineFromList(int numberLine);
        //  public IEnumerable<LineStation> OneLineFromList(Predicate<LineStation> predicate);
        #endregion ConsecutiveStations

        #region LineExit
        public void AddLineExit(LineExit lineExit);
        public void DeleteLineExit(int lineNumber, TimeSpan StartTime);
        public void UpdatingLineExit(LineExit lineExit);
        public LineExit ReturnLineExit(int lineNumber, TimeSpan StartTime);
        public LineExit OneLineExitFromList(int numberLine, TimeSpan StartTime);
        public IEnumerable<LineExit> LineExitList(int numberLine);

        // public IEnumerable<ConsecutiveStations> OneLineFromList(int numberLine);
        #endregion LineExit

        #region BusTraveling
        //public void AddBusTraveling(BusTraveling busTraveling);
        //public void DeleteBusTraveling(int LineExecution, string License_number, string LeavingTime);
        //public void UpdatingBusTraveling(BusTraveling busTraveling);
        //public BusTraveling ReturnBusTraveling(int LineExecution, string License_number, string LeavingTime);
        //public IEnumerable<BusTraveling> BusTravelingList();
        #endregion BusTraveling

        #region  User
        public void AddUser(User user);
        public void DeleteUser(string Username1);
        public void UpdatingUser(User user);
        public User ReturnUser(string Username1);
        public IEnumerable<User> UseresList();
        public bool FindUser(string pass, string UserNam);
        #endregion User
    }
}
