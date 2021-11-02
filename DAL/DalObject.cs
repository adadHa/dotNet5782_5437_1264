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
            DataSource.Initialize();
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
                Id = DataSource.Parcels.Count,
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
            int droneIndex = DataSource.Drones.FindIndex(x => x.Id == droneId);
            int parcelIndex = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
            IDAL.DO.Drone d = DataSource.Drones[droneIndex];
            IDAL.DO.Parcel p = DataSource.Parcels[parcelIndex];
            d.Status = IDAL.DO.DroneStatuses.Shipping;
            p.DroneId = droneId;
            p.Scheduled = DateTime.Now;
            DataSource.Drones[droneIndex] = d;
            DataSource.Parcels[parcelIndex] = p;
        }

        //This function collects a parcel by a drone
        public void CollectParcelByDrone(int parcelId)
        {
            int parcelIndex = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
            IDAL.DO.Parcel p = DataSource.Parcels[parcelIndex];
            p.PickedUp = DateTime.Now;
            DataSource.Parcels[parcelIndex] = p;
        }

        //This funtion supplies a parcel to the customer.
        public void SupplyParcelToCustomer(int parcelId)
        {
            int deliveredParcelIndex = DataSource.Parcels.FindIndex(x => x.Id == parcelId); ;
            IDAL.DO.Parcel p = DataSource.Parcels[deliveredParcelIndex];
            p.Delivered = DateTime.Now;
            DataSource.Parcels[deliveredParcelIndex] = p;
            int droneId = DataSource.Parcels[deliveredParcelIndex].DroneId;
            int droneIndex = DataSource.Drones.FindIndex(x => x.Id == droneId);
            IDAL.DO.Drone d = DataSource.Drones[droneIndex];
            d.Status = IDAL.DO.DroneStatuses.Available;
            DataSource.Drones[droneIndex] = d;
        }

        //This function charges a drone.
        public void ChargeDrone(int droneId)
        {
            int droneIndex = DataSource.Drones.FindIndex(x => x.Id == droneId);
            IDAL.DO.Drone d = DataSource.Drones[droneIndex];
            d.Status = IDAL.DO.DroneStatuses.Maintenance;
            DataSource.Drones[droneIndex] = d;
        }

        //This function stops the charge of the drone.
        public void StopCharging(int droneId/*, int chargingTime*/)
        {
            
        }

        //This function returns a copy of the stations list.
        public IEnumerable<IDAL.DO.Station> ViewStationsList()
        {
            List<IDAL.DO.Station> resultList = new List<IDAL.DO.Station>();
            foreach (IDAL.DO.Station station in DataSource.Stations)
            {
                IDAL.DO.Station s = new IDAL.DO.Station();
                s = station;
                resultList.Add(s);
            }
            return resultList;
        }

        //This function returns a copy of the drones list.
        public IEnumerable<IDAL.DO.Drone> ViewDronesList()
        {
            List<IDAL.DO.Drone> resultList = new List<IDAL.DO.Drone>();
            foreach (IDAL.DO.Drone drone in DataSource.Drones)
            {
                IDAL.DO.Drone d = new IDAL.DO.Drone();
                d = drone;
                resultList.Add(d);
            }
            return resultList;
        }

        //This function returns a copy of the customers list.
        public IEnumerable<IDAL.DO.Customer> ViewCustomersList()
        {
            List<IDAL.DO.Customer> resultList = new List<IDAL.DO.Customer>();
            foreach (IDAL.DO.Customer customer in DataSource.Customers)
            {
                IDAL.DO.Customer c = new IDAL.DO.Customer();
                c = customer;
                resultList.Add(c);
            }
            return resultList;
        }

        //This function returns a copy of the parcels list.
        public IEnumerable<IDAL.DO.Parcel> ViewParcelsList()
        {
            List<IDAL.DO.Parcel> resultList = new List<IDAL.DO.Parcel>();
            foreach (IDAL.DO.Parcel parcel in DataSource.Parcels)
            {
                IDAL.DO.Parcel p = new IDAL.DO.Parcel();
                p = parcel;
                resultList.Add(p);
            }
            return resultList;
        }

        public IEnumerable<IDAL.DO.Parcel> ViewUnbindParcels()
        {
            // create the result list
            int j = 0;
            DateTime defaultDateTime = new DateTime();
            List<IDAL.DO.Parcel> resultList = new List<IDAL.DO.Parcel>();
            foreach (IDAL.DO.Parcel parcel in DataSource.Parcels)
            {
                if(parcel.Scheduled != defaultDateTime)
                {
                    IDAL.DO.Parcel p = new IDAL.DO.Parcel();
                    p = parcel;
                    resultList.Add(p);
                    j++;
                }
            }
            return resultList;
        }

        public IEnumerable<IDAL.DO.Station> ViewStationsWithFreeChargeSlots()
        {
            // create the result list
            int j = 0;
            List<IDAL.DO.Station> resultList = new List<IDAL.DO.Station>();
            foreach (IDAL.DO.Station station in DataSource.Stations)
            {
                if(station.ChargeSlots > 0)
                {
                    resultList[j] = new IDAL.DO.Station();
                    resultList[j] = station;
                    j++;
                }
            }
            return resultList;
        }

        //This function returns the station with the required Id.
        public IDAL.DO.Station ViewStation(int id)
        {
            int index = DataSource.Stations.FindIndex(x => x.Id == id);
            return DataSource.Stations[index];
        }

        //This function returns the drone with the required Id.
        public IDAL.DO.Drone ViewDrone(int id)
        {
            int index = DataSource.Drones.FindIndex(x => x.Id == id);
            return DataSource.Drones[index]; 
        }

        //This function returns the customer with the required Id.
        public IDAL.DO.Customer ViewCustomer(int id)
        {
            int index = DataSource.Customers.FindIndex(x => x.Id == id);
            return DataSource.Customers[index];
        }

        //This function returns the parcel with the required Id.
        public IDAL.DO.Parcel ViewParcel(int id)
        {
            int index = DataSource.Parcels.FindIndex(x => x.Id == id);
            return DataSource.Parcels[index];
        }

    
    }
}