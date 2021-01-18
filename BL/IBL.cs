using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BO;
using DO;

namespace BLAPI
{
    public interface IBL1
    {
        #region BusBo
        public void AddBusBO(BusBO bus);
        public void DeleteBusBO(string License_number);
        public BusBO ShowBus(string License_number);
        public IEnumerable<BusBO> ShowAllBus();
        //public IEnumerable<BusBO> BusInformation();

        #endregion Bus

        #region BusLineStation
        public BusLineBO LineInformation(int numberLine);
        public void AddStationToLine(StationLineBO stationLineBO, int NumberStation1, int NumberStation2, TimeSpan timeSpan, float distance);
        public void DeleteStationFromLine(int LineNumber, int NumberStationToDelete);
        public StationLineBO ReturnStationLine(int LineNumber, int numberStation);
        public IEnumerable<StationLineBO> ReturnLineStationList(int LineNumber);
        #endregion BusLineStation

        #region BusLine
        public void AddBusLineBO(BusLineBO busLineBO);
        public void DeleteBusLineBO(int NumberLine);
        //ReturnBusLineId()
        public int ReturnBusLineIdFromDl();
        public void UdaptingLine(BusLineBO busLineBO);
        #endregion BusLine

        #region Station
        public void AddStationToDo(BusStationBO busStationBO);
        public void DeleteStationFromDo(int numberOfStation);
        public BusStationBO ReturnStationToPL(int numberOfStation);
        public IEnumerable<BusStationBO> ShowStation();
        IEnumerable<BusLineBO> LinePastInStationBOs(int NumberStation);
        #endregion Station

        #region LineExit
        public void AddExitToLine(LineExitBo lineExitBo);
        public void DeleteLineExit(int numberOfLine, TimeSpan StartTime);
        public LineExitBo GetLineExit(int numberOfLine, TimeSpan StartTime);
        public IEnumerable<LineExitBo> ShowlineExit(int numberLine);
        #endregion LineExit

        #region LineTrip
        public TimeSpan TravelTimeBetweenTwoStations(int LineNumber, int NumberStation1, int NumberStation2);
        public IEnumerable<LineTrip> LineTrips(int NumberStation, TimeSpan Time, int numberStationDestination = 0);
        void StartSimulator(TimeSpan startTime, int Rate, Action<TimeSpan> updateTime);
        void StopSimulator();
        #endregion LineTrip

        #region ConsecutiveStationsBo
        public IEnumerable<ConsecutiveStationsBo> ShowConsecutiveStationsBo();
        public ConsecutiveStationsBo ShowOneConsecutiveStations(int number1, int number2);
        public void Udapting(ConsecutiveStationsBo consecutiveStationsBo);
        #endregion  ConsecutiveStationsBo

        #region Useres
        public IEnumerable<UserBo> GetListUsers();
        public void AddUserToDo(UserBo userBo);
        public void DeleteUserFromDo(string Username);
        public void UdptingUser(UserBo userBo);
        //public void ShowUser(string Username);
        public bool FindUser(string pass, string UserNam);
        public UserBo GetUser(string UserName);
        #endregion Useres
    }
}
