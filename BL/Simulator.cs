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
            while (isStopped())
            {
                lock (blObject)
                {
                    try
                    {
                        blObject.BindDrone(droneId);
                    }
                    catch (NoParcelsToBindException ex)
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
                    Thread.Sleep(DELAY);
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
                }
            }
        }
    }
}
