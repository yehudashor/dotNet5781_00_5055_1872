using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using IBL;
using BO;
using DO;

namespace BL
{

    internal class InformationBusLineBl : BusLineBO, IBL1
    {
        internal IDAL dl = DLFactory.GetDL();
        // להציג את התחנות עוקבות בחלון של התחנות על ידי שאילתא מהתחנות עוקבות בDO
        // לעשות רשימה של כל התחנות

        #region BusLineStation
        // קווים שעוברים בתחנה
        IEnumerable<BusLineBO> IBL1.LinePastInStationBOs(int NumberStation)
        {
            try
            {
                IEnumerable<BusLineBO> busLineBOs = from Line in CollectionOfBusLinesBL.busLineBls
                                                    from Line1 in Line.BusStationBO1
                                                    where Line1.StationNumber == NumberStation
                                                    select Line;
                if (busLineBOs != null)
                {
                    var v = from line in busLineBOs
                            orderby line.LineNumber
                            select new
                            { id = line.BusLineID1, NumberLine = line.LineNumber, LastStation1 = line.BusStationBO1[BusStationBO1.Count].StationNumber, ur = line.UrbanInterUrban };
                    return (IEnumerable<BusLineBO>)v;
                }
                else
                {
                    throw new ArgumentException("there is no station that pased in this station!!!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // מידע על הקו שכולל את מעבר התחנות וכו
        BusLineBO IBL1.LineInformation(int numberLine)
        {
            BusLineBO busLineBO = new BusLineBO();
            try
            {
                BusLine busLine = dl.ReturnBusLine(numberLine);
                busLine.CopyPropertiesTo(busLineBO);
                IEnumerable<LineStation> lineStations = dl.OneLineFromList(numberLine);
                lineStations.CopyPropertiesTo(busLineBO.StationLineBOs);

                IEnumerable<LineStation> Result = from AddLineStationToDL in busLineBO.StationLineBOs
                                                  let p = ReturnNewLineStationDoFromBo(AddLineStationToDL)
                                                  orderby AddLineStationToDL.LocationNumberOnLine
                                                  select p;
                Result.CopyPropertiesTo(busLineBO.StationLineBOs);

                for (int i = 0; i < busLineBO.StationLineBOs.Count - 1; i++)
                {
                    busLineBO.DistanceBetweenTooStationsList.Add(dl.DistanceBetweenTooStations(busLineBO.StationLineBOs[i].StationNumberOnLine, busLineBO.StationLineBOs[i + 1].StationNumberOnLine));
                }
                for (int i = 0; i < busLineBO.StationLineBOs.Count - 1; i++)
                {
                    busLineBO.AverageTimeBetweenTooStationsList.Add(dl.AverageTimeBetweenTooStationsList(busLineBO.StationLineBOs[i].StationNumberOnLine, busLineBO.StationLineBOs[i + 1].StationNumberOnLine));
                }
            }
            catch (Exception)
            {

                throw;
            }
            return busLineBO;
        }
        #endregion BusLineStation

        #region Stations

        // הוספה תחנה בודדת
        void IBL1.AddStationToDo(BusStationBO busStationBO)
        {
            BusStation busStation = new BusStation
            {
                StationNumber = busStationBO.StationNumber,
                NameOfStation = busStationBO.NameOfStation,
                StationAddress = busStationBO.StationAddress,
                Latitude = busStationBO.Latitude,
                Longitude = busStationBO.Longitude,
                AccessForDisabled = busStationBO.AccessForDisabled,
                RoofToTheStation = busStationBO.RoofToTheStation,
                IsAvailable3 = busStationBO.IsAvailable3
            };
            try
            {
                dl.AddStation(busStation);
                // public List<StationLineBO> StationLineBOs { get; set; }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // מחיקת תחנה בודדת 
        void IBL1.DeleteStationFromDo(int numberOfStation)
        {
            try
            {
                dl.DeleteStation(numberOfStation);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Stations

        #region BusLine
        // הוספת קו בודד 
        void IBL1.AddBusLineBO(BusLineBO busLineBO)
        {
            // לבדוק תקינות הנתון
            try
            {
                if (busLineBO.BusStationBO1.Count > 2)
                {
                    for (int i = 0; i < BusStationBO1.Count - 1; i++)
                    {
                        ConsecutiveStationsBl consecutiveStations1 = new ConsecutiveStationsBl(BusStationBO1[i].StationNumber, BusStationBO1[i + 1].StationNumber);
                        ConsecutiveStations consecutiveStations = new ConsecutiveStations
                        {
                            StationNumber1 = consecutiveStations1.StationNumber1,
                            StationNumber2 = consecutiveStations1.StationNumber2,
                            DistanceBetweenTooStations = consecutiveStations1.DistanceBetweenTooStations,
                            AverageTime = consecutiveStations1.AverageTime
                        };
                        dl.AddConsecutiveStations(consecutiveStations);
                    }

                    BusLine busLine = new BusLine
                    {
                        LineNumber = busLineBO.LineNumber,
                        AreaBusUrban = (DO.Area1)(int)busLineBO.AreaBusUrban,
                        UrbanInterUrban = busLineBO.UrbanInterUrban,
                        FirstStation = busLineBO.BusStationBO1[0].StationNumber,
                        LastStation = busLineBO.BusStationBO1[busLineBO.BusStationBO1.Count].StationNumber,
                        IsAvailable1 = busLineBO.IsAvailable1
                    };
                    busLineBO.BusLineID1 = dl.AddBusLine(busLine);

                    for (int i = 0; i < busLineBO.BusStationBO1.Count; i++)
                    {
                        StationLineBO stationLineBO = new StationLineBO
                        {
                            BusLineID2 = busLineBO.BusLineID1,
                            StationNumberOnLine = busLineBO.BusStationBO1[i].StationNumber,
                            LocationNumberOnLine = i
                        };
                        busLineBO.StationLineBOs.Add(stationLineBO);
                    }
                    IEnumerable<LineStation> s = from AddLineStationToDL in busLineBO.StationLineBOs
                                                 let p = ReturnNewLineStationDoFromBo(AddLineStationToDL)
                                                 select p;
                    foreach (LineStation item in s)
                    {
                        dl.AddLineStation(item);
                    }
                }
                else
                {
                    //Exception inner תזכורת לבדוק איפה החריגה תתפס 
                    throw new ArgumentException("There must be at least two stations on the line!!!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        void IBL1.DeleteBusLineBO(int NumberLine)
        {
            try
            {
                dl.DeleteBusLine(NumberLine);
                dl.DeleteLineStation(NumberLine);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void AddStationToLine(int LineNumber, int NumberStation)
        {
            try
            {
                BusStationBO busStationBo = new BusStationBO();
                BusStation busStation = new BusStation();
                busStation = dl.ReturnStation(NumberStation);
                busStation.CopyPropertiesTo(busStationBo);
                int index = CollectionOfBusLinesBL.busLineBls.FindIndex(item => item.LineNumber == LineNumber);
                CollectionOfBusLinesBL.busLineBls[index].BusStationBO1.Add(busStationBo);

                LineStation lineStation = new LineStation
                {
                    BusLineID2 = LineNumber,
                    StationNumberOnLine = NumberStation,
                    LocationNumberOnLine = CollectionOfBusLinesBL.busLineBls[index].BusStationBO1.Count()
                };
                dl.AddLineStation(lineStation);

                ConsecutiveStationsBl consecutiveStationsBl = new ConsecutiveStationsBl(CollectionOfBusLinesBL.busLineBls[index].BusStationBO1[BusStationBO1.Count - 1].StationNumber, NumberStation);
                ConsecutiveStations consecutiveStations = new ConsecutiveStations
                {
                    StationNumber1 = consecutiveStationsBl.StationNumber1,
                    StationNumber2 = consecutiveStationsBl.StationNumber2,
                    DistanceBetweenTooStations = consecutiveStationsBl.DistanceBetweenTooStations,
                    AverageTime = consecutiveStationsBl.AverageTime
                };
                dl.AddConsecutiveStations(consecutiveStations);
                // הוספת פונקציה עם ערכי ברירת מחדל 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static LineStation ReturnNewLineStationDoFromBo(StationLineBO func1)
        {
            LineStation lineStation = new LineStation
            {
                BusLineID2 = func1.BusLineID2,
                StationNumberOnLine = func1.StationNumberOnLine,
                LocationNumberOnLine = func1.LocationNumberOnLine,
            };
            lineStation.ChackDelete2 = func1.ChackDelete2;
            return lineStation;
        }
        #endregion BusLine
    }
}


















/// <summary>
/// proprty to list.
/// </summary>
//public List<BusStationBO> RouteTheLine
//{
//    get => routeTheLine;
//    set => routeTheLine = value;
//}
//public bool UrbanInterUrban1
//{
//    get { return UrbanInterUrban; }
//    set { myVar = value; }
//}

//public Area Area1
//{
//    get { return myVar; }
//    //UrbanInterUrban
//    set {
//        if ()
//        {

//        }
//        //myVar = value; 
//    }
//}


//void IBL1.AddBusStationBO(BusStationBO busStationBO)
//{
//    // לבדוק תקינות הנתון

//    BusStation busStation = new BusStation();
//    busStationBO.CopyPropertiesTo(busStation);
//    try
//    {
//        dl.AddStation(busStation);
//    }
//    catch (Exception)
//    {

//        throw;
//    }
//}
