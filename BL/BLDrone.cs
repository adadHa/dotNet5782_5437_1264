using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public enum ElecConsumption // used in ChargeDrone function
        {
            availableDrElectConsumption,
            lightDrElectConsumption,
            mediumDrElectConsumption,
            heavyDrElectConsumption,
            chargingRate
        }
        //this function adds a drone to the database
        public void AddDrone(int id, string model, string weight, int initialStationId)
        {
            try
            {
                double batteryStatus = rand.NextDouble() * rand.Next(20, 41);
                IDAL.DO.Station initialStation = dalObject.ViewStation(initialStationId);
                dalObject.AddDrone(id, model, weight, batteryStatus, "Maintenance");
                BLDrones.Add(new IBL.BO.DroneForList
                {
                    Id = id,
                    Model = model,
                    MaxWeight = (IBL.BO.WheightCategories)Enum.Parse(typeof(IBL.BO.WheightCategories), weight),
                    Battery = batteryStatus,
                    Status = (IBL.BO.DroneStatuses)Enum.Parse(typeof(IBL.BO.DroneStatuses), weight),
                    Location = new IBL.BO.Location()
                    {
                        Longitude = initialStation.Longitude,
                        Latitude = initialStation.Longitude
                    }

                });
                dalObject.ChargeDrone(id, initialStationId);
            }
            catch (DalObject.IdIsAlreadyExistException e)
            {
                throw new IBL.BO.IdIsAlreadyExistException(e.ToString());
            }
            catch (DalObject.NoChargeSlotsException e)
            {
                throw new IBL.BO.NoChargeSlotsException(e.ToString());
            }
        }
        public void UpdateDrone(int id, string newModel)
        {
            try
            {
                dalObject.UpdateDrone(id, newModel);
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IBL.BO.IdIsNotExistException(e.ToString());
            }
        }
        public void ChargeDrone(int id)
        {
            // First, we look for the closet station.
            DroneForList drone = BLDrones.Find(x => x.Id == id);
            List<IDAL.DO.Station> stations =
                (List<IDAL.DO.Station>)dalObject.ViewStationsWithFreeChargeSlots(); // we choose from the available stations.
            IDAL.DO.Station mostCloseStation = stations[0];
            double mostCloseDistance = 0;
            double distance = 0;
            foreach (IDAL.DO.Station station in stations)
            {
                Location stationL = new Location { Latitude = station.Latitude, Longitude = station.Longitude };
                distance = Distance(stationL, drone.Location);
                if (mostCloseDistance > distance)
                {
                    mostCloseDistance = distance;
                    mostCloseStation = station;
                }
            }

            // now we check if the battery status of the drone allow it to get there.
            double consumptionRate = drone.Status == DroneStatuses.Available ?
                                            dalObject.ViewElectConsumptionData()[0] :
                                            dalObject.ViewElectConsumptionData()[(int)drone.MaxWeight + 1]; // Light -- lightDrElectConsumption
                                                                                                            // Medium -- mediumDrElectConsumption
                                                                                                            // Heavy -- heavyDrElectConsumption
            if(drone.Battery <= mostCloseDistance*consumptionRate)
            {
                throw new NotEnoughBatteryException(drone, mostCloseStation);
            }

            else
            {
                dalObject.ChargeDrone(id, mostCloseStation.Id, mostCloseDistance * consumptionRate);
                BLDrones.FindIndex(x => x == drone);
                drone.Location.Latitude = mostCloseStation.Latitude;
                drone.Location.Longitude = mostCloseStation.Longitude;
                BLDrones.FindIndex(x => x )
            }
        }
        public Drone ViewDrone(int id)
        {
            return new Drone();
        }

        // This functions returns the distanse between two locations.
        private double Distance(Location l1, Location l2)
        {
            // calculate the distance using Pythagoras 
            double a = Math.Pow(l1.Latitude - l2.Latitude, 2); // a = (x1-x2)^2
            double b = Math.Pow(l1.Longitude - l2.Longitude, 2);// b = (y1 - y2)^2
            return Math.Sqrt(a + b); // dis = sqrt(a - b)
        }
    }

