using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class BaseStation
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public int ChargeSlots { get; set; }
            public List<BaseStation> ListOfDronesInCharge { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Name: {Name}, Location: {Location}, Charge Slots: {ChargeSlots}, List Of Drones In Charge: {ListOfDronesInCharge}";
            }
        } 
    }
}
