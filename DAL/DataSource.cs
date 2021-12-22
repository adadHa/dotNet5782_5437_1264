using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    struct DataSource
    {
        static internal List<IDAL.DO.Drone> Drones = new List<IDAL.DO.Drone>();
        static internal List<IDAL.DO.Station> Stations = new List<IDAL.DO.Station>();
        static internal List<IDAL.DO.Customer> Customers = new List<IDAL.DO.Customer>();
        static internal List<IDAL.DO.Parcel> Parcels = new List<IDAL.DO.Parcel>();
        static internal List<IDAL.DO.DroneCharge> DroneCharges = new List<IDAL.DO.DroneCharge>();
        static Random rand = new Random();
        internal class Config
        {
            //per hour
            public static double availableDrElectConsumption = 0.10;
            public static double lightDrElectConsumption = 0.15;
            public static double mediumDrElectConsumption = 0.20;
            public static double heavyDrElectConsumption = 0.25;
            public static double chargingRate = 20;

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
            int[] ids = new int[10];
            for (int i = 0; i < 10; i++)
                ids[i] = rand.Next(10000000, 99999999);
            for (int i = 0; i < 10; i++)
            {
                Customers.Add(new IDAL.DO.Customer()
                {
                    Id = ids[i],
                    Name = names[i],
                    Phone = phoneNumbers[i],
                    Longitude = rand.NextDouble() * 180,
                    Latitude = rand.NextDouble() * 180
                });
            }

            // generate general random values that will guide the initialization.
            int shippedParcels = rand.Next(0, 5);  // 0-5 shipped parcels

            // initialize shipped parcels
            for (int i = 0; i < shippedParcels; i++)
            {
                IDAL.DO.WheightCategories wheight = (IDAL.DO.WheightCategories)rand.Next(0, 3);
                IDAL.DO.Priorities priority = (IDAL.DO.Priorities)rand.Next(0, 3);
                int shippingLevel = rand.Next(1, 4); //Ranodimzed shipping level:
                                                     //1-Scheduled ,2-PickedUp, 3-Delivered
                Parcels.Add(new IDAL.DO.Parcel()
                {
                    Id = i,
                    SenderId = Customers[rand.Next(0, 9)].Id,
                    TargetId = Customers[rand.Next(0, 9)].Id,
                    Wheight = wheight,
                    Priority = priority,
                    DroneId = i,
                    Created = DateTime.Now,
                    Scheduled = DateTime.Now,
                    PickedUp = shippingLevel>=2?DateTime.Now:null,
                    Delivered = shippingLevel >= 3 ? DateTime.Now : null
                });
            }
            //initialize waiting parcels
            for (int i = shippedParcels; i < 10; i++)
            {
                IDAL.DO.WheightCategories wheight = (IDAL.DO.WheightCategories)rand.Next(0, 3);
                IDAL.DO.Priorities priority = (IDAL.DO.Priorities)rand.Next(0, 3);
                Parcels.Add(new IDAL.DO.Parcel()
                {
                    Id = shippedParcels + i,
                    SenderId = Customers[rand.Next(0, 9)].Id,
                    TargetId = Customers[rand.Next(0, 9)].Id,
                    Wheight = wheight,
                    Priority = priority,
                    DroneId = -1,
                    Created = DateTime.Now,
                    Scheduled = null,
                    PickedUp = null,
                    Delivered = null
                });
            }
            //initialize stations
            for (int i = 0; i < 2; i++)
            {
                Stations.Add(new IDAL.DO.Station()
                {
                    Id = i,
                    Name = "station" + i.ToString(),
                    Longitude = rand.NextDouble() * 180,
                    Latitude = rand.NextDouble() * 180,
                    FreeChargeSlots = rand.Next(0, 5)
                });
            }

            //initialize Drones
            for (int i = 0; i < shippedParcels; i++) // drones which send the "shipped parcels"
            {
                IDAL.DO.WheightCategories maxWeight = (IDAL.DO.WheightCategories)rand.Next(0, 3);
                Drones.Add(new IDAL.DO.Drone()
                {
                    Id = i,
                    Model = "",
                    MaxWeight = maxWeight,
                });
            }
            for (int i = 5 - shippedParcels; i > 0; i--) // the other drones which are'nt on shipping mode.
            {
                IDAL.DO.WheightCategories maxWeight = (IDAL.DO.WheightCategories)rand.Next(0, 3);
                Drones.Add(new IDAL.DO.Drone()
                {
                    Id = shippedParcels + i,
                    Model = "",
                    MaxWeight = maxWeight,
                });
            }
        }
    }
}





 