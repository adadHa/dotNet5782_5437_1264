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
        public void AddDrone(int id, string model, string weight, double batteryStatus, string droneStatus);
        public void AddCustomer(int id, string name, string phoneNumber, double longitude, double latitude);
        public void AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority, int responsibleDrone);
        public void BindParcel(int parcelId, int droneId);
        public void CollectParcelByDrone(int parcelId);
        public void SupplyParcelToCustomer(int parcelId);
        public void ChargeDrone(int droneId, int stationId);
        public void StopCharging(int droneId/*, int chargingTime*/);
        public IEnumerable<IDAL.DO.Station> ViewStationsList();
        public IEnumerable<IDAL.DO.Drone> ViewDronesList();
        public IEnumerable<IDAL.DO.Customer> ViewCustomersList();
        public IEnumerable<IDAL.DO.Parcel> ViewParcelsList();
        public IEnumerable<IDAL.DO.Parcel> ViewUnbindParcels();
        public IEnumerable<IDAL.DO.Station> ViewStationsWithFreeChargeSlots();
        public IDAL.DO.Station ViewStation(int id);
        public IDAL.DO.Drone ViewDrone(int id);
        public IDAL.DO.Customer ViewCustomer(int id);
        public IDAL.DO.Parcel ViewParcel(int id);
        public double[] ViewElectConsumptionData();





















    }
}
