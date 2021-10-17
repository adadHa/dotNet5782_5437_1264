using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public struct DataSource
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
                    Customers[i] = new IDAL.DO.Customer(i, names[i], phoneNumbers[i], rand.NextDouble() * 360, rand.NextDouble() * 180);
                    CustomersIndex++;
                }
                // generate general random values that will guide the initialization.
                int shippedParcels = rand.Next(0, 5);
                int watingParcels = 10 - shippedParcels;

                // initialize shipped parcels
                for (int i = 0; i < shippedParcels; i++)
                {
                    IDAL.DO.WheightCategories wheight = (IDAL.DO.WheightCategories)rand.Next(0, 2);
                    IDAL.DO.Priorities priority = (IDAL.DO.Priorities)rand.Next(0, 2);
                    int droneId = rand.Next(0, 9);
                    DateTime defaultDate = DateTime.Now;
                    Parcels[i] = new IDAL.DO.Parcel(i,
                                                    rand.Next(0, 9),
                                                    rand.Next(0, 9),
                                                    wheight,
                                                    priority,
                                                    droneId,
                                                    DateTime.Now
                                                    );
                }
                //initialize waiting parcels
                for (int i = 0; i < 10; i++)
                {
                    IDAL.DO.WheightCategories wheight = (IDAL.DO.WheightCategories)rand.Next(0, 2);
                    IDAL.DO.Priorities priority = (IDAL.DO.Priorities)rand.Next(0, 2);
                    int droneId = rand.Next(0, 9);
                    DateTime date = DateTime.Now;
                    Parcels[i] = new IDAL.DO.Parcel(i,
                                                    rand.Next(0, 9),
                                                    rand.Next(0, 9),
                                                    wheight,
                                                    priority,
                                                    droneId,
                                                    date,
                                                    0,
                                                    0,
                                                    0);
                }
                //initialize stations
                for (int i = 0; i < 2; i++)
                {
                    Stations[i] = new IDAL.DO.Station(i, "station" + i.ToString(), rand.NextDouble() * 360, rand.NextDouble() * 180);
                }

                //initialize Drones
                for (int i = 0; i < 5; i++)
                {
                    Drones[i] = new IDAL.DO.Drone();
                    Drones[i].Id = i;
                }
            }
        }
    }
}

