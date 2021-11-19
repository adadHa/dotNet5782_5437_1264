
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
        public void ChargeDrone(int droneId, int stationId)
        {
            try
            {
                int droneIndex = DataSource.Drones.FindIndex(x => x.Id == droneId);
                int stationIndex = DataSource.Stations.FindIndex(x => x.Id == stationId);
                if (droneIndex == -1)
                {
                    throw new IdIsNotExistException(droneId, "Drone");
                }
                if (stationIndex == -1)
                {
                    throw new IdIsNotExistException(stationId, "Station");
                }
                if (DataSource.Stations[stationIndex].ChargeSlots == 0)
                {
                    throw new NoChargeSlotsException(DataSource.Stations[stationIndex]);
                }
                IDAL.DO.Drone d = DataSource.Drones[droneIndex];
                d.Status = IDAL.DO.DroneStatuses.Maintenance;
                DataSource.Drones[droneIndex] = d;
                IDAL.DO.Station s = DataSource.Stations[stationIndex];
                s.ChargeSlots -= 1;
                DataSource.Stations[stationIndex] = s;
                DataSource.DroneCharges.Add(new IDAL.DO.DroneCharge { DroneId = d.Id, StationId = s.Id });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function stops the charge of the drone.
        public void StopCharging(int droneId/*, int chargingTime*/)
        {
            int droneIndex = DataSource.Drones.FindIndex(x => x.Id == droneId);
            if (droneIndex == -1)
            {
                throw new IdIsNotExistException(droneId, "Drone");
            }
            IDAL.DO.Drone d = DataSource.Drones[droneIndex];
            d.Status = IDAL.DO.DroneStatuses.Available;
            //d.Battery = ?\\
            DataSource.Drones[droneIndex] = d;
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
        // This function updates a drone with an new model.
        public void UpdateDrone(int droneId, string newModel)
        {
            try
            {
                int index = DataSource.Drones.FindIndex(x => x.Id == droneId);
                if (index == -1)
                {
                    throw new IdIsNotExistException(droneId, "Drone");
                }
                IDAL.DO.Drone d = DataSource.Drones[index];
                d.Model = newModel;
                DataSource.Drones[index] = d;
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
