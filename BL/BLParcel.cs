using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    internal sealed partial class BL : BlApi.IBL
    {
        //this function adds a parcel to the database
        public void AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority)
        {

            try
            {
                dalObject.AddParcel(customerSenderId, customerReceiverId, weight, priority, -1);

            }
            catch (DalApi.IdIsAlreadyExistException e)
            {
                throw new BO.IdIsAlreadyExistException(e.ToString());
            }
        }
        //this function view the parcel details
        public string ViewParcel(int id)
        {
            return GetParcel(id).ToString();
        }

        //This function returns a ParcelForList from the datasource (on BL) by an index.
        public BO.Parcel GetParcel(int id)
        {
            try
            {
                DO.Parcel dalParcel = dalObject.GetParcel(id);
                BO.DroneInParcel drone = null;
                if (dalParcel.DroneId != -1)
                {
                    BO.DroneForList blDrone = BLDrones[GetBLDroneIndex(dalParcel.DroneId)];
                    drone = new BO.DroneInParcel
                    {
                        Id = blDrone.Id,
                        BatteryStatus = blDrone.Battery,
                        CurrentLocation = blDrone.Location
                    };
                }
                
                BO.Parcel parcel = new BO.Parcel
                {
                    Id = dalParcel.Id,
                    Target = new BO.CustomerInDelivery { Id = dalParcel.TargetId, Name = dalObject.GetCustomer(dalParcel.TargetId).Name },
                    Sender = new BO.CustomerInDelivery { Id = dalParcel.SenderId, Name = dalObject.GetCustomer(dalParcel.SenderId).Name },
                    Wheight = (BO.WheightCategories)dalParcel.Wheight,
                    Priority = (BO.Priorities)dalParcel.Priority,
                    Drone = drone,
                    Created = dalParcel.Created,
                    Scheduled = dalParcel.Scheduled,
                    PickedUp = dalParcel.PickedUp,
                    Delivered = dalParcel.Delivered
                };
                return parcel;
            }
            catch (DalApi.IdIsNotExistException e)
            {
                throw new BO.IdIsNotExistException(e);
            }
        }

        public string ViewParcelsList()
        {
            string result = "";
            foreach (var item in GetParcels())
            {
                result += item.ToString() + "\n";
            }
            return result;
        }

        public string ViewUnbinedParcelsList()
        {
            string result = "";
            foreach (var item in GetParcels(x=>x.DroneId == -1))
            {
                result += item.ToString() + "\n";
            }
            return result;
        }

        public IEnumerable<BO.ParcelForList> GetParcels(Func<DO.Parcel, bool> filter = null)
        {
            List<DO.Parcel> dalParcels = dalObject.GetParcels(filter).ToList();
            List<BO.ParcelForList> resultList = new List<BO.ParcelForList>();
            BO.Statuses s = new BO.Statuses();

            foreach (DO.Parcel parcel in dalParcels)
            {
                if (parcel.Delivered != null) s = BO.Statuses.Delivered;
                else if(parcel.PickedUp != null) s = BO.Statuses.PickedUp;
                else if(parcel.Scheduled != null) s = BO.Statuses.Scheduled;
                else if(parcel.Created != null) s = BO.Statuses.Created;
                resultList.Add(new BO.ParcelForList
                {
                    Id = parcel.Id,
                    SenderName = dalObject.GetCustomer(parcel.SenderId).Name,
                    ReceiverName = dalObject.GetCustomer(parcel.TargetId).Name,
                    MaxWeight = (BO.WheightCategories)parcel.Wheight,
                    Priority = (BO.Priorities)parcel.Priority,
                    ParcelStatus =  s
                });
            }
            return resultList;
        }

    }
}
