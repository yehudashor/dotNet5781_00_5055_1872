using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAPI;
using DO;
//using DO;

namespace DL
{
    sealed class DLXML : IDAL    //internal
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML()
        {
            // InitializationConsecutiveStations();
            //  InitializationLineExits();
        }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion singelton
        //UserXml.xml
        // BusXml.xml
        //StationXml.xml
        //LineXml.xml
        //LineStation.xml
        //ConsecutiveStations.xml
        //LineExit.xml
        public static string busXml = @"BusXml.xml";
        public static string stationXml = @"StationXml.xml";
        public static string LineXml = @"LineXml.xml";
        public static string ConsecutiveStationsXml = @"ConsecutiveStations.xml";
        public static string LineStationXml = @"LineStation.xml";
        public static string LineExitXml = @"LineExit.xml";
        public static Random R = new Random(DateTime.Now.Millisecond);
        //        public static List<Bus> Bus1 = new List<Bus>();
        //        public static List<BusStation> BusStations;
        //public static List<ConsecutiveStations> ConsecutiveStations;
        //        public static List<LineStation> LineStations;
        //        public static List<BusLine> BusLines;
        //        public static List<BusTraveling> BusTravelings = new List<BusTraveling>();
        //        public static List<LineExit> LineExits;
        //        public static int BusLineID = 0;
        //        public static void InitializationLineExits()
        //        {
        //            LineExits = new List<LineExit>
        //            {
        //                new LineExit
        //                {
        //                    BusLineID1 = 0,
        //                    LineStartTime = new TimeSpan(6, 0, 0),
        //                    LineFinishTime = new TimeSpan(10, 0, 0),
        //                    LineFrequencyTime = new TimeSpan(0, 10, 0),
        //                    LineFrequency = 24,
        //                },
        //                new LineExit
        //                {
        //                    BusLineID1 = 0,
        //                    LineStartTime = new TimeSpan(10, 0, 0),
        //                    LineFinishTime = new TimeSpan(15, 0, 0),
        //                    LineFrequencyTime = new TimeSpan(0, 20, 0),
        //                    LineFrequency = 15,
        //                },
        //                new LineExit
        //                {
        //                    BusLineID1 = 0,
        //                    LineStartTime = new TimeSpan(15, 0, 0),
        //                    LineFinishTime = new TimeSpan(23, 0, 0),
        //                    LineFrequencyTime = new TimeSpan(0, 15, 0),
        //                    LineFrequency = 32,
        //                },



        //                new LineExit
        //                {
        //                    BusLineID1 = 1,
        //                    LineStartTime = new TimeSpan(6, 0, 0),
        //                    LineFinishTime = new TimeSpan(15, 0, 0),
        //                    LineFrequencyTime = new TimeSpan(0, 15, 0),
        //                    LineFrequency = 36,
        //                },


        //                new LineExit
        //                {
        //                    BusLineID1 = 1,
        //                    LineStartTime = new TimeSpan(15, 0, 0),
        //                    LineFinishTime = new TimeSpan(23, 0, 0),
        //                    LineFrequencyTime = new TimeSpan(0, 10, 0),
        //                    LineFrequency = 48,
        //                },


        //                new LineExit
        //                {
        //                    BusLineID1 = 2,
        //                    LineStartTime = new TimeSpan(10, 0, 0),
        //                    LineFinishTime = new TimeSpan(20, 0, 0),
        //                    LineFrequencyTime = new TimeSpan(2, 0, 0),
        //                    LineFrequency = 5
        //                },

        //                new LineExit
        //                {
        //                    BusLineID1 = 3,
        //                    LineStartTime = new TimeSpan(8, 0, 0),
        //                     LineFinishTime = new TimeSpan(20, 0, 0),
        //                     LineFrequencyTime = new TimeSpan(1, 0, 0),
        //                     LineFrequency = 12
        //                },

        //                 new LineExit
        //                {
        //                    BusLineID1 = 4,
        //                    LineStartTime = new TimeSpan(6, 0, 0),
        //                     LineFinishTime = new TimeSpan(20, 0, 0),
        //                     LineFrequencyTime = new TimeSpan(0, 30, 0),
        //                     LineFrequency = 28
        //                },

        //                  new LineExit
        //                {
        //                    BusLineID1 = 5,
        //                    LineStartTime = new TimeSpan(5, 0, 0),
        //                     LineFinishTime = new TimeSpan(23, 55, 0),
        //                     LineFrequencyTime = new TimeSpan(0, 20, 0),
        //                     LineFrequency = 58
        //                },

        //                   new LineExit
        //                {
        //                    BusLineID1 = 6,
        //                    LineStartTime = new TimeSpan(7, 0, 0),
        //                },
        //                new LineExit
        //                {
        //                    BusLineID1 = 7,
        //                    LineStartTime = new TimeSpan(14, 0, 0),
        //                },
        //            };
        //            XMLTools.SaveListToXMLSerializer(LineExits, LineExitXml);
        //        }
        //        public static void InitializationBus()
        //        {
        //            for (int i = 0; i < 100; i++)
        //            {
        //                int number = R.Next(1200);
        //                Bus bus = new Bus
        //                {
        //                    StartDate = new DateTime(R.Next(1999, 2020), R.Next(1, 13), R.Next(1, 29)),
        //                    KmForRefueling = number,
        //                    KmForTreatment = number * R.Next(3, 10),
        //                    TotalMiles = number * R.Next(20, 25),
        //                    Status = (TravelMode)R.Next(4),
        //                    IsAvailable = true
        //                };
        //                bus.License_number = bus.StartDate.Year >= 2018 ? R.Next(10000000, 100000000).ToString() : R.Next(1000000, 10000000).ToString();
        //                Bus1.Add(bus);
        //            }
        //            XMLTools.SaveListToXMLSerializer(Bus1, busXml);
        //        }
        //        public static void InitializationStation()
        //        {
        //            BusStations = new List<BusStation>
        //            {
        //                new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 10045,
        //                    NameOfStation = ", שדרות הרצל/ המלאכה",
        //                    StationAddress = "רחוב:שדרות הרצל  עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 10509,
        //                    NameOfStation = " שד.הרצל/משעול איריס",
        //                    StationAddress = " :שדרות הרצל 49 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                 new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 10510
        //,
        //                    NameOfStation = "הרצל/עיריית אופקים ",
        //                    StationAddress = " שדרות הרצל 31 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                  new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 10836,
        //                    NameOfStation = "הרצל/ז'בוטינסקי ",
        //                    StationAddress = " :שדרות הרצל 26 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                   new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 10837,
        //                    NameOfStation = "שדרות הרצל/גיבורי ישראל ",
        //                    StationAddress = " :שדרות הרצל 52 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                    new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 10847,
        //                    NameOfStation = " מכבי אש",
        //                    StationAddress = ":הנשיא 21 עיר: אופקים  ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                     new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =13426 ,
        //                    NameOfStation = "דרך הטייסים/דרך הנביאים ",
        //                    StationAddress = ":דרך הטייסים 17 עיר: אופקים  ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                      new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14531,
        //                    NameOfStation = "ז'בוטינסקי/נחושתן ",
        //                    StationAddress = " :ז'בוטינסקי 16 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                       new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14533,
        //                    NameOfStation = "שוק ",
        //                    StationAddress = " הרב חיים חורי 20 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                        new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14534,
        //                    NameOfStation = "מכבי האש ",
        //                    StationAddress = " :הנשיא 21 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                         new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14537,
        //                    NameOfStation = "החרוב/הארזים ",
        //                    StationAddress = " :החרוב 9 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                          new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14538,
        //                    NameOfStation = "החרוב/דרך בית וגן ",
        //                    StationAddress = "החרוב 9 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                 new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =14540 ,
        //                    NameOfStation = "חפץ חיים/רפאל אלנקווה ",
        //                    StationAddress = "חפץ חיים 4 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                  new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =14541 ,
        //                    NameOfStation = " חפץ חיים/רפאל אלנקווה",
        //                    StationAddress = " :חפץ חיים 2 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                   new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14542,
        //                    NameOfStation = " חפץ חיים/שבזי",
        //                    StationAddress = ":חפץ חיים 16 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                    new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =14904 ,
        //                    NameOfStation = " דוד המלך/אסא",
        //                    StationAddress = " :דוד המלך 72 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                     new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =14905 ,
        //                    NameOfStation = "דוד המלך/אסא ",
        //                    StationAddress = " :דוד המלך 75 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                      new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =14906,
        //                    NameOfStation = " קיבוץ גלויות/ש''ד הרצל",
        //                    StationAddress = " :קיבוץ גלויות 4 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                       new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =14908 ,
        //                    NameOfStation = " קיבוץ גלויות/רחבת דדו",
        //                    StationAddress = ":קיבוץ גלויות 48 עיר: אופקים  ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                        new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14909,
        //                    NameOfStation = " קיבוץ גלויות/קדש",
        //                    StationAddress = " :קיבוץ גלויות 66 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                         new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14910,
        //                    NameOfStation = "קיבוץ גלויות/חיים חורי ",
        //                    StationAddress = " :קיבוץ גלויות 55 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                          new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14912,
        //                    NameOfStation = " חיים חורי/פרי מגדים",
        //                    StationAddress = " פרי מגדים  עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                 new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =14913 ,
        //                    NameOfStation = " פרי מגדים/חיים חורי",
        //                    StationAddress = " :פרי מגדים  עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                  new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14917,
        //                    NameOfStation = " פרי מגדים/העינב",
        //                    StationAddress = " :פרי מגדים 42 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                   new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14918,
        //                    NameOfStation = " פרי מגדים/העינב",
        //                    StationAddress = " :פרי מגדים 42 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                    new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14919,
        //                    NameOfStation = "משה שרת/השקד ",
        //                    StationAddress = ":משה שרת 3 עיר: אופקים  ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                     new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14920,
        //                    NameOfStation = "משה שרת/שדרות הרצל ",
        //                    StationAddress = " משה שרת 6 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                      new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14921,
        //                    NameOfStation = "משה שרת/החרוב ",
        //                    StationAddress = " משה שרת 36 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                       new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =14922 ,
        //                    NameOfStation = " משה שרת/החרוב",
        //                    StationAddress = ":משה שרת 23 עיר: אופקים  ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                        new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14925,
        //                    NameOfStation = " ירושלים/קרית ספר",
        //                    StationAddress = ":ירושלים 41 עיר: אופקים  ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                         new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14926,
        //                    NameOfStation = "קרית ספר/אליהו התשבי ",
        //                    StationAddress = ":קרית ספר 48 עיר: אופקים  ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                          new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =14927 ,
        //                    NameOfStation = " קרית ספר/אליהו התשבי",
        //                    StationAddress = " קרית ספר 79 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                 new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =14928 ,
        //                    NameOfStation = "מגדל המים ",
        //                    StationAddress = " :דרך הנביאים 4 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                  new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = false,
        //                    AccessForDisabled =false,
        //                    StationNumber = 14929,
        //                    NameOfStation = " מגדל המים",
        //                    StationAddress = " בדרך הנביאים 11 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                   new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14930,
        //                    NameOfStation = "קרית ספר/יואל ",
        //                    StationAddress = " קרית ספר 8 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                    new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14931,
        //                    NameOfStation = " פרי מגדים/התמר",
        //                    StationAddress = ":פרי מגדים 86 עיר: אופקים  ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                     new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14932,
        //                    NameOfStation = " פרי מגדים/ערבה",
        //                    StationAddress = "פרי מגדים 96 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                      new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = false,
        //                    AccessForDisabled = true,
        //                    StationNumber = 14933,
        //                    NameOfStation = " פרי מגדים/הגורן",
        //                    StationAddress = " פרי מגדים 124 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                       new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = false,
        //                    StationNumber = 14934,
        //                    NameOfStation = " פרי מגדים/ארבעת המינים",
        //                    StationAddress = " :פרי מגדים 124 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },

