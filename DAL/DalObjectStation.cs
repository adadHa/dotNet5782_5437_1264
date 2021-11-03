using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject
    {
        // This function add a station to the stations data base.
        public void AddStation(int id, string name, int num, double longitude, double latitude)
        {
            DataSource.Stations.Add(new IDAL.DO.Station()
            {
                Id = id,
                Name = name,
                ChargeSlots = num,
                Longitude = longitude,
                Latitude = latitude
            });
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
