using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_01_5055_1872
{
    public class Program
    {
        /// <summary>
        /// The main menu for selecting the options according to the exercise
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
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
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("rong number!!! enter again:");
            }

            Myenum choice = (Myenum)number;
            while (choice != Myenum.Exit)
            {
                switch (choice)
                {
                    case Myenum.EnterNewBus:
                        Console.WriteLine("Please introduce a new bus to the company: ");
                        Addbus(ref buses);
                        break;
                    case Myenum.BusSelection:
                        Console.WriteLine("Choose a bus to travel: ");
                        Busselection(buses);
                        break;
                    case Myenum.RefuelingOrHandling:
                        Console.WriteLine("Treatment or refueling: ");
                        RefuelOrTreat(buses);
                        break;
                    case Myenum.TravelPresentation:
                        Console.WriteLine("View the license number and passenger for all buses in the company: ");
                        foreach (Bus item in buses)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case Myenum.Exit:
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

                choice = (Myenum)number;
                Console.WriteLine();
            }
        }

        /// <summary>
        /// A function that adds a new bus to the company updates the required fields and puts it on the list
        /// </summary>
        /// <param name="buses = list"></param>
        public static void Addbus(ref List<Bus> buses)
        {
            Console.WriteLine("Please enter a license number and start date of activity: ");
            string numberOfbus = Console.ReadLine();
            DateTime detaForBus = DateTime.Parse(Console.ReadLine());
            Bus bus = new Bus
            {
                StartDate = detaForBus,
                License_number = numberOfbus
            };
            buses.Add(bus);
        }

        /// <summary>
        /// Function of issuing a new trip. 
        /// Which includes checking the feasibility of the trip and printing an error in the impossibility and updating the appropriate fields
        /// </summary>
        /// <param name="buses"></param>
        public static void Busselection(List<Bus> buses)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            Console.WriteLine("license number: ");
            string str = Console.ReadLine();
            bool chack = false;
            foreach (Bus item in buses)
            {
                if (item.Nicense_number == str)
                {
                    int number = r.Next(1200);
                    item.KmForRefueling = number;
                    item.KmForTreatment = number;
                    item.TotalMiles = number;
                    Bus.kmForAllBuses = number;
                    chack = true;
                }
            }
            if (!chack)
            {
                Console.WriteLine("The bus is not in the system");
            }
        }

        /// <summary>
        /// Field 3 in the main menu that asks for treatment or refueling and performs according to the user's choice
        /// </summary>
        /// <param name="buses"></param>
        public static void RefuelOrTreat(List<Bus> buses)
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
                    {
                        Console.WriteLine("rong number!!! enter again ");
                    }

                    if (number == 1)
                    {
                        item.Refueling();
                    }

                    if (number == 2)
                    {
                        item.Treatment();
                    }

                    chack = true;
                }
            }
            if (!chack)
            {
                Console.WriteLine("The bus is not in the system");
            }
        }
    }
}
