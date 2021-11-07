using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class DeliveryInTransfer
        {
            public int Id { get; set; }
            public WheightCategories MaxWeight { get; set; }
            public Priorities Priority { get; set; }
            public double TransportDistance { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Max Weight: {MaxWeight}, Priority: {Priority}, Transport Distance: {TransportDistance}";
            }
        } 
    }
}
