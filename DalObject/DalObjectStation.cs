using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace Dal
{
    public partial class DalObject : DalApi.IDal
    {
        // This function add a station to the stations data base.
        public void AddStation(int id, string name, int num, double longitude, double latitude)
        {
            try
            {
                if (DataSource.Stations.FindIndex(x => x.Id == id) != -1)
                {
                    throw new IdIsAlreadyExistException(id, "Station");
                }
                DataSource.Stations.Add(new DO.Station()
                {
                    Id = id,
                    Name = name,
                    FreeChargeSlots = num,
                    Longitude = longitude,
                    Latitude = latitude
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        //This function updates a station with a new name.
        public void UpdateStationName(int id, string newName)
        {
            try
            {
                int index = DataSource.Stations.FindIndex(x => x.Id == id);
                if (DataSource.Stations.FindIndex(x => x.Id == id) == -1)
                {
                    throw new IdIsNotExistException(id, "Station");
                }
                DO.Station s = DataSource.Stations[index];
                s.Name = newName;
                DataSource.Stations[index] = s;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function updates a station with a new charging slots capacity.
        public void UpdateStationChargeSlotsCap(int id, int newNum)
        {
            try
            {
                int index = DataSource.Stations.FindIndex(x => x.Id == id);
                if (DataSource.Stations.FindIndex(x => x.Id == id) == -1)
                {
                    throw new IdIsNotExistException(id, "Station");
                }
                DO.Station s = DataSource.Stations[index];
                s.FreeChargeSlots = newNum;
                DataSource.Stations[index] = s;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function returns a copy of the stations list.
        public IEnumerable<DO.Station> ViewStationsList()
        {
            List<DO.Station> resultList = new List<DO.Station>();
            foreach (DO.Station station in DataSource.Stations)
            {
                DO.Station s = new DO.Station();
                s = station;
                resultList.Add(s);
            }
            return resultList;
        }

        //This function returns the string of the station with the required Id.
        public string ViewStation(int id)
        {
            return GetStation(id).ToString();
        }

        //This function returns the station with the required Id.
        public DO.Station GetStation(int id)
        {
            int index = DataSource.Stations.FindIndex(x => x.Id == id);
            if (index == -1)
            {
                throw new IdIsNotExistException(id, "Station");
            }
            return DataSource.Stations[index];
        }

        //This function returns a filtered copy of the Stations list (according to a given predicate)
        public IEnumerable<DO.Station> GetStations(Func<DO.Station, bool> filter = null)
        {
            if (filter == null)
            {
                return DataSource.Stations;
            }
            return DataSource.Stations.Where(filter);
        }

        //This function returns a filtered copy of the Drone Charges list (according to a given predicate)
        public IEnumerable<DO.DroneCharge> GetDroneCharges(Func<DO.DroneCharge, bool> filter = null)
        {
            if (filter == null)
            {
                return DataSource.DroneCharges;
            }
            return DataSource.DroneCharges.Where(filter);
        }

        public IEnumerable<DO.Station> ViewStationsWithFreeChargeSlots()
        {
            // create the result list
            List<DO.Station> resultList = new List<DO.Station>();
            foreach (DO.Station station in DataSource.Stations)
            {
                if (station.FreeChargeSlots > 0)
                {
                    DO.Station s = new DO.Station();
                    s = station;
                    resultList.Add(s);
                }
            }
            return resultList;
        }

    }
}
