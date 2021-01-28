using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex
{
    public delegate void MyAction(int x);
    public class Lsd
    {
        public event MyAction Func;
        public static void m(int x)
        {
            x += 5 / 400;
        }

        public void Add(MyAction action)
        {
            Func += action;
        }

    }
}
//public event MyAction ValueChanged
//{
//    add
//    {
//        Console.WriteLine("register");
//        Func += value;
//    }
//    remove
//    {
//        Console.WriteLine("un-register");
//        Func -= value;
//    }
//}