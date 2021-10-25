using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    struct DataSource
    {
        static internal IDAL.DO.Drone[] Drones;
        static internal IDAL.DO.Station[] Stations;
        static internal IDAL.DO.Customer[] Customers;
        static internal IDAL.DO.Parcel[] Parcels;
        static internal IDAL.DO.DroneCharge[] DroneCharges;
        static Random rand = new Random();
        static DataSource()
        {
            Drones = new IDAL.DO.Drone[10];
            Stations = new IDAL.DO.Station[5];
            Customers = new IDAL.DO.Customer[100];
            Parcels = new IDAL.DO.Parcel[1000];
            DroneCharges = new IDAL.DO.DroneCharge[15];
        }
        internal class Config
        {
            static internal int DronesIndex;
            static internal int StationsIndex;
            static internal int CustomersIndex;
            static internal int ParcelsIndex;
            static internal int DroneChargeIndex;
            static Config()
            {
                DronesIndex = 0;
                StationsIndex = 0;
                CustomersIndex = 0;
                ParcelsIndex = 0;
                DroneChargeIndex = 0;
            }

            //This function initailizes the data structures.
            static public void Initialize()
            {
                // initialize customers
                string[] names = { "Yosi","Dani","Avi", "Rafi", "Yoel"
                        , "David", "Israel", "Levi", "Ben", "Moti"};
                string[] phoneNumbers = { "05467651241", "0524931828", "0526067135",
                    "0527839442", "0524711136", "0588824245", "0586934625",
                    "0542444196", "0549035643", "0542463885" };

                for (int i = 0; i < 10; i++)
                {
                    Customers[i] = new IDAL.DO.Customer
                    {
                        Id = rand.Next(10000000, 99999999),
                        Name = names[i],
                        Phone = phoneNumbers[i],
                        Longitude = rand.NextDouble() * 180,
                        Latitude = rand.NextDouble() * 180
                    };
                    CustomersIndex++;
                }

                // generate general random values that will guide the initialization.
                int shippedParcels = rand.Next(0, 5);  // 0-5 shipped parcels

                // initialize shipped parcels
                for (int i = 0; i < shippedParcels; i++)
                {
                    IDAL.DO.WheightCategories wheight = (IDAL.DO.WheightCategories)rand.Next(0, 3);
                    IDAL.DO.Priorities priority = (IDAL.DO.Priorities)rand.Next(0, 3);
                    DateTime defaultDate = new DateTime();
                    Parcels[i] = new IDAL.DO.Parcel
                    {
                        Id = i,
                        SenderId = rand.Next(0, 9),
                        TargetId = rand.Next(0, 9),
                        Wheight = wheight,
                        Priority = priority,
                        DroneId = i,
                        Requested = DateTime.Now,
                        Scheduled = defaultDate,
                        PickedUp = defaultDate,
                        Delivered = defaultDate
                    };
                    ParcelsIndex++;

                }
                //initialize waiting parcels
                for (int i = shippedParcels; i < 10; i++)
                {
                    IDAL.DO.WheightCategories wheight = (IDAL.DO.WheightCategories)rand.Next(0, 3);
                    IDAL.DO.Priorities priority = (IDAL.DO.Priorities)rand.Next(0, 3);
                    DateTime defaultDate = new DateTime();
                    Parcels[i] = new IDAL.DO.Parcel
                    {
                        Id = shippedParcels + i,
                        SenderId = rand.Next(0, 9),
                        TargetId = rand.Next(0, 9),
                        Wheight = wheight,
                        Priority = priority,
                        DroneId = 0,
                        Requested = DateTime.Now,
                        Scheduled = defaultDate,
                        PickedUp = defaultDate,
                        Delivered = defaultDate
                    };
                    ParcelsIndex++;
                }
                //initialize stations
                for (int i = 0; i < 2; i++)
                {
                    Stations[i] = new IDAL.DO.Station
                    {
                        Id = i,
                        Name = "station" + i.ToString(),
                        Longitude = rand.NextDouble() * 180,
                        Latitude = rand.NextDouble() * 180,
                        ChargeSlots = rand.Next(0,5)
                    };
                    StationsIndex++;
                }

                //initialize Drones
                for (int i = 0; i < shippedParcels; i++) // drones which send the "shipped parcels"
                {
                    IDAL.DO.WheightCategories maxWeight = (IDAL.DO.WheightCategories)rand.Next(0, 3);
                    double battery = rand.NextDouble()*100;
                    Drones[i] = new IDAL.DO.Drone
                    {
                        Id = i,
                        Model = "",
                        MaxWeight = maxWeight,
                        Status = IDAL.DO.DroneStatuses.Shipping,
                        Battery = battery
                    };
                    DronesIndex++;
                }
                for (int i = 5 - shippedParcels; i > 0; i--) // the other drones which are'nt on shipping mode.
                {
                    IDAL.DO.WheightCategories maxWeight = (IDAL.DO.WheightCategories)rand.Next(0, 3);
                    IDAL.DO.DroneStatuses status = (IDAL.DO.DroneStatuses)rand.Next(0, 2); // "Available" or "Maintenance"
                    double battery = rand.NextDouble()*100;
                    Drones[i] = new IDAL.DO.Drone
                    {
                        Id = shippedParcels + i,
                        Model = "",
                        MaxWeight = maxWeight,
                        Status = status,
                        Battery = battery
                    };
                    DronesIndex++;
                }
            }
        }
    }
}




