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
        //public IEnumerable<BusBO> BusInformation();

        #endregion Bus

        #region BusLineStation
        public BusLineBO LineInformation(int numberLine);
        public void AddStationToLine(StationLineBO stationLineBO);
        public void DeleteStationFromLine(int LineNumber, int NumberStationToDelete);
        public StationLineBO ReturnStationLine(int LineNumber, int numberStation);

        #endregion BusLineStation

        #region BusLine
        public void AddBusLineBO(BusLineBO busLineBO);
        public void DeleteBusLineBO(int NumberLine);
        //ReturnBusLineId()
        public int ReturnBusLineIdFromDl();
        #endregion BusLine

        #region Station
        public void AddStationToDo(BusStationBO busStationBO);
        public void DeleteStationFromDo(int numberOfStation);
        public BusStationBO ReturnStationToPL(int numberOfStation);
        public IEnumerable<BusStationBO> ShowStation();
        IEnumerable<BusLineBO> LinePastInStationBOs(int NumberStation);
        #endregion Station

        #region LineExit
        public void AddExitToLine(LineExitBo lineExitBo, int numberLine);
        public void DeleteLineExit(int numberOfLine, string startLineExit);
        public IEnumerable<LineExitBo> ShowlineExit(int numberLine);
        #endregion LineExit

        #region ConsecutiveStationsBo
        public IEnumerable<ConsecutiveStationsBo> ShowConsecutiveStationsBo();
        #endregion  ConsecutiveStationsBo

        #region Useres
        public void AddUserToDo(UserBo userBo);
        public void DeleteUserFromDo(string Username);
        public void UdptingUser(UserBo userBo);
        //public void ShowUser(string Username);
        public bool FindUser(string pass, string UserNam);
        #endregion Useres
    }
}
