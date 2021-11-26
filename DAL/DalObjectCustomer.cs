using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        // This function add a customer to the customers data base.
        public void AddCustomer(int id, string name, string phoneNumber, double longitude, double latitude)
        {
            try
            {
                if (DataSource.Customers.FindIndex(x => x.Id == id) != -1)
                {
                    throw new IdIsAlreadyExistException(id, $"Customer {name}");
                }
                DataSource.Customers.Add(new IDAL.DO.Customer()
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
        public void UpdateCustomerName(int id, string newName)
        {
            try
            {
                int index = DataSource.Customers.FindIndex(x => x.Id == id);
                if (DataSource.Customers.FindIndex(x => x.Id == id) == -1)
                {
                    throw new IdIsNotExistException(id, "Station");
                }
                IDAL.DO.Customer c = DataSource.Customers[index];
                c.Name = newName;
                DataSource.Customers[index] = c;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This function updates a customer with a new name.
        public void UpdateCustomrePhoneNumber(int id, string newPhoneNumber)
        {
            try
            {
                int index = DataSource.Customers.FindIndex(x => x.Id == id);
                if (DataSource.Customers.FindIndex(x => x.Id == id) == -1)
                {
                    throw new IdIsNotExistException(id, "Station");
                }
                IDAL.DO.Customer c = DataSource.Customers[index];
                c.Phone = newPhoneNumber;
                DataSource.Customers[index] = c;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //This function returns a copy of the customers list.
        public IEnumerable<IDAL.DO.Customer> ViewCustomersList()
        {
            List<IDAL.DO.Customer> resultList = new List<IDAL.DO.Customer>();
            foreach (IDAL.DO.Customer customer in DataSource.Customers)
            {
                IDAL.DO.Customer c = new IDAL.DO.Customer();
                c = customer;
                resultList.Add(c);
            }
            return resultList;
        }

        //This function returns the customer with the required Id.
        public string ViewCustomer(int id)
        {
            return ViewCustomer(id).ToString();
        }
        public IDAL.DO.Customer GetCustomer(int id)
        {
            int index = DataSource.Customers.FindIndex(x => x.Id == id);
            if (index == -1)
            {
                throw new IdIsNotExistException(id, $"Customer");
            }
            return DataSource.Customers[index];
        }

        //This function returns a filtered copy of the Customers list (according to a given predicate)
        public IEnumerable<IDAL.DO.Customer> GetCustomers(Func<IDAL.DO.Customer, bool> filter = null)
        {
            return DataSource.Customers.Where(filter).ToList();
        }
    }
}