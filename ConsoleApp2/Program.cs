using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    //class x
    //{
    //    public int MyProperty { get; set; }
    //}
    //public interface I { int n { get; } }
    //public class my<T>where T : I
    //{
    //    List<T> ts = new List<T>();
    //    public bool Add(T v)
    //    {
    //        foreach (T item in ts)
    //        {
    //            if (item.)
    //            {

    //            }
    //        }
    //    }
    //}
    public static class ListTools
    {
        public static IEnumerable<string> PrintByPredicate<T>(this IEnumerable<T> col, Func<T, bool> pred, Func<T, string> func)
        {
            return from t in col
                   where pred(t)
                   select t.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<int> ts = new List<int>() { 1, 2, 3, 4 };
            foreach (string item in ts.PrintByPredicate(x => x % 2 == 0, t => t + "num"))
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
