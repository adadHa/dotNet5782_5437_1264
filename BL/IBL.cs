using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public interface IBL
    {
        public void AddStation(int id, string name, int freeChargingSlots, BO.Location location);
        public void AddDrone(int id, string model, string weight, int initialStation);
        public void AddCustomer(int id, string name, string phoneNumber);
        public void AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority);
        public void UpdateDrone(int id, string newModel);
        public void UpdateStation(int id, string newName, int newNum);
        public void UpdateCustomer(int id, string newName, string newPhoneNumber);
        public void ChargeDrone(int id);
        public void ReleaseDroneFromCharging(int id, double chargingTime);
        public BO.Drone ViewDrone(int id);
    }
}
