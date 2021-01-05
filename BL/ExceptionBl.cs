using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ExceptionBl : Exception
    {
        public ExceptionBl(string messge, Exception exceptionInner) : base(messge, exceptionInner) { }
    }
}
