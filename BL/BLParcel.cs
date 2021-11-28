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

        public string ViewParcel(int id)
        {
            return null;
        }

        private IBL.BO.Parcel GetParcel(int id)
        {
            return null;
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

        private IEnumerable<IBL.BO.ParcelForList> GetParcels(Func<IDAL.DO.Parcel, bool> filter = null)
        {
            List<IDAL.DO.Parcel> dalParcels = dalObject.GetParcels(filter).ToList();
            List<IBL.BO.ParcelForList> resultList = new List<IBL.BO.ParcelForList>();
            IBL.BO.Statuses s = new IBL.BO.Statuses();

            foreach (IDAL.DO.Parcel parcel in dalParcels)
            {
                if (parcel.Delivered != null) s = IBL.BO.Statuses.Provided;
                else if(parcel.PickedUp != null) s = IBL.BO.Statuses.Ascribed;
                else if(parcel.Requested != null) s = IBL.BO.Statuses.Collected;
                else if(parcel.Scheduled != null) s = IBL.BO.Statuses.Created;
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
