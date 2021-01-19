using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DS
{
    public static class DataSource
    {
        static DataSource()
        {
            InitializationUser();
            InitializationStation();
            InitializationLine();
            InitializationLineStation();
            InitializationConsecutiveStations();
            InitializationLineExits();
            for (int i = 0; i < 30; i++)
            {
                InitializationBus();
            }
        }
        public static Random R = new Random(DateTime.Now.Millisecond);
        public static List<Bus> Bus1 = new List<Bus>();
        public static List<BusStation> BusStations;
        public static List<ConsecutiveStations> ConsecutiveStations;
        public static List<LineStation> LineStations;
        public static List<BusLine> BusLines;
        public static List<BusTraveling> BusTravelings = new List<BusTraveling>();
        public static List<LineExit> LineExits;
        public static List<User> Users = new List<User>();
        public static void InitializationLineExits()
        {
            LineExits = new List<LineExit>
            {
                new LineExit
                {
                    BusLineID1 = 0,
                    LineStartTime = new TimeSpan(6, 0, 0),
                    LineFinishTime = new TimeSpan(10, 0, 0),
                    LineFrequencyTime = new TimeSpan(0, 10, 0),
                    LineFrequency = 24,
                },
                new LineExit
                {
                    BusLineID1 = 0,
                    LineStartTime = new TimeSpan(10, 0, 0),
                    LineFinishTime = new TimeSpan(15, 0, 0),
                    LineFrequencyTime = new TimeSpan(0, 20, 0),
                    LineFrequency = 15,
                },
                new LineExit
                {
                    BusLineID1 = 0,
                    LineStartTime = new TimeSpan(15, 0, 0),
                    LineFinishTime = new TimeSpan(23, 0, 0),
                    LineFrequencyTime = new TimeSpan(0, 15, 0),
                    LineFrequency = 32,
                },



                new LineExit
                {
                    BusLineID1 = 1,
                    LineStartTime = new TimeSpan(6, 0, 0),
                    LineFinishTime = new TimeSpan(15, 0, 0),
                    LineFrequencyTime = new TimeSpan(0, 15, 0),
                    LineFrequency = 36,
                },


                new LineExit
                {
                    BusLineID1 = 1,
                    LineStartTime = new TimeSpan(15, 0, 0),
                    LineFinishTime = new TimeSpan(23, 0, 0),
                    LineFrequencyTime = new TimeSpan(0, 10, 0),
                    LineFrequency = 48,
                },


                new LineExit
                {
                    BusLineID1 = 2,
                    LineStartTime = new TimeSpan(10, 0, 0),
                    LineFinishTime = new TimeSpan(20, 0, 0),
                    LineFrequencyTime = new TimeSpan(2, 0, 0),
                    LineFrequency = 5
                },

                new LineExit
                {
                    BusLineID1 = 3,
                    LineStartTime = new TimeSpan(8, 0, 0),
                     LineFinishTime = new TimeSpan(20, 0, 0),
                     LineFrequencyTime = new TimeSpan(1, 0, 0),
                     LineFrequency = 12
                },

                 new LineExit
                {
                    BusLineID1 = 4,
                    LineStartTime = new TimeSpan(6, 0, 0),
                     LineFinishTime = new TimeSpan(20, 0, 0),
                     LineFrequencyTime = new TimeSpan(0, 30, 0),
                     LineFrequency = 28
                },

                  new LineExit
                {
                    BusLineID1 = 5,
                    LineStartTime = new TimeSpan(5, 0, 0),
                     LineFinishTime = new TimeSpan(23, 55, 0),
                     LineFrequencyTime = new TimeSpan(0, 20, 0),
                     LineFrequency = 58
                },

                   new LineExit
                {
                    BusLineID1 = 6,
                    LineStartTime = new TimeSpan(7, 0, 0),
                },
                new LineExit
                {
                    BusLineID1 = 7,
                    LineStartTime = new TimeSpan(14, 0, 0),
                },
            };
        }
        public static void InitializationBus()
        {
            int number = R.Next(1200);
            Bus bus = new Bus
            {
                License_number = R.Next(1000000, 100000000).ToString(),
                StartDate = new DateTime(R.Next(1999, 2020), R.Next(1, 13), R.Next(1, 29)),
                KmForRefueling = number,
                KmForTreatment = number * R.Next(3, 10),
                TotalMiles = number * R.Next(20, 25),
                Status = (TravelMode)R.Next(4),
                IsAvailable = true
            };
            Bus1.Add(bus);
        }
        public static void InitializationUser()
        {
            Users = new List<User>
            {
                new User
                {
                    ChackDelete = true,
                    Username = "yehuda",
                    Password = "yehudashor789",
                    ManagementPermission = true,
                    Permission1 = Permission.ManagementPermission
                },

                new User
                {
                    ChackDelete = true,
                    Username = "1",
                    Password = "a",
                    ManagementPermission = true,
                    Permission1 = Permission.ManagementPermission
                },

                new User
                {
                    ChackDelete = true,
                    Username = "Shor1998",
                    Password = "05276351",
                    ManagementPermission = false,
                    Permission1 = Permission.WithoutManagementPermission
                },

                new User
                {
                    ChackDelete = false,
                    Username = "koko",
                    Password = "111111",
                    ManagementPermission = true,
                    Permission1 = Permission.ManagementPermission
                },

                new User
                {
                    ChackDelete = true,
                    Username = "gggg",
                    Password = "222222",
                    ManagementPermission = true,
                    Permission1 = Permission.ManagementPermission
                }
            };
        }

        public static void InitializationStation()
        {
            BusStations = new List<BusStation>
            {
                new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 10045,
                    NameOfStation = ", שדרות הרצל/ המלאכה",
                    StationAddress = "רחוב:שדרות הרצל  עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 10509,
                    NameOfStation = " שד.הרצל/משעול איריס",
                    StationAddress = " :שדרות הרצל 49 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                 new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 10510
,
                    NameOfStation = "הרצל/עיריית אופקים ",
                    StationAddress = " שדרות הרצל 31 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                  new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 10836,
                    NameOfStation = "הרצל/ז'בוטינסקי ",
                    StationAddress = " :שדרות הרצל 26 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                   new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 10837,
                    NameOfStation = "שדרות הרצל/גיבורי ישראל ",
                    StationAddress = " :שדרות הרצל 52 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                    new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 10847,
                    NameOfStation = " מכבי אש",
                    StationAddress = ":הנשיא 21 עיר: אופקים  ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                     new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =13426 ,
                    NameOfStation = "דרך הטייסים/דרך הנביאים ",
                    StationAddress = ":דרך הטייסים 17 עיר: אופקים  ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                      new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14531,
                    NameOfStation = "ז'בוטינסקי/נחושתן ",
                    StationAddress = " :ז'בוטינסקי 16 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                       new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14533,
                    NameOfStation = "שוק ",
                    StationAddress = " הרב חיים חורי 20 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                        new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14534,
                    NameOfStation = "מכבי האש ",
                    StationAddress = " :הנשיא 21 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                         new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14537,
                    NameOfStation = "החרוב/הארזים ",
                    StationAddress = " :החרוב 9 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                          new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14538,
                    NameOfStation = "החרוב/דרך בית וגן ",
                    StationAddress = "החרוב 9 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                 new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =14540 ,
                    NameOfStation = "חפץ חיים/רפאל אלנקווה ",
                    StationAddress = "חפץ חיים 4 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                  new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =14541 ,
                    NameOfStation = " חפץ חיים/רפאל אלנקווה",
                    StationAddress = " :חפץ חיים 2 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                   new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14542,
                    NameOfStation = " חפץ חיים/שבזי",
                    StationAddress = ":חפץ חיים 16 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                    new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =14904 ,
                    NameOfStation = " דוד המלך/אסא",
                    StationAddress = " :דוד המלך 72 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                     new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =14905 ,
                    NameOfStation = "דוד המלך/אסא ",
                    StationAddress = " :דוד המלך 75 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                      new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =14906,
                    NameOfStation = " קיבוץ גלויות/ש''ד הרצל",
                    StationAddress = " :קיבוץ גלויות 4 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                       new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =14908 ,
                    NameOfStation = " קיבוץ גלויות/רחבת דדו",
                    StationAddress = ":קיבוץ גלויות 48 עיר: אופקים  ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                        new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14909,
                    NameOfStation = " קיבוץ גלויות/קדש",
                    StationAddress = " :קיבוץ גלויות 66 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                         new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14910,
                    NameOfStation = "קיבוץ גלויות/חיים חורי ",
                    StationAddress = " :קיבוץ גלויות 55 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                          new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14912,
                    NameOfStation = " חיים חורי/פרי מגדים",
                    StationAddress = " פרי מגדים  עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                 new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =14913 ,
                    NameOfStation = " פרי מגדים/חיים חורי",
                    StationAddress = " :פרי מגדים  עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                  new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14917,
                    NameOfStation = " פרי מגדים/העינב",
                    StationAddress = " :פרי מגדים 42 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                   new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14918,
                    NameOfStation = " פרי מגדים/העינב",
                    StationAddress = " :פרי מגדים 42 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                    new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14919,
                    NameOfStation = "משה שרת/השקד ",
                    StationAddress = ":משה שרת 3 עיר: אופקים  ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                     new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14920,
                    NameOfStation = "משה שרת/שדרות הרצל ",
                    StationAddress = " משה שרת 6 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                      new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14921,
                    NameOfStation = "משה שרת/החרוב ",
                    StationAddress = " משה שרת 36 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                       new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =14922 ,
                    NameOfStation = " משה שרת/החרוב",
                    StationAddress = ":משה שרת 23 עיר: אופקים  ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                        new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14925,
                    NameOfStation = " ירושלים/קרית ספר",
                    StationAddress = ":ירושלים 41 עיר: אופקים  ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                         new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14926,
                    NameOfStation = "קרית ספר/אליהו התשבי ",
                    StationAddress = ":קרית ספר 48 עיר: אופקים  ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                          new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =14927 ,
                    NameOfStation = " קרית ספר/אליהו התשבי",
                    StationAddress = " קרית ספר 79 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                 new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =14928 ,
                    NameOfStation = "מגדל המים ",
                    StationAddress = " :דרך הנביאים 4 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                  new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = false,
                    AccessForDisabled =false,
                    StationNumber = 14929,
                    NameOfStation = " מגדל המים",
                    StationAddress = " בדרך הנביאים 11 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                   new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14930,
                    NameOfStation = "קרית ספר/יואל ",
                    StationAddress = " קרית ספר 8 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                    new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14931,
                    NameOfStation = " פרי מגדים/התמר",
                    StationAddress = ":פרי מגדים 86 עיר: אופקים  ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                     new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber = 14932,
                    NameOfStation = " פרי מגדים/ערבה",
                    StationAddress = "פרי מגדים 96 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                      new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = false,
                    AccessForDisabled = true,
                    StationNumber = 14933,
                    NameOfStation = " פרי מגדים/הגורן",
                    StationAddress = " פרי מגדים 124 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                       new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = false,
                    StationNumber = 14934,
                    NameOfStation = " פרי מגדים/ארבעת המינים",
                    StationAddress = " :פרי מגדים 124 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },

                        new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = false,
                    StationNumber = 14935,
                    NameOfStation = " דוד המלך/שלמה המלך",
                    StationAddress = ":דוד המלך 14 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                         new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = false,
                    StationNumber = 14936,
                    NameOfStation = " דוד המלך/שלמה המלך",
                    StationAddress = "דוד המלך 7 עיר: אופקים  ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                          new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =15285 ,
                    NameOfStation = " שד. הרצל/מבצע קדש",
                    StationAddress = ":שדרות הרצל 27 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
                 new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =15287 ,
                    NameOfStation = " החיד''א/בן איש חי",
                    StationAddress = " החיד''א 13 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },

                  new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = false,
                    StationNumber =15289 ,
                    NameOfStation = "דוד בוזגלו/רפאל אלנקווה ",
                    StationAddress = " :דוד בוזגלו  עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },

                   new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = false,
                    AccessForDisabled = true,
                    StationNumber = 15290,
                    NameOfStation = " דוד בוזגלו/רפאל אלנקווה",
                    StationAddress = " :דוד בוזגלו  עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },

                    new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =15291 ,
                    NameOfStation = "שד. הרצל/הנשיא ",
                    StationAddress = " שדרות הרצל  עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },

                     new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = true,
                    StationNumber =15294 ,
                    NameOfStation = " שדרות הרצל/כצנלסון",
                    StationAddress = " שדרות הרצל 48 עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },

                      new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation =false,
                    AccessForDisabled = true,
                    StationNumber = 15296,
                    NameOfStation = " שדרות הרצל",
                    StationAddress = " שדרות הרצל 85 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },

                       new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = true,
                    AccessForDisabled = false,
                    StationNumber = 15298,
                    NameOfStation = " דרך הטייסים/דרך בוזגלו",
                    StationAddress = ":דרך הטייסים  עיר: אופקים  ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },

                        new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = false,
                    AccessForDisabled = true,
                    StationNumber = 15299,
                    NameOfStation = "דרך הטייסים/רבי עקיבא ",
                    StationAddress = " :דרך הטייסים 49 עיר: אופקים ",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },

                         new BusStation
                {
                    IsAvailable3 = true,
                    RoofToTheStation = false,
                    AccessForDisabled = false,
                    StationNumber = 15300,
                    NameOfStation = " בית ספר שושנים/החיד''א",
                    StationAddress = " :החיד''א  עיר: אופקים",
                    Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                    Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3)
                },
            };

        }

        public static void InitializationLine()
        {
            BusLines = new List<BusLine>
            {
                #region קו 40 אופקים
                new BusLine
                {
                     LineNumber = 40,
                     GetAvailable = Available.Available,
                     GetUrban = Urban.Urban,
                     AreaBusUrban = Area1.South,
                     FirstStation = 10045,
                     LastStation=14540,
                },
                #endregion קו 40 אופקים

                new BusLine
                {
                     LineNumber = 41,
                     GetAvailable = Available.Available,
                     GetUrban = Urban.Urban,
                     AreaBusUrban = Area1.South,
                     FirstStation=10510,
                     LastStation=14538,
                },

                new BusLine
                {
                     LineNumber = 42,
                      GetAvailable = Available.Available,
                     GetUrban = Urban.Urban,
                     AreaBusUrban = Area1.South,
                     FirstStation=14540,
                     LastStation=14912,
                },

                new BusLine
                {
                     LineNumber = 43,
                     GetAvailable = Available.Available,
                     GetUrban = Urban.Urban,
                     AreaBusUrban = Area1.South,
                     FirstStation=14905,
                     LastStation=14919,
                },

                new BusLine
                {
                     LineNumber = 44,
                     GetAvailable = Available.Available,
                     GetUrban = Urban.Urban,
                     AreaBusUrban = Area1.South,
                     FirstStation= 14540,
                     LastStation=14912,
                },

                new BusLine
                {
                     LineNumber = 45,
                      GetAvailable = Available.Available,
                     GetUrban = Urban.Urban,
                     AreaBusUrban = Area1.South,
                     FirstStation=14533,
                     LastStation=14906,
                },

                new BusLine
                {
                     LineNumber = 46,
                      GetAvailable = Available.Available,
                     GetUrban = Urban.Urban,
                     AreaBusUrban = Area1.South,
                     FirstStation=14931,
                     LastStation=15289,
                },

                new BusLine
                {
                     LineNumber = 47,
                      GetAvailable = Available.Available,
                     GetUrban = Urban.Urban,
                     AreaBusUrban = Area1.South,
                     FirstStation=14927,
                     LastStation=14936
,
                },

                new BusLine
                {
                     LineNumber = 48,
                      GetAvailable = Available.Available,
                     GetUrban = Urban.Urban,
                     AreaBusUrban = Area1.South,
                     FirstStation=10509,
                     LastStation=14541,
                },

                new BusLine
                {
                     LineNumber = 49,
                      GetAvailable = Available.Available,
                     GetUrban = Urban.Urban,
                     AreaBusUrban = Area1.South,
                     FirstStation=14541,
                     LastStation=15300,
                },

                new BusLine
                {
                     LineNumber = 50,
                      GetAvailable = Available.Notavailable,
                     GetUrban = Urban.Urban,
                     AreaBusUrban = Area1.South,
                     FirstStation=14917,
                     LastStation=14928,
                },
            };
            for (int i = 0; i < BusLines.Count; i++)
            {
                BusLines[i].BusLineID1 = NumbersAreRunning.BusLineID;
                ++NumbersAreRunning.BusLineID;
            }
        }

        public static void InitializationLineStation()
        {
            //10045 10509 10510 13426 14531 14533 14534 14537 14538 14540
            //10510 10836 10837 10847 13426 14531 14533 14534 14537 14538
            //14540 14541 14542 14904 14905 14906 14908 14909 14910 14912
            //14905 14906 14908 14909 14910 14912 14913 14917 14918 14919
            //14540 14541 14542 14904 14905 14906 14908 14909 14910 14912 
            //14533 14534 14537 14538 14540 14541 14542 14904 14905 14906
            //14931 14932 14933 14934 14935 14936 15285 15287 15289 15290
            //14927 14928 14929 14930 14931 14932 14933 14934 14935 14936
            //10509 10847 13426 14531 14533 14534 14537 14538 14540 14541
            //14541 14542 14904 14905 14906 14908 14909 14910 14912 15300
            //14917 14918 14919 14920 14921 14922 14925 14926 14927 14928
            LineStations = new List<LineStation>
            #region  40 תחנות קו מס' 
            {
                new LineStation
                {
                    BusLineID2 = 0,
                    StationNumberOnLine = 10045,
                    ChackDelete2 = true,
                    LocationNumberOnLine = 0,
                },
                new LineStation
                {
                    BusLineID2 = 0,
                    StationNumberOnLine = 10509,

                    ChackDelete2 = true,
                    LocationNumberOnLine = 1,
                },
                new LineStation
                {
                    BusLineID2 = 0,
                    StationNumberOnLine = 10510,

                    ChackDelete2 = true,
                    LocationNumberOnLine = 2,
                },
                new LineStation
                {
                    BusLineID2 = 0,
                    StationNumberOnLine = 13426,
                    ChackDelete2 = true,
                    LocationNumberOnLine = 3,
                },
                new LineStation
                {
                    BusLineID2 = 0,
                    StationNumberOnLine = 14531,
                    ChackDelete2 = true,
                    LocationNumberOnLine = 4,
                },
                new LineStation
                {
                    BusLineID2 = 0,
                    StationNumberOnLine = 14533,
                    ChackDelete2 = true,
                    LocationNumberOnLine = 5,
                },
                new LineStation
                {
                    BusLineID2 = 0,
                    StationNumberOnLine = 14534,
                    ChackDelete2 = true,
                    LocationNumberOnLine = 6,
                },
                new LineStation
                {
                    BusLineID2 = 0,
                    StationNumberOnLine = 14537,
                    ChackDelete2 = true,
                    LocationNumberOnLine = 7,
                },
                new LineStation
                {
                    BusLineID2 = 0,
                    StationNumberOnLine = 14538,
                    ChackDelete2 = true,
                    LocationNumberOnLine = 8,
                },
                new LineStation
                {
                    BusLineID2 = 0,
                    StationNumberOnLine = 14540,
                    ChackDelete2 = true,
                    LocationNumberOnLine = 9,
                },
                #endregion
                #region 41 -1תחנות קו מס' 
               
                    new LineStation
                    {
                        BusLineID2 = 1,
                        StationNumberOnLine = 10510,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 0,
                    },
                    new LineStation
                    {
                        BusLineID2 = 1,
                        StationNumberOnLine = 10836,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 1,
                    },
                    new LineStation
                    {
                        BusLineID2 = 1,
                        StationNumberOnLine = 10837,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 2,
                    },
                    new LineStation
                    {
                        BusLineID2 = 1,
                        StationNumberOnLine = 10847,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 3,
                    },
                    new LineStation
                    {
                        BusLineID2 = 1,
                        StationNumberOnLine = 13426,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 4,
                    },
                    new LineStation
                    {
                        BusLineID2 = 1,
                        StationNumberOnLine = 14531,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 5,
                    },
                    new LineStation
                    {
                        BusLineID2 = 1,
                        StationNumberOnLine = 14533,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 6,
                    },
                    new LineStation
                    {
                        BusLineID2 = 1,
                        StationNumberOnLine = 14534,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 7,
                    },
                    new LineStation
                    {
                        BusLineID2 = 1,
                        StationNumberOnLine = 14537,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 8,
                    },
                    new LineStation
                    {
                        BusLineID2 = 1,
                        StationNumberOnLine = 14538,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 9,
                    },
                    #endregion 41 -1תחנות קו מס'
                    #region 42-2תחנות קו מס' 
                    new LineStation
                    {
                        BusLineID2 = 2,
                        StationNumberOnLine = 14540,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 0,
                    },
                    new LineStation
                    {
                        BusLineID2 = 2,
                        StationNumberOnLine = 14541,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 1,
                    },
                    new LineStation
                    {
                        BusLineID2 = 2,
                        StationNumberOnLine = 14542,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 2,
                    },
                    new LineStation
                    {
                        BusLineID2 = 2,
                        StationNumberOnLine = 14904,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 3,
                    },
                    new LineStation
                    {
                        BusLineID2 = 2,
                        StationNumberOnLine = 14905,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 4,
                    },
                    new LineStation
                    {
                        BusLineID2 = 2,
                        StationNumberOnLine = 14906,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 5,
                    },
                    new LineStation
                    {
                        BusLineID2 = 2,
                        StationNumberOnLine = 14908,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 6,
                    },
                    new LineStation
                    {
                        BusLineID2 = 2,
                        StationNumberOnLine = 14909,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 7,
                    },
                    new LineStation
                    {
                        BusLineID2 = 2,
                        StationNumberOnLine = 14910,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 8,
                    },
                    new LineStation
                    {
                        BusLineID2 = 2,
                        StationNumberOnLine = 14912,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 9,
                    },
                    #endregion
                    #region 43-3תחנות קו מס' 
                 
                    new LineStation
                    {
                        BusLineID2 = 3,
                        StationNumberOnLine = 14905,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 0,
                    },
                    new LineStation
                    {
                        BusLineID2 = 3,
                        StationNumberOnLine = 14906,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 1,
                    },
                    new LineStation
                    {
                        BusLineID2 = 3,
                        StationNumberOnLine = 14908,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 2,
                    },
                    new LineStation
                    {
                        BusLineID2 = 3,
                        StationNumberOnLine = 14909,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 3,
                    },
                    new LineStation
                    {
                        BusLineID2 = 3,
                        StationNumberOnLine = 14910,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 4,
                    },
                    new LineStation
                    {
                        BusLineID2 = 3,
                        StationNumberOnLine = 14912,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 5,
                    },
                    new LineStation
                    {
                        BusLineID2 = 3,
                        StationNumberOnLine = 14913,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 6,
                    },
                    new LineStation
                    {
                        BusLineID2 = 3,
                        StationNumberOnLine = 14917,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 7,
                    },
                    new LineStation
                    {
                        BusLineID2 = 3,
                        StationNumberOnLine = 14918,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 8,
                    },
                    new LineStation
                    {
                        BusLineID2 = 3,
                        StationNumberOnLine = 14919,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 9,
                    },
                    #endregion
                    #region 44-4תחנות קו מס
                  
                    new LineStation
                    {
                        BusLineID2 = 4,
                        StationNumberOnLine = 14540,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 0,
                    },
                    new LineStation
                    {
                        BusLineID2 = 4,
                        StationNumberOnLine = 14541,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 1,
                    },
                    new LineStation
                    {
                        BusLineID2 = 4,
                        StationNumberOnLine = 14542,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 2,
                    },
                    new LineStation
                    {
                        BusLineID2 = 4,
                        StationNumberOnLine = 14904,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 3,
                    },
                    new LineStation
                    {
                        BusLineID2 = 4,
                        StationNumberOnLine = 14905,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 4,
                    },
                    new LineStation
                    {
                        BusLineID2 = 4,
                        StationNumberOnLine = 14906,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 5,
                    },
                    new LineStation
                    {
                        BusLineID2 = 4,
                        StationNumberOnLine = 14908,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 6,
                    },
                    new LineStation
                    {
                        BusLineID2 = 4,
                        StationNumberOnLine = 14909,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 7,
                    },
                    new LineStation
                    {
                        BusLineID2 = 4,
                        StationNumberOnLine = 14910,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 8,
                    },
                    new LineStation
                    {
                        BusLineID2 = 4,
                        StationNumberOnLine = 14912,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 9,
                    },
                    #endregion
                    #region 45-5תחנות קו מס' 
                    new LineStation
                    {
                        BusLineID2 = 5,
                        StationNumberOnLine = 14533,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 0,
                    },
                    new LineStation
                    {
                        BusLineID2 = 5,
                        StationNumberOnLine = 14534,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 1,
                    },
                    new LineStation
                    {
                        BusLineID2 = 5,
                        StationNumberOnLine = 14537,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 2,
                    },
                    new LineStation
                    {
                        BusLineID2 = 5,
                        StationNumberOnLine = 14538,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 3,
                    },
                    new LineStation
                    {
                        BusLineID2 = 5,
                        StationNumberOnLine = 14540,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 4,
                    },
                    new LineStation
                    {
                        BusLineID2 = 5,
                        StationNumberOnLine = 14541,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 5,
                    },
                    new LineStation
                    {
                        BusLineID2 = 5,
                        StationNumberOnLine = 14542,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 6,
                    },
                    new LineStation
                    {
                        BusLineID2 = 5,
                        StationNumberOnLine = 14904,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 7,
                    },
                    new LineStation
                    {
                        BusLineID2 = 5,
                        StationNumberOnLine = 14905,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 8,
                    },
                    new LineStation
                    {
                        BusLineID2 = 5,
                        StationNumberOnLine = 14906,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 9,
                    },
                    #endregion
                    #region תחנות קו מס46-6' 
                   
                    new LineStation
                    {
                        BusLineID2 = 6,
                        StationNumberOnLine = 14931,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 0,
                    },
                    new LineStation
                    {
                        BusLineID2 = 6,
                        StationNumberOnLine = 14932,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 1,
                    },
                    new LineStation
                    {
                        BusLineID2 = 6,
                        StationNumberOnLine = 14933,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 2,
                    },
                    new LineStation
                    {
                        BusLineID2 = 6,
                        StationNumberOnLine = 14934,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 3,
                    },
                    new LineStation
                    {
                        BusLineID2 = 6,
                        StationNumberOnLine = 14935,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 4,
                    },
                    new LineStation
                    {
                        BusLineID2 = 6,
                        StationNumberOnLine = 14936,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 5,
                    },
                    new LineStation
                    {
                        BusLineID2 = 6,
                        StationNumberOnLine = 15285,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 6,
                    },
                    new LineStation
                    {
                        BusLineID2 = 6,
                        StationNumberOnLine = 15287,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 7,
                    },
                    new LineStation
                    {
                        BusLineID2 = 6,
                        StationNumberOnLine = 15289,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 8,
                    },
                    new LineStation
                    {
                        BusLineID2 = 6,
                        StationNumberOnLine = 15290,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 9,
                    },
                    #endregion
                    #region 47-7תחנות קו מס' 
                    
                    new LineStation
                    {
                        BusLineID2 = 7,
                        StationNumberOnLine = 14927,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 0,
                    },
                    new LineStation
                    {
                        BusLineID2 = 7,
                        StationNumberOnLine = 14928,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 1,
                    },
                    new LineStation
                    {
                        BusLineID2 = 7,
                        StationNumberOnLine = 14929,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 2,
                    },
                    new LineStation
                    {
                        BusLineID2 = 7,
                        StationNumberOnLine = 14930,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 3,
                    },
                    new LineStation
                    {
                        BusLineID2 = 7,
                        StationNumberOnLine = 14931,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 4,
                    },
                    new LineStation
                    {
                        BusLineID2 = 7,
                        StationNumberOnLine = 14932,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 5,
                    },
                    new LineStation
                    {
                        BusLineID2 = 7,
                        StationNumberOnLine = 14933,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 6,
                    },
                    new LineStation
                    {
                        BusLineID2 = 7,
                        StationNumberOnLine = 14934,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 7,
                    },
                    new LineStation
                    {
                        BusLineID2 = 7,
                        StationNumberOnLine = 14935,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 8,
                    },
                    new LineStation
                    {
                        BusLineID2 = 7,
                        StationNumberOnLine = 14936,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 9,
                    },
                    #endregion
                    #region 48-8תחנות קו מס' 
                  
                    new LineStation
                    {
                        BusLineID2 = 8,
                        StationNumberOnLine = 10509,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 0,
                    },
                    new LineStation
                    {
                        BusLineID2 = 8,
                        StationNumberOnLine = 10847,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 1,
                    },
                    new LineStation
                    {
                        BusLineID2 = 8,
                        StationNumberOnLine = 13426,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 2,
                    },
                    new LineStation
                    {
                        BusLineID2 = 8,
                        StationNumberOnLine = 14531,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 3,
                    },
                    new LineStation
                    {
                        BusLineID2 = 8,
                        StationNumberOnLine = 14533,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 4,
                    },
                    new LineStation
                    {
                        BusLineID2 = 8,
                        StationNumberOnLine = 14534,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 5,
                    },
                    new LineStation
                    {
                        BusLineID2 = 8,
                        StationNumberOnLine = 14537,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 6,
                    },
                    new LineStation
                    {
                        BusLineID2 = 8,
                        StationNumberOnLine = 14538,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 7,
                    },
                    new LineStation
                    {
                        BusLineID2 = 8,
                        StationNumberOnLine = 14540,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 8,
                    },
                    new LineStation
                    {
                        BusLineID2 = 8,
                        StationNumberOnLine = 14541,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 9,
                    },
                    #endregion
                    #region 49-9תחנות קו מס' 
                   
                    new LineStation
                    {
                        BusLineID2 = 9,
                        StationNumberOnLine = 14541,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 0,
                    },
                    new LineStation
                    {
                        BusLineID2 = 9,
                        StationNumberOnLine = 14542,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 1,
                    },
                    new LineStation
                    {
                        BusLineID2 = 9,
                        StationNumberOnLine = 14904,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 2,
                    },
                    new LineStation
                    {
                        BusLineID2 = 9,
                        StationNumberOnLine = 14905,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 3,
                    },
                    new LineStation
                    {
                        BusLineID2 = 9,
                        StationNumberOnLine = 14906,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 4,
                    },
                    new LineStation
                    {
                        BusLineID2 = 9,
                        StationNumberOnLine = 14908,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 5,
                    },
                    new LineStation
                    {
                        BusLineID2 = 9,
                        StationNumberOnLine = 14909,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 6,
                    },
                    new LineStation
                    {
                        BusLineID2 = 9,
                        StationNumberOnLine = 14910,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 7,
                    },
                    new LineStation
                    {
                        BusLineID2 = 9,
                        StationNumberOnLine = 14912,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 8,
                    },
                    new LineStation
                    {
                        BusLineID2 = 9,
                        StationNumberOnLine = 15300,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 9,
                    },
                    #endregion              
                    #region תחנות קו מס50-10'  
                    new LineStation
                    {
                        BusLineID2 = 10,
                        StationNumberOnLine = 14917,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 0,
                    },
                    new LineStation
                    {
                        BusLineID2 = 10,
                        StationNumberOnLine = 14918,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 1,
                    },
                    new LineStation
                    {
                        BusLineID2 = 10,
                        StationNumberOnLine = 14919,

                        ChackDelete2 = true,
                        LocationNumberOnLine = 2,
                    },
                    new LineStation
                    {
                        BusLineID2 = 10,
                        StationNumberOnLine = 14920,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 3,
                    },
                    new LineStation
                    {
                        BusLineID2 = 10,
                        StationNumberOnLine = 14921,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 4,
                    },
                    new LineStation
                    {
                        BusLineID2 = 10,
                        StationNumberOnLine = 14922,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 5,
                    },
                    new LineStation
                    {
                        BusLineID2 = 10,
                        StationNumberOnLine = 14925,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 6,
                    },
                    new LineStation
                    {
                        BusLineID2 = 10,
                        StationNumberOnLine = 14926,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 7,
                    },
                    new LineStation
                    {
                        BusLineID2 = 10,
                        StationNumberOnLine = 14927,
                        ChackDelete2 = true,
                        LocationNumberOnLine = 8,
                    },
                    new LineStation
                    {
                        BusLineID2 = 10,
                        StationNumberOnLine = 14928,
                ChackDelete2 = true,
                        LocationNumberOnLine = 9,
                    }
                    #endregion
            };
        }
        public static TimeSpan GetTimeBeforeLunch()
        {
            return new TimeSpan(0, R.Next(1, 7), R.Next(0, 60));
        }
        public static float GetDistanceBeforeLunch()
        {
            return (float)((float)(R.NextDouble() * (4.6 - 1)) + 1);
        }
        //14905 14906 14908 14909 14910 14912 14913 14917 14918 14919
        //14540 14541 14542 14904 14905 14906 14908 14909 14910 14912 
        //14533 14534 14537 14538 14540 14541 14542 14904 14905 14906
        //14931 14932 14933 14934 14935 14936 15285 15287 15289 15290
        //14927 14928 14929 14930 14931 14932 14933 14934 14935 14936
        //10509 10847 13426 14531 14533 14534 14537 14538 14540 14541
        //14541 14542 14904 14905 14906 14908 14909 14910 14912 15300
        //14917 14918 14919 14920 14921 14922 14925 14926 14927 14928
        public static void InitializationConsecutiveStations()
        {
            ConsecutiveStations = new List<ConsecutiveStations>
            {
            // 50
            new ConsecutiveStations
            {
                StationNumber1 = 14917,
                StationNumber2 = 14918,
                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                AverageTime = GetTimeBeforeLunch()
            },
            new ConsecutiveStations
            {
                StationNumber1 = 14918,
                StationNumber2 = 14919,
                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                AverageTime = GetTimeBeforeLunch()
            },

                            new ConsecutiveStations
                            {
                                StationNumber1 = 14919,
                                StationNumber2 = 14920,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14920,
                                StationNumber2 = 14921,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14921,
                                StationNumber2 = 14922,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14922,
                                StationNumber2 = 14925,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14925,
                                StationNumber2 = 14926,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14926,
                                StationNumber2 = 14927,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14927,
                                StationNumber2 = 14928,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            
                            
                            


                            // 40 
                              new ConsecutiveStations
                              {
                                  StationNumber1 = 10045,
                                  StationNumber2 = 10509,
                                  DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                  AverageTime = GetTimeBeforeLunch()

                              },
                              new ConsecutiveStations
                              {
                                  StationNumber1 = 10509,
                                  StationNumber2 = 10510,
                                  DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                  AverageTime = GetTimeBeforeLunch()
                              },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 10510,
                                StationNumber2 = 13426,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 13426,
                                StationNumber2 = 14531,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14531,
                                StationNumber2 = 14533,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14533,
                                StationNumber2 = 14534,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14534,
                                StationNumber2 = 14537,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14537,
                                StationNumber2 = 14538,

                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14538,
                                StationNumber2 = 14540,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },





                            // 41

                            new ConsecutiveStations

                            {
                                StationNumber1 = 10510,
                                StationNumber2 = 10836,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 10836,
                                StationNumber2 = 10837,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                             new ConsecutiveStations
                             {
                                 StationNumber1 = 10837,
                                 StationNumber2 = 10847,
                                 DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                 AverageTime = GetTimeBeforeLunch()
                             },
                              new ConsecutiveStations
                              {
                                  StationNumber1 = 10847,
                                  StationNumber2 = 13426,
                                  DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                  AverageTime = GetTimeBeforeLunch()
                              },

                            // 42
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14540,
                                StationNumber2 = 14541,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },

                            new ConsecutiveStations
                            {
                                StationNumber1 = 14541,
                                StationNumber2 = 14542
            ,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14542,
                                StationNumber2 = 14904,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14904,
                                StationNumber2 = 14905,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14905,
                                StationNumber2 = 14906,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14906,
                                StationNumber2 = 14908,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14908
             ,
                                StationNumber2 = 14909,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14909,
                                StationNumber2 = 14910,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14910,
                                StationNumber2 = 14912,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },


                            // 43
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14912,
                                StationNumber2 = 14913,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },

                            new ConsecutiveStations
                            {
                                StationNumber1 = 14913,
                                StationNumber2 = 14917,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                          
                            // 45

                            

                            // 46
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14931,
                                StationNumber2 = 14932,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },

                            new ConsecutiveStations
                            {
                                StationNumber1 = 14932,
                                StationNumber2 = 14933,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },

                            new ConsecutiveStations
                            {
                                StationNumber1 = 14933,
                                StationNumber2 = 14934,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },




                            new ConsecutiveStations
                            {
                                StationNumber1 = 14934,
                                StationNumber2 = 14935,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14935,
                                StationNumber2 = 14936,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14936,
                                StationNumber2 = 15285,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 15285,
                                StationNumber2 = 15287,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 15287,
                                StationNumber2 = 15289,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 15289,
                                StationNumber2 = 15290,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },


                            // 47
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14928,
                                StationNumber2 = 14929,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14929,
                                StationNumber2 = 14930,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14930,
                                StationNumber2 = 14931,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14931,
                                StationNumber2 = 14932,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14932,
                                StationNumber2 = 14933,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14933,
                                StationNumber2 = 14934,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14934,
                                StationNumber2 = 14935,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },
                            new ConsecutiveStations
                            {
                                StationNumber1 = 14935,
                                StationNumber2 = 14936,
                                DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                AverageTime = GetTimeBeforeLunch()
                            },

                            // 48
                                new ConsecutiveStations
                                {
                                    StationNumber1 = 10509,
                                    StationNumber2 = 10847,
                                    DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                    AverageTime = GetTimeBeforeLunch()
                                },
                              

                                  // 49
                                  new ConsecutiveStations
                                  {
                                      StationNumber1 = 14912,
                                      StationNumber2 = 15300,
                                      DistanceBetweenTooStations = GetDistanceBeforeLunch(),
                                      AverageTime = GetTimeBeforeLunch()
                                  },
             };
        }
    }
}

















