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

        public void AddParcel(int id, int customerSenderId, int customerReceiverId, string weight, string priority, int responsibleDrone)
        {
            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel
            {
                Id = id,
                SenderId = customerSenderId,
                TargetId = customerReceiverId,
                Wheight = (IDAL.DO.WheightCategories)Enum.Parse(typeof(IDAL.DO.WheightCategories), weight),
                Priority = (IDAL.DO.Priorities)Enum.Parse(typeof(IDAL.DO.Priorities), priority),
                DroneId = responsibleDrone
            };

            DataSource.Parcels[DataSource.Config.ParcelsIndex] = parcel;
            DataSource.Config.ParcelsIndex++;
        }

        public void ViewStation(int stationIndex)
        {
            Console.WriteLine(DataSource.Stations[DataSource.Config.StationsIndex].ToString());
        }

        public void ViewDrone(int droneIndex)
        {
            Console.WriteLine(DataSource.Drones[DataSource.Config.DronesIndex].ToString()); 
        }

        public void ViewCustomer(int customerIndex)
        {
            Console.WriteLine(DataSource.Customers[DataSource.Config.CustomersIndex].ToString());
        }

        public void ViewParcel(int parcelIndex)
        {
            Console.WriteLine(DataSource.Parcels[DataSource.Config.ParcelsIndex].ToString());
        }
    }
}