﻿using System;
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

        //This function returns the station with the required Id.
        public IDAL.DO.Station ViewStation(int id)
        {
            int index = DataSource.Stations.FindIndex(x => x.Id == id);
            return DataSource.Stations[index];
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
