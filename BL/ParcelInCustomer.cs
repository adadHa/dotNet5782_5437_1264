using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelInCustomer
    {
        public int Id { get; set; }
        public Priorities? Priority { get; set; }
        public WheightCategories Wheight { get; set; }
        public Statuses ParcelStatus { get; set; }
        public CustomerInDelivery TheOtherSide { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Priority: {Priority}, Wheight: {Wheight}, Parcel stutus: {ParcelStatus}, The other client: {TheOtherSide.ToString()}"; 
        }
    }
}
