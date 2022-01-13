using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
namespace Dal
{
    struct DataSource
    {
        static internal List<DO.Drone> Drones = new List<DO.Drone>();
        static internal List<DO.Station> Stations = new List<DO.Station>();
        static internal List<DO.Customer> Customers = new List<DO.Customer>();
        static internal List<DO.Parcel> Parcels = new List<DO.Parcel>();
        static internal List<DO.DroneCharge> DroneCharges = new List<DO.DroneCharge>();
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
            // generate general values that will guide the initialization.
            int numberOfCustomers = 30;
            int numberOfStations = 15;
            int maxFreeChargeSlotsCap = 7;
            int parcelsNumber = 30;
            int shippedParcels = rand.Next(0,20);
            int waitingParcels = parcelsNumber - shippedParcels;

            string[] names = { "Yosi","Dani","Avi", "Rafi", "Yoel"
                        , "David", "Israel", "Levi", "Ben", "Moti"};
            string[] secondName = { "Levi","Cohen","Haim", "Wolfes", "Shushan"
                        , "Biton", "Ben-Haim", "Ben Shalom", "Ben-David", "Shtain"};
            int[] ids = new int[numberOfCustomers];
            for (int i = 0; i < numberOfCustomers; i++)
                ids[i] = rand.Next(10000000, 99999999);
            for (int i = 0; i < numberOfCustomers; i++)
            {
                Customers.Add(new Customer()
                {
                    Id = ids[i],
                    Name = names[rand.Next(0, 10)] + " " + secondName[rand.Next(0, 10)],
                    Phone = "05" + rand.Next(10000000, 99999999).ToString(),
                    Longitude = rand.NextDouble() + 31.23,
                    Latitude = rand.NextDouble() + 35.13
                });
            }

            // initialize shipped parcels
            for (int i = 0; i < shippedParcels; i++)
            {
                DO.WheightCategories wheight = (DO.WheightCategories)rand.Next(0, 3);
                DO.Priorities priority = (DO.Priorities)rand.Next(0, 3);
                int shippingLevel = rand.Next(1, 4); //Ranodimzed shipping level:
                                                     //1-Scheduled ,2-PickedUp, 3-Delivered
                Parcels.Add(new DO.Parcel()
                {
                    Id = i,
                    SenderId = Customers[rand.Next(0, numberOfCustomers)].Id,
                    TargetId = Customers[rand.Next(0, numberOfCustomers)].Id,
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
            for (int i = shippedParcels; i < parcelsNumber; i++)
            {
                DO.WheightCategories wheight = (DO.WheightCategories)rand.Next(0, 3);
                DO.Priorities priority = (DO.Priorities)rand.Next(0, 3);
                Parcels.Add(new DO.Parcel()
                {
                    Id = i,
                    SenderId = Customers[rand.Next(0, numberOfCustomers)].Id,
                    TargetId = Customers[rand.Next(0, numberOfCustomers)].Id,
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
            for (int i = 0; i < numberOfStations; i++)
            {
                Stations.Add(new DO.Station()
                {
                    Id = i,
                    Name = "station" + i.ToString(),
                    Longitude = rand.NextDouble() + 31.23,
                    Latitude = rand.NextDouble() + 35.13,
                    FreeChargeSlots = rand.Next(0, maxFreeChargeSlotsCap)
                });
            }

            //initialize Drones
            for (int i = 0; i < shippedParcels; i++) // drones which send the "shipped parcels"
            {
                DO.WheightCategories maxWeight = (DO.WheightCategories)rand.Next(0, 3);
                Drones.Add(new DO.Drone()
                {
                    Id = i,
                    Model = "DroneRobots" + rand.Next(1000, 9999),
                    MaxWeight = maxWeight,
                });
            }
            for (int i = shippedParcels; i < parcelsNumber; i++) // the other drones which are'nt on shipping mode.
            {
                DO.WheightCategories maxWeight = (DO.WheightCategories)rand.Next(0, 3);
                Drones.Add(new DO.Drone()
                {
                    Id = i,
                    Model = "DroneRobots" + rand.Next(1000,9999),
                    MaxWeight = maxWeight
                });
            }
        }
    }
}





 