//    /// <summary>
//    /// 
//    /// </summary>
//    public static class DataSource
//    {
//        static DataSource()
//        {
//            for (int i = 0; i < R.Next(25, 35); i++)
//            {
//                InitializationBus();
//            }
//            for (int i = 0; i < 100; i++)
//            {
//                InitializationStation();
//            }
//            for (int i = 0; i < 10; i++)
//            {
//                InitializationBusLines();
//            }
//            //for (int i = 0; i < 98; i++)
//            //{
//            //    InitializationConsecutiveStations(i);
//            //}
//            InitializationLineStation();
//        }
//        public static Random R = new Random(DateTime.Now.Millisecond);
//        public static List<Bus> Bus1 = new List<Bus>();
//        public static List<BusStation> BusStations = new List<BusStation>();
//        public static List<ConsecutiveStations> ConsecutiveStations = new List<ConsecutiveStations>();
//        public static List<LineStation> LineStations = new List<LineStation>();
//        public static List<BusLine> BusLines = new List<BusLine>();
//        public static List<BusTraveling> BusTravelings = new List<BusTraveling>();
//        public static List<LineExit> LineExits = new List<LineExit>();
//        public static List<User> Users = new List<User>();

//        static void InitializationUser()
//        {
//            Users = new List<User>
//            {
//                new User
//                {
//                    ChackDelete = true,
//                    Username = "yehuda",
//                    Password = "yehudashor789",
//                    ManagementPermission = true,
//                    Permission1 = Permission.ManagementPermission
//                },

