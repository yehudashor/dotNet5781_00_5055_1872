using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace IBL
{
    public interface IBL1
    {
        #region BusBo
        // public void DeleteBusBO(int License_number);
        //public void AddBusBO(BusBO bus);
        // public BusLineBO BusInformation(int License_number);
        #endregion Bus


        #region BusLineStation
        public BusLineBO LineInformation(int numberLine);
        public void AddStationToLine(int LineNumber, int NumberStation);
        #endregion BusLineStation

        #region BusLine
        public void AddBusLineBO(BusLineBO busLineBO);
        public void DeleteBusLineBO(int NumberLine);
        #endregion BusLine

        #region Station
        public void AddStationToDo(BusStationBO busStationBO);
        public void DeleteStationFromDo(int numberOfStation);
        IEnumerable<BusLineBO> LinePastInStationBOs(int NumberStation);
        #endregion Station


    }
}
