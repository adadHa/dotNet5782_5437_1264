using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        // This function add a parcel to the parcels data base.
        public void AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority, int responsibleDrone)
        {
            try
            {
                if (DataSource.Customers.FindIndex(x => x.Id == customerSenderId) == -1)
                {
                    throw new IdIsNotExistException(customerSenderId, "Sender");
                }
                if (DataSource.Customers.FindIndex(x => x.Id == customerReceiverId) == -1)
                {
                    throw new IdIsNotExistException(customerReceiverId, "Receiver");
                }
                if (responsibleDrone != 0 && DataSource.Drones.FindIndex(x => x.Id == responsibleDrone) == -1)
                {
                    throw new IdIsNotExistException(responsibleDrone, "Responsible drone");
                }
                DateTime d = new DateTime();
                DataSource.Parcels.Add(new IDAL.DO.Parcel()
                {
                    Id = DataSource.Parcels.Count,
                    SenderId = customerSenderId,
                    TargetId = customerReceiverId,
                    Wheight = (IDAL.DO.WheightCategories)Enum.Parse(typeof(IDAL.DO.WheightCategories), weight),
                    Priority = (IDAL.DO.Priorities)Enum.Parse(typeof(IDAL.DO.Priorities), priority),
                    DroneId = responsibleDrone,
                    Requested = DateTime.Now,
                    Scheduled = d,
                    PickedUp = d,
                    Delivered = d
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function binds a parcel to a drone.
        public void BindParcel(int parcelId, int droneId)
        {
            try
            {
                if (DataSource.Drones.FindIndex(x => x.Id == droneId) == -1)
                {
                    throw new IdIsNotExistException(droneId, "Drone");
                }
                if (DataSource.Parcels.FindIndex(x => x.Id == parcelId) == -1)
                {
                    throw new IdIsNotExistException(parcelId, "Parcel");
                }
                int droneIndex = DataSource.Drones.FindIndex(x => x.Id == droneId);
                int parcelIndex = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
                IDAL.DO.Drone d = DataSource.Drones[droneIndex];
                IDAL.DO.Parcel p = DataSource.Parcels[parcelIndex];
                //d.Status = IDAL.DO.DroneStatuses.Shipping;
                p.DroneId = droneId;
                p.Scheduled = DateTime.Now;
                DataSource.Drones[droneIndex] = d;
                DataSource.Parcels[parcelIndex] = p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function collects a parcel by a drone (Its status was already changed to "shipping" on the bind function).
        public void CollectParcelByDrone(int parcelId)
        {
            try
            {
                if (DataSource.Parcels.FindIndex(x => x.Id == parcelId) == -1)
                {
                    throw new IdIsNotExistException(parcelId, "Parcel");
                }
                int parcelIndex = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
                IDAL.DO.Parcel p = DataSource.Parcels[parcelIndex];
                p.PickedUp = DateTime.Now;
                DataSource.Parcels[parcelIndex] = p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This funtion supplies a parcel to the customer.
        public void SupplyParcelToCustomer(int parcelId)
        {
            try
            {
                if (DataSource.Parcels.FindIndex(x => x.Id == parcelId) == -1)
                {
                    throw new IdIsNotExistException(parcelId, "Parcel");
                }
                int deliveredParcelIndex = DataSource.Parcels.FindIndex(x => x.Id == parcelId); ;
                IDAL.DO.Parcel p = DataSource.Parcels[deliveredParcelIndex];
                p.Delivered = DateTime.Now;
                DataSource.Parcels[deliveredParcelIndex] = p;
                int droneId = DataSource.Parcels[deliveredParcelIndex].DroneId;
                int droneIndex = DataSource.Drones.FindIndex(x => x.Id == droneId);
                IDAL.DO.Drone d = DataSource.Drones[droneIndex];
                //d.Status = IDAL.DO.DroneStatuses.Available;
                DataSource.Drones[droneIndex] = d;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function returns a copy of the parcels list.
        public IEnumerable<IDAL.DO.Parcel> ViewParcelsList()
        {
            List<IDAL.DO.Parcel> resultList = new List<IDAL.DO.Parcel>();
            foreach (IDAL.DO.Parcel parcel in DataSource.Parcels)
            {
                IDAL.DO.Parcel p = new IDAL.DO.Parcel();
                p = parcel;
                resultList.Add(p);
            }
            return resultList;
        }

        public IEnumerable<IDAL.DO.Parcel> ViewUnbindParcels()
        {
            // create the result list
            DateTime defaultDateTime = new DateTime();
            List<IDAL.DO.Parcel> resultList = new List<IDAL.DO.Parcel>();
            foreach (IDAL.DO.Parcel parcel in DataSource.Parcels)
            {
                if (parcel.Scheduled != defaultDateTime)
                {
                    IDAL.DO.Parcel p = new IDAL.DO.Parcel();
                    p = parcel;
                    resultList.Add(p);
                }
            }
            return resultList;
        }

        //This function returns the string of the parcel with the required Id.
        public string ViewParcel(int id)
        {
            return GetParcel(id).ToString();
        }

        //This function returns the parcel with the required Id.
        public IDAL.DO.Parcel GetParcel(int id)
        {
            if (DataSource.Parcels.FindIndex(x => x.Id == id) == -1)
            {
                throw new IdIsNotExistException(id, "Parcel");
            }
            int index = DataSource.Parcels.FindIndex(x => x.Id == id);
            return DataSource.Parcels[index];
        }

        //This function returns a filtered copy of the Parcels list (according to a given predicate)
        public IEnumerable<IDAL.DO.Parcel> GetParcels(Func<IDAL.DO.Parcel, bool> filter = null)
        {
            return DataSource.Parcels.Where(filter);
        }

    }
}
