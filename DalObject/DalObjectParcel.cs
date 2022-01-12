using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using System.Runtime.CompilerServices;
namespace Dal
{
    public partial class DalObject : DalApi.IDal
    {
        // This function add a parcel to the parcels data base.
        [MethodImpl(MethodImplOptions.Synchronized)]
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
                if (responsibleDrone != -1 && DataSource.Drones.FindIndex(x => x.Id == responsibleDrone) == -1)
                {
                    throw new IdIsNotExistException(responsibleDrone, "Responsible drone");
                }
                DataSource.Parcels.Add(new DO.Parcel()
                {
                    Id = DataSource.Parcels.Count,
                    SenderId = customerSenderId,
                    TargetId = customerReceiverId,
                    Wheight = (DO.WheightCategories)Enum.Parse(typeof(DO.WheightCategories), weight),
                    Priority = (DO.Priorities)Enum.Parse(typeof(DO.Priorities), priority),
                    DroneId = responsibleDrone,
                    Created = DateTime.Now,
                    Scheduled = null,
                    PickedUp = null,
                    Delivered = null
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function binds a parcel to a drone.
        [MethodImpl(MethodImplOptions.Synchronized)]
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
                DO.Drone d = DataSource.Drones[droneIndex];
                DO.Parcel p = DataSource.Parcels[parcelIndex];
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CollectParcelByDrone(int droneId, int parcelId)
        {
            try
            {
                if (DataSource.Parcels.FindIndex(x => x.Id == parcelId) == -1)
                {
                    throw new IdIsNotExistException(parcelId, "Parcel");
                }
                int parcelIndex = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
                DO.Parcel p = DataSource.Parcels[parcelIndex];
                p.PickedUp = DateTime.Now;
                DataSource.Parcels[parcelIndex] = p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This funtion supplies a parcel to the customer.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SupplyParcelToCustomer(int parcelId)
        {
            try
            {
                if (DataSource.Parcels.FindIndex(x => x.Id == parcelId) == -1)
                {
                    throw new IdIsNotExistException(parcelId, "Parcel");
                }
                int deliveredParcelIndex = DataSource.Parcels.FindIndex(x => x.Id == parcelId); ;
                DO.Parcel p = DataSource.Parcels[deliveredParcelIndex];
                p.Delivered = DateTime.Now;
                DataSource.Parcels[deliveredParcelIndex] = p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function returns a copy of the parcels list.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Parcel> ViewParcelsList()
        {
            List<DO.Parcel> resultList = new List<DO.Parcel>();
            foreach (DO.Parcel parcel in DataSource.Parcels)
            {
                DO.Parcel p = new DO.Parcel();
                p = parcel;
                resultList.Add(p);
            }
            return resultList;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Parcel> ViewUnbindParcels()
        {
            // create the result list
            List<DO.Parcel> resultList = new List<DO.Parcel>();
            foreach (DO.Parcel parcel in DataSource.Parcels)
            {
                if (parcel.Scheduled != null)
                {
                    DO.Parcel p = new DO.Parcel();
                    p = parcel;
                    resultList.Add(p);
                }
            }
            return resultList;
        }

        //This function returns the string of the parcel with the required Id.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string ViewParcel(int id)
        {
            return GetParcel(id).ToString();
        }

        //This function returns the parcel with the required Id.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Parcel GetParcel(int id)
        {
            if (DataSource.Parcels.FindIndex(x => x.Id == id) == -1)
            {
                throw new IdIsNotExistException(id, "Parcel");
            }
            int index = DataSource.Parcels.FindIndex(x => x.Id == id);
            return DataSource.Parcels[index];
        }

        //This function returns a filtered copy of the Parcels list (according to a given predicate)
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Parcel> GetParcels(Func<DO.Parcel, bool> filter = null)
        {
            if (filter == null)
            {
                return DataSource.Parcels;
            }
            return DataSource.Parcels.Where(filter);
        }

    }
}
