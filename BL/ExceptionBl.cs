using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class BOExceptionBus : Exception
    {
        public string messge2;
        public BOExceptionBus(string messge, Exception innerException) : base(messge, innerException)
        {
            messge2 = ((DO.ExceptionBus)innerException).messge2;
            //messge2 = messge1;
        }
        public override string ToString()
        {
            return base.ToString() + $", Error: {messge2}";
        }
    }

    [Serializable]
    public class BOExceptionStation : Exception
    {
        public int Id;
        public BOExceptionStation(string messge, Exception innerException) : base(messge, innerException)
        {
            Id = ((DO.ExceptionStation)innerException).Id;
        }
        public override string ToString()
        {
            return base.ToString() + $", Error: {Id} ";
        }
    }

    [Serializable]
    public class BOExceptionLine : Exception
    {
        public int Id;
        public BOExceptionLine(string messge, Exception innerException) : base(messge, innerException)
        {
            Id = ((DO.ExceptionLine)innerException).Id;
        }
        public override string ToString()
        {
            return base.ToString() + $", Error: {Id}";
        }
    }
    [Serializable]
    public class BOExceptionLineStation : Exception
    {
        public int Id;
        public int Id1;
        public BOExceptionLineStation(string messge, Exception innerException) : base(messge, innerException)
        {
            Id = ((DO.ExceptionLineStation)innerException).Id;
            Id1 = ((DO.ExceptionLineStation)innerException).Id1;
        }
        public override string ToString()
        {
            return base.ToString() + $", Error: {Id} And {Id1}";
        }
    }
    [Serializable]
    public class BOExceptionLineExit : Exception
    {
        public int Id;
        public TimeSpan Id1;
        public BOExceptionLineExit(string messge, Exception innerException) : base(messge, innerException)
        {
            Id = ((DO.ExceptionLineExit)innerException).Id;
            Id1 = ((DO.ExceptionLineExit)innerException).Id1;
        }
        public override string ToString()
        {
            return base.ToString() + $", Error";
        }
    }
    public class BOExceptionUser : Exception
    {
        public string Id;
        public BOExceptionUser(string messge, Exception innerException) : base(messge, innerException)
        {
            Id = ((DO.ExceptionUser)innerException).Id;
        }
        public override string ToString()
        {
            return base.ToString() + $", Error: {Id}";
        }
    }

    public class BOExceptionConsecutiveStations : Exception
    {
        public int Id;
        public int Id1;
        public BOExceptionConsecutiveStations(string messge, Exception innerException) : base(messge, innerException)
        {
            Id = ((DO.ExceptionConsecutiveStations)innerException).Id;
            Id1 = ((DO.ExceptionConsecutiveStations)innerException).Id1;
        }
        public override string ToString()
        {
            return base.ToString() + $", Error";
        }
    }
}
