using System;
using System.Collections.Generic;
using System.Linq;
using IBL.BO;
namespace BL
{
    public partial class BL : IBL.IBL
    {

        //this function adds a station to the database
        public void AddStation(int id, string name, int freeChargingSlots, Location location)
        {

            try
            {
                dalObject.AddStation(id, name, freeChargingSlots, location.Longitude, location.Latitude);
            }
            catch (DalObject.IdIsAlreadyExistException e)
            {
                throw new IdIsAlreadyExistException(e.ToString());
            }
            catch (DalObject.NoChargeSlotsException e)
            {
                throw new NoChargeSlotsException(e.ToString());
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
                throw new IdIsNotExistException(e);
            }
        }
        //this function view the station details
        public string ViewStation(int id)
        {
            return GetStation(id).ToString();
        }
        //This function returns a StationForList from the datasource (on BL) by an index.
        private Station GetStation(int id)
        {
            try
            {
                List<IDAL.DO.DroneCharge> l = dalObject.GetDroneCharges(x => x.StationId == id).ToList();
                List<DroneInCharge> listOfDronesInCharge = new List<DroneInCharge>();
                foreach (IDAL.DO.DroneCharge droneCharge in l)
                {
                    DroneForList BLDrone = BLDrones[GetBLDroneIndex(droneCharge.DroneId)];
                    listOfDronesInCharge.Add(new DroneInCharge
                    {
                        DroneId = BLDrone.Id,
                        BatteryStatus = BLDrone.Battery
                    });
                }

                IDAL.DO.Station station = dalObject.GetStation(id);
                Station resultStation = new Station
                {
                    Id = station.Id,
                    Name = station.Name,
                    Location = new Location { Latitude = station.Latitude, Longitude = station.Longitude },
                    FreeChargeSlots = station.FreeChargeSlots,
                    ListOfDronesInCharge = listOfDronesInCharge
                };
                return resultStation;
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IdIsNotExistException(e);
            }
        }
        //this function view the station details
        public string ViewStationsList()
        {
            string result = "";
            foreach (var item in GetStations())
            {
                result += item.ToString() + "\n";
            }
            return result;
        }
        public string ViewStationsWithFreeChargeSlots()
        {
            string result = "";
            foreach (var item in GetStations(x => x.FreeChargeSlots > 0))
            {
                result += item.ToString() + "\n";
            }
            return result;
        }
        public IEnumerable<StationForList> GetStations(Func<IDAL.DO.Station, bool> filter = null)
        {
            List<IDAL.DO.Station> dalStations = dalObject.GetStations(filter).ToList();
            List<StationForList> resultList = new List<StationForList>();
            int occupiedChargingSlots = 0;
            foreach (IDAL.DO.Station station in dalStations)
            {
                occupiedChargingSlots = dalObject.GetDroneCharges(x => x.StationId == station.Id).Count();
                resultList.Add(new StationForList
                {
                    Id = station.Id,
                    Name = station.Name,
                    AvailableChargingSlots = station.FreeChargeSlots,
                    OccupiedChargingSlots = occupiedChargingSlots
                });
            }
            return resultList;
        }

        //This function return the most close station's location to a given loaction
        //check on a filtered stations list if a filter was provided
        private Location GetMostCloseStationLocation(Location location, Func<IDAL.DO.Station, bool> filter = null)
        {
            List<IDAL.DO.Station> stations =
                    dalObject.GetStations(filter).ToList(); // we choose from the available stations.
            if (stations.Count() == 0) return null;
            IDAL.DO.Station mostCloseStation = stations[0];
            double mostCloseDistance = 0;
            double distance = 0;
            foreach (IDAL.DO.Station station in stations)
            {
                Location stationL = new Location { Latitude = station.Latitude, Longitude = station.Longitude };
                distance = Distance(stationL, location);
                if (mostCloseDistance > distance)
                {
                    mostCloseDistance = distance;
                    mostCloseStation = station;
                }
            }
            return new Location { Latitude = mostCloseStation.Latitude, Longitude = mostCloseStation.Longitude };
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