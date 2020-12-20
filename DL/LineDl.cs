using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using DS;
namespace DL
{
    public class LineDl : IDAL
    {
        // public static LineDl Instance => instance;
        // נשאר בקשות לפי תנאי סינון 
        // עדכון נשאר
        #region Bus
        void IDAL.AddBus(Bus bus)
        {
            if (DataSource.Bus1.Exists(bus1 => bus1.License_number == bus.License_number && bus1.IsAvailable == true))
            {
                throw new ArgumentException("The buses alrady exist in the compny");
            }
            else
            {
                DataSource.Bus1.Add(bus);
            }
        }
        void IDAL.DeleteBus(string License_number)
        {
            if (DataSource.Bus1.Exists(bus1 => bus1.License_number == License_number && bus1.IsAvailable == true))
            {
                foreach (Bus item in DataSource.Bus1)
                {
                    if (item.License_number == License_number)
                    {
                        item.IsAvailable = false;
                    }
                }
            }
            else
            {
                throw new ArgumentException("The buses not exist in the compny");
            }
        }

        Bus IDAL.ReturnBusToBl(string License_number)
        {
            Bus bus = DataSource.Bus1.Find(bus1 => bus1.License_number == License_number);
            return bus ?? throw new ArgumentNullException("The buses not exist in the compny");
        }

        IEnumerable<Bus> IDAL.BusList()
        {
            return from bus in DataSource.Bus1
                   where bus.IsAvailable
                   select bus;
        }
        #endregion Bus

        #region Station
        void IDAL.AddStation(BusStation station)
        {
            if (!DataSource.BusStations.Exists(station1 => station1.StationNumber == station.StationNumber && station1.IsAvailable3 == true))
            {
                throw new ArgumentException("The Starion alrady exist in the compny");
            }
            else
            {
                DataSource.BusStations.Add(station);
            }
        }

        void IDAL.DeleteStation(int numberStation)
        {
            if (DataSource.BusStations.Exists(station1 => station1.StationNumber == numberStation && station1.IsAvailable3 == true))
            {
                foreach (BusStation item in DataSource.BusStations)
                {
                    if (item.StationNumber == numberStation)
                    {
                        item.IsAvailable3 = false;
                    }
                }
            }
            else
            {
                throw new ArgumentException("The Station not exist in the compny");
            }
        }

        BusStation IDAL.ReturnStation(int numberStation)
        {
            BusStation station = DataSource.BusStations.Find(station1 => station1.StationNumber == numberStation);
            return station ?? throw new ArgumentNullException("The Station not exist in the compny");
        }

        IEnumerable<BusStation> IDAL.StationList()
        {
            return from station in DataSource.BusStations
                   where station.IsAvailable3 == true
                   select station;
        }
        #endregion Station

        #region BusLine
        void IDAL.AddBusLine(BusLine line)
        {
            line.BusLineID1 = NumbersAreRunning.BusLineID;
            NumbersAreRunning.BusLineID++;
            DataSource.BusLines.Add(line);
        }

        void IDAL.DeleteBusLine(int BusLineID)
        {
            if (DataSource.BusLines.Exists(line => line.BusLineID1 == BusLineID && line.IsAvailable1 == true))
            {
                foreach (BusLine item in DataSource.BusLines)
                {
                    if (item.BusLineID1 == BusLineID)
                    {
                        item.IsAvailable1 = false;
                    }
                }
            }
            else
            {
                throw new ArgumentException("The line not exist in the compny");
            }
        }

        BusLine IDAL.ReturnBusLine(int numberLineId)
        {
            BusLine busLine = DataSource.BusLines.Find(line => line.BusLineID1 == numberLineId);
            return busLine ?? throw new ArgumentNullException("The line not exist in the compny");
        }

        IEnumerable<BusLine> IDAL.BusLinesList()
        {
            return from line in DataSource.BusLines
                   where line.IsAvailable1 == true
                   select line;
        }
        #endregion BusLine







        #region LineStation

        void IDAL.AddLineStation(LineStation lineStation)
        {
            if (DataSource.LineStations.Exists(lineStation1 => lineStation1.BusLineID2 == lineStation.BusLineID2 && lineStation1.ChackDelete2))
            {
                bool flag = false;
                IEnumerable<LineStation> Chack = from chack in DataSource.LineStations
                                                 where chack.BusLineID2 == lineStation.BusLineID2
                                                 select chack;
                foreach (LineStation item in Chack)
                {
                    if (item.StationNumberOnLine == lineStation.StationNumberOnLine)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    DataSource.LineStations.Add(lineStation);
                }
                else
                {
                    throw new ArgumentException("the Station alrady exist in the this line");
                }
            }
            if (DataSource.LineStations.Exists(lineStation1 => lineStation1.BusLineID2 != lineStation.BusLineID2))
            {
                DataSource.LineStations.Add(lineStation);
            }
            if (DataSource.LineStations.Exists(lineStation1 => lineStation1.BusLineID2 == lineStation.BusLineID2 && !lineStation1.ChackDelete2))
            {
                throw new ArgumentException("the line is exist but he found as deleted");
            }
        }

        void IDAL.DeleteLineStation(int NumberLine, int stationNumber)
        {
            if (DataSource.LineStations.Exists(lineStation1 => lineStation1.BusLineID2 == NumberLine))
            {
                foreach (LineStation item in DataSource.LineStations)
                {
                    if (item.BusLineID2 == NumberLine && item.StationNumberOnLine == stationNumber)
                    {
                        item.ChackDelete2 = !item.ChackDelete2 ? throw new ArgumentException("the LineStation exist but she found as deleted") : false;
                    }
                }
            }
            else
            {
                throw new ArgumentException("the line isnt exist in the listDs");
            }
        }

        LineStation IDAL.ReturnLineStation(int numberLine, int stationNumber)
        {
            LineStation lineStation1 = DataSource.LineStations.Find(lineStation => lineStation.BusLineID2 == numberLine && lineStation.StationNumberOnLine == stationNumber);
            return lineStation1 ?? throw new ArgumentNullException("The lineStation not exist in the compny");
        }

        IEnumerable<LineStation> IDAL.LineStationList()
        {
            return from line in DataSource.LineStations
                   where line.ChackDelete2
                   select line;
        }


        IEnumerable<LineStation> IDAL.OneLineFromList(int numberLine)
        {
            IEnumerable<LineStation> OneLineStation = from line in DataSource.LineStations
                                                      where line.ChackDelete2 && line.BusLineID2 == numberLine
                                                      select line;
            return OneLineStation ?? throw new ArgumentNullException("the line dsnt exist in the compny");
        }
        #endregion LineStation
    }
}
