using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class BaseStation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int ChargeSlots { get; set; }
        public List ListOfDronesInCharge { get; set; }
    }
}