//                new User
//                {
//                    ChackDelete = true,
//                    Username = "45454",
//                    Password = "yehudashor789",
//                    ManagementPermission = true,
//                    Permission1 = Permission.ManagementPermission
//                },

//                new User
//                {
//                    ChackDelete = true,
//                    Username = "Shor1998",
//                    Password = "05276351",
//                    ManagementPermission = false,
//                    Permission1 = Permission.WithoutManagementPermission
//                },

//                new User
//                {
//                    ChackDelete = false,
//                    Username = "koko",
//                    Password = "111111",
//                    ManagementPermission = true,
//                    Permission1 = Permission.ManagementPermission
//                },

//                new User
//                {
//                    ChackDelete = true,
//                    Username = "gggg",
//                    Password = "222222",
//                    ManagementPermission = true,
//                    Permission1 = Permission.ManagementPermission
//                }
//            };
//        }

//        public static void InitializationStation()
//        {


//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    },

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }

//            //    new BusStation
//            //    {
//            //         IsAvailable3 = true,
//            //         StationId1 = NumbersAreRunning.StationID,
//            //         StationNumber = R.Next(1000000),
//            //         //NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//            //        // StationAddress = stationaddress1[R.Next(42)],
//            //         RoofToTheStation = true,
//            //         AccessForDisabled = true,
//            //         Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//            //         Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//            //    }


