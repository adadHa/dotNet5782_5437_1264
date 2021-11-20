using System;
using System.Collections.Generic;
namespace BL
{
    public partial class BL : IBL.IBL
    {
        private IDAL.IDal dalObject;
        private List<IBL.BO.DroneForList> BLDrones = new List<IBL.BO.DroneForList>();
        public double availableDrElectConsumption;
        public double lightDrElectConsumption;
        public double mediumDrElectConsumption;
        public double heavyDrElectConsumption;
        public double chargingRate;
        static Random rand = new Random();

        public BL()
        {
            dalObject = new DalObject.DalObject();
        }

        //this function adds a station to the database
        public void AddStation(int id, string name, int freeChargingSlots, IBL.BO.Location location)
        {
            try
            {
                dalObject.AddStation(id, name, freeChargingSlots, location.Longitude, location.Latitude);
            }
            catch (DalObject.IdIsAlreadyExistException e)
            {
                throw new IBL.BO.IdIsAlreadyExistException(e.ToString());
            }
            catch (DalObject.NoChargeSlotsException e)
            {
                throw new IBL.BO.NoChargeSlotsException(e.ToString());
            }

        }

        //This function updates a station with a new name and/or a new charging slots capacity.
        public void UpdateStation(int id, string newName, int newNum)
        {
            try
            {
                if (newName != "") // check if there was an input for this value
                {
                    dalObject.UpdateStationName(id, newName);
                }
                if (newNum != -1) // check if there was an input for this value
                {
                    if (newNum >= 0) // it should be a positive number
                        dalObject.UpdateStationChargeSlotsCap(id, newNum);
                    else
                        throw new ArgumentOutOfRangeException("charging slots capacity");
                }
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IBL.BO.IdIsNotExistException(e.ToString());
            }
        }






    }
}
/*public BL()
       {
           IDal.IDal dalObject = new DalObject.DalObject();
           double[] arr = dalObject.ViewElectConsumptionData();
           availableDrElectConsumption = arr[0];
           lightDrElectConsumption = arr[1];
           mediumDrElectConsumption = arr[2];
           heavyDrElectConsumption = arr[3];
           chargingRate = arr[4];

           BLDrones = (List<Drone>)dalObject.ViewDronesList();
           List<StationForList> stations = (List<StationForList>)dalObject.ViewStationsList();
           for (int i = 0; i < BLDrones.Count; i++)
           {
               Drone drone = BLDrones[i];
               if (drone.Status != DroneStatuses.Available)
               {
                   drone.Status = (DroneStatuses)rand.Next(0, 2);
                   if (drone.Status == DroneStatuses.Maintenance)
                   {
                       randomStation = stations[rand.Next(0, stations.Count)].;
                       drone.Battery  = rand.NextDouble() * 20;
                   }
                   if (drone.Status == DroneStatuses.Shipping)
                   {

                   }
               }
           }
         }
          */