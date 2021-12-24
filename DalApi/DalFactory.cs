using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public static class DalFactory
    {
        public static IDal GetDal(string dataFormat)
        {
            switch (dataFormat)
            {
                case "DalObject":
                    return DalO;
                case "DalXml":
                    return null;//
                default:
                    return null;
            }
        }
    }
}