        //                        new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = false,
        //                    StationNumber = 14935,
        //                    NameOfStation = " דוד המלך/שלמה המלך",
        //                    StationAddress = ":דוד המלך 14 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                         new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = false,
        //                    StationNumber = 14936,
        //                    NameOfStation = " דוד המלך/שלמה המלך",
        //                    StationAddress = "דוד המלך 7 עיר: אופקים  ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                          new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =15285 ,
        //                    NameOfStation = " שד. הרצל/מבצע קדש",
        //                    StationAddress = ":שדרות הרצל 27 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //                 new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =15287 ,
        //                    NameOfStation = " החיד''א/בן איש חי",
        //                    StationAddress = " החיד''א 13 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },

        //                  new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = false,
        //                    StationNumber =15289 ,
        //                    NameOfStation = "דוד בוזגלו/רפאל אלנקווה ",
        //                    StationAddress = " :דוד בוזגלו  עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },

        //                   new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = false,
        //                    AccessForDisabled = true,
        //                    StationNumber = 15290,
        //                    NameOfStation = " דוד בוזגלו/רפאל אלנקווה",
        //                    StationAddress = " :דוד בוזגלו  עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },

        //                    new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =15291 ,
        //                    NameOfStation = "שד. הרצל/הנשיא ",
        //                    StationAddress = " שדרות הרצל  עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },

        //                     new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = true,
        //                    StationNumber =15294 ,
        //                    NameOfStation = " שדרות הרצל/כצנלסון",
        //                    StationAddress = " שדרות הרצל 48 עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },

        //                      new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation =false,
        //                    AccessForDisabled = true,
        //                    StationNumber = 15296,
        //                    NameOfStation = " שדרות הרצל",
        //                    StationAddress = " שדרות הרצל 85 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },

        //                       new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = true,
        //                    AccessForDisabled = false,
        //                    StationNumber = 15298,
        //                    NameOfStation = " דרך הטייסים/דרך בוזגלו",
        //                    StationAddress = ":דרך הטייסים  עיר: אופקים  ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },

        //                        new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = false,
        //                    AccessForDisabled = true,
        //                    StationNumber = 15299,
        //                    NameOfStation = "דרך הטייסים/רבי עקיבא ",
        //                    StationAddress = " :דרך הטייסים 49 עיר: אופקים ",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },

        //                         new BusStation
        //                {
        //                    IsAvailable3 = true,
        //                    RoofToTheStation = false,
        //                    AccessForDisabled = false,
        //                    StationNumber = 15300,
        //                    NameOfStation = " בית ספר שושנים/החיד''א",
        //                    StationAddress = " :החיד''א  עיר: אופקים",
        //                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
        //                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
        //                },
        //            };
        //            XMLTools.SaveListToXMLSerializer(BusStations, stationXml);
        //        }
        //        public static void InitializationLine()
        //        {
        //            BusLines = new List<BusLine>
        //            {
        //                #region קו 40 אופקים
        //                new BusLine
        //                {
        //                     LineNumber = 40,
        //                     GetAvailable = Available.Available,
        //                     GetUrban = Urban.Urban,
        //                     AreaBusUrban = Area1.South,
        //                     FirstStation = 10045,
        //                     LastStation=14540,
        //                },
        //                #endregion קו 40 אופקים

        //                new BusLine
        //                {
        //                     LineNumber = 41,
        //                     GetAvailable = Available.Available,
        //                     GetUrban = Urban.Urban,
        //                     AreaBusUrban = Area1.South,
        //                     FirstStation=10510,
        //                     LastStation=14538,
        //                },

        //                new BusLine
        //                {
        //                     LineNumber = 42,
        //                      GetAvailable = Available.Available,
        //                     GetUrban = Urban.Urban,
        //                     AreaBusUrban = Area1.South,
        //                     FirstStation=14540,
        //                     LastStation=14912,
        //                },

        //                new BusLine
        //                {
        //                     LineNumber = 43,
        //                     GetAvailable = Available.Available,
        //                     GetUrban = Urban.Urban,
        //                     AreaBusUrban = Area1.South,
        //                     FirstStation=14905,
        //                     LastStation=14919,
        //                },

        //                new BusLine
        //                {
        //                     LineNumber = 44,
        //                     GetAvailable = Available.Available,
        //                     GetUrban = Urban.Urban,
        //                     AreaBusUrban = Area1.South,
        //                     FirstStation= 14540,
        //                     LastStation=14912,
        //                },

        //                new BusLine
        //                {
        //                     LineNumber = 45,
        //                      GetAvailable = Available.Available,
        //                     GetUrban = Urban.Urban,
        //                     AreaBusUrban = Area1.South,
        //                     FirstStation=14533,
        //                     LastStation=14906,
        //                },

        //                new BusLine
        //                {
        //                     LineNumber = 46,
        //                      GetAvailable = Available.Available,
        //                     GetUrban = Urban.Urban,
        //                     AreaBusUrban = Area1.South,
        //                     FirstStation=14931,
        //                     LastStation=15289,
        //                },