//            //};
//            string[] stationaddress1 = new string[42];
//            string[] stationaddress2 = new string[42];
//            bool[] chack = new bool[2] { true, false };
//            AddAdress(ref stationaddress1);
//            NameOfStation(ref stationaddress2);
//            BusStation busStation = new BusStation
//            {
//                StationId1 = NumbersAreRunning.StationID,
//                StationNumber = R.Next(1000000),
//                Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
//                Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
//                NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
//                StationAddress = stationaddress1[R.Next(42)],
//                AccessForDisabled = chack[R.Next(2)],
//                IsAvailable3 = true,
//                RoofToTheStation = chack[R.Next(2)]
//            };
//            BusStations.Add(busStation);
//            NumbersAreRunning.StationID++;
//        }

//        static void InitializationBus()
//        {
//            int number = R.Next(1200);
//            Bus bus = new Bus
//            {
//                License_number = R.Next(1000000, 100000000).ToString(),
//                StartDate = new DateTime(R.Next(1999, 2020), R.Next(1, 13), R.Next(1, 29)),
//                KmForRefueling = number,
//                KmForTreatment = number * R.Next(3, 10),
//                TotalMiles = number * R.Next(20, 25),
//                Status = (TravelMode)R.Next(4)
//            };
//            Bus1.Add(bus);
//        }

