using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WheightCategories MaxWeight { get; set; }
        public DroneStatuses Status { get; set; }
        public double Battery { get; set; }
        public Location Location { get; set; }
        public ParcelInTransfer TransferedParcel { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}\n" +
                   $"Model: {Model}\n" +
                   $"Max Weight: {MaxWeight}\n" +
                   $"Status: {Status}\n" +
                   $"Battery: {Battery}%\n" +
                   $"Location:{Location}\n" +
                   $"Parcel In Transfer: {TransferedParcel.ToString()}";
        }
    } 
}

