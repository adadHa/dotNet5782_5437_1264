using System;
using System.Collections.Generic;
using System.Linq;
namespace BL
{
    public partial class BL : IBL.IBL
    {

        //this function adds a station to the database
        public void AddStation(int id, string name, int freeChargingSlots, IBL.BO.Location location)
        {

            try
            {
                dalObject.AddStation(id, name, freeChargingSlots, location.Longitude, location.Latitude);
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

        //This function updates a station with a new name and/or a new charging slots capacity.
        public void UpdateStation(int id, string newName, int newNum)
        {
            try
            {
                if (newName != "") // check if there was an input for this value
                {
                    dalObject.UpdateStationName(id, newName);
                }
                if (newNum != -1) // check if there was an input for this value
                {
                    if (newNum >= 0) // it should be a positive number
                        dalObject.UpdateStationChargeSlotsCap(id, newNum);
                    else
                        throw new ArgumentOutOfRangeException("charging slots capacity");
                }
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IBL.BO.IdIsNotExistException(e.ToString());
            }
        }
        //this function view the station details
        public string ViewStation(int id)
        {
            return GetStation(id).ToString();
        }
        //This function returns a StationForList from the datasource (on BL) by an index.
        private IBL.BO.Station GetStation(int id)
        {
            try
            {
                List<IDAL.DO.DroneCharge> l = (List<IDAL.DO.DroneCharge>)dalObject.GetDroneCharges(x => x.StationId == id);
                List<IBL.BO.DroneForList> listOfDronesInCharge = new List<IBL.BO.DroneForList>();
                foreach (IDAL.DO.DroneCharge droneCharge in l)
                {
                    listOfDronesInCharge.Add(BLDrones[GetBLDroneIndex(droneCharge.DroneId)]);
                }

                IDAL.DO.Station station = dalObject.GetStation(id);
                IBL.BO.Station resultStation = new IBL.BO.Station
                {
                    Id = station.Id,
                    Name = station.Name,
                    Location = new IBL.BO.Location { Latitude = station.Latitude, Longitude = station.Longitude },
                    ChargeSlots = station.ChargeSlots,
                    ListOfDronesInCharge = listOfDronesInCharge
                };
                return resultStation;
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IBL.BO.IdIsNotExistException(e.ToString());
            }
        }
        //this function view the station details
        public string ViewStations()
        {

        }
        public string ViewStationsList()
        {
            string result = "";
            foreach (var item in GetStations())
            {
                result += item.ToString() + "\n";
            }
            return result;
        }

        private IEnumerable<IBL.BO.StationForList> GetStations()
        {
            List<IDAL.DO.Station> dalStations = dalObject.GetStations().ToList();
            List<IBL.BO.StationForList> resultList = new List<IBL.BO.StationForList>();
            int availableChargingSlots = 0, occupiedChargingSlots = 0;
            foreach (IDAL.DO.Station station in dalStations)
            {
                occupiedChargingSlots = dalObject.GetDroneCharges(x => x.StationId == station.Id).Count();
                availableChargingSlots = station.ChargeSlots - occupiedChargingSlots;
                resultList.Add(new IBL.BO.StationForList
                {
                    Id = station.Id,
                    Name = station.Name,
                    AvailableChargingSlots = availableChargingSlots,
                    OccupiedChargingSlots = occupiedChargingSlots
                });

            }
            return resultList;
        }

    }
}
/*public BL()
       {
           IDal.IDal dalObject = new DalObject.DalObject();
           

           BLDrones = (List<Drone>)dalObject.ViewDronesList();
           List<StationForList> stations = (List<StationForList>)dalObject.ViewStationsList();
           for (int i = 0; i < BLDrones.Count; i++)
           {
               Drone drone = BLDrones[i];
               if (drone.Status != DroneStatuses.Available)
               {
                   drone.Status = (DroneStatuses)rand.Next(0, 2);
                   if (drone.Status == DroneStatuses.Maintenance)
                   {
                       randomStation = stations[rand.Next(0, stations.Count)].;
                       drone.Battery  = rand.NextDouble() * 20;
                   }
                   if (drone.Status == DroneStatuses.Shipping)
                   {

                   }
               }
           }
         }          */