//        //static void InitializationConsecutiveStations(int i)
//        //{
//        //    ConsecutiveStations consecutiveStations = new ConsecutiveStations
//        //    {
//        //        StationNumber1 = BusStations[i].StationNumber,
//        //        StationNumber2 = BusStations[i + 1].StationNumber,
//        //        DistanceBetweenTooStations = R.Next(50),
//        //        AverageTime = R.Next(3, 20)
//        //    };
//        //    ConsecutiveStations.Add(consecutiveStations);
//        //}

//        static void InitializationBusLines()
//        {
//            bool[] chack = new bool[2] { true, false };
//            BusLine busLine = new BusLine
//            {
//                BusLineID1 = NumbersAreRunning.BusLineID,
//                LineNumber = R.Next(999),
//                AreaBusInterUrban = (Area)R.Next(5),
//                UrbanInterUrban = chack[R.Next(2)],
//                IsAvailable1 = true
//            };
//            BusLines.Add(busLine);
//            NumbersAreRunning.BusLineID++;
//        }

//        static void InitializationLineStation()
//        {
//            for (int i = 0; i < BusLines.Count; i++)
//            {
//                for (int j = 0; j < 10; j++)
//                {
//                    LineStation lineStation = new LineStation
//                    {
//                        BusLineID2 = BusLines[i].BusLineID1
//                    };
//                    lineStation.StationNumberOnLine = BusStations[j].StationNumber;
//                    lineStation.LocationNumberOnLine = j;
//                    lineStation.ChackDelete2 = true;
//                    LineStations.Add(lineStation);

