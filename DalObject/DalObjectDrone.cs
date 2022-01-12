
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using System.Runtime.CompilerServices;
namespace Dal
{
    public partial class DalObject : DalApi.IDal
    {
        // This function add a drone to the drones data base.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, string model, string weight)
        {
            try
            {
                if (DataSource.Drones.FindIndex(x => x.Id == id) != -1)
                {
                    throw new IdIsAlreadyExistException(id, "Drone");
                }
                DataSource.Drones.Add(new DO.Drone()
                {
                    Id = id,
                    Model = model,
                    MaxWeight = (DO.WheightCategories)Enum.Parse(typeof(DO.WheightCategories), weight),
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function charges a drone.
        [MethodImpl(MethodImplOptions.Synchronized)]
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
                if (DataSource.Stations[stationIndex].FreeChargeSlots == 0)
                {
                    throw new NoChargeSlotsException(DataSource.Stations[stationIndex]);
                }
                DO.Drone d = DataSource.Drones[droneIndex];
                DO.Station s = DataSource.Stations[stationIndex];
                s.FreeChargeSlots -= 1;
                DataSource.Stations[stationIndex] = s;
                DataSource.DroneCharges.Add(new DO.DroneCharge { DroneId = d.Id, StationId = s.Id });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function stops the charge of the drone.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void StopCharging(int droneId)
        {
            int droneIndex = DataSource.Drones.FindIndex(x => x.Id == droneId);
            if (droneIndex == -1)
            {
                throw new IdIsNotExistException(droneId, "Drone");
            }
            int chargingIndex = DataSource.DroneCharges.FindIndex(x => x.DroneId == droneId);
            DO.DroneCharge charging = DataSource.DroneCharges[chargingIndex];
            int stationIndex = DataSource.Stations.FindIndex(x => x.Id == charging.StationId);
            DO.Station s = DataSource.Stations[stationIndex];

            s.FreeChargeSlots += 1;
            DataSource.Stations[stationIndex] = s;
            DataSource.DroneCharges.Remove(charging);
        }

        // This function updates a drone with an new model.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(int droneId, string newModel)
        {
            try
            {
                int index = DataSource.Drones.FindIndex(x => x.Id == droneId);
                if (index == -1)
                {
                    throw new IdIsNotExistException(droneId, "Drone");
                }
                DO.Drone d = DataSource.Drones[index];
                d.Model = newModel;
                DataSource.Drones[index] = d;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function returns the string of the drone with the required Id.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string ViewDrone(int id)
        {
            return GetDrone(id).ToString();
        }

        //This function returns the drone with the required Id.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Drone GetDrone(int id)
        {
            int index = DataSource.Drones.FindIndex(x => x.Id == id);
            if (index == -1)
            {
                throw new IdIsNotExistException(id, "Drone");
            }
            return DataSource.Drones[index];
        }

        //This function returns a copy of the drones list.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Drone> ViewDronesList()
        {
            List<DO.Drone> resultList = new List<DO.Drone>();
            foreach (DO.Drone drone in DataSource.Drones)
            {
                DO.Drone d = new DO.Drone();
                d = drone;
                resultList.Add(d);
            }
            return resultList;
        }

        //This function returns a filtered copy of the drones list (according to given predicate)
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Drone> GetDrones(Func<DO.Drone, bool> filter = null)
        {
            if (filter == null)
            {
                return DataSource.Drones;
            }
            return DataSource.Drones.Where(filter); 
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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
