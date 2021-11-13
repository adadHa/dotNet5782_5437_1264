using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    namespace BO
    {
        class DeliveryAtCustomer
        {
            public int Id { get; set; }
            public WheightCategories Wheight { get; set; }
            public Priorities Priority { get; set; }
            public Statuses Status { get; set; }
            public CustomerInDelivery customerInDelivery { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Wheight: {Wheight}, Priority: {Priority}, status: {Status}, customer in delivery: {customerInDelivery}";
            }
        } 
    }
}
