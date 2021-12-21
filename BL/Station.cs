using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int FreeChargeSlots { get; set; }
        public List<DroneInCharge> ListOfDronesInCharge { get; set; }
        public override string ToString()
        {
            string listOfDronesInCharge = "";
            foreach (DroneInCharge drone in ListOfDronesInCharge)
            {
                listOfDronesInCharge += "\n    " + drone.ToString();
            }

            if (listOfDronesInCharge == "") listOfDronesInCharge = "all charging slots are free";

            return $"Id: {Id}\n" +
                   $"Name: {Name}\n" +
                   $"Location: {Location}\n" +
                   $"Charge Slots: {FreeChargeSlots}\n" +
                   $"List Of Drones In Charge: {listOfDronesInCharge}";
        }
    } 
}