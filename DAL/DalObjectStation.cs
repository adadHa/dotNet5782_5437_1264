using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
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
                DataSource.Stations.Add(new IDAL.DO.Station()
                {
                    Id = id,
                    Name = name,
                    ChargeSlots = num,
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
                IDAL.DO.Station s = DataSource.Stations[index];
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
                IDAL.DO.Station s = DataSource.Stations[index];
                s.ChargeSlots = newNum;
                DataSource.Stations[index] = s;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function returns a copy of the stations list.
        public IEnumerable<IDAL.DO.Station> ViewStationsList()
        {
            List<IDAL.DO.Station> resultList = new List<IDAL.DO.Station>();
            foreach (IDAL.DO.Station station in DataSource.Stations)
            {
                IDAL.DO.Station s = new IDAL.DO.Station();
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
        public IDAL.DO.Station GetStation(int id)
        {
            int index = DataSource.Stations.FindIndex(x => x.Id == id);
            if (index == -1)
            {
                throw new IdIsNotExistException(id, "Station");
            }
            return DataSource.Stations[index];
        }

        //This function returns a filtered copy of the Stations list (according to a given predicate)
        public IEnumerable<IDAL.DO.Station> GetStations(Func<IDAL.DO.Station, bool> filter = null)
        {
            return DataSource.Stations.Where(filter).ToList();
        }

        //This function returns a filtered copy of the Drone Charges list (according to a given predicate)
        public IEnumerable<IDAL.DO.DroneCharge> GetDroneCharges(Func<IDAL.DO.DroneCharge, bool> filter = null)
        {
            return DataSource.DroneCharges.Where(filter).ToList();
        }

        public IEnumerable<IDAL.DO.Station> ViewStationsWithFreeChargeSlots()
        {
            // create the result list
            List<IDAL.DO.Station> resultList = new List<IDAL.DO.Station>();
            foreach (IDAL.DO.Station station in DataSource.Stations)
            {
                if (station.ChargeSlots > 0)
                {
                    IDAL.DO.Station s = new IDAL.DO.Station();
                    s = station;
                    resultList.Add(s);
                }
            }
            return resultList;
        }

    }
}
