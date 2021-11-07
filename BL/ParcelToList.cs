using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class ParcelToList
        {
            public int Id { get; set; }
            public string NameSender { get; set; }
            public string NameReceiver { get; set; }
            public WheightCategories MaxWeight { get; set; }
            public Priorities Priority { get; set; }
            public Statuses StatusPrcel { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Name Sender: {NameSender}, Name Receiver: {NameReceiver}, Max Weight: {MaxWeight}, Priority: {Priority}, Status Prcel: {StatusPrcel}";
            }
        } 
    }
}
