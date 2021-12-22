using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;
namespace DalApi
{
    public interface IDal
    {
        public void AddStation(int id, string name, int num, double longitude, double latitude);
        public void AddDrone(int id, string model, string weight);
        public void AddCustomer(int id, string name, string phoneNumber, double longitude, double latitude);
        public void AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority, int responsibleDrone);
        public void BindParcel(int parcelId, int droneId);
        public void CollectParcelByDrone(int droneId, int parcelId);
        public void SupplyParcelToCustomer(int parcelId);
        public void ChargeDrone(int droneId, int stationId);
        public void StopCharging(int droneId/*, int chargingTime*/);
        public void UpdateDrone(int droneId, string newModel);
        public void UpdateStationName(int id, string newName);
        public void UpdateStationChargeSlotsCap(int id, int newNum);
        public void UpdateCustomerName(int id, string newName);
        public void UpdateCustomrePhoneNumber(int id, string newPhoneNumber);
        public IEnumerable<DO.Station> ViewStationsList();
        public IEnumerable<DO.Drone> ViewDronesList();
        public IEnumerable<DO.Customer> ViewCustomersList();
        public IEnumerable<DO.Parcel> ViewParcelsList();
        public IEnumerable<DO.Parcel> ViewUnbindParcels();
        public IEnumerable<DO.Station> ViewStationsWithFreeChargeSlots();
        public string ViewStation(int id);
        public string ViewDrone(int id);
        public string ViewCustomer(int id);
        public string ViewParcel(int id);
        public DO.Station GetStation(int id);
        public DO.Drone GetDrone(int id);
        public DO.Customer GetCustomer(int id);
        public DO.Parcel GetParcel(int id);
        public IEnumerable<DO.Station> GetStations(Func<DO.Station, bool> filter = null);
        public IEnumerable<DO.Drone> GetDrones(Func<DO.Drone, bool> filter = null);
        public IEnumerable<DO.Customer> GetCustomers(Func<DO.Customer, bool> filter = null);
        public IEnumerable<DO.Parcel> GetParcels(Func<DO.Parcel, bool> filter = null);
        public IEnumerable<DO.DroneCharge> GetDroneCharges(Func<DO.DroneCharge, bool> filter = null);
        public double[] ViewElectConsumptionData();




















    }
}
