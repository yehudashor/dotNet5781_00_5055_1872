using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_5055_1872
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome5055();
            Welcome1872();
            Console.ReadKey();
        }
        static partial void Welcome1872();
        private static void Welcome5055()
        {

            Console.WriteLine("Enter your name: ");
            string yehudamendy = Console.ReadLine();
            Console.WriteLine("{0} ", yehudamendy + " welcome to my first console application");
        }
    }
}
