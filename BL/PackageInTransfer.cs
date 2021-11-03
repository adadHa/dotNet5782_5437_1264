using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class PackageInTransfer
    {
        public int Id { get; set; }
        public Priorities Priority { get; set; }
        public CustomerInDelivery Sender { get; set; }
        public CustomerInDelivery Receiver { get; set; }
    }
}
