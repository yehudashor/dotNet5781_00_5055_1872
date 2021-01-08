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
    public class ExceptionDl : Exception
    {
        public ExceptionDl(string messge) : base(messge) { }
    }
}
