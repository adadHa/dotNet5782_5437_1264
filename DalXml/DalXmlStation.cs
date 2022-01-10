﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
namespace DalXml
{
    public sealed partial class DalXml : IDal
    {
        void IDal.AddStation(int id, string name, int num, double longitude, double latitude)
        {
            List<Station> list = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            if (list.FindIndex(x => x.Id == id) != -1)
                throw new IdIsAlreadyExistException(id, "Drone");
            list.Add(
                new Station
                {
                    Id = id,
                    Name = name,
                    FreeChargeSlots = num,
                    Longitude = longitude,
                    Latitude = latitude
                });
            XMLTools.SaveListToXMLSerializer<Station>(list, StationsPath);
        }
        void IDal.UpdateStationName(int id, string newName)
        {
            List<Station> list = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            if (list.FindIndex(x => x.Id == id) != -1)
                throw new IdIsNotExistException(id, "Station");
            Station station = list.Find(x => x.Id == id);
            list.Remove(station);
            station.Name = newName;
            list.Add(station);

            XMLTools.SaveListToXMLSerializer<Station>(list, StationsPath);
        }

        void IDal.UpdateStationChargeSlotsCap(int id, int newNum)
        {
            List<Station> list = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            if (list.FindIndex(x => x.Id == id) != -1)
                throw new IdIsNotExistException(id, "Station");
            Station station = list.Find(x => x.Id == id);
            list.Remove(station);
            station.FreeChargeSlots = newNum;
            list.Add(station);

            XMLTools.SaveListToXMLSerializer<Station>(list, StationsPath);
        }

        #region get functions
        Station IDal.GetStation(int id)
        {
            List<Station> list = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            Station? station = list.Find(x => x.Id == id);
            if (station == null)
                return (Station)station;
            else
                throw new IdIsNotExistException(id, "Station");
        }

        IEnumerable<Station> IDal.GetStations(Func<Station, bool> filter)
        {
            List<Station> list = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            return list.Where(filter);
        }
        #endregion

        IEnumerable<Station> IDal.ViewStationsList()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Station> IDal.ViewStationsWithFreeChargeSlots()
        {
            throw new NotImplementedException();
        }

        string IDal.ViewStation(int id)
        {
            throw new NotImplementedException();
        }
    }

}
