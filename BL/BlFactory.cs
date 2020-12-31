using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL;
using BLAPI;

namespace BLAPI
{
    public static class BLFactory
    {
        public static IBL1 GetBL(string type)
        {
            switch (type)
            {
                case "1":
                    return new InformationBusLineBl();
                case "2":
                default:
                    return new InformationBusLineBl();
            }
        }
    }
}
