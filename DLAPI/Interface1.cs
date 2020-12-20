using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DLAPI
{
    public interface IDAL
    {
        #region Bus
        public void AddBus(Bus bus);
        public void DeleteBus(string License_number);

        public Bus ReturnBusToBl(string License_number);

        public IEnumerable<Bus> BusList();

        // public IEnumerable<object> FilteringBusList(Predicate<Bus> predicate);
        #endregion Bus



        #region station
        public void AddStation(BusStation station);
        public void DeleteStation(int numberStation);

        public BusStation ReturnStation(int numberStation);

        public IEnumerable<BusStation> StationList();
        #endregion station


        #region BusLine
        public void AddBusLine(BusLine Line);
        public void DeleteBusLine(int BusLineID);
        public BusLine ReturnBusLine(int numberLineId);
        public IEnumerable<BusLine> BusLinesList();
        #endregion BusLine

        #region LineStation
        public void AddLineStation(LineStation lineStation);
        public void DeleteLineStation(int NumberLine, int stationNumber);
        public LineStation ReturnLineStation(int numberLine, int stationNumber);
        public IEnumerable<LineStation> LineStationList();

        public IEnumerable<LineStation> OneLineFromList(int numberLine);
        //  public IEnumerable<LineStation> OneLineFromList(Predicate<LineStation> predicate);
        #endregion LineStation

    }
}
