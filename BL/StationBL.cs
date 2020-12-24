using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BL
{
    internal class StationBL : BusStationBO
    {
        private static readonly Random line = new Random(DateTime.Now.Millisecond);
        public StationBL() { }
        public StationBL(int stationNumber, string nameOfStation, bool accessForDisabled, bool roofToTheStation, bool isAvailable3, string stationAddress = "")
        {
            StationNumber = stationNumber;
            NameOfStation = nameOfStation;
            StationAddress = stationAddress;
            Latitude = (float)((float)(line.NextDouble() * (33.3 - 31)) + 31);
            Longitude = (float)((float)(line.NextDouble() * (35.5 - 34.3)) + 34.3);
            AccessForDisabled = accessForDisabled;
            RoofToTheStation = roofToTheStation;
            IsAvailable3 = isAvailable3;
        }
        public int StationNumber1
        {
            get => StationNumber;

            set => StationNumber = (value < 1000000 && value > 0) ? value : throw new ArgumentException("Incorrect station number: ");
        }

    }
}
