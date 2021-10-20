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
        static Random rand = new Random();
        static DataSource()
        {
            Drones = new IDAL.DO.Drone[10];
            Stations = new IDAL.DO.Station[5];
            Customers = new IDAL.DO.Customer[100];
            Parcels = new IDAL.DO.Parcel[1000];
        }
        internal class Config
        {
            static internal int DronesIndex;
            static internal int StationsIndex;
            static internal int CustomersIndex;
            static internal int ParcelsIndex;

            static Config()
            {
                DronesIndex = 0;
                StationsIndex = 0;
                CustomersIndex = 0;
                ParcelsIndex = 0;
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
                    Customers[i] = new IDAL.DO.Customer{Id = rand.Next(10000000,99999999),
                                                        Name = names[i], 
                                                        Phone = phoneNumbers[i],
                                                        Longitude = rand.NextDouble() * 360,
                                                        Lattitude = rand.NextDouble() * 180)};
                    CustomersIndex++;
                }

                // generate general random values that will guide the initialization.
                int shippedParcels = rand.Next(0, 5);  // 0-5 shipped parcels
                int watingParcels = 10 - shippedParcels; // 5-10 waiting parcels

                // initialize shipped parcels
                for (int i = 0; i < shippedParcels; i++)
                {
                    IDAL.DO.WheightCategories wheight = (IDAL.DO.WheightCategories)rand.Next(0, 2);
                    IDAL.DO.Priorities priority = (IDAL.DO.Priorities)rand.Next(0, 2);
                    DateTime defaultDate = DateTime();
                    Parcels[i] = new IDAL.DO.Parcel{Id = i,
                                                    SenderId = rand.Next(0, 9),
                                                    Target_Id = rand.Next(0, 9),
                                                    Wheight =  wheight,
                                                    Priority = priority,
                                                    DroneId = i,
                                                    Requested = DateTime.Now,
                                                    Scheduled = defaultDate,
                                                    PickedUp = defaultDate,
                                                    Delivered = defaultDate
                                                    };
                    
                }
                //initialize waiting parcels
                for (int i = 0; i < watingParcels; i++)
                {
                    wheight = (IDAL.DO.WheightCategories)rand.Next(0, 2);
                    priority = (IDAL.DO.Priorities)rand.Next(0, 2);
                    Parcels[i] = new IDAL.DO.Parcel{Id = shippedParcels + i,
                                                    SenderId = rand.Next(0, 9),
                                                    Target_Id = rand.Next(0, 9),
                                                    Wheight =  wheight,
                                                    Priority = priority,
                                                    DroneId = 0,
                                                    Requested = defaultDate,
                                                    Scheduled = defaultDate,
                                                    PickedUp = defaultDate,
                                                    Delivered = defaultDate
                                                    };
                }
                //initialize stations
                for (int i = 0; i < 2; i++)
                {
                    Stations[i] = new IDAL.DO.Station{Id = i,
                                                     Name = "station" + i.ToString(),
                                                     Longitude = rand.NextDouble() * 360,
                                                     Lattitude = rand.NextDouble() * 180)};
                }

                //initialize Drones
                for (int i = 0; i < shippedParcels; i++) // drone which send the "shipped parcels"
                {
                    IDAL.DO.Model models = [];
                    IDAL.DO.WheightCategories maxWeight = (IDAL.DO.WheightCategories)rand.Next(0, 2);
                    double battery = rand.NextDouble(); 
                    Drones[i] = new IDAL.DO.Drone{  Id = i,
                                                    Model = models[i],
                                                    MaxWeight = maxWeight,
                                                    Status = IDAL.DO.DroneStatuses.Shipping,
                                                    Battery = battery
                                                    };
                }
                for (int i = 0; i < watingParcels; i++) // the othre drones
                {
                    IDAL.DO.Model models = [];
                    IDAL.DO.WheightCategories maxWeight = (IDAL.DO.WheightCategories)rand.Next(0, 2);
                    IDAL.DO.DroneStatuses status = (IDAL.DO.DroneStatuses)rand.Next(0, 1); // "Available" or "Maintenance"
                    double battery = rand.NextDouble(); 
                    Drones[i] = new IDAL.DO.Drone{  Id = shippedParcels + i,
                                                    Model = models[i],
                                                    MaxWeight = maxWeight,
                                                    Status = status,
                                                    Battery = battery
                                                    };
                } 
            }
        }
    }
}

