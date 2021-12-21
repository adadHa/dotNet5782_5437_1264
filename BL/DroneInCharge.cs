using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInCharge
    {
        public int DroneId { get; set; }
        public double BatteryStatus { get; set; }
        public override string ToString()
        {
            return $"Drone Id: {DroneId}, Battery Status: {BatteryStatus}";
        }
    } 
}
