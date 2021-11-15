using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void AddDrone(int id, string model, string weight, int initialStationId)
        {
            try
            {
                double batteryStatus = rand.NextDouble() * rand.Next(20, 41);
                dalObject.AddDrone(id, model, weight, batteryStatus, "Maintenance");
                BLDrones.Add(new IBL.BO.DroneForList
                {
                    Id = id,
                    Model = model,
                    MaxWeight = (IBL.BO.WheightCategories)Enum.Parse(typeof(IBL.BO.WheightCategories), weight),
                    Battery = batteryStatus,
                    Status = (IBL.BO.DroneStatuses)Enum.Parse(typeof(IBL.BO.DroneStatuses), weight),

                });
                dalObject.ChargeDrone(id, initialStationId);
            }
            catch (Exception e)
            {
                throw ConvertIdalToBlException(e);
            }
        }
    }
}