        //                new BusLine
        //                {
        //                     LineNumber = 47,
        //                      GetAvailable = Available.Available,
        //                     GetUrban = Urban.Urban,
        //                     AreaBusUrban = Area1.South,
        //                     FirstStation=14927,
        //                     LastStation=14936
        //,
        //                },

        //                new BusLine
        //                {
        //                     LineNumber = 48,
        //                      GetAvailable = Available.Available,
        //                     GetUrban = Urban.Urban,
        //                     AreaBusUrban = Area1.South,
        //                     FirstStation=10509,
        //                     LastStation=14541,
        //                },

        //                new BusLine
        //                {
        //                     LineNumber = 49,
        //                      GetAvailable = Available.Available,
        //                     GetUrban = Urban.Urban,
        //                     AreaBusUrban = Area1.South,
        //                     FirstStation=14541,
        //                     LastStation=15300,
        //                },

        //                new BusLine
        //                {
        //                     LineNumber = 50,
        //                      GetAvailable = Available.Notavailable,
        //                     GetUrban = Urban.Urban,
        //                     AreaBusUrban = Area1.South,
        //                     FirstStation=14917,
        //                     LastStation=14928,
        //                },
        //            };

        //            for (int i = 0; i < BusLines.Count; i++)
        //            {
        //                BusLines[i].BusLineID1 = BusLineID;
        //                ++BusLineID;
        //            }
        //            XMLTools.SaveListToXMLSerializer(BusLines, LineXml);
        //        }
        //        public static void InitializationLineStation()
        //        {
        //            //10045 10509 10510 13426 14531 14533 14534 14537 14538 14540
        //            //10510 10836 10837 10847 13426 14531 14533 14534 14537 14538
        //            //14540 14541 14542 14904 14905 14906 14908 14909 14910 14912
        //            //14905 14906 14908 14909 14910 14912 14913 14917 14918 14919
        //            //14540 14541 14542 14904 14905 14906 14908 14909 14910 14912 
        //            //14533 14534 14537 14538 14540 14541 14542 14904 14905 14906
        //            //14931 14932 14933 14934 14935 14936 15285 15287 15289 15290
        //            //14927 14928 14929 14930 14931 14932 14933 14934 14935 14936
        //            //10509 10847 13426 14531 14533 14534 14537 14538 14540 14541
        //            //14541 14542 14904 14905 14906 14908 14909 14910 14912 15300
        //            //14917 14918 14919 14920 14921 14922 14925 14926 14927 14928
        //            LineStations = new List<LineStation>
        //            #region  40 תחנות קו מס' 
        //            {
        //                new LineStation
        //                {
        //                    BusLineID2 = 0,
        //                    StationNumberOnLine = 10045,
        //                    ChackDelete2 = true,
        //                    LocationNumberOnLine = 0,
        //                },
        //                new LineStation
        //                {
        //                    BusLineID2 = 0,
        //                    StationNumberOnLine = 10509,

        //                    ChackDelete2 = true,
        //                    LocationNumberOnLine = 1,
        //                },
        //                new LineStation
        //                {
        //                    BusLineID2 = 0,
        //                    StationNumberOnLine = 10510,

        //                    ChackDelete2 = true,
        //                    LocationNumberOnLine = 2,
        //                },
        //                new LineStation
        //                {
        //                    BusLineID2 = 0,
        //                    StationNumberOnLine = 13426,
        //                    ChackDelete2 = true,
        //                    LocationNumberOnLine = 3,
        //                },
        //                new LineStation
        //                {
        //                    BusLineID2 = 0,
        //                    StationNumberOnLine = 14531,
        //                    ChackDelete2 = true,
        //                    LocationNumberOnLine = 4,
        //                },
        //                new LineStation
        //                {
        //                    BusLineID2 = 0,
        //                    StationNumberOnLine = 14533,
        //                    ChackDelete2 = true,
        //                    LocationNumberOnLine = 5,
        //                },
        //                new LineStation
        //                {
        //                    BusLineID2 = 0,
        //                    StationNumberOnLine = 14534,
        //                    ChackDelete2 = true,
        //                    LocationNumberOnLine = 6,
        //                },
        //                new LineStation
        //                {
        //                    BusLineID2 = 0,
        //                    StationNumberOnLine = 14537,
        //                    ChackDelete2 = true,
        //                    LocationNumberOnLine = 7,
        //                },
        //                new LineStation
        //                {
        //                    BusLineID2 = 0,
        //                    StationNumberOnLine = 14538,
        //                    ChackDelete2 = true,
        //                    LocationNumberOnLine = 8,
        //                },
        //                new LineStation
        //                {
        //                    BusLineID2 = 0,
        //                    StationNumberOnLine = 14540,
        //                    ChackDelete2 = true,
        //                    LocationNumberOnLine = 9,
        //                },
        //                #endregion
        //                #region 41 -1תחנות קו מס' 

        //                    new LineStation
        //                    {
        //                        BusLineID2 = 1,
        //                        StationNumberOnLine = 10510,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 0,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 1,
        //                        StationNumberOnLine = 10836,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 1,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 1,
        //                        StationNumberOnLine = 10837,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 2,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 1,
        //                        StationNumberOnLine = 10847,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 3,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 1,
        //                        StationNumberOnLine = 13426,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 4,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 1,
        //                        StationNumberOnLine = 14531,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 5,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 1,
        //                        StationNumberOnLine = 14533,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 6,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 1,
        //                        StationNumberOnLine = 14534,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 7,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 1,
        //                        StationNumberOnLine = 14537,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 8,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 1,
        //                        StationNumberOnLine = 14538,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 9,
        //                    },
        //                    #endregion 41 -1תחנות קו מס'
        //                    #region 42-2תחנות קו מס' 
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 2,
        //                        StationNumberOnLine = 14540,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 0,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 2,
        //                        StationNumberOnLine = 14541,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 1,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 2,
        //                        StationNumberOnLine = 14542,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 2,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 2,
        //                        StationNumberOnLine = 14904,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 3,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 2,
        //                        StationNumberOnLine = 14905,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 4,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 2,
        //                        StationNumberOnLine = 14906,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 5,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 2,
        //                        StationNumberOnLine = 14908,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 6,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 2,
        //                        StationNumberOnLine = 14909,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 7,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 2,
        //                        StationNumberOnLine = 14910,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 8,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 2,
        //                        StationNumberOnLine = 14912,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 9,
        //                    },
        //                    #endregion
        //                    #region 43-3תחנות קו מס' 

        //                    new LineStation
        //                    {
        //                        BusLineID2 = 3,
        //                        StationNumberOnLine = 14905,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 0,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 3,
        //                        StationNumberOnLine = 14906,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 1,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 3,
        //                        StationNumberOnLine = 14908,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 2,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 3,
        //                        StationNumberOnLine = 14909,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 3,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 3,
        //                        StationNumberOnLine = 14910,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 4,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 3,
        //                        StationNumberOnLine = 14912,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 5,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 3,
        //                        StationNumberOnLine = 14913,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 6,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 3,
        //                        StationNumberOnLine = 14917,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 7,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 3,
        //                        StationNumberOnLine = 14918,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 8,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 3,
        //                        StationNumberOnLine = 14919,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 9,
        //                    },
        //                    #endregion
        //                    #region 44-4תחנות קו מס

        //                    new LineStation
        //                    {
        //                        BusLineID2 = 4,
        //                        StationNumberOnLine = 14540,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 0,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 4,
        //                        StationNumberOnLine = 14541,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 1,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 4,
        //                        StationNumberOnLine = 14542,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 2,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 4,
        //                        StationNumberOnLine = 14904,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 3,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 4,
        //                        StationNumberOnLine = 14905,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 4,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 4,
        //                        StationNumberOnLine = 14906,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 5,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 4,
        //                        StationNumberOnLine = 14908,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 6,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 4,
        //                        StationNumberOnLine = 14909,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 7,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 4,
        //                        StationNumberOnLine = 14910,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 8,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 4,
        //                        StationNumberOnLine = 14912,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 9,
        //                    },
        //                    #endregion
        //                    #region 45-5תחנות קו מס' 
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 5,
        //                        StationNumberOnLine = 14533,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 0,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 5,
        //                        StationNumberOnLine = 14534,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 1,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 5,
        //                        StationNumberOnLine = 14537,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 2,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 5,
        //                        StationNumberOnLine = 14538,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 3,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 5,
        //                        StationNumberOnLine = 14540,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 4,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 5,
        //                        StationNumberOnLine = 14541,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 5,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 5,
        //                        StationNumberOnLine = 14542,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 6,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 5,
        //                        StationNumberOnLine = 14904,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 7,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 5,
        //                        StationNumberOnLine = 14905,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 8,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 5,
        //                        StationNumberOnLine = 14906,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 9,
        //                    },
        //                    #endregion
        //                    #region תחנות קו מס46-6' 

