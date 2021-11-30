using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        //this function adds a parcel to the database
        public void AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority)
        {

            try
            {
                dalObject.AddParcel(customerSenderId, customerReceiverId, weight, priority, -1);

            }
            catch (DalObject.IdIsAlreadyExistException e)
            {
                throw new IBL.BO.IdIsAlreadyExistException(e.ToString());
            }
        }
        //this function view the parcel details
        public string ViewParcel(int id)
        {
            return GetParcel(id).ToString();
        }

        //This function returns a ParcelForList from the datasource (on BL) by an index.
        private IBL.BO.Parcel GetParcel(int id)
        {
            try
            {
                IDAL.DO.Parcel dalParcel = dalObject.GetParcel(id);
                IBL.BO.DroneInParcel drone = null;
                if (dalParcel.DroneId != -1)
                {
                    IBL.BO.DroneForList blDrone = BLDrones[GetBLDroneIndex(dalParcel.DroneId)];
                    drone = new IBL.BO.DroneInParcel
                    {
                        Id = blDrone.Id,
                        BatteryStatus = blDrone.Battery,
                        CurrentLocation = blDrone.Location
                    };
                }
                
                IBL.BO.Parcel parcel = new IBL.BO.Parcel
                {
                    Id = dalParcel.Id,
                    Target = new IBL.BO.CustomerInDelivery { Id = dalParcel.TargetId, Name = dalObject.GetCustomer(dalParcel.TargetId).Name },
                    Sender = new IBL.BO.CustomerInDelivery { Id = dalParcel.SenderId, Name = dalObject.GetCustomer(dalParcel.SenderId).Name },
                    Wheight = (IBL.BO.WheightCategories)dalParcel.Wheight,
                    Priority = (IBL.BO.Priorities)dalParcel.Priority,
                    Drone = drone,
                    Created = dalParcel.Created,
                    Scheduled = dalParcel.Scheduled,
                    PickedUp = dalParcel.PickedUp,
                    Delivered = dalParcel.Delivered
                };
                return parcel;
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IBL.BO.IdIsNotExistException(e);
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

        public IEnumerable<IBL.BO.ParcelForList> GetParcels(Func<IDAL.DO.Parcel, bool> filter = null)
        {
            List<IDAL.DO.Parcel> dalParcels = dalObject.GetParcels(filter).ToList();
            List<IBL.BO.ParcelForList> resultList = new List<IBL.BO.ParcelForList>();
            IBL.BO.Statuses s = new IBL.BO.Statuses();

            foreach (IDAL.DO.Parcel parcel in dalParcels)
            {
                if (parcel.Delivered != null) s = IBL.BO.Statuses.Delivered;
                else if(parcel.PickedUp != null) s = IBL.BO.Statuses.PickedUp;
                else if(parcel.Scheduled != null) s = IBL.BO.Statuses.Scheduled;
                else if(parcel.Created != null) s = IBL.BO.Statuses.Created;
                resultList.Add(new IBL.BO.ParcelForList
                {
                    Id = parcel.Id,
                    SenderName = dalObject.GetCustomer(parcel.SenderId).Name,
                    ReceiverName = dalObject.GetCustomer(parcel.TargetId).Name,
                    MaxWeight = (IBL.BO.WheightCategories)parcel.Wheight,
                    Priority = (IBL.BO.Priorities)parcel.Priority,
                    ParcelStatus =  s
                });
            }
            return resultList;
        }

    }
}
