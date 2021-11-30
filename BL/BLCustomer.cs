using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        //this function adds a customer to the database
        public void AddCustomer(int id, string name, string phoneNumber)
        {
            try
            {
                dalObject.AddCustomer(id, name, phoneNumber, 0, 0);

            }
            catch (DalObject.IdIsAlreadyExistException e)
            {
                throw new IBL.BO.IdIsAlreadyExistException(e.ToString());
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
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IBL.BO.IdIsNotExistException(e);
            }
        }
        //this function view the customer details
        public string ViewCustomer(int id)
        {
            return GetCustomer(id).ToString();
        }

        //This function returns a CustomerForList from the datasource (on BL) by an index.
        private IBL.BO.Customer GetCustomer(int id)
        {
            try
            {
                IDAL.DO.Customer dalCustomer = dalObject.GetCustomer(id);
                //build the parcelsFromCustomer list
                List<IBL.BO.ParcelInCustomer> parcelsFromCustomer = new List<IBL.BO.ParcelInCustomer>();
                List<IDAL.DO.Parcel> dalParcelsFromCustomer = dalObject.GetParcels(x => x.SenderId == id).ToList();
                IBL.BO.Statuses parcelStatus = new IBL.BO.Statuses();
                foreach (IDAL.DO.Parcel parcel in dalParcelsFromCustomer)
                {

                    if (parcel.Delivered != null) parcelStatus = IBL.BO.Statuses.Delivered;
                    else if (parcel.PickedUp != null) parcelStatus = IBL.BO.Statuses.PickedUp;
                    else if (parcel.Scheduled != null) parcelStatus = IBL.BO.Statuses.Scheduled;
                    else if (parcel.Created != null) parcelStatus = IBL.BO.Statuses.Created;
                    parcelsFromCustomer.Add(new IBL.BO.ParcelInCustomer
                    {
                        Id = parcel.Id,
                        Priority = (IBL.BO.Priorities)parcel.Priority,
                        Wheight = (IBL.BO.WheightCategories)parcel.Wheight,
                        ParcelStatus = parcelStatus,
                        TheOtherSide = new IBL.BO.CustomerInDelivery { Id = parcel.TargetId, Name = dalObject.GetCustomer(parcel.TargetId).Name}
                    });
                }

                //build the parcelsListToCustomer list
                List<IBL.BO.ParcelInCustomer> parcelsToCustomer = new List<IBL.BO.ParcelInCustomer>();
                List<IDAL.DO.Parcel> dalParcelsToCustomer = dalObject.GetParcels(x => x.TargetId == id).ToList();
                foreach (IDAL.DO.Parcel parcel in dalParcelsToCustomer)
                {
                    if (parcel.Delivered != null) parcelStatus = IBL.BO.Statuses.Delivered;
                    else if (parcel.PickedUp != null) parcelStatus = IBL.BO.Statuses.PickedUp;
                    else if (parcel.Scheduled != null) parcelStatus = IBL.BO.Statuses.Scheduled;
                    else if (parcel.Created != null) parcelStatus = IBL.BO.Statuses.Created;
                    parcelsToCustomer.Add(new IBL.BO.ParcelInCustomer
                    {
                        Id = parcel.Id,
                        Priority = (IBL.BO.Priorities)parcel.Priority,
                        Wheight = (IBL.BO.WheightCategories)parcel.Wheight,
                        ParcelStatus = parcelStatus,
                        TheOtherSide = new IBL.BO.CustomerInDelivery { Id = parcel.SenderId, Name = dalObject.GetCustomer(parcel.SenderId).Name }
                    });
                }
                //finally, build the customer
                IBL.BO.Customer customer = new IBL.BO.Customer
                {
                    Id = dalCustomer.Id,
                    Name = dalCustomer.Name,
                    Phone = dalCustomer.Phone,
                    Location = new IBL.BO.Location { Latitude = dalCustomer.Latitude, Longitude = dalCustomer.Longitude },
                    ParcelsFromCustomer = parcelsFromCustomer,
                    ParcelsToCustomer = parcelsToCustomer
                };
                return customer;
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IBL.BO.IdIsNotExistException(e);
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

        public IEnumerable<IBL.BO.CustomerForList> GetCustomers(Func<IDAL.DO.Customer, bool> filter = null)
        {
            List<IDAL.DO.Customer> dalCustomers = dalObject.GetCustomers().ToList();
            List<IBL.BO.CustomerForList> resultList = new List<IBL.BO.CustomerForList>();
            int counterSendAndSupplied = 0, counterSendAndNotSupplied = 0,
                counterRecievedParcels = 0, counterParcelsOnMyWay = 0;
            foreach (IDAL.DO.Customer customer in dalCustomers)
            {
                counterSendAndSupplied = dalObject.GetParcels(x => x.SenderId == customer.Id && x.Delivered != null).Count();
                counterSendAndNotSupplied = dalObject.GetParcels(x => x.SenderId == customer.Id && x.Delivered == null).Count();
                counterRecievedParcels = dalObject.GetParcels(x => x.TargetId == customer.Id && x.Delivered != null).Count();
                counterParcelsOnMyWay = dalObject.GetParcels(x => x.TargetId == customer.Id && x.Delivered == null).Count();
                resultList.Add(new IBL.BO.CustomerForList
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