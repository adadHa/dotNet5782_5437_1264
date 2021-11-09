using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void AddDrone(int id, string model, string weight, int initialStationId)
        {
            // to begin with, we check for the existance and availability of the initial station:
            try
            {
                IDAL.DO.Station initialStation = dalObject.ViewStation(initialStationId);
                if (initialStation.ChargeSlots == 0)
                {
                }
            }
            catch (Exception)
            {
                throw;
            }

            double batteryStatus = rand.NextDouble() * rand.Next(20, 41);
            dalObject.AddDrone(id, model, weight, batteryStatus, "Maintenance");
            BLDrones.Add(new DroneForList
            {
                Id = id,
                Model = model,
                MaxWeight = (WheightCategories)Enum.Parse(typeof(WheightCategories), weight),
                Battery = batteryStatus,
                Status = (DroneStatuses)Enum.Parse(typeof(DroneStatuses), weight),



            });
        }
    }
}
