using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    internal sealed partial class BL : BlApi.IBL
    {
        //this function adds a customer to the database
        public void AddCustomer(int id, string name, string phoneNumber,BO.Location location)
        {
            try
            {
                dalObject.AddCustomer(id, name, phoneNumber, location.Longitude, location.Latitude);

            }
            catch (DalApi.IdIsAlreadyExistException e)
            {
                throw new BO.IdIsAlreadyExistException(e.ToString());
            }
        }
        //this function updates the customer
        public void UpdateCustomer(int id, string newName, string newPhoneNumber)
        {
            try
            {
                if (newName != "") // check if there was an input for this value
                {
                    dalObject.UpdateCustomerName(id, newName);
                }
                if (newPhoneNumber != "") // check if there was an input for this value
                {
                    dalObject.UpdateCustomrePhoneNumber(id, newPhoneNumber);
                }
            }
            catch (DalApi.IdIsNotExistException e)
            {
                throw new BO.IdIsNotExistException(e);
            }
        }
        //this function view the customer details
        public string ViewCustomer(int id)
        {
            return GetCustomer(id).ToString();
        }

        //This function returns a CustomerForList from the datasource (on BL) by an index.
        private BO.Customer GetCustomer(int id)
        {
            try
            {
                DO.Customer dalCustomer = dalObject.GetCustomer(id);
                //build the parcelsFromCustomer list
                List<BO.ParcelInCustomer> parcelsFromCustomer = new List<BO.ParcelInCustomer>();
                List<DO.Parcel> dalParcelsFromCustomer = dalObject.GetParcels(x => x.SenderId == id).ToList();
                BO.Statuses parcelStatus = new BO.Statuses();
                foreach (DO.Parcel parcel in dalParcelsFromCustomer)
                {

                    if (parcel.Delivered != null) parcelStatus = BO.Statuses.Delivered;
                    else if (parcel.PickedUp != null) parcelStatus = BO.Statuses.PickedUp;
                    else if (parcel.Scheduled != null) parcelStatus = BO.Statuses.Scheduled;
                    else if (parcel.Created != null) parcelStatus = BO.Statuses.Created;
                    parcelsFromCustomer.Add(new BO.ParcelInCustomer
                    {
                        Id = parcel.Id,
                        Priority = (BO.Priorities)parcel.Priority,
                        Wheight = (BO.WheightCategories)parcel.Wheight,
                        ParcelStatus = parcelStatus,
                        TheOtherSide = new BO.CustomerInDelivery { Id = parcel.TargetId, Name = dalObject.GetCustomer(parcel.TargetId).Name}
                    });
                }

                //build the parcelsListToCustomer list
                List<BO.ParcelInCustomer> parcelsToCustomer = new List<BO.ParcelInCustomer>();
                List<DO.Parcel> dalParcelsToCustomer = dalObject.GetParcels(x => x.TargetId == id).ToList();
                foreach (DO.Parcel parcel in dalParcelsToCustomer)
                {
                    if (parcel.Delivered != null) parcelStatus = BO.Statuses.Delivered;
                    else if (parcel.PickedUp != null) parcelStatus = BO.Statuses.PickedUp;
                    else if (parcel.Scheduled != null) parcelStatus = BO.Statuses.Scheduled;
                    else if (parcel.Created != null) parcelStatus = BO.Statuses.Created;
                    parcelsToCustomer.Add(new BO.ParcelInCustomer
                    {
                        Id = parcel.Id,
                        Priority = (BO.Priorities)parcel.Priority,
                        Wheight = (BO.WheightCategories)parcel.Wheight,
                        ParcelStatus = parcelStatus,
                        TheOtherSide = new BO.CustomerInDelivery { Id = parcel.SenderId, Name = dalObject.GetCustomer(parcel.SenderId).Name }
                    });
                }
                //finally, build the customer
                BO.Customer customer = new BO.Customer
                {
                    Id = dalCustomer.Id,
                    Name = dalCustomer.Name,
                    Phone = dalCustomer.Phone,
                    Location = new BO.Location { Latitude = dalCustomer.Latitude, Longitude = dalCustomer.Longitude },
                    ParcelsFromCustomer = parcelsFromCustomer,
                    ParcelsToCustomer = parcelsToCustomer
                };
                return customer;
            }
            catch (DalApi.IdIsNotExistException e)
            {
                throw new BO.IdIsNotExistException(e);
            }

        }

        public string ViewCustomersList()
        {
            string result = "";
            foreach (var item in GetCustomers())
            {
                result += item.ToString() + "\n";
            }
            return result;
        }

        public IEnumerable<BO.CustomerForList> GetCustomers(Func<DO.Customer, bool> filter = null)
        {
            List<DO.Customer> dalCustomers = dalObject.GetCustomers().ToList();
            List<BO.CustomerForList> resultList = new List<BO.CustomerForList>();
            int counterSendAndSupplied = 0, counterSendAndNotSupplied = 0,
                counterRecievedParcels = 0, counterParcelsOnMyWay = 0;
            foreach (DO.Customer customer in dalCustomers)
            {
                counterSendAndSupplied = dalObject.GetParcels(x => x.SenderId == customer.Id && x.Delivered != null).Count();
                counterSendAndNotSupplied = dalObject.GetParcels(x => x.SenderId == customer.Id && x.Delivered == null).Count();
                counterRecievedParcels = dalObject.GetParcels(x => x.TargetId == customer.Id && x.Delivered != null).Count();
                counterParcelsOnMyWay = dalObject.GetParcels(x => x.TargetId == customer.Id && x.Delivered == null).Count();
                resultList.Add(new BO.CustomerForList
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    CounterSendAndSupplied = counterSendAndSupplied,
                    CounterSendAndNotSupplied = counterSendAndNotSupplied,
                    CounterRecievedParcels = counterRecievedParcels,
                    CounterParcelsOnMyWay = counterParcelsOnMyWay
                });
            }
            return resultList;
        }

    }
}