        //                    new LineStation
        //                    {
        //                        BusLineID2 = 6,
        //                        StationNumberOnLine = 14931,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 0,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 6,
        //                        StationNumberOnLine = 14932,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 1,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 6,
        //                        StationNumberOnLine = 14933,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 2,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 6,
        //                        StationNumberOnLine = 14934,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 3,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 6,
        //                        StationNumberOnLine = 14935,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 4,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 6,
        //                        StationNumberOnLine = 14936,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 5,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 6,
        //                        StationNumberOnLine = 15285,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 6,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 6,
        //                        StationNumberOnLine = 15287,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 7,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 6,
        //                        StationNumberOnLine = 15289,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 8,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 6,
        //                        StationNumberOnLine = 15290,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 9,
        //                    },
        //                    #endregion
        //                    #region 47-7תחנות קו מס' 
        // 14927 14928 14929 14930 14931  14932 14933 14934 14935
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 7,
        //                        StationNumberOnLine = 14927,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 0,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 7,
        //                        StationNumberOnLine = 14928,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 1,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 7,
        //                        StationNumberOnLine = 14929,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 2,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 7,
        //                        StationNumberOnLine = 14930,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 3,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 7,
        //                        StationNumberOnLine = 14931,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 4,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 7,
        //                        StationNumberOnLine = 14932,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 5,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 7,
        //                        StationNumberOnLine = 14933,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 6,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 7,
        //                        StationNumberOnLine = 14934,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 7,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 7,
        //                        StationNumberOnLine = 14935,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 8,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 7,
        //                        StationNumberOnLine = 14936,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 9,
        //                    },
        //                    #endregion
        //                    #region 48-8תחנות קו מס' 

        //                    new LineStation
        //                    {
        //                        BusLineID2 = 8,
        //                        StationNumberOnLine = 10509,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 0,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 8,
        //                        StationNumberOnLine = 10847,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 1,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 8,
        //                        StationNumberOnLine = 13426,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 2,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 8,
        //                        StationNumberOnLine = 14531,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 3,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 8,
        //                        StationNumberOnLine = 14533,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 4,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 8,
        //                        StationNumberOnLine = 14534,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 5,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 8,
        //                        StationNumberOnLine = 14537,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 6,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 8,
        //                        StationNumberOnLine = 14538,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 7,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 8,
        //                        StationNumberOnLine = 14540,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 8,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 8,
        //                        StationNumberOnLine = 14541,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 9,
        //                    },
        //                    #endregion
        //                    #region 49-9תחנות קו מס' 

        //                    new LineStation
        //                    {
        //                        BusLineID2 = 9,
        //                        StationNumberOnLine = 14541,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 0,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 9,
        //                        StationNumberOnLine = 14542,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 1,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 9,
        //                        StationNumberOnLine = 14904,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 2,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 9,
        //                        StationNumberOnLine = 14905,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 3,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 9,
        //                        StationNumberOnLine = 14906,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 4,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 9,
        //                        StationNumberOnLine = 14908,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 5,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 9,
        //                        StationNumberOnLine = 14909,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 6,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 9,
        //                        StationNumberOnLine = 14910,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 7,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 9,
        //                        StationNumberOnLine = 14912,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 8,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 9,
        //                        StationNumberOnLine = 15300,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 9,
        //                    },
        //                    #endregion              
        //                    #region תחנות קו מס50-10'  
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 10,
        //                        StationNumberOnLine = 14917,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 0,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 10,
        //                        StationNumberOnLine = 14918,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 1,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 10,
        //                        StationNumberOnLine = 14919,

        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 2,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 10,
        //                        StationNumberOnLine = 14920,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 3,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 10,
        //                        StationNumberOnLine = 14921,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 4,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 10,
        //                        StationNumberOnLine = 14922,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 5,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 10,
        //                        StationNumberOnLine = 14925,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 6,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 10,
        //                        StationNumberOnLine = 14926,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 7,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 10,
        //                        StationNumberOnLine = 14927,
        //                        ChackDelete2 = true,
        //                        LocationNumberOnLine = 8,
        //                    },
        //                    new LineStation
        //                    {
        //                        BusLineID2 = 10,
        //                        StationNumberOnLine = 14928,
        //                ChackDelete2 = true,
        //                        LocationNumberOnLine = 9,
        //                    }
        //                    #endregion
        //            };
        //            XMLTools.SaveListToXMLSerializer(LineStations, LineStationXml);
        //        }
        //public static TimeSpan GetTimeBeforeLunch()
        //{
        //    return new TimeSpan(0, R.Next(1, 7), R.Next(0, 60));
        //}
        //public static float GetDistanceBeforeLunch()
        //{
        //    return (float)((float)(R.NextDouble() * (4.6 - 1)) + 1);
        //}
        ////14905 14906 14908 14909 14910 14912 14913 14917 14918 14919
        ////14540 14541 14542 14904 14905 14906 14908 14909 14910 14912 
        ////14533 14534 14537 14538 14540 14541 14542 14904 14905 14906
        ////14931 14932 14933 14934 14935 14936 15285 15287 15289 15290
        ////14927 14928 14929 14930 14931 14932 14933 14934 14935 14936
        ////10509 10847 13426 14531 14533 14534 14537 14538 14540 14541
        ////14541 14542 14904 14905 14906 14908 14909 14910 14912 15300
        ////14917 14918 14919 14920 14921 14922 14925 14926 14927 14928
        //public static void InitializationConsecutiveStations()
        //{
        //    ConsecutiveStations = new List<ConsecutiveStations>
        //            {
        //            // 50
        //            new ConsecutiveStations
        //            {
        //                StationNumber1 = 14917,
        //                StationNumber2 = 14918,
        //                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                AverageTime = GetTimeBeforeLunch()
        //            },
        //            new ConsecutiveStations
        //            {
        //                StationNumber1 = 14918,
        //                StationNumber2 = 14919,
        //                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                AverageTime = GetTimeBeforeLunch()
        //            },

        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14919,
        //                                StationNumber2 = 14920,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14920,
        //                                StationNumber2 = 14921,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14921,
        //                                StationNumber2 = 14922,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14922,
        //                                StationNumber2 = 14925,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14925,
        //                                StationNumber2 = 14926,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14926,
        //                                StationNumber2 = 14927,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14927,
        //                                StationNumber2 = 14928,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },





        //                            // 40 
        //                              new ConsecutiveStations
        //                              {
        //                                  StationNumber1 = 10045,
        //                                  StationNumber2 = 10509,
        //                                  DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                  AverageTime = GetTimeBeforeLunch()

        //                              },
        //                              new ConsecutiveStations
        //                              {
        //                                  StationNumber1 = 10509,
        //                                  StationNumber2 = 10510,
        //                                  DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                  AverageTime = GetTimeBeforeLunch()
        //                              },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 10510,
        //                                StationNumber2 = 13426,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 13426,
        //                                StationNumber2 = 14531,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14531,
        //                                StationNumber2 = 14533,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14533,
        //                                StationNumber2 = 14534,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14534,
        //                                StationNumber2 = 14537,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14537,
        //                                StationNumber2 = 14538,

        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14538,
        //                                StationNumber2 = 14540,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },





        //                            // 41

        //                            new ConsecutiveStations

        //                            {
        //                                StationNumber1 = 10510,
        //                                StationNumber2 = 10836,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 10836,
        //                                StationNumber2 = 10837,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                             new ConsecutiveStations
        //                             {
        //                                 StationNumber1 = 10837,
        //                                 StationNumber2 = 10847,
        //                                 DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                 AverageTime = GetTimeBeforeLunch()
        //                             },
        //                              new ConsecutiveStations
        //                              {
        //                                  StationNumber1 = 10847,
        //                                  StationNumber2 = 13426,
        //                                  DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                  AverageTime = GetTimeBeforeLunch()
        //                              },

