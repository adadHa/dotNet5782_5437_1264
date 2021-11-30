using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelInTransfer
        {
            public int? Id { get; set; }
            public Priorities? Priority { get; set; }
            public CustomerInDelivery? Sender { get; set; }
            public CustomerInDelivery? Receiver { get; set; }
            public override string ToString()
            {
                if (Id != null)
                    return $"Id: {Id}\n" +
                        $"                    Priority: {Priority}\n" +
                        $"                    Sender: {Sender.ToString()}\n" +
                        $"                    Receiver: {Receiver.ToString()}";
                else
                    return $"No transfered parcel";
            }
        }
    }
}
