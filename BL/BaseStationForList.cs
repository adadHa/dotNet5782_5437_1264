using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class BaseStationForList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int AvailableChargingStations { get; set; }
            public int OccupiedChargingStations { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Name: {Name}, Available Charging Stations: {AvailableChargingStations}, Occupied Charging Stations: {OccupiedChargingStations}";
            }
        } 
    }
}
