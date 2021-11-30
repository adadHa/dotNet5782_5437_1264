using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public enum WheightCategories { Light, Medium, Heavy }
        public enum Priorities { Regular, Fast, Emergency }
        public enum Statuses { Created, Scheduled, PickedUp, Delivered }
        public enum DroneStatuses { Available, Maintenance, Shipping } 
    }
}
