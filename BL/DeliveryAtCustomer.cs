using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class DeliveryAtCustomer
    {
        public int Id { get; set; }
        public WheightCategories Wheight { get; set; }
        public Priorities Priority { get; set; }
        public Statuses status { get; set; }
        public CustomerInDelivery Source { get; set; }
        public CustomerInDelivery Destination { get; set; }
    }
}
