using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class ParcelToList
    {
        public int Id { get; set; }
        public string NameSender { get; set; }
        public string NameReceiver { get; set; }
        public WheightCategories MaxWeight { get; set; }
        public Priorities Priority { get; set; }
        public Statuses StatusPrcel { get; set; }
    }
}