//                    if (j == 0)
//                    {
//                        BusLines[i].FirstStation = BusStations[j].StationNumber;
//                    }
//                    if (j == 9)
//                    {
//                        BusLines[i].LastStation = BusStations[j].StationNumber;
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// A function designed to initialize the station addresses is called in an initial initialization function.
//        /// </summary>
//        /// <param name="stationaddress1"></param>
//        static void AddAdress(ref string[] stationaddress1)
//        {
//            stationaddress1[0] = " כנסת ישראל/רות ";
//            stationaddress1[1] = " כנסת ישראל/הושע";
//            stationaddress1[2] = "יואל/כנסת ישראל ";
//            stationaddress1[3] = " כנסת ישראל/עובדיה";
//            stationaddress1[4] = " הרב עובדיה יוסף/דרך מנחם בגין";
//            stationaddress1[5] = "שדה תעופה בן גוריון/טרמינל 3";
//            stationaddress1[6] = "תרכבת בני ברק";
//            stationaddress1[7] = "הרב אלישיב/באר יעקב ";
//            stationaddress1[8] = "ת.רכבת מזכרת בתיה/איסוף ";
//            stationaddress1[9] = "בר כוכבא/ת.מרכזית פתח תקווה ";
//            stationaddress1[10] = "הרצל/החלוצים ";
//            stationaddress1[11] = " הרצל/החלוצים";
//            stationaddress1[12] = "הרצל/התמרים  ";
//            stationaddress1[13] = "הרצל/הפרחים ";
//            stationaddress1[14] = "הרצל/התמנים ";
//            stationaddress1[15] = " חזון אי''ש/הרב אלישיב";
//            stationaddress1[16] = "חזון אי''ש/הרב קנייבסקי ";
//            stationaddress1[17] = "חזון אי''ש/הרב שטיינמן ";
//            stationaddress1[18] = " חזון אי''ש/הרב פוברסקי";
//            stationaddress1[19] = "חזון אי''ש/הרב ברמן ";
//            stationaddress1[20] = " שדרות רימון/שדרות חיטה";
//            stationaddress1[21] = "שדרות רימון/שדרות שעורה ";
//            stationaddress1[22] = "שדרות רימון/שדרות גפן ";
//            stationaddress1[23] = " שדרות רימון/שדרות תאנה";
//            stationaddress1[24] = " שדרות רימון/שדרות תמר";
//            stationaddress1[25] = "עמנואל זיסמן/דוד כהן ";
//            stationaddress1[26] = "עמנואל זיסמן/אהרון כהן ";
//            stationaddress1[27] = "עמנואל זיסמן/משה כהן ";
//            stationaddress1[28] = " עמנואל זיסמן/יצחק כהן";
//            stationaddress1[29] = "עמנואל זיסמן/יעקב כהן ";
//            stationaddress1[30] = "נעמי שמר/דר' ג'ין קלוס פישמן ";
//            stationaddress1[31] = " נעמי שמר/דר' ג'ין חדוה פישמן";
//            stationaddress1[32] = "נעמי שמר/דר' ג'ין קרלוס השודד פישמן ";
//            stationaddress1[33] = " קוקו השמן";
//            stationaddress1[34] = "יונתן הקטן  ";
//            stationaddress1[35] = "איראן בוחרת ישראל ";
//            stationaddress1[36] = "טראמפ הלך באסההה ";
//            stationaddress1[37] = "מה הלוז פה ";
//            stationaddress1[38] = " גני תקווה";
//            stationaddress1[39] = "קרית אונו  ";
//            stationaddress1[40] = " האדמור מנדי כהנא";
//        }

