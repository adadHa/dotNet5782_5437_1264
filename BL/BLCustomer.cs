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
                throw new IBL.BO.IdIsNotExistException(e.ToString());
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

        }

    }
}