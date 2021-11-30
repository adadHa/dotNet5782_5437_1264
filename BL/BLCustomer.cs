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
            return null;
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

        private IEnumerable<IBL.BO.CustomerForList> GetCustomers()
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