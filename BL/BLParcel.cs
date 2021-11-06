using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL
    {
        public void AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority)
        {
            dalObject.AddParcel(customerSenderId, customerReceiverId, weight, priority, 0);
        }
    }
}
