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
        void IDal.AddDrone(int id, string model, string weight)
        {
            throw new NotImplementedException();
        }
        void IDal.ChargeDrone(int droneId, int stationId)
        {
            throw new NotImplementedException();
        }

        void IDal.StopCharging(int droneId)
        {
            throw new NotImplementedException();
        }

        void IDal.UpdateDrone(int droneId, string newModel)
        {
            throw new NotImplementedException();
        }
        Drone IDal.GetDrone(int id)
        {
            throw new NotImplementedException();
        }
        IEnumerable<Drone> IDal.GetDrones(Func<Drone, bool> filter)
        {
            throw new NotImplementedException();
        }



        IEnumerable<Drone> IDal.ViewDronesList()
        {
            throw new NotImplementedException();
        }

        string IDal.ViewDrone(int id)
        {
            throw new NotImplementedException();
        }

    }

}
