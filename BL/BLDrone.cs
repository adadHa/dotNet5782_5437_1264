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
                IDAL.DO.Station initialStation = dalObject.GetStation(initialStationId);
                dalObject.AddDrone(id, model, weight);
                BLDrones.Add(new IBL.BO.DroneForList
                {
                    Id = id,
                    Model = model,
                    MaxWeight = (IBL.BO.WheightCategories)Enum.Parse(typeof(IBL.BO.WheightCategories), weight),
                    Battery = batteryStatus,
                    Status = IBL.BO.DroneStatuses.Maintenance,
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
            try
            {
                // First, we look for the closet station.
                DroneForList drone = GetDrone(id);
                if (drone == null)
                {
                    throw new DalObject.IdIsNotExistException(id, "Drone");
                }
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
                if (drone.Battery <= mostCloseDistance * consumptionRate)
                {
                    throw new NotEnoughBatteryException(drone, mostCloseStation);
                }

                else
                {
                    //finaly we change desired fields
                    dalObject.ChargeDrone(id, mostCloseStation.Id);
                    int droneIndex = BLDrones.FindIndex(x => x == drone);
                    drone.Location.Latitude = mostCloseStation.Latitude;
                    drone.Location.Longitude = mostCloseStation.Longitude;
                    drone.Battery -= mostCloseDistance * consumptionRate;
                    drone.Status = DroneStatuses.Maintenance;
                    BLDrones[droneIndex] = drone;
                }
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IBL.BO.IdIsNotExistException(e.ToString());
            }
        }
        public void ReleaseDroneFromCharging(int id, double chargingTime)
        {
            try
            {
                DroneForList d = GetDrone(id);
                if (d.Status != DroneStatuses.Maintenance)
                {
                    throw new DroneCannotBeReleasedException(d);
                }
                else
                {
                    dalObject.StopCharging(id);
                    d.Status = DroneStatuses.Available;
                    d.Battery = chargingTime * dalObject.ViewElectConsumptionData()[0];
                    BLDrones[GetDroneIndex(id)] = d;
                }
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IBL.BO.IdIsNotExistException(e.ToString());
            }
        }

        // This functions returns the distanse between two locations.
        private double Distance(Location l1, Location l2)
        {
            // calculate the distance using Pythagoras 
            double a = Math.Pow(l1.Latitude - l2.Latitude, 2); // a = (x1-x2)^2
            double b = Math.Pow(l1.Longitude - l2.Longitude, 2);// b = (y1 - y2)^2
            return Math.Sqrt(a + b); // dis = sqrt(a - b)
        }

        //This function returns a DroneForList from the datasource (on BL) by an index.
        private DroneForList GetDrone(int id)
        {
            return BLDrones[GetDroneIndex(id)];
        }

        //This function returns an index to the desired drone id from the list on BL database
        private int GetDroneIndex(int id)
        {
            try
            {
                int i = BLDrones.FindIndex(x => x.Id == id);
                if (i == -1)
                {
                    throw new DalObject.IdIsNotExistException(id, "Drone"); // ??? should we throw Dal exception about bl's drone exception?
                }
                return i;
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IBL.BO.IdIsNotExistException(e.ToString());
            }
        }

        public IEnumerable<DroneForList> GetDrones(Func<DroneForList, bool> filter = null)
        {
            return BLDrones.Where(filter);
        }
    }


}

