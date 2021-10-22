using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalObject
{
    public class DalObject
    {
        static DalObject()
        {
            DataSource.Config.Initialize();
        }

        public void AddStation(int id, string name, int num, double longitude, double latitude)
        {
            IDAL.DO.Station station = new IDAL.DO.Station
            {
                Id = id,
                Name = name,
                ChargeSlots = num,
                Longitude = longitude,
                Latitude = latitude
            };

            DataSource.Stations[DataSource.Config.StationsIndex] = station;
            DataSource.Config.StationsIndex++;
        }

        public void AddDrone(int id, string model, string weight, int batteryStatus, string droneStatus)
        {
            IDAL.DO.Drone drone = new IDAL.DO.Drone
            {
                Id = id,
                Model = model,
                MaxWeight = (IDAL.DO.WheightCategories)Enum.Parse(typeof(IDAL.DO.WheightCategories), weight),
                Battery = batteryStatus,
                Status = (IDAL.DO.DroneStatuses)Enum.Parse(typeof(IDAL.DO.DroneStatuses), droneStatus)
            };

            DataSource.Drones[DataSource.Config.DronesIndex] = drone;
            DataSource.Config.DronesIndex++;
        }

        public void AddCustomer(int id, string name, string phoneNumber, double longitude, double latitude)
        {
            IDAL.DO.Customer customer = new IDAL.DO.Customer
            {
                Id = id,
                Name = name,
                Phone = phoneNumber,
                Longitude = longitude,
                Latitude = latitude
            };

            DataSource.Customers[DataSource.Config.CustomersIndex] = customer;
            DataSource.Config.CustomersIndex++;
        }

        public void AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority, int responsibleDrone)
        {
            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel
            {
                Id = DataSource.ParcelsIndex,
                SenderId = customerSenderId,
                TargetId = customerReceiverId,
                Wheight = (IDAL.DO.WheightCategories)Enum.Parse(typeof(IDAL.DO.WheightCategories), weight),
                Priority = (IDAL.DO.Priorities)Enum.Parse(typeof(IDAL.DO.Priorities), priority),
                DroneId = responsibleDrone
            };

            DataSource.Parcels[DataSource.Config.ParcelsIndex] = parcel;
            DataSource.Config.ParcelsIndex++;

            return DataSource.Config.ParcelsIndex;
        }

        public void BindParcel(int parcelId, int droneId)
        {
            DataSource.Drones[droneId].Status = shipping;
            DataSource.Parcels[parcelId].DroneId = droneId;
            DataSource.Parcels[parcelId].Scheduled = new DateTime();
        }

        public void CollectParcelByDrone()
        {

        }

        public void SupplyParcelToCustomer()
        {
            
        }

        public void ChargeDrone()
        {
            
        }

        public void StopCharging()
        {
            
        }

        public IDAL.DO.Station[] ViewStationsList()
        {
            IDAL.DO.Station[] resultList = new IDAL.DO.Station[DataSource.StationsIndex];
            for(int i=0; i < StationsIndex); i++)
            {
                resultList[i] = new IDAL.DO.Station();
                resultList[i] = DataSource.Stations[DataSource.Config.StationsIndex];
            }
            return resultList;
        }

        public IDAL.DO.Drone[] ViewDronesList()
        {
            
        }

        public IDAL.DO.Customer[] ViewCustomersList()
        {
            
        }

        public IDAL.DO.Parcel[] ViewParcelsList()
        {
            
        }
        
        public IDAL.DO.Parcel[] ViewUnbindParcels()
        {
            
        }

        public IDAL.DO.Parcel[] ViewStationsWithFreeChargeSlots()
        {
            
        }

        public IDAL.DO.Station ViewStation(int index)
        {
            return DataSource.Stations[DataSource.Config.index];
        }

        public IDAL.DO.Drone ViewDrone(int index)
        {
            return DataSource.Drones[DataSource.Config.index]; 
        }

        public IDAL.DO.Customer ViewCustomer(int index)
        {
            return DataSource.Customers[DataSource.Config.index];
        }

        public IDAL.DO.parcel ViewParcel(int index)
        {
            return DataSource.Parcels[DataSource.Config.index];
        }
    }
}