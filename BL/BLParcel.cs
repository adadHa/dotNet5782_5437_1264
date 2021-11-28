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
                dalObject.AddParcel(customerSenderId, customerReceiverId, weight, priority, 0);

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
            

    }
}
