using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalObject;
namespace IDAL
{
    public interface IDal
    {
        public void AddStation(int id, string name, int num, double longitude, double latitude);
        public void AddDrone(int id, string model, string weight);
        public void AddCustomer(int id, string name, string phoneNumber, double longitude, double latitude);
        public void AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority, int responsibleDrone);
        public void BindParcel(int parcelId, int droneId);
        public void CollectParcelByDrone(int parcelId);
        public void SupplyParcelToCustomer(int parcelId);
        public void ChargeDrone(int droneId, int stationId);
        public void StopCharging(int droneId/*, int chargingTime*/);
        public void UpdateDrone(int droneId, string newModel);
        public void UpdateStationName(int id, string newName);
        public void UpdateStationChargeSlotsCap(int id, int newNum);
        public void UpdateCustomerName(int id, string newName);
        public void UpdateCustomrePhoneNumber(int id, string newPhoneNumber);
        public IEnumerable<IDAL.DO.Station> ViewStationsList();
        public IEnumerable<IDAL.DO.Drone> ViewDronesList();
        public IEnumerable<IDAL.DO.Customer> ViewCustomersList();
        public IEnumerable<IDAL.DO.Parcel> ViewParcelsList();
        public IEnumerable<IDAL.DO.Parcel> ViewUnbindParcels();
        public IEnumerable<IDAL.DO.Station> ViewStationsWithFreeChargeSlots();
        public string ViewStation(int id);
        public string ViewDrone(int id);
        public string ViewCustomer(int id);
        public string ViewParcel(int id);
        public IDAL.DO.Station GetStation(int id);
        public IDAL.DO.Drone GetDrone(int id);
        public IDAL.DO.Customer GetCustomer(int id);
        public IDAL.DO.Parcel GetParcel(int id);
        public IEnumerable<IDAL.DO.Station> GetStations(Func<IDAL.DO.Station, bool> filter = null);
        public IEnumerable<IDAL.DO.Drone> GetDrones(Func<IDAL.DO.Drone, bool> filter = null);
        public IEnumerable<IDAL.DO.Customer> GetCustomers(Func<IDAL.DO.Customer, bool> filter = null);
        public IEnumerable<IDAL.DO.Parcel> GetParcels(Func<IDAL.DO.Parcel, bool> filter = null);
        public IEnumerable<IDAL.DO.DroneCharge> GetDroneCharges(Func<IDAL.DO.DroneCharge, bool> filter = null);
        public double[] ViewElectConsumptionData();





















    }
}
