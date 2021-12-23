using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    class DalFactory
    {
        public static IDal GetDal(string dataFormat)
        {
            switch (dataFormat)
            {
                case "DalObject":
                    return null;
                case "DalXml":
                    return null;//
                default:
                    return null;
            }
        }
    }
}
