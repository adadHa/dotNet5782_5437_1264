using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void AddCustomer(int id, string name, string phoneNumber)
        {
            try
            {
                dalObject.AddCustomer(id, name, phoneNumber, 0, 0);

            }
            catch (Exception e)
            {
                throw ConvertIdalToBlException(e);
            }
        }
    }
}
