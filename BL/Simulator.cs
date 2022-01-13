using System;
using BO;
using System.Threading;
using System.Linq;


namespace BL
{
    class Simulator
    {
        int DELAY = 500;
        double velocity = 25;
        public Simulator(BlApi.IBL blObject, int droneId, Action updatePl, Func<bool> isStopped)
        {
            DateTime? currentTime = DateTime.Now;
            DateTime? chargeTime = null;
            
            while (!isStopped())
            {
                lock (blObject)
                {
                    try
                    {
                        if(blObject.GetDrone(droneId).Status == DroneStatuses.Available)
                        {
                            blObject.BindDrone(droneId);
                            updatePl();
                            Thread.Sleep(DELAY);
                        }
                    }
                    catch (NoParcelsToBindException)
                    {
                        while (blObject.GetDrone(droneId).Battery != 100)
                        {
                            blObject.ChargeDrone(droneId);
                            updatePl();
                            Thread.Sleep(DELAY);
                            blObject.ReleaseDroneFromCharging(droneId, DELAY);
                            updatePl();
                            Thread.Sleep(DELAY);
                        }
                    }
                    
                }
                if (chargeTime != null)
                {                    
                    lock (blObject)
                    {
                        blObject.CollectParcelByDrone(droneId);
                        updatePl();
                    }
                    Thread.Sleep(DELAY);
                    lock (blObject)
                    {
                        blObject.SupplyParcel(droneId);
                        updatePl();
                    }
                    Thread.Sleep(DELAY);
                }
            }
        }
    }
}
