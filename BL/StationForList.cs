using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class StationForList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int AvailableChargingSlots { get; set; }
            public int OccupiedChargingSlots { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Name: {Name}, Available Charging Slots: {AvailableChargingSlots}, Occupied Charging Slots: {OccupiedChargingSlots}";
            }
        } 
    }
}
