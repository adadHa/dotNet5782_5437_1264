using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Parcel
        {
            public int Id { get; set; }
            public CustomerInDelivery Sender { get; set; }
            public CustomerInDelivery Target { get; set; }
            public WheightCategories Wheight { get; set; }
            public Priorities Priority { get; set; }
            public DroneInParcel Drone { get; set; }
            public DateTime? Created { get; set; }
            public DateTime? Scheduled { get; set; }
            public DateTime? PickedUp { get; set; }
            public DateTime? Delivered { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}\n" +
                    $"Sender: {Sender.ToString()}\n" +
                    $"Target: {Target.ToString()}\n" +
                    $"Wheight: {Wheight}\n" +
                    $"Priority: {Priority}\n" +
                    $"Drone: {Drone.ToString()}\n" +
                    $"Created: {Created}\n" +
                    $"Scheduled: {Scheduled}\n" +
                    $"Picked Up: {PickedUp}\n" +
                    $"Delivered: {Delivered}";
            }
        } 
    }
}
