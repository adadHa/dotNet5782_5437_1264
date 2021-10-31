﻿using System;
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
            DataSource.Stations.Add(new IDAL.DO.Station()
            {
                Id = id,
                Name = name,
                ChargeSlots = num,
                Longitude = longitude,
                Latitude = latitude
            });
        }

        // This function add a drone to the drones data base.
        public void AddDrone(int id, string model, string weight, int batteryStatus, string droneStatus)
        {
            DataSource.Drones.Add(new IDAL.DO.Drone()
            {
                Id = id,
                Model = model,
                MaxWeight = (IDAL.DO.WheightCategories)Enum.Parse(typeof(IDAL.DO.WheightCategories), weight),
                Battery = batteryStatus,
                Status = (IDAL.DO.DroneStatuses)Enum.Parse(typeof(IDAL.DO.DroneStatuses), droneStatus)
            });
        }

        // This function add a customer to the customers data base.
        public void AddCustomer(int id, string name, string phoneNumber, double longitude, double latitude)
        {
            DataSource.Customers.Add( new IDAL.DO.Customer()
            {
                Id = id,
                Name = name,
                Phone = phoneNumber,
                Longitude = longitude,
                Latitude = latitude
            });
        }

        // This function add a parcel to the parcels data base.
        public void AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority, int responsibleDrone)
        {
            DataSource.Parcels.Add( new IDAL.DO.Parcel()
            {
                Id = DataSource.Config.ParcelsIndex,
                SenderId = customerSenderId,
                TargetId = customerReceiverId,
                Wheight = (IDAL.DO.WheightCategories)Enum.Parse(typeof(IDAL.DO.WheightCategories), weight),
                Priority = (IDAL.DO.Priorities)Enum.Parse(typeof(IDAL.DO.Priorities), priority),
                DroneId = responsibleDrone
            });
        }

        //This function binds a parcel to a drone.
        public void BindParcel(int parcelId, int droneId)
        {
            int droneIndex = DroneIdToIndex(droneId);
            int parcelIndex = ParcelIdToIndex(parcelId);
            
            IDAL.Do.Drone d = DataSource.Drones[droneIndex];
            IDAL.Do.Parcel p = DataSource.Parcels[parcelIndex];
            d.Status = IDAL.DO.DroneStatuses.Shipping;
            p.DroneId = droneId;
            p.Scheduled = DateTime.Now;
            DataSource.Drones[droneIndex] = d;
            DataSource.Parcels[parcelIndex] = p;
        }

        //This function collects a parcel by a drone
        public void CollectParcelByDrone(int parcelId)
        {
            int parcelIndex = ParcelIdToIndex(parcelId);
            IDAL.Do.Parcel p = DataSource.Parcels[parcelIndex];
            p.PickedUp = DateTime.Now;
            DataSource.Parcels[parcelIndex] = p;
        }

        //This funtion supplies a parcel to the customer.
        public void SupplyParcelToCustomer(int parcelId)
        {
            int deliveredParcelIndex = ParcelIdToIndex(parcelId);
            IDAL.Do.Parcel p = DataSource.Parcels[deliveredParcelIndex];
            p.Delivered = DateTime.Now;
            DataSource.Parcels[deliveredParcelIndex] = p;
            int droneId = DroneIdToIndex(DataSource.Parcels[deliveredParcelIndex].DroneId);
            int droneIndex = DroneIdToIndex(droneId);
            IDAL.Do.Drone d = DataSource.Drones[droneIndex];
            d.Status = IDAL.DO.DroneStatuses.Available;
            DataSource.Drones[droneIndex] = d;
        }

        //This function charges a drone.
        public void ChargeDrone(int droneId)
        {
            int droneIndex = DroneIdToIndex(droneId);
            IDAL.Do.Drone d = DataSource.Drones[droneIndex];
            d.Status = IDAL.DO.DroneStatuses.Maintenance;
            DataSource.Drones[droneIndex] = d;
        }

        //This function stops the charge of the drone.
        public void StopCharging(int droneId, int chargingTime)
        {
            
        }

        //This function returns a copy of the stations list.
        public IEnumerable<IDAL.DO.Station> ViewStationsList()
        {
            List<IDAL.DO.Station> resultList = new List<IDAL.DO.Station>();
            for(int i=0; i < DataSource.Stations; i++)
            {
                IDAL.DO.Station s = new IDAL.DO.Station();
                s = DataSource.Stations[i];
                resultList.Add(s);
            }
            return resultList;
        }

        //This function returns a copy of the drones list.
        public IDAL.DO.Drone[] ViewDronesList()
        {
            List<IDAL.DO.Drone> resultList = new List<IDAL.DO.Drone>();
            for (int i = 0; i < DataSource.Config.StationsIndex; i++)
            {
                IDAL.DO.Drone d = new IDAL.DO.Drone();
                d = DataSource.Drones[i];
                resultList.Add(d);
            }
            return resultList;
        }

        //This function returns a copy of the customers list.
        public IDAL.DO.Customer[] ViewCustomersList()
        {
            List<IDAL.DO.Customer> resultList = new List<IDAL.DO.Customer>();
            for (int i = 0; i < DataSource.Config.CustomersIndex; i++)
            {
                IDAL.DO.Customer c = new IDAL.DO.Customer();
                c = DataSource.Customers[i];
                resultList.Add(c);
            }
            return resultList;
        }

        //This function returns a copy of the parcels list.
        public IDAL.DO.Parcel[] ViewParcelsList()
        {
            List<IDAL.DO.Parcel> resultList = new List<IDAL.DO.Parcel>();
            for (int i = 0; i < DataSource.Config.ParcelsIndex; i++)
            {
                IDAL.DO.Parcel p = new IDAL.DO.Parcel();
                p = DataSource.Parcels[i];
                resultList.Add(p);
            }
            return resultList;
        }

        public IDAL.DO.Parcel[] ViewUnbindParcels()
        {
            // create the result list
            int j = 0;
            defaultDateTime = new DateTime();
            List<IDAL.DO.Parcel> resultList = new List<IDAL.DO.Parcel>();
            for (int i = 0; i < DataSource.Config.ParcelsIndex; i++)
            {
                if(DataSource.Parcels[i].Scheduled != defaultDateTime):
                {
                    IDAL.DO.Parcel p = new IDAL.DO.Parcel();
                    p = DataSource.Parcels[i];
                    resultList.Add(p);
                    j++;
                }
            }
            return resultList;
        }

        public IDAL.DO.Parcel[] ViewStationsWithFreeChargeSlots()
        {
            // create the result list
            int j = 0;
            defaultDateTime = new DateTime();
            List<IDAL.DO.Station> resultList = new List<IDAL.DO.Station>();
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

        public string Sexagesimal(double longitude, double latitude)
        {
            return 
        }
    }
}