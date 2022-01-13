using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace DalXml
{
    public sealed partial class DalXml : IDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<DroneCharge> IDal.GetDroneCharges(Func<DroneCharge, bool> filter)
        {
            List<DroneCharge> list = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DronesChargesPath);
            if (filter == null) return list;
            return list.Where(filter);
        }
    }

}