        //                            // 42
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14540,
        //                                StationNumber2 = 14541,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },

        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14541,
        //                                StationNumber2 = 14542
        //            ,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14542,
        //                                StationNumber2 = 14904,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14904,
        //                                StationNumber2 = 14905,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14905,
        //                                StationNumber2 = 14906,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14906,
        //                                StationNumber2 = 14908,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14908
        //             ,
        //                                StationNumber2 = 14909,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14909,
        //                                StationNumber2 = 14910,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14910,
        //                                StationNumber2 = 14912,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },


        //                            // 43
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14912,
        //                                StationNumber2 = 14913,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },

        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14913,
        //                                StationNumber2 = 14917,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },

        //                            // 45



        //                            // 46
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14931,
        //                                StationNumber2 = 14932,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },

        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14932,
        //                                StationNumber2 = 14933,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },

        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14933,
        //                                StationNumber2 = 14934,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },

        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14934,
        //                                StationNumber2 = 14935,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14935,
        //                                StationNumber2 = 14936,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14936,
        //                                StationNumber2 = 15285,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 15285,
        //                                StationNumber2 = 15287,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 15287,
        //                                StationNumber2 = 15289,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 15289,
        //                                StationNumber2 = 15290,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },


        //                            // 47
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14928,
        //                                StationNumber2 = 14929,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14929,
        //                                StationNumber2 = 14930,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
        //                            //
        //                            new ConsecutiveStations
        //                            {
        //                                StationNumber1 = 14930,
        //                                StationNumber2 = 14931,
        //                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                AverageTime = GetTimeBeforeLunch()
        //                            },
                                    //new ConsecutiveStations
                                    //{
                                    //    StationNumber1 = 14931,
                                    //    StationNumber2 = 14932,
                                    //    DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                    //    AverageTime = GetTimeBeforeLunch()
                                    //},
                                    //new ConsecutiveStations
                                    //{
                                    //    StationNumber1 = 14932,
                                    //    StationNumber2 = 14933,
                                    //    DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                    //    AverageTime = GetTimeBeforeLunch()
                                    //},
                                    //new ConsecutiveStations
                                    //{
                                    //    StationNumber1 = 14933,
                                    //    StationNumber2 = 14934,
                                    //    DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                    //    AverageTime = GetTimeBeforeLunch()
                                    //},
                                    //new ConsecutiveStations
                                    //{
                                    //    StationNumber1 = 14934,
                                    //    StationNumber2 = 14935,
                                    //    DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                    //    AverageTime = GetTimeBeforeLunch()
                                    //},
                                    //new ConsecutiveStations
                                    //{
                                    //    StationNumber1 = 14935,
                                    //    StationNumber2 = 14936,
                                    //    DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                    //    AverageTime = GetTimeBeforeLunch()
                                    //},

                                    // 48
        //                                new ConsecutiveStations
        //                                {
        //                                    StationNumber1 = 10509,
        //                                    StationNumber2 = 10847,
        //                                    DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                    AverageTime = GetTimeBeforeLunch()
        //                                },


        //                                  // 49
        //                                  new ConsecutiveStations
        //                                  {
        //                                      StationNumber1 = 14912,
        //                                      StationNumber2 = 15300,
        //                                      DistanceBetweenTooStations = GetDistanceBeforeLunch(),
        //                                      AverageTime = GetTimeBeforeLunch()
        //                                  },
        //             };
        //    for (int i = 0; i < ConsecutiveStations.Count; i++)
        //    {
        //        AddConsecutiveStations1(ConsecutiveStations[i]);
        //    }
        //    //XMLTools.SaveListToXMLSerializer(ConsecutiveStations, ConsecutiveStationsXml);
        //}
        //public static void AddConsecutiveStations1(ConsecutiveStations consecutiveStations)
        //{
        //    XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);
        //    XElement consecutiveStations1 = (from p in element.Elements()
        //                                     where p.Element("StationNumber1").Value == consecutiveStations.StationNumber1.ToString() && p.Element("StationNumber2").Value == consecutiveStations.StationNumber2.ToString()
        //                                     select p).FirstOrDefault();
        //    if (consecutiveStations1 != null)
        //    {
        //        throw new ExceptionConsecutiveStations(consecutiveStations.StationNumber1, consecutiveStations.StationNumber2, "There are already two such stations on the list!!!");
        //    }

        //    XElement consecutive = new XElement("ConsecutiveStations", new XElement("StationNumber1", consecutiveStations.StationNumber1),
        //                           new XElement("StationNumber2", consecutiveStations.StationNumber2),
        //                           new XElement("DistanceBetweenTooStations", consecutiveStations.DistanceBetweenTooStations),
        //                           new XElement("AverageTime", consecutiveStations.AverageTime.ToString()));
        //    element.Add(consecutive);

        //    XMLTools.SaveListToXMLElement(element, ConsecutiveStationsXml);
        //}
        #region  User
        string userXml = @"UserXml.xml";
        void IDAL.AddUser(User user)
        {
            XElement element = XMLTools.LoadListFromXMLElement(userXml);
            XElement user1 = (from p in element.Elements()
                              where p.Element("Username").Value == user.Username
                              select p).FirstOrDefault();
            if (user1 != null)
            {
                throw new ExceptionUser(user.Username, "the User alrdy exist in the compny!!!");
            }
            XElement personElem = new XElement("User", new XElement("Username", user.Username),
                                  new XElement("Password", user.Password),
                                  new XElement("Permission1", user.Permission1),
                                  new XElement("ManagementPermission", user.ManagementPermission),
                                  new XElement("ChackDelete", user.ChackDelete));
            element.Add(personElem);

            XMLTools.SaveListToXMLElement(element, userXml);
        }
        void IDAL.DeleteUser(string Username1)
        {
            XElement element = XMLTools.LoadListFromXMLElement(userXml);
            XElement user1 = (from p in element.Elements()
                              where p.Element("Username").Value == Username1
                              select p).FirstOrDefault();
            if (user1 == null)
            {
                throw new ExceptionUser(Username1, "the user not exist in the list!!!");
            }
            else
            {
                user1.Remove();
                XMLTools.SaveListToXMLElement(element, userXml);
            }
        }
        void IDAL.UpdatingUser(User user)
        {
            XElement element = XMLTools.LoadListFromXMLElement(userXml);
            XElement user1 = (from p in element.Elements()
                              where p.Element("Username").Value == user.Username
                              select p).FirstOrDefault();

            if (user1 != null)
            {
                user1.Element("Password").Value = user.Password;
                user1.Element("Permission1").Value = user.Permission1.ToString();
                user1.Element("ManagementPermission").Value = user.ManagementPermission.ToString();
                user1.Element("ChackDelete").Value = user.ChackDelete.ToString();
                XMLTools.SaveListToXMLElement(element, userXml);
            }
            else
            {
                throw new ExceptionUser(user.Username, "The user not exist in the compny!!!");
            }
        }
        User IDAL.ReturnUser(string Username1)
        {
            XElement element = XMLTools.LoadListFromXMLElement(userXml);

            User user1 = (from user in element.Elements()
                          where user.Element("Username").Value == Username1
                          select new User()
                          {
                              Username = user.Element("Username").Value,
                              Password = user.Element("Password").Value,
                              Permission1 = (Permission)Enum.Parse(typeof(Permission), user.Element("Permission1").Value),
                              ManagementPermission = bool.Parse(user.Element("ManagementPermission").Value),
                              ChackDelete = bool.Parse(user.Element("ChackDelete").Value)
                          }
                        ).FirstOrDefault();

            return user1 ?? throw new ExceptionUser(Username1, "the user not exist in the list!!!");
        }
        IEnumerable<User> IDAL.UseresList()
        {
            XElement element = XMLTools.LoadListFromXMLElement(userXml);

            return from user in element.Elements()
                   let p = new User()
                   {
                       Username = user.Element("Username").Value,
                       Password = user.Element("Password").Value,
                       Permission1 = (Permission)Enum.Parse(typeof(Permission), user.Element("Permission1").Value),
                       ManagementPermission = bool.Parse(user.Element("ManagementPermission").Value),
                       ChackDelete = bool.Parse(user.Element("ChackDelete").Value)
                   }
                   where p.ChackDelete
                   select p;
        }

