using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
namespace DalXml
{
    public sealed partial class DalXml : IDal
    {
        IEnumerable<DroneCharge> IDal.GetDroneCharges(Func<DroneCharge, bool> filter)
        {
            List<DroneCharge> list = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DronesChargesPath);
            return list.Where(filter);
        }
    }

}
