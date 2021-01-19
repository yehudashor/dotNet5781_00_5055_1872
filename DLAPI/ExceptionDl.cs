using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ExceptionBus : Exception
    {
        public string messge2;
        public ExceptionBus(string messge, string messge1) : base(messge)
        {
            messge2 = messge1;
        }
        public override string ToString()
        {
            return base.ToString() + $", bad bus id: {messge2}";
        }
    }
    [Serializable]
    public class ExceptionStation : Exception
    {
        public int Id;
        public ExceptionStation(int id, string messge) : base(messge)
        {
            Id = id;
        }
        public override string ToString()
        {
            return base.ToString() + $", bad Station id: {Id} ";
        }
    }
    [Serializable]
    public class ExceptionLine : Exception
    {
        public int Id;
        public ExceptionLine(int id, string messge) : base(messge)
        {
            Id = id;
        }
        public override string ToString()
        {
            return base.ToString() + $", bad line id: {Id}";
        }
    }
    [Serializable]
    public class ExceptionLineStation : Exception
    {
        public int Id;
        public int Id1;
        public ExceptionLineStation(int id, int id1, string messge) : base(messge)
        {
            Id = id;
            Id1 = id1;
        }
        public override string ToString()
        {
            return base.ToString() + $", bad Line Station id: {Id} And {Id1}";
        }
    }
    [Serializable]
    public class ExceptionLineExit : Exception
    {
        public int Id;
        public TimeSpan Id1;
        public ExceptionLineExit(int id, TimeSpan id1, string messge) : base(messge)
        {
            Id = id;
            Id1 = id1;
        }
        public override string ToString()
        {
            return base.ToString() + $", bad Line Exit id: {Id} And {Id1}";
        }
    }
    [Serializable]
    public class ExceptionUser : Exception
    {
        public string Id;
        public ExceptionUser(string id, string messge) : base(messge)
        {
            Id = id;
        }
        public override string ToString() => base.ToString() + $", bad User id: {Id}";
        //{
            //return  + 
        //}
    }

    public class ExceptionConsecutiveStations : Exception
    {
        public int Id;
        public int Id1;
        public ExceptionConsecutiveStations(int id, int id1, string messge) : base(messge)
        {
            Id = id;
            Id1 = id1;
        }
        public override string ToString()
        {
            return base.ToString() + $", bad Consecutive Stations id: {Id} And {Id1}";
        }
    }


    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }
}
