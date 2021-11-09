using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        // This function add a drone to the drones data base.
        public void AddDrone(int id, string model, string weight, double batteryStatus, string droneStatus)
        {
            try
            {
                if (DataSource.Drones.FindIndex(x => x.Id == id) != -1)
                {
                    throw new IdIsAlreadyExistException(id, "Drone");
                }
                DataSource.Drones.Add(new IDAL.DO.Drone()
                {
                    Id = id,
                    Model = model,
                    MaxWeight = (IDAL.DO.WheightCategories)Enum.Parse(typeof(IDAL.DO.WheightCategories), weight),
                    Battery = batteryStatus,
                    Status = (IDAL.DO.DroneStatuses)Enum.Parse(typeof(IDAL.DO.DroneStatuses), droneStatus)
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function charges a drone.
        public void ChargeDrone(int droneId)
        {
            try
            {
                int droneIndex = DataSource.Drones.FindIndex(x => x.Id == droneId);
                if (droneIndex == -1)
                {
                    throw new IdIsNotExistException(droneId, "Drone");
                }
                IDAL.DO.Drone d = DataSource.Drones[droneIndex];
                d.Status = IDAL.DO.DroneStatuses.Maintenance;
                DataSource.Drones[droneIndex] = d;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function stops the charge of the drone.
        public void StopCharging(int droneId/*, int chargingTime*/)
        {

        }

        //This function returns the drone with the required Id.
        public IDAL.DO.Drone ViewDrone(int id)
        {
            try
            {
                int index = DataSource.Drones.FindIndex(x => x.Id == id);
                if (index == -1)
                {
                    throw new IdIsNotExistException(id, "Drone");
                }
                return DataSource.Drones[index];
            }
            catch (Exception)
            {
                throw;
            }
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

        public double[] ViewElectConsumptionData()
        {
            double[] arr = {DataSource.Config.availableDrElectConsumption,
                         DataSource.Config.lightDrElectConsumption,
                         DataSource.Config.mediumDrElectConsumption,
                         DataSource.Config.heavyDrElectConsumption,
                         DataSource.Config.chargingRate
            };
            return arr;
        }
    }
}

    