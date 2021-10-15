﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        class Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WheightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }

            public override string ToString()
            {
                return $"Id: {Id}, Model: {Model}, MaxWeight: {MaxWeight}, Status: {Status}, Battery: {Battery}";
            }
        }
    }
}
