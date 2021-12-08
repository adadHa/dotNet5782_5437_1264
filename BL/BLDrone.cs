using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

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
            double[] arr = dalObject.ViewElectConsumptionData();
            availableDrElectConsumption = arr[0];
            lightDrElectConsumption = arr[1];
            mediumDrElectConsumption = arr[2];
            heavyDrElectConsumption = arr[3];
            chargingRate = arr[4];
            InitializeDrones();

        }

        //this functions initialize the drones
        private void InitializeDrones()
        {

            List<IDAL.DO.Drone> dalDrones = (List<IDAL.DO.Drone>)dalObject.GetDrones();
            foreach (IDAL.DO.Drone dalDrone in dalDrones)
            {
                DroneForList newDrone = new DroneForList
                {
                    Id = dalDrone.Id,
                    Model = dalDrone.Model,
                    MaxWeight = (IBL.BO.WheightCategories)dalDrone.MaxWeight
                };
                if (dalObject.GetParcels(x => x.Delivered == null && x.DroneId == dalDrone.Id).Count() == 1)
                {
                    IDAL.DO.Parcel p = dalObject.GetParcels(x => x.Delivered == null && x.DroneId == dalDrone.Id).ToList()[0];
                    newDrone.DeliveredParcelNumber = p.Id;
                    newDrone.Status = DroneStatuses.Shipping;

                    Location senderLocation = new Location
                    {
                        Latitude = dalObject.GetCustomer(p.SenderId).Latitude,
                        Longitude = dalObject.GetCustomer(p.SenderId).Longitude
                    };
                    Location targetLocation = new Location
                    {
                        Latitude = dalObject.GetCustomer(p.TargetId).Latitude,
                        Longitude = dalObject.GetCustomer(p.TargetId).Longitude
                    };

                    if (p.PickedUp == null) // parcel was binded but not picked up
                    {
                        newDrone.Location = GetMostCloseStationLocation(senderLocation);
                        //calculate the battery
                        double consumptionRate = getConsumptionRate((WheightCategories)dalDrone.MaxWeight);
                        double minimalBattery = (Distance(newDrone.Location, senderLocation)
                                                + Distance(senderLocation, targetLocation)) * consumptionRate;
                        newDrone.Battery = rand.NextDouble();
                        if (newDrone.Battery < minimalBattery) newDrone.Battery = minimalBattery;
                    }

                    else if (p.Delivered == null) // parcel was picked up but not deliverd
                    {
                        newDrone.Location = senderLocation;
                        //calculate the battery
                        double consumptionRate = getConsumptionRate((WheightCategories)dalDrone.MaxWeight);
                        double minimalBattery = Distance(senderLocation, targetLocation) * consumptionRate;
                        newDrone.Battery = rand.NextDouble() * 100;
                        if (newDrone.Battery < minimalBattery) newDrone.Battery = minimalBattery;
                    }
                }
                else
                {
                    newDrone.Status = (DroneStatuses)rand.Next(0, 2); // rand between Available and Maintenance
                    newDrone.DeliveredParcelNumber = -1;
                }

                if (newDrone.Status == DroneStatuses.Maintenance)
                {
                    List<IDAL.DO.Station> stations = dalObject.GetStations().ToList();
                    IDAL.DO.Station randStation;
                    do
                    {
                        int randIndex = rand.Next(0, stations.Count());
                        randStation = stations[randIndex];
                        newDrone.Location = new Location { Latitude = randStation.Latitude, Longitude = randStation.Longitude };
                        newDrone.Battery = rand.NextDouble() * 20;
                    } while (randStation.FreeChargeSlots == 0);
                    dalObject.ChargeDrone(newDrone.Id, randStation.Id);
                }

                else if (newDrone.Status == DroneStatuses.Available)
                {
                    // build the customersWithRecievedParcels list:
                    List<IDAL.DO.Customer> customersWithRecievedParcels = new List<IDAL.DO.Customer>();
                    foreach (IDAL.DO.Customer customer in dalObject.GetCustomers())
                    {
                        if (dalObject.GetParcels(p => p.TargetId == customer.Id).Count() > 0)
                            customersWithRecievedParcels.Add(customer);
                    }
                    int randIndex = rand.Next(0, customersWithRecievedParcels.Count());
                    IDAL.DO.Customer randCustomer = customersWithRecievedParcels[randIndex];
                    newDrone.Location = new Location { Latitude = randCustomer.Latitude, Longitude = randCustomer.Longitude };

                    // calculate the battery
                    double consumptionRate = getConsumptionRate((WheightCategories)dalDrone.MaxWeight);
                    double minimalBattery = Distance(newDrone.Location, GetMostCloseStationLocation(newDrone.Location))
                                            * consumptionRate; // the desired battery to overcame the distance between the most close station to the drone
                    newDrone.Battery = rand.NextDouble() * 100;
                    if (newDrone.Battery < minimalBattery) newDrone.Battery = minimalBattery;
                }
                BLDrones.Add(newDrone);
            }
        }



        //this function adds a drone to the database
        public void AddDrone(int id, string model, string weight, int initialStationId)
        {

            try
            {
                double batteryStatus = rand.NextDouble() * rand.Next(20, 41);
                IDAL.DO.Station initialStation = dalObject.GetStation(initialStationId);
                dalObject.AddDrone(id, model, weight);
                BLDrones.Add(new IBL.BO.DroneForList
                {
                    Id = id,
                    Model = model,
                    MaxWeight = (IBL.BO.WheightCategories)Enum.Parse(typeof(IBL.BO.WheightCategories), weight),
                    Battery = batteryStatus,
                    DeliveredParcelNumber = -1,
                    Status = IBL.BO.DroneStatuses.Maintenance,
                    Location = new IBL.BO.Location()
                    {
                        Longitude = initialStation.Longitude,
                        Latitude = initialStation.Longitude
                    }

                });
                dalObject.ChargeDrone(id, initialStationId);
            }
            catch (DalObject.IdIsAlreadyExistException e)
            {
                throw new IdIsAlreadyExistException(e.ToString());
            }
            catch (DalObject.NoChargeSlotsException e)
            {
                throw new NoChargeSlotsException(e.ToString());
            }
        }
        //this function updates the drone
        public void UpdateDrone(int id, string newModel)
        {

            try
            {
                BLDrones[GetBLDroneIndex(id)].Model = newModel;
                dalObject.UpdateDrone(id, newModel);
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IdIsNotExistException(e);
            }
        }
        //this fuction charge a drone who needs to be charged
        public void ChargeDrone(int id)
        {
            try
            {
                // First, we look for the closet station.
                DroneForList drone = BLDrones[GetBLDroneIndex(id)];
                if (drone == null)
                    throw new DalObject.IdIsNotExistException(id, "Drone");
                if (drone.Status != DroneStatuses.Available)
                    throw new DroneCannotBeChargedException(drone);
                List<IDAL.DO.Station> stations =
                    dalObject.GetStations(x => x.FreeChargeSlots > 0).ToList(); // we choose from the available stations.
                IDAL.DO.Station mostCloseStation = stations[0];
                double mostCloseDistance = 0;
                double distance = 0;
                foreach (IDAL.DO.Station station in stations)
                {
                    Location stationL = new Location { Latitude = station.Latitude, Longitude = station.Longitude };
                    distance = Distance(stationL, drone.Location);
                    if (mostCloseDistance > distance)
                    {
                        mostCloseDistance = distance;
                        mostCloseStation = station;
                    }
                }

                // now we check if the battery status of the drone allow it to get there.
                double consumptionRate = getConsumptionRate(drone.MaxWeight);
                // Heavy -- heavyDrElectConsumption
                if (drone.Battery <= mostCloseDistance * consumptionRate)
                {
                    throw new NotEnoughBatteryException(drone, mostCloseStation);
                }

                else
                {
                    //finaly we change desired fields
                    dalObject.ChargeDrone(id, mostCloseStation.Id);
                    int droneIndex = BLDrones.FindIndex(x => x == drone);
                    drone.Location.Latitude = mostCloseStation.Latitude;
                    drone.Location.Longitude = mostCloseStation.Longitude;
                    drone.Battery -= mostCloseDistance * consumptionRate;
                    drone.Status = DroneStatuses.Maintenance;
                    BLDrones[droneIndex] = drone;
                }
            }
            catch (DalObject.NoChargeSlotsException e)
            {
                throw new NoChargeSlotsException(e.ToString());
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IdIsNotExistException(e);
            }
        }

        //This function get an wheight category and returns its power consumption rate
        // if no parameter is given, the function will return the charging rate
        private double getConsumptionRate(WheightCategories? maxWeight = null)
        {
            switch (maxWeight)
            {
                case WheightCategories.Light:
                    return lightDrElectConsumption;
                case WheightCategories.Medium:
                    return mediumDrElectConsumption;
                case WheightCategories.Heavy:
                    return heavyDrElectConsumption;
                default:
                    return chargingRate;
            }
        }

        //this function release a drone from charge and updates his baterry status after the charge
        public void ReleaseDroneFromCharging(int id, double chargingTime)
        {
            try
            {
                DroneForList d = BLDrones[GetBLDroneIndex(id)];
                if (d.Status != DroneStatuses.Maintenance)
                {
                    throw new DroneCannotBeReleasedException(d);
                }
                else
                {
                    dalObject.StopCharging(id);
                    d.Status = DroneStatuses.Available;
                    d.Battery += chargingTime * dalObject.ViewElectConsumptionData()[0];
                    BLDrones[GetBLDroneIndex(id)] = d;
                }
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IdIsNotExistException(e);
            }
        }

        // This functions returns the distanse between two locations.
        private double Distance(Location l1, Location l2)
        {
            // calculate the distance using Pythagoras 
            double a = Math.Pow(l1.Latitude - l2.Latitude, 2); // a = (x1-x2)^2
            double b = Math.Pow(l1.Longitude - l2.Longitude, 2);// b = (y1 - y2)^2
            return Math.Sqrt(a + b); // dis = sqrt(a - b)
        }

        public void BindDrone(int id)
        {
            try
            {
                DroneForList drone = BLDrones[GetBLDroneIndex(id)];
                if (drone.Status != DroneStatuses.Available)
                    throw new DroneIsAlreadyBindException(drone);
                List<IDAL.DO.Parcel> availableParcels =
                    dalObject.GetParcels(p => IsAbleToDoDelivery(drone, p)).ToList(); // get only available parcels
                if (availableParcels.Count() == 0)
                    throw new NoParcelsToBindException(drone);
                IDAL.DO.Parcel bestParcelToBind = availableParcels[0];
                foreach (IDAL.DO.Parcel parcel in availableParcels)
                {
                    if (IsMoreGoodToBind(drone, parcel, bestParcelToBind))
                    {
                        bestParcelToBind = parcel;
                    }
                }
                BLDrones[GetBLDroneIndex(id)].Status = DroneStatuses.Shipping;
                BLDrones[GetBLDroneIndex(id)].DeliveredParcelNumber = bestParcelToBind.Id;
                dalObject.BindParcel(bestParcelToBind.Id, id);
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IdIsNotExistException(e);
            }
        }

        //This function determine whether a drone is able to do a delivery <=>
        // 1. its wheight capacity is large enough.
        // 2. and is able to drive the path:
        //   drone current location --> sender location --> target location --> most close station (to the target)
        private bool IsAbleToDoDelivery(DroneForList drone, IDAL.DO.Parcel p)
        {
            if ((int)p.Wheight > (int)drone.MaxWeight) return false;
            IDAL.DO.Customer sender = dalObject.GetCustomer(p.SenderId);
            Location senderLoaction = new Location { Latitude = sender.Latitude, Longitude = sender.Longitude };
            IDAL.DO.Customer target = dalObject.GetCustomer(p.TargetId);
            Location targetLoaction = new Location { Latitude = target.Latitude, Longitude = target.Longitude };
            double consumptionRate = getConsumptionRate(drone.MaxWeight);
            double totalDistance = Distance(drone.Location, senderLoaction) + Distance(senderLoaction, targetLoaction)
                                    + Distance(targetLoaction, GetMostCloseStationLocation(targetLoaction));
            double desiredBattery = totalDistance * consumptionRate;
            if (drone.Battery >= desiredBattery)
                return true;
            else return false;
        }

        //This function determine who, of two customers, has a more suitable parcel to bind to the drone.
        // the function returns true if the parcel of customer1 is more suitable.
        private bool IsMoreGoodToBind(DroneForList drone, IDAL.DO.Parcel parcel1, IDAL.DO.Parcel parcel2)
        {
            IDAL.DO.Customer p1Sender = dalObject.GetCustomer(parcel1.SenderId);
            Location p1SenderLocation = new Location { Latitude = p1Sender.Latitude, Longitude = p1Sender.Longitude };
            IDAL.DO.Customer p2Sender = dalObject.GetCustomer(parcel2.SenderId);
            Location p2SenderLocation = new Location { Latitude = p2Sender.Latitude, Longitude = p2Sender.Longitude };
            if (parcel1.Priority > parcel2.Priority)
                return true;
            else if (parcel1.Priority == parcel2.Priority &&
                Distance(drone.Location, p1SenderLocation) < Distance(drone.Location, p2SenderLocation))
                return true;
            else return false;
        }

        //This function collects a parcel by its shipping drone
        public void CollectParcelByDrone(int id)
        {
            try
            {
                DroneForList drone = BLDrones[GetBLDroneIndex(id)];
                if (drone.Status != DroneStatuses.Shipping)
                    throw new DroneCannotCollectParcelException(drone);
                IDAL.DO.Parcel parcel = dalObject.GetParcels(p => p.DroneId == id).ToList()[0];
                if (parcel.PickedUp != null)
                    throw new DroneCannotCollectParcelException(drone, parcel);
                IDAL.DO.Customer sender = dalObject.GetCustomer(parcel.SenderId);
                dalObject.CollectParcelByDrone(id, parcel.Id);

                // update battery and location
                Location senderLocation = new Location { Latitude = sender.Latitude, Longitude = sender.Longitude };
                double consumption = Distance(drone.Location, senderLocation) * getConsumptionRate(drone.MaxWeight);
                drone.Battery -= consumption;
                drone.Location = senderLocation;
                BLDrones[GetBLDroneIndex(id)] = drone;
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IdIsNotExistException(e);
            }
        }

        // This function ,akes a drone supply its parcel to its target customer.
        public void SupplyParcel(int id)
        {
            try
            {
                DroneForList drone = BLDrones[GetBLDroneIndex(id)];
                if (drone.Status != DroneStatuses.Shipping)
                    throw new DroneCannotSupplyParcelException(drone);
                IDAL.DO.Parcel parcel = dalObject.GetParcels(p => p.DroneId == id).ToList()[0];
                if (parcel.Delivered != null)
                    throw new DroneCannotSupplyParcelException(drone, parcel);
                // update the parcel on data layer:
                dalObject.SupplyParcelToCustomer(parcel.Id);
                // update the drone on BL layer:
                drone.Status = DroneStatuses.Available;
                IDAL.DO.Customer target = dalObject.GetCustomer(parcel.TargetId);
                Location targetLoaction = new Location { Latitude = target.Latitude, Longitude = target.Longitude };
                double consumption = Distance(drone.Location, targetLoaction) * getConsumptionRate(drone.MaxWeight);
                drone.Battery -= consumption;
                drone.Location = targetLoaction;
                BLDrones[GetBLDroneIndex(id)] = drone;
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IdIsNotExistException(e);
            }
        }


        //this function view the drones details
        public string ViewDrone(int id)
        {
            return GetDrone(id).ToString();
        }
        private Drone GetDrone(int id)
        {
            try
            {
                if (BLDrones.FindIndex(x => x.Id == id) == -1) throw new IdIsNotExistException(id, "Drone");
                DroneForList drone = BLDrones[GetBLDroneIndex(id)];
                ParcelInTransfer transferedParcel = new ParcelInTransfer();
                if (drone.Status == DroneStatuses.Shipping)
                {
                    IDAL.DO.Parcel p = dalObject.GetParcels(x => x.DroneId == id).ToList()[0];
                    transferedParcel.Id = p.Id;
                    transferedParcel.Wheight = (WheightCategories)p.Wheight;
                    transferedParcel.Priority = (Priorities)p.Priority;
                    transferedParcel.Sender = new CustomerInDelivery { Id = p.SenderId, Name = dalObject.GetCustomer(p.SenderId).Name };
                    transferedParcel.Receiver = new CustomerInDelivery { Id = p.TargetId, Name = dalObject.GetCustomer(p.TargetId).Name };
                    transferedParcel.PickUpLocation = new Location { Latitude = dalObject.GetCustomer(p.SenderId).Latitude, Longitude = dalObject.GetCustomer(p.SenderId).Longitude };
                    transferedParcel.TargetLocation = new Location { Latitude = dalObject.GetCustomer(p.TargetId).Latitude, Longitude = dalObject.GetCustomer(p.TargetId).Longitude };
                }

                Drone resultDrone = new Drone
                {
                    Id = drone.Id,
                    Model = drone.Model,
                    MaxWeight = drone.MaxWeight,
                    Status = drone.Status,
                    Battery = drone.Battery,
                    Location = drone.Location,
                    TransferedParcel = transferedParcel
                };
                return resultDrone;
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IdIsNotExistException(e);
            }
        }

        //This function returns an index to the desired drone id from the list on BL database
        private int GetBLDroneIndex(int id)
        {
            try
            {
                int i = BLDrones.FindIndex(x => x.Id == id);
                if (i == -1)
                {
                    throw new DalObject.IdIsNotExistException(id, "Drone"); // ??? should we throw Dal exception about bl's drone exception?
                }
                return i;
            }
            catch (DalObject.IdIsNotExistException e)
            {
                throw new IdIsNotExistException(e);
            }
        }

        public string ViewDronesList()
        {
            string result = "";
            foreach (var item in GetDrones())
            {
                result += item.ToString() + "\n";
            }
            return result;
        }

        public IEnumerable<DroneForList> GetDrones(Func<DroneForList, bool> filter = null)
        {
            if (filter == null)
                return BLDrones;
            List<DroneForList> a = BLDrones.Where(filter).ToList();
            return BLDrones.Where(filter).ToList();
        }
    }


}

