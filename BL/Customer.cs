using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public Location Location { get; set; }
            public List<ParcelInCustomer> ParcelsFromCustomer { get; set; }
            public List<ParcelInCustomer> ParcelsToCustomer { get; set; }
            public override string ToString()
            {
                string parcelsFromCustomer = "";
                string parcelsToCustomer = "";
                foreach (ParcelInCustomer parcel in ParcelsFromCustomer)
                {
                    parcelsFromCustomer += "\n    " + parcel.ToString();
                }
                foreach (ParcelInCustomer parcel in ParcelsToCustomer)
                {
                    parcelsToCustomer += "\n    " + parcel.ToString();
                }
                return $"Id: {Id}\n" +
                    $"Name: {Name}\n" +
                    $"Phone: {Phone}\n" +
                    $"Location: {Location}\n" +
                    $"Delivery List From Customer: {parcelsFromCustomer}\n" +
                    $"Delivery List To Customer:{parcelsToCustomer}";
            }
        }
    }
}
