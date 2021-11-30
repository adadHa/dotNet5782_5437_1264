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
            public DateTime? Created { get; set; }
            public DateTime? Scheduled { get; set; }
            public DateTime? PickedUp { get; set; }
            public DateTime? Delivered { get; set; }

            public override string ToString()
            {
                return $"Id: {Id}, Sender Id: {SenderId}, Target Id: {TargetId}, Wheight: {Wheight}, Priority: {Priority}, Created: {Created}, DroneId: {DroneId}, Scheduled: {Scheduled}, Picked Up: {PickedUp}, Delivered: {Delivered}";
            }
        }
    }
}

