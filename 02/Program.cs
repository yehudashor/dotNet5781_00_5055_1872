using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace dotNet_02_5055_1872
{
    class Program
    {
        static Random r = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            CollectionOfBusLines collectionOfbusLines = new CollectionOfBusLines();
            List<BusLineStation> buslinestation = new List<BusLineStation>();

            // Initial initialization of ten lines according to the requirements within the loop has a call to function.
            int numberOfStatian;
            int numberLine;
            for (int i = 0; i < 10; i++)
            {
                numberLine = r.Next(999);
                numberOfStatian = r.Next(2, 10);
                BusLine busLine = new BusLine(numberLine);
                collectionOfbusLines.CollectionOfLines = AddLineFirstly(ref busLine, ref buslinestation, numberOfStatian);
                Console.WriteLine("good the Line insrted to list ");
            }

            //Two lines that will pass at all stations as required in the exercise.
            for (int i = 0; i < 2; i++)
            {
                numberLine = r.Next(999);
                BusLine busLine1 = new BusLine(numberLine)
                {
                    RouteTheLine = buslinestation
                };
                collectionOfbusLines.CollectionOfLines = busLine1;
            }

            int number;
            Console.WriteLine(@" 
             Enter 1 to add a new bus line and station to line:
             Enter 2 to delete a bus line and to delete a station:
             Enter 3 to find the lines that pass through the station:
             Enter 4 to Print the data:
             Enter -1 to Exit :");
            Console.WriteLine();
            Console.WriteLine("Enter a number to choose from the enum");

            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("rong number!!! enter again:");
            }

            myenum choice = (myenum)number;

            while (choice != myenum.Exit)
            {
                switch (choice)
                {
                    case myenum.AddNewBus:
                        try
                        {
                            AddNewBus1(ref collectionOfbusLines);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;
                    case myenum.AddStatian:
                        try
                        {
                            AddStatian1(ref collectionOfbusLines);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case myenum.DeleteBus:
                        try
                        {
                            DeleteLine(ref collectionOfbusLines);
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case myenum.DeleteStatian:
                        try
                        {
                            DeleteStation(ref collectionOfbusLines);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case myenum.SearchLinesAtTheStation:
                        try
                        {
                            SearchBus(ref collectionOfbusLines);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case myenum.SearchTimeTravelOptions:
                        try
                        {
                            SearchTime(ref collectionOfbusLines);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case myenum.PrintOllBuses:
                        foreach (BusLine item1 in collectionOfbusLines)
                        {
                            Console.WriteLine(item1);
                        }
                        break;

                    case myenum.PrintBusesAndStations:

                        List<int> temp = new List<int>();
                        foreach (BusLine item2 in collectionOfbusLines)
                        {
                            foreach (BusLineStation item3 in item2.RouteTheLine)
                            {
                                temp.Add(item3.StationNumber);
                            }
                        }

                        for (int i = 0; i < temp.Count; i++)
                        {
                            List<int> temp1 = collectionOfbusLines.StationLines(temp[i]);
                            Console.WriteLine("At station number: " + temp[i] + " The lines pass is: ");
                            for (int j = 0; j < temp1.Count; j++)
                            {
                                Console.WriteLine(temp1[j]);
                            }
                            temp1.Clear();
                        }
                        break;

                    case myenum.Exit:
                        break;
                    default:
                        Console.WriteLine("end of progrem: ");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("enter a number: ");
                while (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("rong number!!! enter again: ");
                }

                choice = (myenum)number;
                Console.WriteLine();
            }
            _ = Console.ReadKey();
        }

        /// <summary>
        /// A function that receives an object of the bus line type, a list of objects of line stations, and the number of stations in the line.
        /// and makes an initial boot to ten lines and includes the requirements.
        /// </summary>
        /// <param name="busLine"></param>
        /// <param name="buslinestation"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static BusLine AddLineFirstly(ref BusLine busLine, ref List<BusLineStation> buslinestation, int count)
        {
            List<BusLineStation> buslinestation1 = new List<BusLineStation>();
            int stationNumber;
            string[] stationaddress1 = new string[42];

            AddAdress(ref stationaddress1);

            for (int i = 0; i < count; i++)
            {
                stationNumber = r.Next(999999);
                buslinestation1.Add(new BusLineStation(stationNumber, stationaddress1[r.Next(42)]));
            }

            foreach (BusLineStation item in buslinestation1)
            {
                buslinestation.Add(item);
            }

            busLine.RouteTheLine = buslinestation1;
            return busLine;
        }

        /// <summary>
        /// Adding a new line
        /// </summary>
        /// <param name="collectionOfbusLines"></param>
        public static void AddNewBus1(ref CollectionOfBusLines collectionOfbusLines)
        {
            int numberline, NumberOfStation, NumStation;
            string Address;
            Console.WriteLine("Enter a line number: ");
            while (!int.TryParse(Console.ReadLine(), out numberline))
            {
                Console.WriteLine("rong number!!! enter again:");
            }
            Console.WriteLine("Enter the number of stations in the line: ");
            while (!int.TryParse(Console.ReadLine(), out NumberOfStation))
            {
                Console.WriteLine("rong number!!! enter again: ");
            }
            if (NumberOfStation < 2)
            {
                throw new ArgumentException("Error!!! The line must have a minimum of two stations");
            }
            else
            {
                BusLine busLineadd = new BusLine(numberline);
                for (int i = 0; i < NumberOfStation; i++)
                {
                    Console.WriteLine("Enter number of statian: ");
                    while (!int.TryParse(Console.ReadLine(), out NumStation))
                    {
                        Console.WriteLine("rong number!!! enter again: ");
                    }
                    try
                    {
                        if (!PlaceStations(NumStation, busLineadd, collectionOfbusLines))
                        {
                            Console.WriteLine("enter Address of the statian");
                            Address = Console.ReadLine();
                            busLineadd.AddLineToTheRoundTrip(i, new BusLineStation(NumStation, Address));
                        }
                        else
                        {
                            --i;
                            throw new ArgumentException("Error!!! The station was in a different location");
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                collectionOfbusLines.CollectionOfLines = busLineadd;
                Console.WriteLine("Very Good");
            }
        }

        /// <summary>
        /// Adding a new station.
        /// </summary>
        /// <param name="collectionOfbusLines"></param>
        public static void AddStatian1(ref CollectionOfBusLines collectionOfbusLines)
        {
            bool flag = false;
            int numberline, index, NumStation;
            string Address;
            Console.WriteLine("Select the line number you want to add a station: ");
            while (!int.TryParse(Console.ReadLine(), out numberline))
            {
                Console.WriteLine("rong number!!! enter again:");
            }

            foreach (BusLine item in collectionOfbusLines)
            {
                if (item.LineNumber == numberline)
                {
                    flag = true;
                    Console.WriteLine("Enter a number that represents the location of the new station on the line: ");
                    while (!int.TryParse(Console.ReadLine(), out index))
                    {
                        Console.WriteLine("rong number!!! enter again: ");
                    }

                    Console.WriteLine("Enter number of statian: ");
                    while (!int.TryParse(Console.ReadLine(), out NumStation))
                    {
                        Console.WriteLine("rong number!!! enter again: ");
                    }
                    try
                    {
                        if (!PlaceStations(NumStation, item, collectionOfbusLines))
                        {
                            Console.WriteLine("enter address of statian: ");
                            Address = Console.ReadLine();
                            item.AddLineToTheRoundTrip(index, new BusLineStation(NumStation, Address));
                        }
                        else
                        {
                            throw new ArgumentException("Error!!! The station was in a different location");
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            if (!flag)
            {
                throw new ArgumentException("Error!!! The line was not found");
            }
            Console.WriteLine("Very Good");
        }

        /// <summary>
        /// Delete a line from the list.
        /// </summary>
        /// <param name="collectionOfbusLines"></param>
        public static void DeleteLine(ref CollectionOfBusLines collectionOfbusLines)
        {
            int LineToDelete;
            Console.WriteLine("Enter a line number to delete: ");
            while (!int.TryParse(Console.ReadLine(), out LineToDelete))
            {
                Console.WriteLine("rong number!!! enter again: ");
            }
             collectionOfbusLines.DeleteLineFromRoute(LineToDelete);
            Console.WriteLine("Very Good");
        }

        /// <summary>
        /// Deleting a station from the line route.
        /// </summary>
        /// <param name="collectionOfbusLine"></param>
        public static void DeleteStation(ref CollectionOfBusLines collectionOfbusLines)
        {
            bool flag = false, flag1 = false;
            int StatianToDelete, lineonumber;
            Console.WriteLine("Enter a line number: ");
            while (!int.TryParse(Console.ReadLine(), out lineonumber))
            {
                Console.WriteLine("rong number!!! enter again: ");
            }

            Console.WriteLine("Enter a statian number to delete: ");
            while (!int.TryParse(Console.ReadLine(), out StatianToDelete))
            {
                Console.WriteLine("rong number!!! enter again: ");
            }

            foreach (BusLine item in collectionOfbusLines)
            {
                if (item.LineNumber == lineonumber)
                {
                    flag = true;
                    if (item.InRoute(StatianToDelete))
                    {
                        flag1 = true;
                        try
                        {
                            if (item.RouteTheLine.Count < 3)
                            {
                                throw new FormatException("Error!!! The line must have a minimum of two stations. Deleting the station is not possible");
                            }
                            item.DeleteLineStationFromRoute(StatianToDelete);
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                if (!flag)
                {
                    throw new ArgumentException("Error!!! The line was not found");
                }
                if (!flag1)
                {
                    throw new ArgumentException("Error!!! The Station was not found");
                }
            }
        }

        /// <summary>
        /// Lines passing through the station according to the station number.
        /// </summary>
        /// <param name="collectionOfbusLines"></param>
        public static void SearchBus(ref CollectionOfBusLines collectionOfbusLines)
        {
            int Ssrarch;
            Console.WriteLine("Enter a statian number to search: ");
            while (!int.TryParse(Console.ReadLine(), out Ssrarch))
            {
                Console.WriteLine("rong number!!! enter again: ");
            }

            List<int> temp = collectionOfbusLines.StationLines(Ssrarch);
            Console.WriteLine("The lines that pass through this station are: ");
            foreach (int item in temp)
            {
                Console.WriteLine();
                Console.WriteLine(item);
            }
            Console.WriteLine("Very Good");
        }

        /// <summary>
        /// Printing options sorted from short to long travel times between two stations.
        /// </summary>
        /// <param name="collectionOfbusLines"></param>
        public static void SearchTime(ref CollectionOfBusLines collectionOfbusLines)
        {
            int StartingStation, DestinationStation;
            Console.WriteLine();
            Console.WriteLine("Please enter a departure station: ");
            while (!int.TryParse(Console.ReadLine(), out StartingStation))
            {
                Console.WriteLine("rong number!!! enter again: ");
            }

            Console.WriteLine("Please enter a departure station: ");
            while (!int.TryParse(Console.ReadLine(), out DestinationStation))
            {
                Console.WriteLine("rong number!!! enter again: ");
            }

            List<BusLine> busLines = new List<BusLine>();
            bool flag = false;
            foreach (BusLine item in collectionOfbusLines)
            {
                if (item.InRoute(StartingStation) && item.InRoute(DestinationStation))
                {
                    flag = true;
                    BusLine bus = item.SubRouteOfTheLine(StartingStation, DestinationStation);
                    bus.LineNumber = item.LineNumber;
                    bus.Area1 = item.Area1;
                    busLines.Add(bus);
                }
            }

            if (!flag)
            {
                throw new ArgumentException("Error!!! The two stations were not found in any line");
            }

            collectionOfbusLines.CollectionOfLines1 = busLines;
            _ = collectionOfbusLines.SortTravelTimesOnLines();
            foreach (BusLine item in collectionOfbusLines)
            {
                Console.WriteLine("Line number:  {0}", item);
            }
        }

        /// <summary>
        /// An auxiliary function that checks that no two station numbers are the same in different areas.
        /// </summary>
        /// <param name="numberOfStation"></param>
        /// <param name="busLineadd"></param>
        /// <param name="collectionOfbusLines"></param>
        /// <returns></returns>
        public static bool PlaceStations(int numberOfStation, BusLine busLineadd, CollectionOfBusLines collectionOfbusLines)
        {
            bool flag = false;
            foreach (BusLine item in collectionOfbusLines)
            {
                if (item.Area1 != busLineadd.Area1)
                {
                    foreach (BusLineStation item1 in item.RouteTheLine)
                    {
                        if (item1.StationNumber == numberOfStation)
                        {
                            flag = true;
                        }
                    }
                }
            }
            return flag;
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
    }
}



