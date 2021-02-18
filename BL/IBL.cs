using System;
using System.Collections.Generic;
using BO;

namespace BLAPI
{
    public interface IBL1
    {
        #region LineTrip
        /// <summary>
        /// A function that receives a number of lines and two stops from the route and returns the travel time between them.
        /// The function is activated by a process in pl.
        /// </summary>
        /// <param name="LineNumber"></param>
        /// <param name="NumberStation1"></param>
        /// <param name="NumberStation2"></param>
        /// <returns></returns>
        TimeSpan TravelTimeBetweenTwoStations(int LineNumber, int NumberStation1, int NumberStation2);

        /// <summary>
        /// Function that receives number of station and time and returns with a process of arrival times
        /// updated station of the lines on the way to the station
        /// only those who have already left the departure station and have not yet reached the above station.
        /// In addition returns for each line the departure time and arrival time. (Everything is of course updated).
        /// </summary>
        /// <param name="NumberStation"></param>
        /// <param name="Time"></param>
        /// <param name="numberStationDestination"></param>
        /// <returns></returns>
        IEnumerable<LineTrip> LineTrips(int NumberStation, TimeSpan Time, int numberStationDestination = 0);

        /// <summary>
        /// A function that receives a line number and a station number, 
        /// and returns the arrival time of all the lines that left and on the way to the station.
        /// </summary>
        IEnumerable<string> TimeCamingToCurrnetStation(int LineNumber, int NumberStation);
        #endregion LineTrip

        #region BusLineStation
        /// <summary>
        /// A function that receives a number and returns the line information and includes 
        /// its list of stations arranged + times and distances from station to station + station names + the schedule of the line is divided according to frequencies.
        /// </summary>
        /// <param name="numberLine" = The line number to display its details.></param>
        /// <returns></returns>
        BusLineBO LineInformation(int numberLine);

        /// <summary>
        /// A function that adds a station to a line by location (excluding first and last station). 
        /// And includes in the addition the addition of two objects of successive stations of the stations located before and after it in the trajectory and following it.
        /// </summary>
        /// <param name="stationLineBO"></param>
        /// <param name="NumberStation1"></param>
        /// <param name="NumberStation2"></param>
        /// <param name="timeSpan"></param>
        /// <param name="distance"></param>
        void AddStationToLine(StationLineBO stationLineBO, int NumberStation1, int NumberStation2, TimeSpan timeSpan, float distance);

        /// <summary>
        /// A function that deletes a station from a line. After deletion arranges the location of the stations in the appropriate order in DL.
        /// In addition, it adds another object of the station's successive stations before and after it, 
        /// and returns the above object to the user for display to the user.
        /// </summary>
        /// <param name="stationLineBO"></param>
        /// <returns></returns>
        ConsecutiveStationsBo DeleteStationFromLine(int IdLine, int NumberStation);

        /// <summary>
        /// Returns a single station of a particular line.
        /// </summary>
        /// <param name="LineNumber"></param>
        /// <param name="numberStation"></param>
        /// <returns></returns>
        StationLineBO ReturnStationLine(int LineNumber, int numberStation);

        /// <summary>
        /// Returns a list of stations of a particular line 
        /// including information about times and distances and the names of the stations.
        /// </summary>
        /// <param name="LineNumber"></param>
        /// <returns></returns>
        IEnumerable<StationLineBO> ReturnLineStationList(int LineNumber);

        #endregion BusLineStation

        #region BusLine
        /// <summary>
        /// Adding a line that includes the details of the line and the first and last station of the line (then you can of course add more stations to the line).
        /// And additionally includes adding an object of consecutive stations.
        /// </summary>
        /// <param name="busLineBO"></param>
        void AddBusLineBO(BusLineBO busLineBO);

        /// <summary>
        /// Deletion of a line that includes deletion of line details + line stations + line exits.
        /// </summary>
        /// <param name="NumberLine"></param>
        void DeleteBusLineBO(int NumberLine);

        /// <summary>
        /// Update line information.
        /// </summary>
        /// <param name="busLineBO"></param>
        void UdaptingLine(BusLineBO busLineBO);

        int ReturnBusLineIdFromDl();
        #endregion BusLine

        #region Station
        void AddStationToDo(BusStationBO busStationBO);
        void DeleteStationFromDo(int numberOfStation);
        BusStationBO ReturnStationToPL(int numberOfStation);
        IEnumerable<BusStationBO> ShowStation();

        /// <summary>
        /// A function that receives a station number and returns information about the lines passing through the station.
        /// </summary>
        /// <param name="NumberStation"></param>
        /// <returns></returns>
        IEnumerable<BusLineBO> LinePastInStationBOs(int NumberStation);
        #endregion Station

        #region LineExit
        void AddExitToLine(LineExitBo lineExitBo);
        void DeleteLineExit(int numberOfLine, TimeSpan StartTime);
        LineExitBo GetLineExit(int numberOfLine, TimeSpan StartTime);
        IEnumerable<LineExitBo> ShowlineExit(int numberLine);
        #endregion LineExit

        #region ConsecutiveStationsBo
        IEnumerable<ConsecutiveStationsBo> ShowConsecutiveStationsBo();
        ConsecutiveStationsBo ShowOneConsecutiveStations(int number1, int number2);
        void Udapting(ConsecutiveStationsBo consecutiveStationsBo);
        #endregion  ConsecutiveStationsBo

        #region BusBo
        /// <summary>
        /// Crud principle on a physical bus object that includes data integrity and calculations.
        /// </summary>
        /// <param name="bus"></param>
        void AddBusBO(BusBO bus);
        void DeleteBusBO(string License_number);
        BusBO ShowBus(string License_number);
        IEnumerable<BusBO> ShowAllBus();
        void ChackBus(BusBO bus);

        #endregion Bus

        #region Useres
        IEnumerable<UserBo> GetListUsers();
        void AddUserToDo(UserBo userBo);
        void DeleteUserFromDo(string Username);
        void UdptingUser(UserBo userBo);
        bool FindUser(string pass, string UserNam);
        UserBo GetUser(string UserName);
        #endregion Useres
    }
}