        bool IDAL.FindUser(string pass, string UserNam)
        {
            XElement element = XMLTools.LoadListFromXMLElement(userXml);
            User user1 = (from user in element.Elements()
                          where user.Element("Username").Value == UserNam && user.Element("Password").Value == pass
                          select new User()
                          {
                              Username = user.Element("Username").Value,
                              Password = user.Element("Password").Value,
                              Permission1 = (Permission)Enum.Parse(typeof(Permission), user.Element("Permission1").Value),
                              ManagementPermission = bool.Parse(user.Element("ManagementPermission").Value),
                              ChackDelete = bool.Parse(user.Element("ChackDelete").Value)
                          }
                        ).FirstOrDefault();
            return user1 != null && user1.ChackDelete && user1.Permission1 == Permission.ManagementPermission
                ? true
                : throw new ExceptionUser(UserNam, "Rong user!!!");
        }
        #endregion User

        #region Bus

        void IDAL.AddBus(Bus bus)
        {
            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);

            if (buses.Exists(bus1 => bus1.License_number == bus.License_number && bus1.IsAvailable == true))
            {
                throw new ExceptionBus(bus.License_number, $"bad bus id {bus.License_number}");
            }
            else
            {
                buses.Add(bus);
                XMLTools.SaveListToXMLSerializer(buses, busXml);
            }
        }

        void IDAL.DeleteBus(string License_number)
        {
            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);
            int index = buses.FindIndex(item => item.License_number == License_number && item.IsAvailable);
            buses[index].IsAvailable = index == -1 ? throw new ExceptionBus(License_number, $"bad bus id {License_number}") : false;
            XMLTools.SaveListToXMLSerializer(buses, busXml);
        }
        void IDAL.UpdatingBus(Bus bus)
        {
            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);
            int index = buses.FindIndex(bus1 => bus1.License_number == bus.License_number);
            buses[index] = index == -1 ? throw new ExceptionBus(bus.License_number, $"bad bus id {bus.License_number}") : bus;
            XMLTools.SaveListToXMLSerializer(buses, busXml);
        }
        Bus IDAL.ReturnBusToBl(string License_number)
        {
            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);
            Bus bus = buses.Find(bus1 => bus1.License_number == License_number);
            return bus ?? throw new ExceptionBus(License_number, $"bad bus id {License_number}");
        }
        IEnumerable<Bus> IDAL.BusLists()
        {
            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(busXml);
            return from bus in buses
                   where bus.IsAvailable
                   select bus;
        }
        #endregion Bus

        #region Station
        void IDAL.AddStation(BusStation station)
        {
            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
            if (busStations.Exists(station1 => station1.StationNumber == station.StationNumber && station1.IsAvailable3))
            {
                throw new ExceptionStation(station.StationNumber, "bad id - The Station alrady exist in the compny: {station.StationNumber}");
            }
            else
            {
                busStations.Add(station);
            }
            XMLTools.SaveListToXMLSerializer(busStations, stationXml);
        }
        void IDAL.DeleteStation(int numberStation)
        {
            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
            int index = busStations.FindIndex(station1 => station1.StationNumber == numberStation);
            if (index == -1)
            {
                throw new ExceptionStation(numberStation, "bad id - The Station not exist in the compny: {numberStation}");
            }
            else
            {
                List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
                List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
                busStations[index].IsAvailable3 = true ? false : throw new ExceptionStation(numberStation, "The station exists but has already been deleted");
                foreach (LineStation item in lineStations.Where(item => item.StationNumberOnLine == numberStation))
                {
                    item.ChackDelete2 = false;
                }
                XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
                _ = consecutives.RemoveAll(item => item.StationNumber1 == numberStation || item.StationNumber2 == numberStation);
                XMLTools.SaveListToXMLSerializer(consecutives, ConsecutiveStationsXml);
            }
            XMLTools.SaveListToXMLSerializer(busStations, stationXml);
        }
        void IDAL.UpdatingStation(BusStation station)
        {
            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
            int index = busStations.FindIndex(station1 => station1.StationNumber == station.StationNumber);
            busStations[index] = index == -1 ? throw new ExceptionStation(station.StationNumber, "bad id - The Station not exist in the compny: {station.StationNumber}") : station;
            XMLTools.SaveListToXMLSerializer(busStations, stationXml);
        }
        BusStation IDAL.ReturnStation(int numberStation)
        {
            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
            BusStation station = busStations.Find(station1 => station1.StationNumber == numberStation);
            return station ?? throw new ExceptionStation(numberStation, "bad id - The Station not exist in the compny: {numberStation}");
        }
        IEnumerable<BusStation> IDAL.StationList()
        {
            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
            return from station in busStations
                   where station.IsAvailable3 == true
                   select station;
        }
        #endregion Station

        #region BusLine
        int IDAL.BusLineId()
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            return busLines.Count;
        }
        int IDAL.AddBusLine(BusLine line)
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            line.BusLineID1 = BusLine.BusLineID;
            BusLine.BusLineID++;
            busLines.Add(line);
            XMLTools.SaveListToXMLSerializer(busLines, LineXml);
            return line.BusLineID1;
        }
        void IDAL.DeleteBusLine(int BusLineID)
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            int index = busLines.FindIndex(BusLine => BusLine.BusLineID1 == BusLineID);
            busLines[index].GetAvailable = index == -1
                ? throw new ExceptionLine(BusLineID, " bad id line - The line not exist in the compny!!!")
                : busLines[index].GetAvailable == Available.Notavailable
                    ? throw new ExceptionLine(BusLineID, " bad id line - The Line exists but has already been deleted!!!")
                    : Available.Notavailable;
            XMLTools.SaveListToXMLSerializer(busLines, LineXml);
        }
        void IDAL.UpdatingBusLine(BusLine line)
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            int index = busLines.FindIndex(line1 => line1.BusLineID1 == line.BusLineID1);
            busLines[index] = index == -1 ? throw new ExceptionLine(line.BusLineID1, " bad id line - The BusLine not exist in the compny!!!") : line;
            XMLTools.SaveListToXMLSerializer(busLines, LineXml);
        }
        BusLine IDAL.ReturnBusLine(int numberLineId)
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            BusLine busLine = busLines.Find(line => line.BusLineID1 == numberLineId);
            return busLine ?? throw new ExceptionLine(numberLineId, "bad id line - The line not exist in the compny!!!");
        }
        IEnumerable<BusLine> IDAL.BusLinesList()
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            return from line in busLines
                   where line.GetAvailable == Available.Available
                   select line;
        }
        IEnumerable<BusLine> IDAL.BusLinesList(int numberLine)
        {
            List<BusLine> busLines = XMLTools.LoadListFromXMLSerializer<BusLine>(LineXml);
            return from line in busLines
                   where line.GetAvailable == Available.Available && line.BusLineID1 == numberLine
                   select line;
        }
        #endregion BusLine

        #region LineStation
        void IDAL.AddLineStation(LineStation lineStation)
        {
            List<BusStation> busStations = XMLTools.LoadListFromXMLSerializer<BusStation>(stationXml);
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            if (busStations.Exists(station1 => station1.StationNumber == lineStation.StationNumberOnLine && !station1.IsAvailable3))
            {
                throw new ExceptionLineStation(lineStation.BusLineID2, lineStation.StationNumberOnLine, "The Station not exist in the compny");
            }

            if (lineStations.Exists(lineStation1 => lineStation1.BusLineID2 == lineStation.BusLineID2 && lineStation1.StationNumberOnLine == lineStation.StationNumberOnLine && lineStation1.ChackDelete2))
            {
                throw new ExceptionLineStation(lineStation.BusLineID2, lineStation.StationNumberOnLine, "the Station line alrady exist in the this line!!!");
            }

            if (!lineStations.Exists(item => item.BusLineID2 == lineStation.BusLineID2))
            {
                lineStations.Add(lineStation);
                XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
            }
        }
        void IDAL.DeleteLineStation1(int NumberLine)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            int index = lineStations.FindIndex(lineStation => lineStation.BusLineID2 == NumberLine && lineStation.ChackDelete2);
            _ = index != -1
                ? lineStations.RemoveAll(item => item.BusLineID2 == NumberLine)
                : throw new ExceptionLineStation(NumberLine, NumberLine, "the line isnt exist in the list!!!");
            XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
        }
        void IDAL.DeleteOneLineStation(int NumberLine, int stationNumber)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            int index = lineStations.FindIndex(lineStation => lineStation.BusLineID2 == NumberLine && lineStation.StationNumberOnLine == stationNumber);
            if (index != -1 && !lineStations[index].ChackDelete2)
            {
                throw new ExceptionLineStation(NumberLine, stationNumber, "the station found but she deleted!!!");
            }

            if (index != -1 && lineStations[index].ChackDelete2)
            {
                lineStations[index].ChackDelete2 = false;
            }

            if (index == -1)
            {
                throw new ExceptionLineStation(NumberLine, stationNumber, "the line station isnt exist in the compny!!!");
            }
            XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
        }
        void IDAL.DeleteLineStation(int NumberLine)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            int index = lineStations.FindIndex(lineStation => lineStation.BusLineID2 == NumberLine && lineStation.ChackDelete2);
            if (index != -1)
            {
                foreach (LineStation item in lineStations)
                {
                    if (item.BusLineID2 == NumberLine && item.ChackDelete2)
                    {
                        item.ChackDelete2 = false;
                    }
                }
                XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
            }
            else
            {
                throw new ExceptionLineStation(NumberLine, NumberLine, "the line isnt exist in the list!!!");
            }
        }
        void IDAL.UpdatingLineStation(LineStation lineStation)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            int index = lineStations.FindIndex(lineStation1 => lineStation1.BusLineID2 == lineStation.BusLineID2 && lineStation1.StationNumberOnLine == lineStation.StationNumberOnLine);
            lineStations[index] = index == -1 ? throw new ExceptionLineStation(lineStation.BusLineID2, lineStation.StationNumberOnLine, "The lineStation not exist in the compny!!!") : lineStation;
            XMLTools.SaveListToXMLSerializer(lineStations, LineStationXml);
        }
        LineStation IDAL.ReturnLineStation(int numberLine, int stationNumber)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            LineStation lineStation1 = lineStations.Find(lineStation => lineStation.BusLineID2 == numberLine && lineStation.StationNumberOnLine == stationNumber);
            return lineStation1 ?? throw new ExceptionLineStation(numberLine, stationNumber, "The lineStation not exist in the compny!!!");
        }
        IEnumerable<LineStation> IDAL.LineStationList()
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            return from line in lineStations
                   where line.ChackDelete2
                   select line;
        }
        IEnumerable<LineStation> IDAL.OneLineFromList(Predicate<LineStation> predicate)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            return from line in lineStations
                   where predicate(line)
                   select line ?? throw new ExceptionLineStation(line.BusLineID2, line.StationNumberOnLine, "the line dsnt exist in the compny");
        }
        IEnumerable<int> IDAL.LinesFromList(int numberStation)
        {
            List<LineStation> lineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationXml);
            IEnumerable<int> Lines = from line in lineStations
                                     where line.StationNumberOnLine == numberStation && line.ChackDelete2
                                     select line.BusLineID2;
            return Lines ?? throw new ExceptionLineStation(numberStation, numberStation, "there is no lines that pass in this station!!!");
        }
        #endregion LineStation

        #region ConsecutiveStations

        void IDAL.AddConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);
            XElement consecutiveStations1 = (from p in element.Elements()
                                             where p.Element("StationNumber1").Value == consecutiveStations.StationNumber1.ToString() && p.Element("StationNumber2").Value == consecutiveStations.StationNumber2.ToString()
                                             select p).FirstOrDefault();
            if (consecutiveStations1 != null)
            {
                throw new ExceptionConsecutiveStations(consecutiveStations.StationNumber1, consecutiveStations.StationNumber2, "There are already two such stations on the list!!!"); 
            }

            XElement consecutive = new XElement("ConsecutiveStations", new XElement("StationNumber1", consecutiveStations.StationNumber1),
                                   new XElement("StationNumber2", consecutiveStations.StationNumber2),
                                   new XElement("DistanceBetweenTooStations", consecutiveStations.DistanceBetweenTooStations),
                                   new XElement("AverageTime", consecutiveStations.AverageTime.ToString()));
            element.Add(consecutive);

            XMLTools.SaveListToXMLElement(element, ConsecutiveStationsXml);
        }

        void IDAL.DeleteConsecutiveStations(int stationNumber1, int stationNumber2)
        {


            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);
            XElement consecutiveStations1 = (from p in element.Elements()
                                             where p.Element("StationNumber1").Value == stationNumber1.ToString() && p.Element("StationNumber2").Value == stationNumber2.ToString()
                                             select p).FirstOrDefault();
            if (consecutiveStations1 == null)
            {
                throw new ExceptionConsecutiveStations(stationNumber1, stationNumber2, "There are no two such stations on the list!!!");
            }
            consecutiveStations1.Remove();

            XMLTools.SaveListToXMLElement(element, ConsecutiveStationsXml);
        }

        void IDAL.UpdatingConsecutiveStations(ConsecutiveStations consecutiveStations)
        {
            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);
            XElement consecutiveStations1 = (from p in element.Elements()
                                             where p.Element("StationNumber1").Value == consecutiveStations.StationNumber1.ToString() && p.Element("StationNumber2").Value == consecutiveStations.StationNumber2.ToString()
                                             select p).FirstOrDefault();

            if (consecutiveStations1 != null)
            {
                consecutiveStations1.Element("StationNumber1").Value = consecutiveStations.ToString();
                consecutiveStations1.Element("StationNumber2").Value = consecutiveStations.StationNumber2.ToString();
                consecutiveStations1.Element("DistanceBetweenTooStations").Value = consecutiveStations.DistanceBetweenTooStations.ToString();
                consecutiveStations1.Element("AverageTime").Value = consecutiveStations.AverageTime.ToString();
                XMLTools.SaveListToXMLElement(element, ConsecutiveStationsXml);
            }
            else
            {
                throw new ExceptionConsecutiveStations(consecutiveStations.StationNumber1, consecutiveStations.StationNumber2, "The consecutiveStations not exist in the compny!!!");
            }
        }

        ConsecutiveStations IDAL.ReturnConsecutiveStation(int stationNumber1, int stationNumber2)
        {
            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);
            ConsecutiveStations consecutiveStations = (from p in element.Elements()
                                                       where p.Element("StationNumber1").Value == stationNumber1.ToString() && p.Element("StationNumber2").Value == stationNumber2.ToString()
                                                       select new ConsecutiveStations()
                                                       {
                                                           StationNumber1 = int.Parse(p.Element("StationNumber1").Value),
                                                           StationNumber2 = int.Parse(p.Element("StationNumber2").Value),
                                                           DistanceBetweenTooStations = float.Parse(p.Element("DistanceBetweenTooStations").Value),
                                                           AverageTime = TimeSpan.Parse(p.Element("AverageTime").Value)
                                                           //  AverageTime = TimeSpan.ParseExact(p.Element("AverageTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                                                       }).FirstOrDefault();

            return consecutiveStations ?? throw new ExceptionConsecutiveStations(stationNumber1, stationNumber2, "There are no two such stations on the list!!!");
        }

        IEnumerable<ConsecutiveStations> IDAL.ConsecutiveStationsList()
        {
            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);

            return from p in element.Elements()
                   let s = new ConsecutiveStations()
                   {
                       StationNumber1 = int.Parse(p.Element("StationNumber1").Value),
                       StationNumber2 = int.Parse(p.Element("StationNumber2").Value),
                       DistanceBetweenTooStations = float.Parse(p.Element("DistanceBetweenTooStations").Value),
                       AverageTime = TimeSpan.ParseExact(p.Element("AverageTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                   }
                   select s;
        }

        bool IDAL.ChackExistingConsecutiveStations(Predicate<ConsecutiveStations> predicate)
        {
            List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
            return consecutives.Exists(predicate);
        }

        float IDAL.DistanceBetweenTooStations(int numberStation1, int numberStation2)
        {
            List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
            int index = consecutives.FindIndex(consecutiveStations1 => consecutiveStations1.StationNumber1 == numberStation1 && consecutiveStations1.StationNumber2 == numberStation2);
            return index != -1
                ? consecutives[index].DistanceBetweenTooStations
                : throw new ExceptionConsecutiveStations(numberStation1, numberStation2, "There are no two such stations on the list!!!");
        }

        TimeSpan IDAL.AverageTimeBetweenTooStationsList(int numberStation1, int numberStation2)
        {
            XElement element = XMLTools.LoadListFromXMLElement(ConsecutiveStationsXml);
            ConsecutiveStations consecutiveStations = (from p in element.Elements()
                                                       where p.Element("StationNumber1").Value == numberStation1.ToString() && p.Element("StationNumber2").Value == numberStation2.ToString()
                                                       select new ConsecutiveStations()
                                                       {
                                                           StationNumber1 = int.Parse(p.Element("StationNumber1").Value),
                                                           StationNumber2 = int.Parse(p.Element("StationNumber2").Value),
                                                           DistanceBetweenTooStations = float.Parse(p.Element("DistanceBetweenTooStations").Value),
                                                           AverageTime = TimeSpan.ParseExact(p.Element("AverageTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                                                       }).FirstOrDefault();

            return consecutiveStations == null ? throw new ExceptionConsecutiveStations(numberStation1, numberStation2, "There are no two such stations on the list!!!") : consecutiveStations.AverageTime;

            //List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
            //int index = consecutives.FindIndex(consecutiveStations1 => consecutiveStations1.StationNumber1 == numberStation1 && consecutiveStations1.StationNumber2 == numberStation2);
            //return index != -1
            //    ? consecutives[index].AverageTime
        }

        //bool IDAL.ChackExistingConsecutiveStations(Predicate<ConsecutiveStations> predicate)
        //{
        //    List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
        //    return consecutives.Exists(predicate);
        //}

        //void IDAL.AddConsecutiveStations(ConsecutiveStations consecutiveStations)
        //{
        //    List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
        //    if (consecutives.Exists(consecutiveStations1 => consecutiveStations1.StationNumber1 == consecutiveStations.StationNumber1 && consecutiveStations1.StationNumber2 == consecutiveStations.StationNumber2))
        //    {
        //        throw new ExceptionConsecutiveStations(consecutiveStations.StationNumber1, consecutiveStations.StationNumber2, "There are already two such stations on the list!!!");
        //    }
        //    else
        //    {
        //        consecutives.Add(consecutiveStations);
        //        XMLTools.SaveListToXMLSerializer(consecutives, ConsecutiveStationsXml);
        //    }
        //}

        //void IDAL.DeleteConsecutiveStations(int stationNumber1, int stationNumber2)
        //{
        //    List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
        //    if (consecutives.Exists(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber2))
        //    {
        //        ConsecutiveStations item = consecutives.Find(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber2);
        //        _ = consecutives.Remove(item);
        //        XMLTools.SaveListToXMLSerializer(consecutives, ConsecutiveStationsXml);
        //    }
        //    else
        //    {
        //        throw new ExceptionConsecutiveStations(stationNumber1, stationNumber2, "There are no two such stations on the list!!!");
        //    }
        //}

        //void IDAL.UpdatingConsecutiveStations(ConsecutiveStations consecutiveStations)
        //{
        //    List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
        //    int index = consecutives.FindIndex(consecutiveStations1 => consecutiveStations1.StationNumber1 == consecutiveStations.StationNumber1 && consecutiveStations1.StationNumber2 == consecutiveStations.StationNumber2);
        //    consecutives[index] = index == -1 ? throw new ExceptionConsecutiveStations(consecutiveStations.StationNumber1, consecutiveStations.StationNumber2, "The consecutiveStations not exist in the compny!!!") : consecutiveStations;
        //    XMLTools.SaveListToXMLSerializer(consecutives, ConsecutiveStationsXml);
        //}

        //ConsecutiveStations IDAL.ReturnConsecutiveStation(int stationNumber1, int stationNumber2)
        //{
        //    List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
        //    ConsecutiveStations item = null;
        //    if (consecutives.Exists(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber2))
        //    {
        //        item = consecutives.Find(consecutiveStations1 => consecutiveStations1.StationNumber1 == stationNumber1 && consecutiveStations1.StationNumber2 == stationNumber2);
        //    }
        //    return item ?? throw new ExceptionConsecutiveStations(stationNumber1, stationNumber2, "There are no two such stations on the list!!!");
        //}
        //IEnumerable<ConsecutiveStations> IDAL.ConsecutiveStationsList()
        //{
        //    List<ConsecutiveStations> consecutives = XMLTools.LoadListFromXMLSerializer<ConsecutiveStations>(ConsecutiveStationsXml);
        //    return from Consecutive in consecutives
        //           select Consecutive;
        //}
        #endregion ConsecutiveStations

        #region LineExit
        void IDAL.AddLineExit(LineExit lineExit)
        {
            List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
            if (lineExits.Exists(lineExit1 => lineExit1.BusLineID1 == lineExit.BusLineID1 && lineExit1.LineStartTime == lineExit.LineStartTime))
            {
                throw new ExceptionLineExit(lineExit.BusLineID1, lineExit.LineStartTime, "the LineExit alrdy exist in the list in the same time");
            }
            else
            {
                lineExits.Add(lineExit);
                XMLTools.SaveListToXMLSerializer(lineExits, LineExitXml);
            }
        }
        void IDAL.DeleteLineExit(int lineNumber, TimeSpan StartTime)
        {
            List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
            int index = lineExits.FindIndex(lineExit1 => lineExit1.BusLineID1 == lineNumber && lineExit1.LineStartTime == StartTime);
            if (index == -1)
            {
                throw new ExceptionLineExit(lineNumber, StartTime, "the LineExit not found!!!");
            }
            else
            {
                lineExits.RemoveAt(index);
                XMLTools.SaveListToXMLSerializer(lineExits, LineExitXml);
            }
        }
        void IDAL.UpdatingLineExit(LineExit lineExit)
        {
            List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
            int index = lineExits.FindIndex(lineExit1 => lineExit1.BusLineID1 == lineExit.BusLineID1 && lineExit1.LineStartTime == lineExit.LineStartTime);
            lineExits[index] = index == -1 ? throw new ExceptionLineExit(lineExit.BusLineID1, lineExit.LineStartTime, "The lineExit not exist in the compny") : lineExit;
            XMLTools.SaveListToXMLSerializer(lineExits, LineExitXml);
        }
        LineExit IDAL.ReturnLineExit(int lineNumber, TimeSpan StartTime)
        {
            List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
            LineExit lineExit = lineExits.FirstOrDefault(lineExit1 => lineExit1.BusLineID1 == lineNumber && lineExit1.LineStartTime == StartTime);
            return lineExit ?? throw new ExceptionLineExit(lineNumber, StartTime, "the LineExit not exist in the list");
        }
        LineExit IDAL.OneLineExitFromList(int numberLine, TimeSpan StartTime)
        {
            List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
            LineExit b = lineExits.FirstOrDefault(lineExit => lineExit.BusLineID1 == numberLine && lineExit.LineStartTime == StartTime);
            return b = default ? throw new ExceptionLineExit(numberLine, StartTime, "the line Exit dsnt Exist") : b;
        }
        IEnumerable<LineExit> IDAL.LineExitList(int numberLine)
        {
            List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
            return from lineExit in lineExits
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
        //    int index = DataSource.BusTravelings.FindIndex(BusTravelings1 => BusTravelings1.License_number1 == License_number && BusTravelings1.LineInExecution == LineExecution && BusTravelings1.LeavingTime == LeavingTime);
        //    if (index == -1)
        //    {
        //        throw new ExceptionDl("the BusTraveling not exist!!!");
        //    }
        //    else
        //    {
        //        DataSource.LineExits.RemoveAt(index);
        //    }
        //}
        //void IDAL.UpdatingBusTraveling(BusTraveling busTraveling)
        //{
        //    int index = DataSource.BusTravelings.FindIndex(BusTravelings1 => BusTravelings1.License_number1 == busTraveling.License_number1 && BusTravelings1.LineInExecution == busTraveling.LineInExecution && BusTravelings1.LeavingTime == busTraveling.LeavingTime);
        //    DataSource.BusTravelings[index] = index == -1 ? throw new ExceptionDl("The busTraveling not exist in the compny!!!") : busTraveling.Clone();
        //}
        //public BusTraveling ReturnBusTraveling(int LineExecution, string License_number, string LeavingTime)
        //{
        //    BusTraveling busTraveling = DataSource.BusTravelings.FirstOrDefault(BusTravelings1 => BusTravelings1.License_number1 == License_number && BusTravelings1.LineInExecution == LineExecution && BusTravelings1.LeavingTime == LeavingTime);
        //    return busTraveling.Clone(); ?? throw new ExceptionDl("the busTraveling not exist in the list!!!");
        //}

        //public IEnumerable<BusTraveling> BusTravelingList()
        //{
        //    return from busTraveling in DataSource.BusTravelings
        //           select busTraveling;
        //}
        #endregion BusTraveling
    }
}