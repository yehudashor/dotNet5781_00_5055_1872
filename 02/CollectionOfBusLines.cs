using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
//using dotNet_02_5055_1872;

namespace dotNet_02_5055_1872
{

    public class CollectionOfBusLines : BusLine, IEnumerable
    {
        /// <summary>
        /// List of bus line objects
        /// </summary>
        protected List<BusLine> collectionOfLines = new List<BusLine>();

        /// <summary>
        /// proprty to list
        /// </summary>
        public List<BusLine> CollectionOfLines1
        {
            get => collectionOfLines;
            set => collectionOfLines = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public CollectionOfBusLines() { }

        /// <summary>
        /// Inserting a line into the collection: The proprty receives an object of type bus line type and checks whether it already appears in the list.
        /// The assumption is if such a line no longer exists in the list then add it.
        /// If there is such a line number then if it appears once then this line it represents the return line of the above line and the stations of the first and last line are opposite to it so insert it.
        /// If there is more than twice the line then drop an exception.
        /// </summary>
        public BusLine CollectionOfLines
        {
            set
            {
                int count = 0, sum = 0;
                BusLine Temp = null;
                foreach (BusLine item in collectionOfLines)
                {
                    if (value.LineNumber != item.LineNumber)
                    {
                        count++;
                    }

                    else
                    {
                        Temp = item;
                        sum++;
                    }
                }

                if (count == collectionOfLines.Count)
                {
                    collectionOfLines.Add(value);
                }

                else
                {
                    if (sum == 1)
                    {
                        value.FirstStation = Temp.LastStation;
                        value.LastStation = Temp.FirstStation;
                        collectionOfLines.Add(value);
                    }

                    if (sum > 1)
                    {
                        throw new ArgumentException("There is already one such line in the system, insert another line");
                    }
                }
            }
        }

        /// <summary>
        /// Gets a line number and deletes its occurrence if the line exists Drop an exception.
        /// </summary>
        /// <param name="NumberOfLine"></param>
        public void DeleteLineFromRoute(int NumberOfLine)
        {
            BusLine l = null;
            foreach (BusLine item in collectionOfLines)
            {
                if (item.LineNumber == NumberOfLine)
                {
                    l = item;
                }
            }

            _ = l == null ? throw new FormatException("The line is not on the list") : collectionOfLines.Remove(l);
        }

        /// <summary>
        /// A method that receives a station number and returns the list of line numbers passing through it.
        /// </summary>
        /// <param name="numberOfStation"></param>
        /// <returns></returns>
        public List<int> StationLines(int numberOfStation)
        {
            List<int> temp = new List<int>();
            foreach (BusLine item in collectionOfLines)
            {
                foreach (BusLineStation item1 in item.RouteTheLine)
                {
                    if (item1.StationNumber == numberOfStation)
                    {
                        temp.Add(item.LineNumber);
                    }
                }
            }
            return temp.Count == 0 ? throw new FormatException("Error!!! There are no lines at this station at all") : temp;
        }

        /// <summary>
        /// indexer that gets a line number and returns its instance if there is no exception throws.
        /// </summary>
        /// <param name="numberOfline"></param>
        /// <returns></returns>
        public BusLine this[int NumberOfline]
        {
            get
            {
                int index = 0;
                bool flag = false;
                for (int i = 0; i < collectionOfLines.Count; i++)
                {
                    if (collectionOfLines[i].LineNumber == NumberOfline)
                    {
                        index = i;
                        flag = true;
                    }
                }
                return !flag ? throw new ArgumentException("The line is not in the system !!!") : collectionOfLines[index];
            }
        }

        /// <summary>
        /// Ienumerable interface Which makes the department a shareholder.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return collectionOfLines.GetEnumerator();
        }

        /// <summary>
        /// A method that counts the total travel times in lines according to the one we implemented the Icomparable interface.
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public List<BusLine> SortTravelTimesOnLines()
        {
            CollectionOfLines1.Sort();
            return CollectionOfLines1;
        }
    }
}
