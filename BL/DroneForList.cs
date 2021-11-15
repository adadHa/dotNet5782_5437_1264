using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class DroneForList
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WheightCategories MaxWeight { get; set; }
            public double Battery { get; set; }
            public DroneStatuses Status { get; set; }
            public Location Location { get; set; }
            public int NumberPacelDelivered { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Model: {Model}, Max Weight: {MaxWeight}, Battery: {Battery}, Status: {Status}, Location: {Location}, Number Pacel Delivered:{NumberPacelDelivered}";
            }
        } 
    }
}
