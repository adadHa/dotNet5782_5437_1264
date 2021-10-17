using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WheightCategories Wheight { get; set; }
            public Priorities Priority { get; set; }
            public int DroneId { get; set; }
            public DateTime Requested { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }

            public Parcel(int id, int senderId, int targetId, WheightCategories wheight, Priorities priority, int droneId, DateTime requested, DateTime scheduled, DateTime pickedUp, DateTime delivered)
            {
                Id = id;
                SenderId = senderId;
                TargetId = targetId;
                Wheight = wheight;
                Priority = priority;
                DroneId = droneId;
                Requested = requested;
                Scheduled = scheduled;
                PickedUp = pickedUp;
                Delivered = delivered;
            }

            public override string ToString()
            {
                return $"Id: {Id}, SenderId: {SenderId}, TargetId: {TargetId}, Wheight: {Wheight}, Priority: {Priority}, Requested: {Requested}, DroneId: {DroneId}, Scheduled: {Scheduled}, PickedUp: {PickedUp}, Delivered: {Delivered}";
            }
        }
    }
}

