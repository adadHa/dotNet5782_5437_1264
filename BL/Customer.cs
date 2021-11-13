using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public Location Location { get; set; }
            public List<ParcelInTransfer> DeliveryListFromCustomer { get; set; }
            public List<ParcelInTransfer> DeliveryListToCustomer { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Name: {Name}, Phone: {Phone}, Location: {Location}, Delivery List From Customer: {DeliveryListFromCustomer}, Delivery List To Customer:{DeliveryListToCustomer}";
            }
        }
    }
}
