using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerForList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public int NumberOfParcelsSendAndSupplied { get; set; }
            public string NumberOfParcelsSend { get; set; }
            public string NumberOfParcelsSupplied { get; set; }
            public string NumberOfParcelnWayToTheCustomer { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Name: {Name}, Phone: {Phone}, Number Of Parcels Send And Supplied: {NumberOfParcelsSendAndSupplied}, Number Of Parcels Send: {NumberOfParcelsSend},Number Of Parcels Supplied:{NumberOfParcelsSupplied}, Number Of Parce ln Way To The Customer:{NumberOfParcelnWayToTheCustomer}";
            }
        } 
    }
}
