using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            DataSource.Config.Initialize();
        }

        //This function gets an Id an returns the index in the array of customers.
        private int CustomerIdToIndex(int id)
        {
            int index = 0;
            for (int i = 0; i < DataSource.Config.CustomersIndex; i++)
                if (DataSource.Customers[i].Id == id)
                     index = i;
            return index;
        }

        //This function gets an Id an returns the index in the array of drones.
        private int DroneIdToIndex(int id)
        {
            int index = 0;
            for (int i = 0; i < DataSource.Config.DronesIndex; i++)
                if (DataSource.Drones[i].Id == id)
                    index = i;
            return index;
        }

        //This function gets an Id an returns the index in the array of parcels.
        private int ParcelIdToIndex(int id)
        {
            int index = 0;
            for (int i = 0; i < DataSource.Config.ParcelsIndex; i++)
                if (DataSource.Parcels[i].Id == id)
                    index = i;
            return index;
        }

        //This function gets an Id an returns the index in the array of stations.
        private int StationIdToIndex(int id)
        {
            int index = 0;
            for (int i = 0; i < DataSource.Config.StationsIndex; i++)
                if (DataSource.Stations[i].Id == id)
                    index = i;
            return index;
        }

        // This function add a station to the stations data base.
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

        // This function add a drone to the drones data base.
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

        // This function add a customer to the customers data base.
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

        // This function add a parcel to the parcels data base.
        public int AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority, int responsibleDrone)
        {
            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel
            {
                Id = DataSource.Config.ParcelsIndex,
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

        //This function binds a parcel to a drone.
        public void BindParcel(int parcelId, int droneId)
        {
            int droneIndex = DroneIdToIndex(droneId);
            int parcelIndex = ParcelIdToIndex(parcelId);

            DataSource.Drones[droneIndex].Status = IDAL.DO.DroneStatuses.Shipping;
            DataSource.Parcels[parcelIndex].DroneId = droneId;
            DataSource.Parcels[parcelIndex].Scheduled = new DateTime();
        }

        //This function collects a parcel by a drone
        public void CollectParcelByDrone(int parcelId)
        {
            int parcelIndex = ParcelIdToIndex(parcelId);
            DataSource.Parcels[parcelIndex].PickedUp = DateTime.Now;
        }

        //This funtion supplies a parcel to the customer.
        public void SupplyParcelToCustomer(int parcelId)
        {
            int deliveredParcelIndex = ParcelIdToIndex(parcelId);
            DataSource.Parcels[deliveredParcelIndex].Delivered = DateTime.Now;
            int droneId = DroneIdToIndex(DataSource.Parcels[deliveredParcelIndex].DroneId);
            int droneIndex = DroneIdToIndex(droneId);
            DataSource.Drones[droneIndex].Status = IDAL.DO.DroneStatuses.Available;

        }

        //This function charges a drone.
        public void ChargeDrone(int droneId)
        {
            int droneIndex = DroneIdToIndex(droneId);
            DataSource.Drones[droneIndex].Status = IDAL.DO.DroneStatuses.Maintenance;
        }

        //This function stops the charge of the drone.
        public void StopCharging()
        {
            
        }

        //This function returns a copy of the stations list.
        public IDAL.DO.Station[] ViewStationsList()
        {
            IDAL.DO.Station[] resultList = new IDAL.DO.Station[DataSource.Config.StationsIndex];
            for(int i=0; i < DataSource.Config.StationsIndex; i++)
            {
                resultList[i] = new IDAL.DO.Station();
                resultList[i] = DataSource.Stations[i];
            }
            return resultList;
        }

        //This function returns a copy of the drones list.
        public IDAL.DO.Drone[] ViewDronesList()
        {
            IDAL.DO.Drone[] resultList = new IDAL.DO.Drone[DataSource.Config.DronesIndex];
            for (int i = 0; i < DataSource.Config.StationsIndex; i++)
            {
                resultList[i] = new IDAL.DO.Drone();
                resultList[i] = DataSource.Drones[i];
            }
            return resultList;
        }

        //This function returns a copy of the customers list.
        public IDAL.DO.Customer[] ViewCustomersList()
        {
            IDAL.DO.Customer[] resultList = new IDAL.DO.Customer[DataSource.Config.CustomersIndex];
            for (int i = 0; i < DataSource.Config.CustomersIndex; i++)
            {
                resultList[i] = new IDAL.DO.Customer();
                resultList[i] = DataSource.Customers[i];
            }
            return resultList;
        }

        //This function returns a copy of the parcels list.
        public IDAL.DO.Parcel[] ViewParcelsList()
        {
            IDAL.DO.Parcel[] resultList = new IDAL.DO.Parcel[DataSource.Config.ParcelsIndex];
            for (int i = 0; i < DataSource.Config.ParcelsIndex; i++)
            {
                resultList[i] = new IDAL.DO.Parcel();
                resultList[i] = DataSource.Parcels[i];
            }
            return resultList;
        }

        public IDAL.DO.Parcel[] ViewUnbindParcels()
        {
            // calculate the size of th result list
            int size = 0;
            for (int i = 0; i < DataSource.Config.ParcelsIndex; i++)
                if(resultList[j].Scheduled != defaultDateTime):
                    size++;
            // create the result list
            int j = 0;
            defaultDateTime = new DateTime();
            IDAL.DO.Parcel[] resultList = new IDAL.DO.Parcel[size];
            for (int i = 0; i < DataSource.Config.ParcelsIndex; i++)
            {
                if(DataSource.Parcels[i].Scheduled != defaultDateTime):
                {
                    resultList[j] = new IDAL.DO.Parcel();
                    resultList[j] = DataSource.Parcels[i];
                    j++;
                }
            }
            return resultList;
        }

        public IDAL.DO.Parcel[] ViewStationsWithFreeChargeSlots()
        {
            // calculate the size of the result list
            int size = 0;
            for (int i = 0; i < DataSource.Config.StationsIndex; i++)
                if(resultList[j].Scheduled != defaultDateTime):
                    size++;
            // create the result list
            int j = 0;
            defaultDateTime = new DateTime();
            IDAL.DO.Station[] resultList = new IDAL.DO.Station[size];
            for (int i = 0; i < DataSource.Config.StationsIndex; i++)
            {
                if(DataSource.Stations[i].ChargeSlots > 0):
                {
                    resultList[j] = new IDAL.DO.Station();
                    resultList[j] = DataSource.Stations[i];
                    j++;
                }
            }
            return resultList;
        }

        //This function returns the station with the required Id.
        public IDAL.DO.Station ViewStation(int id)
        {
            int index = StationIdToIndex(id);
            return DataSource.Stations[index];
        }

        //This function returns the drone with the required Id.
        public IDAL.DO.Drone ViewDrone(int id)
        {
            int index = DroneIdToIndex(id);
            return DataSource.Drones[index]; 
        }

        //This function returns the customer with the required Id.
        public IDAL.DO.Customer ViewCustomer(int id)
        {
            int index = CustomerIdToIndex(id);
            return DataSource.Customers[index];
        }

        //This function returns the parcel with the required Id.
        public IDAL.DO.Parcel ViewParcel(int id)
        {
            int index = ParcelIdToIndex(id);
            return DataSource.Parcels[index];
        }
    }
}