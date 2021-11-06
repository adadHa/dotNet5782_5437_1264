using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    interface IBL
    {
        public void AddStation(int id, string name, int freeChargingSlots, Location location);
        public void AddDrone(int id, string model, string weight, int initialStation);
        public void AddCustomer(int id, string name, string phoneNumber);
        public void AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority);
    }
}
