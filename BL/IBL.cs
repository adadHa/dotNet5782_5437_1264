using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface IBL
    {
        public void AddStation(int id, string name, int freeChargingSlots, BO.Location location);
        public void AddDrone(int id, string model, string weight, int initialStation);
        public void AddCustomer(int id, string name, string phoneNumber, BO.Location location);
        public void AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority);


        public void UpdateDrone(int id, string newModel);
        public void UpdateStation(int id, string newName, int newNum);
        public void UpdateCustomer(int id, string newName, string newPhoneNumber);
        public void ChargeDrone(int id);
        public void ReleaseDroneFromCharging(int id, double chargingTime);
        public void BindDrone(int id);
        public void CollectParcelByDrone(int id);
        public void SupplyParcel(int id);

        public string ViewStation(int id);
        public string ViewDrone(int id);
        public string ViewCustomer(int id);
        public string ViewParcel(int id);

        public Parcel GetParcel(int id);

        public IEnumerable<StationForList> GetStations(Func<DO.Station, bool> filter = null);
        public IEnumerable<DroneForList> GetDrones(Func<DroneForList, bool> filter = null);
        public IEnumerable<CustomerForList> GetCustomers(Func<DO.Customer, bool> filter = null);
        public IEnumerable<ParcelForList> GetParcels(Func<DO.Parcel, bool> filter = null);


        public string ViewStationsList();
        public string ViewDronesList();
        public string ViewCustomersList();
        public string ViewParcelsList();
        public string ViewUnbinedParcelsList();
        public string ViewStationsWithFreeChargeSlots();

    }
}