//        static void NameOfStation(ref string[] stationaddress1)
//        {
//            stationaddress1[0] = "רחוב כנסת ישראל ";
//            stationaddress1[1] = "רחוב כנסת ישראל";
//            stationaddress1[2] = "רחוב יואל ";
//            stationaddress1[3] = "רחוב כנסת ישראל";
//            stationaddress1[4] = "רחוב הרב עובדיה יוסף";
//            stationaddress1[5] = "רחוב שדה תעופה בן גוריון ";
//            stationaddress1[6] = "רחוב רכבת בני ברק";
//            stationaddress1[7] = "רחוב הרב אלישיב ";
//            stationaddress1[8] = "רחוב ת.רכבת מזכרת בתיה ";
//            stationaddress1[9] = "רחוב בר כוכבא ";
//            stationaddress1[10] = "רחוב הרצל ";
//            stationaddress1[11] = "רחוב הרצל";
//            stationaddress1[12] = "רחוב הרצל";
//            stationaddress1[13] = "רחוב הרצל";
//            stationaddress1[14] = "רחוב הרצל";
//            stationaddress1[15] = "רחוב חזון איש";
//            stationaddress1[16] = "רחוב הרב קנייבסקי";
//            stationaddress1[17] = "רחוב הרב שטיינמן";
//            stationaddress1[18] = "רחוב הרב פוברסקי";
//            stationaddress1[19] = "רחוב הרב ברמן ";
//            stationaddress1[20] = "רחוב שדרות חיטה";
//            stationaddress1[21] = "רחוב שדרות שעורה  ";
//            stationaddress1[22] = "רחוב שדרות גפן ";
//            stationaddress1[23] = "רחוב שדרות תאנה";
//            stationaddress1[24] = "רחוב שדרות תמר";
//            stationaddress1[25] = "רחוב דוד כהן ";
//            stationaddress1[26] = "רחוב אהרון כהן ";
//            stationaddress1[27] = "רחוב משה כהן ";
//            stationaddress1[28] = "רחוב יצחק כהן";
//            stationaddress1[29] = "רחוב יעקב כהן ";
//            stationaddress1[30] = "רחוב נעמי שמר ";
//            stationaddress1[31] = "רחוב דר' ג'ין חדוה פישמן";
//            stationaddress1[32] = "רחוב השודד פישמן";
//            stationaddress1[33] = "רחוב קוקו השמן";
//            stationaddress1[34] = "רחוב יונתן הקטן  ";
//            stationaddress1[35] = "רחוב איראן בוחרת ישראל ";
//            stationaddress1[36] = "רחוב טראמפ הלך באסההה ";
//            stationaddress1[37] = "רחוב מה הלוז פה ";
//            stationaddress1[38] = "רחוב גני תקווה";
//            stationaddress1[39] = "רחוב קרית אונו";
//            stationaddress1[40] = "רחוב האדמור מנדי כהנא";
//        }
//    }
//}
