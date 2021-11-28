using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelForList
        {
            public int Id { get; set; }
            public string SenderName { get; set; }
            public string ReceiverName { get; set; }
            public WheightCategories MaxWeight { get; set; }
            public Priorities Priority { get; set; }
            public Statuses ParcelStatus { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Sender Name: {SenderName}, Receiver Name: {ReceiverName}, Max Weight: {MaxWeight}, Priority: {Priority}, Status Prcel: {ParcelStatus}";
            }
        } 
    }
}
