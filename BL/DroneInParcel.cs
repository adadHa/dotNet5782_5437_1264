using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class DroneInParcel
        {
            public int Id { get; set; }
            public double BatteryStatus { get; set; }
            public Location CurrentlyLocation { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Battery Status: {BatteryStatus}, Currently Location: {CurrentlyLocation}";
            }
        } 
    }
}
