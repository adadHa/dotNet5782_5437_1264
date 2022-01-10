using System;
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
        public void AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority, int responsibleDrone)
        {
            List<Parcel> list = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            list.Add(
                new Parcel
                {
                    //Id = 
                    SenderId = customerSenderId,
                    TargetId = customerReceiverId,
                    Wheight = (DO.WheightCategories)Enum.Parse(typeof(DO.WheightCategories), weight),
                    Priority = (DO.Priorities)Enum.Parse(typeof(DO.Priorities), priority),
                    DroneId = responsibleDrone,
                });
            XMLTools.SaveListToXMLSerializer<Parcel>(list, ParcelsPath);
        }

        #region getters
        public Parcel GetParcel(int id)
        {
            List<Parcel> list = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            Parcel? parcel = list.Find(x => x.Id == id);
            if (parcel == null)
                return (Parcel)parcel;
            else
                throw new IdIsNotExistException(id, "Parcel");
        }

        public IEnumerable<Parcel> GetParcels(Func<Parcel, bool> filter = null)
        {
            List<Parcel> list = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            return list.Where(filter);
        }

        IEnumerable<Parcel> IDal.ViewUnbindParcels()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region functions: operations oon parcels
        void IDal.BindParcel(int parcelId, int droneId)
        {
            List<Parcel> parcelsList = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            List<Drone> dronesList = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);

            Drone drone = dronesList.Find(x => x.Id == droneId);
            Parcel parcel = parcelsList.Find(x => x.Id == parcelId);

            if (dronesList.FindIndex(x => x.Id == droneId) != -1)
                throw new IdIsNotExistException(droneId, "Drone");
            if (parcelsList.FindIndex(x => x.Id == parcelId) != -1)
                throw new IdIsNotExistException(parcelId, "Parcel");

            parcelsList.Remove(parcel);
            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;
            parcelsList.Add(parcel);
            XMLTools.SaveListToXMLSerializer<Parcel>(parcelsList, ParcelsPath);
        }

        void IDal.CollectParcelByDrone(int droneId, int parcelId)
        {
            List<Parcel> parcelsList = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);

            Parcel parcel = parcelsList.Find(x => x.Id == parcelId);

            if (parcelsList.FindIndex(x => x.Id == parcelId) != -1)
                throw new IdIsNotExistException(parcelId, "Parcel");

            parcelsList.Remove(parcel);
            parcel.PickedUp = DateTime.Now;
            parcelsList.Add(parcel);
            XMLTools.SaveListToXMLSerializer<Parcel>(parcelsList, ParcelsPath);
        }

        void IDal.SupplyParcelToCustomer(int parcelId)
        {
            List<Parcel> parcelsList = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);

            Parcel parcel = parcelsList.Find(x => x.Id == parcelId);

            if (parcelsList.FindIndex(x => x.Id == parcelId) != -1)
                throw new IdIsNotExistException(parcelId, "Parcel");

            parcelsList.Remove(parcel);
            parcel.Delivered = DateTime.Now;
            parcelsList.Add(parcel);
            XMLTools.SaveListToXMLSerializer<Parcel>(parcelsList, ParcelsPath);
        }
        #endregion
    }

}
