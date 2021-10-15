using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    class DataSource
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
                    Customers[i] = new IDAL.DO.Customer();
                    Customers[i].Id = i;
                    Customers[i].Name = names[i];
                    Customers[i].Phone = phoneNumbers[i];
                    Customers[i].Longitude = rand.NextDouble() * 360;
                    Customers[i].Latitude = rand.NextDouble() * 180;
                }

                //initialize stations
                for (int i = 0; i < 2; i++)
                {
                    Stations[i] = new IDAL.DO.Station();
                    Stations[i].Id = i;
                    Stations[i].Name = "station" + i.ToString();
                    Stations[i].Longitude = rand.NextDouble() * 360;
                    Stations[i].Lattitude = rand.NextDouble() * 180;
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

