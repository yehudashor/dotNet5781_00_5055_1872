using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_01_5055_1872
{ 
enum myenum {EnterNewBus = 1 , BusSelection , RefuelingOrHandling, TravelPresentation , Exit = -1};
    class Program
    {   
        static void Main(string[] args)
        {
            List<Bus> buses = new List<Bus>();
            int number;
            Console.WriteLine(@" 
             enter 1 to EnterNewBus:
             enter 2 to BusSelection:
             enter 3 to  RefuelingOrHandling:
             enter 4 to TravelPresentation:
                enter -1 to Exit :");
            Console.WriteLine("enter a number: ");
            while (!int.TryParse(Console.ReadLine() , out number))
                Console.WriteLine("rong number!!! enter again:");
            myenum choice = (myenum)number;
            while (choice != myenum.Exit)
            {
                switch (choice)
                {
                    case myenum.EnterNewBus:
                        Console.WriteLine("Please introduce a new bus to the company: ");
                        addbus(ref buses);
                        break;
                    case myenum.BusSelection:
                        Console.WriteLine("Choose a bus to travel: ");
                        Busselection(buses);
                        break;
                    case myenum.RefuelingOrHandling:
                        Console.WriteLine("Treatment or refueling: ");
                        RefuelOrTreat(buses);
                        break;
                    case myenum.TravelPresentation:
                        Console.WriteLine("View the license number and passenger for all buses in the company: ");
                        foreach (Bus item in buses)
                            Console.WriteLine(item);
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
                    Console.WriteLine("rong number!!! enter again: ");
                choice = (myenum)number;
                Console.WriteLine();
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buses"></param>
        static void addbus(ref List<Bus> buses)
        {
            Console.WriteLine("Please enter a license number and start date of activity: ");
            string numberOfbus = Console.ReadLine();
            DateTime detaForBus = DateTime.Parse(Console.ReadLine());
            Bus bus = new Bus();
            bus.StartDate = detaForBus;
            bus.License_number = numberOfbus;
            buses.Add(bus);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buses"></param>
        /// <param name="numofbus"></param>
        /// <returns></returns>
        static bool findbus(List<Bus> buses, string numofbus)
        {
            bool flag = false;
            foreach (Bus item in buses)
            {
                if (item.Nicense_number == numofbus)
                    flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buses"></param>
        static void Busselection(List<Bus> buses)
        {
            int number = 0;
            //Random r = new Random();
            Random r = new Random(DateTime.Now.Millisecond);
            Console.WriteLine("license number: ");
            string str = Console.ReadLine();
            bool chack = false;
            foreach (Bus item in buses)
            {
                if (item.Nicense_number == str)
                {
                    number = r.Next(1200);
                    item.KmForRefueling = number;
                    item.KmForTreatment = number;
                    item.TotalMiles = number;
                    Bus.kmForAllBuses = number;
                        chack = true;
                }
            }
            if (!chack)
                Console.WriteLine("The bus is not in the system");          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buses"></param>
        static void RefuelOrTreat(List<Bus> buses)
        {
            int number;
            Console.WriteLine("license number: ");
            string numberofbus = Console.ReadLine();
            bool chack = false;
            foreach (Bus item in buses)
            {
                if (item.Nicense_number == numberofbus)
                {
                    Console.WriteLine("Choose 1 to refuel or 2 to treat: ");
                    while (!int.TryParse(Console.ReadLine(), out number))
                        Console.WriteLine("rong number!!! enter again ");
                    if (number == 1)
                        item.Refueling();
                    if (number == 2)
                        item.Treatment();
                    chack = true;
                }
            }
            if (!chack)
                Console.WriteLine("The bus is not in the system");
        }
    }
}
