﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class DroneInParcel
    {
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
        public Location CurrentlyLocation { get; set; }
    }
}
