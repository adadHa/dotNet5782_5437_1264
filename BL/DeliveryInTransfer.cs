using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class DeliveryInTransfer
    {
        public int Id { get; set; }
        public WheightCategories MaxWeight { get; set; }
        public Priorities Priority { get; set; }
        public double TransportDistance { get; set; }
    }
}
