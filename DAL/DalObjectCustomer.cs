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
            DataSource.Customers.Add( new IDAL.DO.Customer()
            {
                Id = id,
                Name = name,
                Phone = phoneNumber, 
                Longitude = longitude,
                Latitude = latitude
            });
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
        public IDAL.DO.Customer ViewCustomer(int id)
        {
            int index = DataSource.Customers.FindIndex(x => x.Id == id);
            return DataSource.Customers[index];
        }
    }
}