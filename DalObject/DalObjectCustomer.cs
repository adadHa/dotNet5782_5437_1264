using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using DalApi;

namespace Dal
{
    public partial class DalObject : DalApi.IDal
    {


        // This function add a customer to the customers data base.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string name, string phoneNumber, double longitude, double latitude)
        {
            try
            {
                if (DataSource.Customers.FindIndex(x => x.Id == id) != -1)
                {
                    throw new IdIsAlreadyExistException(id, $"Customer {name}");
                }
                DataSource.Customers.Add(new DO.Customer()
                {
                    Id = id,
                    Name = name,
                    Phone = phoneNumber,
                    Longitude = longitude,
                    Latitude = latitude
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function updates a customer with a new name.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerName(int id, string newName)
        {
            try
            {
                int index = DataSource.Customers.FindIndex(x => x.Id == id);
                if (DataSource.Customers.FindIndex(x => x.Id == id) == -1)
                {
                    throw new IdIsNotExistException(id, "Station");
                }
                DO.Customer c = DataSource.Customers[index];
                c.Name = newName;
                DataSource.Customers[index] = c;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function updates a customer with a new name.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomrePhoneNumber(int id, string newPhoneNumber)
        {
            try
            {
                int index = DataSource.Customers.FindIndex(x => x.Id == id);
                if (DataSource.Customers.FindIndex(x => x.Id == id) == -1)
                {
                    throw new IdIsNotExistException(id, "Station");
                }
                DO.Customer c = DataSource.Customers[index];
                c.Phone = newPhoneNumber;
                DataSource.Customers[index] = c;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function returns a copy of the customers list.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Customer> ViewCustomersList()
        {
            List<DO.Customer> resultList = new List<DO.Customer>();
            foreach (DO.Customer customer in DataSource.Customers)
            {
                DO.Customer c = new DO.Customer();
                c = customer;
                resultList.Add(c);
            }
            return resultList;
        }

        //This function returns the customer with the required Id.
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string ViewCustomer(int id)
        {
            return ViewCustomer(id).ToString();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Customer GetCustomer(int id)
        {
            int index = DataSource.Customers.FindIndex(x => x.Id == id);
            if (index == -1)
            {
                throw new IdIsNotExistException(id, $"Customer");
            }
            return DataSource.Customers[index];
        }

        //This function returns a filtered copy of the Customers list (according to a given predicate)
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Customer> GetCustomers(Func<DO.Customer, bool> filter = null)
        {
            if (filter == null)
            {
                return DataSource.Customers;
            }
                return DataSource.Customers.Where(filter);
        }
    }
}