using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DS
{
    public class DataSource
    {
        public DataSource()
        {
            for (int i = 0; i < R.Next(25, 35); i++)
            {
                InitializationBus();
            }
            for (int i = 0; i < 100; i++)
            {
                InitializationStation();
            }
            for (int i = 0; i < 100; i++)
            {
                InitializationConsecutiveStations(i);
            }
            for (int i = 0; i < 10; i++)
            {
                InitializationBusLines();
            }
        }
        public static Random R = new Random(DateTime.Now.Millisecond);

        public static List<Bus> Bus1 { get; set; }
        public static List<BusStation> BusStations { get; set; }
        public static List<ConsecutiveStations> ConsecutiveStations { get; set; }
        public static List<LineStation> LineStations { get; set; }
        public static List<BusLine> BusLines { get; set; }
        public static List<BusTraveling> BusTravelings { get; set; }
        public static List<LineExit> LineExits { get; set; }
        public static List<User> Users { get; set; }

        public static void InitializationStation()
        {
            string[] stationaddress1 = new string[42];
            string[] stationaddress2 = new string[42];
            bool[] chack = new bool[2] { true, false };
            AddAdress(ref stationaddress1);
            NameOfStation(ref stationaddress2);
            BusStation busStation = new BusStation
            {
                StationId1 = NumbersAreRunning.StationID,
                StationNumber = R.Next(1000000),
                Latitude = (float)((float)(R.NextDouble() * (33.3 - 31)) + 31),
                Longitude = (float)((float)(R.NextDouble() * (35.5 - 34.3)) + 34.3),
                NameOfStation = stationaddress2[R.Next(42)] + "\t" + R.Next(100),
                StationAddress = stationaddress1[R.Next(42)],
                AccessForDisabled = chack[R.Next(3)],
                IsAvailable3 = true,
                RoofToTheStation = chack[R.Next(3)]
            };
            BusStations.Add(busStation);
            NumbersAreRunning.StationID++;
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
                Status = (TravelMode)R.Next(4)
            };
            Bus1.Add(bus);
        }



        public static void InitializationConsecutiveStations(int i)
        {
            ConsecutiveStations consecutiveStations = new ConsecutiveStations
            {
                StationNumber1 = BusStations[i].StationNumber,
                StationNumber2 = BusStations[i + 1].StationNumber,
                DistanceBetweenTooStations = R.Next(50),
                AverageTime = R.Next(3, 20)
            };
            ConsecutiveStations.Add(consecutiveStations);
        }

        public static void InitializationBusLines()
        {
            bool[] chack = new bool[2] { true, false };
            BusLine busLine = new BusLine
            {
                BusLineID1 = NumbersAreRunning.BusLineID,
                LineNumber = R.Next(999),
                AreaBusInterUrban = (Area)R.Next(5),
                UrbanInterUrban = chack[R.Next(3)]
            };
            BusLines.Add(busLine);
            NumbersAreRunning.BusLineID++;
        }

        public static void InitializationLineStation()
        {
            int k = 0, j;
            for (int i = 0; i < BusLines.Count; i++)
            {
                LineStation lineStation = new LineStation
                {
                    BusLineID2 = BusLines[i].BusLineID1
                };
                for (j = 0; j < 55; j += R.Next(1, 4))
                {
                    lineStation.StationNumberOnLine = BusStations[j].StationNumber;
                    lineStation.LocationNumberOnLine = k;
                    LineStations.Add(lineStation);
                    if (j == 0)
                    {
                        BusLines[i].FirstStation = BusStations[j].StationNumber;
                    }
                    k++;
                }
                BusLines[i].LastStation = BusStations[j].StationNumber;
                k = 0;
            }
        }

        /// <summary>
        /// A function designed to initialize the station addresses is called in an initial initialization function.
        /// </summary>
        /// <param name="stationaddress1"></param>
        public static void AddAdress(ref string[] stationaddress1)
        {
            stationaddress1[0] = " כנסת ישראל/רות ";
            stationaddress1[1] = " כנסת ישראל/הושע";
            stationaddress1[2] = "יואל/כנסת ישראל ";
            stationaddress1[3] = " כנסת ישראל/עובדיה";
            stationaddress1[4] = " הרב עובדיה יוסף/דרך מנחם בגין";
            stationaddress1[5] = "שדה תעופה בן גוריון/טרמינל 3";
            stationaddress1[6] = "תרכבת בני ברק";
            stationaddress1[7] = "הרב אלישיב/באר יעקב ";
            stationaddress1[8] = "ת.רכבת מזכרת בתיה/איסוף ";
            stationaddress1[9] = "בר כוכבא/ת.מרכזית פתח תקווה ";
            stationaddress1[10] = "הרצל/החלוצים ";
            stationaddress1[11] = " הרצל/החלוצים";
            stationaddress1[12] = "הרצל/התמרים  ";
            stationaddress1[13] = "הרצל/הפרחים ";
            stationaddress1[14] = "הרצל/התמנים ";
            stationaddress1[15] = " חזון אי''ש/הרב אלישיב";
            stationaddress1[16] = "חזון אי''ש/הרב קנייבסקי ";
            stationaddress1[17] = "חזון אי''ש/הרב שטיינמן ";
            stationaddress1[18] = " חזון אי''ש/הרב פוברסקי";
            stationaddress1[19] = "חזון אי''ש/הרב ברמן ";
            stationaddress1[20] = " שדרות רימון/שדרות חיטה";
            stationaddress1[21] = "שדרות רימון/שדרות שעורה ";
            stationaddress1[22] = "שדרות רימון/שדרות גפן ";
            stationaddress1[23] = " שדרות רימון/שדרות תאנה";
            stationaddress1[24] = " שדרות רימון/שדרות תמר";
            stationaddress1[25] = "עמנואל זיסמן/דוד כהן ";
            stationaddress1[26] = "עמנואל זיסמן/אהרון כהן ";
            stationaddress1[27] = "עמנואל זיסמן/משה כהן ";
            stationaddress1[28] = " עמנואל זיסמן/יצחק כהן";
            stationaddress1[29] = "עמנואל זיסמן/יעקב כהן ";
            stationaddress1[30] = "נעמי שמר/דר' ג'ין קלוס פישמן ";
            stationaddress1[31] = " נעמי שמר/דר' ג'ין חדוה פישמן";
            stationaddress1[32] = "נעמי שמר/דר' ג'ין קרלוס השודד פישמן ";
            stationaddress1[33] = " קוקו השמן";
            stationaddress1[34] = "יונתן הקטן  ";
            stationaddress1[35] = "איראן בוחרת ישראל ";
            stationaddress1[36] = "טראמפ הלך באסההה ";
            stationaddress1[37] = "מה הלוז פה ";
            stationaddress1[38] = " גני תקווה";
            stationaddress1[39] = "קרית אונו  ";
            stationaddress1[40] = " האדמור מנדי כהנא";
        }

        public static void NameOfStation(ref string[] stationaddress1)
        {
            stationaddress1[0] = "רחוב כנסת ישראל ";
            stationaddress1[1] = "רחוב כנסת ישראל";
            stationaddress1[2] = "רחוב יואל ";
            stationaddress1[3] = "רחוב כנסת ישראל";
            stationaddress1[4] = "רחוב הרב עובדיה יוסף";
            stationaddress1[5] = "רחוב שדה תעופה בן גוריון ";
            stationaddress1[6] = "רחוב רכבת בני ברק";
            stationaddress1[7] = "רחוב הרב אלישיב ";
            stationaddress1[8] = "רחוב ת.רכבת מזכרת בתיה ";
            stationaddress1[9] = "רחוב בר כוכבא ";
            stationaddress1[10] = "רחוב הרצל ";
            stationaddress1[11] = "רחוב הרצל";
            stationaddress1[12] = "רחוב הרצל";
            stationaddress1[13] = "רחוב הרצל";
            stationaddress1[14] = "רחוב הרצל";
            stationaddress1[15] = "רחוב חזון איש";
            stationaddress1[16] = "רחוב הרב קנייבסקי";
            stationaddress1[17] = "רחוב הרב שטיינמן";
            stationaddress1[18] = "רחוב הרב פוברסקי";
            stationaddress1[19] = "רחוב הרב ברמן ";
            stationaddress1[20] = "רחוב שדרות חיטה";
            stationaddress1[21] = "רחוב שדרות שעורה  ";
            stationaddress1[22] = "רחוב שדרות גפן ";
            stationaddress1[23] = "רחוב שדרות תאנה";
            stationaddress1[24] = "רחוב שדרות תמר";
            stationaddress1[25] = "רחוב דוד כהן ";
            stationaddress1[26] = "רחוב אהרון כהן ";
            stationaddress1[27] = "רחוב משה כהן ";
            stationaddress1[28] = "רחוב יצחק כהן";
            stationaddress1[29] = "רחוב יעקב כהן ";
            stationaddress1[30] = "רחוב נעמי שמר ";
            stationaddress1[31] = "רחוב דר' ג'ין חדוה פישמן";
            stationaddress1[32] = "רחוב השודד פישמן";
            stationaddress1[33] = "רחוב קוקו השמן";
            stationaddress1[34] = "רחוב יונתן הקטן  ";
            stationaddress1[35] = "רחוב איראן בוחרת ישראל ";
            stationaddress1[36] = "רחוב טראמפ הלך באסההה ";
            stationaddress1[37] = "רחוב מה הלוז פה ";
            stationaddress1[38] = "רחוב גני תקווה";
            stationaddress1[39] = "רחוב קרית אונו";
            stationaddress1[40] = "רחוב האדמור מנדי כהנא";
        }
    }
}
