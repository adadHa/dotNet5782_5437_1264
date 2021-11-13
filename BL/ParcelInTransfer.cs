using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class ParcelInTransfer
        {
            public int Id { get; set; }
            public Priorities Priority { get; set; }
            public CustomerInDelivery Sender { get; set; }
            public CustomerInDelivery Receiver { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Priority: {Priority}, Sender: {Sender}, Receiver: {Receiver}";
            }
        } 
    }
}
