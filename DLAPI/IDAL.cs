using System;
using System.Collections.Generic;
using DO;
/// <summary>
/// An interface which realizes the service contract towards the bl layer, according to the crud principle.
/// </summary>
namespace DLAPI
{
    /// <summary>
    /// Each entity has the option of adding, deleting, updating, requesting a single object, requesting a complete collection, requests according to filtering conditions. 
    /// Deletion is done with a Boolean field indicating the deletion of the entity.
    /// </summary>
    public interface IDAL
    {
        #region Bus
        void AddBus(Bus bus);

        void DeleteBus(string License_number);

        void UpdatingBus(Bus bus);

        Bus ReturnBusToBl(string License_number);

        IEnumerable<Bus> BusLists();
        #endregion Bus

        #region station
        void AddStation(BusStation station);
        void DeleteStation(int numberStation);
        void UpdatingStation(BusStation station);
        BusStation ReturnStation(int numberStation);
        IEnumerable<BusStation> StationList();
        #endregion station

        #region BusLine
        int AddBusLine(BusLine Line);
        void DeleteBusLine(int BusLineID);
        void UpdatingBusLine(BusLine Line);
        BusLine ReturnBusLine(int numberLineId);
        IEnumerable<BusLine> BusLinesList();
        int BusLineId();
        IEnumerable<BusLine> BusLinesList(int numberLine);
        #endregion BusLine

        #region LineStation
        void AddLineStation(LineStation lineStation);
        void DeleteOneLineStation(int NumberLine, int stationNumber);
        void DeleteLineStation(int NumberLine);
        void DeleteLineStation1(int NumberLine);
        LineStation ReturnLineStation(int numberLine, int stationNumber);
        IEnumerable<LineStation> LineStationList();
        void UpdatingLineStation(LineStation lineStation);
        IEnumerable<LineStation> OneLineFromList(Predicate<LineStation> predicate);
        IEnumerable<int> LinesFromList(int numberStation);
        #endregion LineStation

        #region ConsecutiveStations
        bool ChackExistingConsecutiveStations(Predicate<ConsecutiveStations> predicate);
        void AddConsecutiveStations(ConsecutiveStations consecutiveStations);
        void DeleteConsecutiveStations(int stationNumber1, int stationNumber2);
        void UpdatingConsecutiveStations(ConsecutiveStations consecutiveStations);
        ConsecutiveStations ReturnConsecutiveStation(int stationNumber1, int stationNumber2);
        IEnumerable<ConsecutiveStations> ConsecutiveStationsList();
        float DistanceBetweenTooStations(int numberStation1, int numberStation2);
        TimeSpan AverageTimeBetweenTooStationsList(int numberStation1, int numberStation2);
        IEnumerable<ConsecutiveStations> ConsecutiveStationsListPredicate(Predicate<ConsecutiveStations> predicate);
        #endregion ConsecutiveStations

        #region LineExit
        void AddLineExit(LineExit lineExit);
        void DeleteLineExit(int lineNumber, TimeSpan StartTime);
        void UpdatingLineExit(LineExit lineExit);
        LineExit ReturnLineExit(int lineNumber, TimeSpan StartTime);
        LineExit OneLineExitFromList(int numberLine, TimeSpan StartTime);
        IEnumerable<LineExit> LineExitList(int numberLine);
        #endregion LineExit

        #region BusTraveling
        //public void AddBusTraveling(BusTraveling busTraveling);
        //public void DeleteBusTraveling(int LineExecution, string License_number, string LeavingTime);
        //public void UpdatingBusTraveling(BusTraveling busTraveling);
        //public BusTraveling ReturnBusTraveling(int LineExecution, string License_number, string LeavingTime);
        //public IEnumerable<BusTraveling> BusTravelingList();
        #endregion BusTraveling

        #region  User
        void AddUser(User user);
        void DeleteUser(string Username1);
        void UpdatingUser(User user);
        User ReturnUser(string Username1);
        IEnumerable<User> UseresList();
        bool FindUser(string pass, string UserNam);
        #endregion User
    }
}
