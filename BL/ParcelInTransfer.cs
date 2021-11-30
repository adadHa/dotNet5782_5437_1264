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
            public bool IsOnWayOrWaiting { get; set; }
            public Priorities? Priority { get; set; }
            public WheightCategories? Wheight { get; set; }
            public CustomerInDelivery? Sender { get; set; }
            public CustomerInDelivery? Receiver { get; set; }
            public Location? PickUpLocation { get; set; }
            public Location? TargetLocation { get; set; }
            public override string ToString()
            {
                string parcelStatus = IsOnWayOrWaiting == true ? "parcel on the way to customer" : "parcel is wating to be picked up";
                if (Id != null)
                    return $"Id: {Id}\n" +
                        $"                    parcel status: {parcelStatus}\n" +
                        $"                    Priority: {Priority}\n" +
                        $"                    Wheight: {Wheight}\n" +
                        $"                    Sender: {Sender.ToString()}\n" +
                        $"                    Receiver: {Receiver.ToString()}\n" +
                        $"                    Pick-Up Location: {PickUpLocation}\n" +
                        $"                    Target Location: {TargetLocation}\n";
                else
                    return $"No transfered parcel";
            }
        }
    }
}
