using IDAL.DO;
using System;
using System.Runtime.Serialization;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        internal class DroneCannotBeReleasedException : Exception
        {
            private DroneForList d;
            public DroneCannotBeReleasedException()
            {
            }

            public DroneCannotBeReleasedException(DroneForList d)
            {
                this.d = d;
            }

            public DroneCannotBeReleasedException(string message) : base(message)
            {
            }

            public DroneCannotBeReleasedException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected DroneCannotBeReleasedException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }

            public override string ToString()
            {
                return $"Cannot release Drone {d.Id} from charging, probably beacause it is not being charging right now" +
                    $"\n (dron status: {d.Status})";
            }
        }
        [Serializable]
        internal class NoChargeSlotsException : Exception
        {

            public NoChargeSlotsException()
            {
            }

            public NoChargeSlotsException(Station initialStation)
            {
            }

            public NoChargeSlotsException(string message) : base(message)
            {
            }

            public NoChargeSlotsException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected NoChargeSlotsException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }

            public override string ToString()
            {
                return Message;
            }
        }
        [Serializable]
        internal class NotEnoughBatteryException : Exception
        {
            private DroneForList Drone;
            private IDAL.DO.Station Station;
            public NotEnoughBatteryException()
            {
            }

            public NotEnoughBatteryException(DroneForList drone, IDAL.DO.Station station)
            {
                Drone = drone;
                Station = station;
            }

            public NotEnoughBatteryException(string message) : base(message)
            {
            }

            public NotEnoughBatteryException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected NotEnoughBatteryException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }

            public override string ToString()
            {
                return $"Drone {Drone.Id} cannot be charged beacause it has only {Drone.Battery}% battery \n" +
                    $"which is not enough to get to the closet station {Station.Name}";
            }
        }



        [Serializable]
        internal class IdIsNotExistException : Exception
        {
            private int Id { get; set; }
            private string Type { get; set; }
            public IdIsNotExistException(int id, string type)
            {
                Id = id;
                Type = type;
            }
            public IdIsNotExistException(DalObject.IdIsNotExistException e)
            {
                Id = e.Id;
                Type = e.Type;
            }
            public IdIsNotExistException(string message) : base(message)
            {
            }

            public IdIsNotExistException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected IdIsNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
            public override string ToString()
            {
                return $"{Type} with Id = {Id} is not exist. please try another id \n";
            }
        }

        [Serializable]
        internal class IdIsAlreadyExistException : Exception
        {
            public IdIsAlreadyExistException()
            {
            }

            public IdIsAlreadyExistException(string message) : base(message)
            {
            }

            public IdIsAlreadyExistException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected IdIsAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
            public override string ToString()
            {
                return Message;
            }
        }

        [Serializable]
        internal class DroneCannotBeChargedException : Exception
        {
            private DroneForList drone;

            public DroneCannotBeChargedException()
            {
            }

            public DroneCannotBeChargedException(DroneForList drone)
            {
                this.drone = drone;
            }

            public DroneCannotBeChargedException(string message) : base(message)
            {
            }

            public DroneCannotBeChargedException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected DroneCannotBeChargedException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }

            public override string ToString()
            {
                if (drone.Status == DroneStatuses.Maintenance)
                {
                    return $"Drone {drone.Id} is already charging";
                }
                else //drone.Status = DroneStatuses.Shipping
                    return $"Drone {drone.Id} cannot charge beacause it is shipping now a parcel";

            }
        }
        [Serializable]
        internal class NoParcelsToBindException : Exception
        {
            private DroneForList drone;

            public NoParcelsToBindException()
            {
            }

            public NoParcelsToBindException(DroneForList drone)
            {
                this.drone = drone;
            }

            public NoParcelsToBindException(string message) : base(message)
            {
            }

            public NoParcelsToBindException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected NoParcelsToBindException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }

            public override string ToString()
            {
                return $"Drone {drone.Id} couldn't be linked to any parcel. " +
                    $"\n It may be bacause all parcels are already linked to other drones";
            }
        }
        [Serializable]
        internal class DroneIsAlreadyBindException : Exception
        {
            private DroneForList drone;

            public DroneIsAlreadyBindException()
            {
            }

            public DroneIsAlreadyBindException(DroneForList drone)
            {
                this.drone = drone;
            }

            public DroneIsAlreadyBindException(string message) : base(message)
            {
            }

            public DroneIsAlreadyBindException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected DroneIsAlreadyBindException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }

            public override string ToString()
            {
                if (drone.Status == DroneStatuses.Shipping)
                    return $"Drone {drone.Id} is already linked to another parcel.";
                else
                    return $"Drone {drone.Id} is on maintenance and cannot be linked.";
            }
        }

        [Serializable]
        internal class DroneCannotCollectParcelException : Exception
        {
            private DroneForList drone;
            private IDAL.DO.Parcel? parcel;

            public DroneCannotCollectParcelException()
            {
            }

            public DroneCannotCollectParcelException(string message) : base(message)
            {
            }
            public DroneCannotCollectParcelException(DroneForList drone, IDAL.DO.Parcel? parcel = null)
            {
                this.drone = drone;
                this.parcel = parcel;
            }

            public DroneCannotCollectParcelException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected DroneCannotCollectParcelException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }

            public override string ToString()
            {
                if (drone.Status != DroneStatuses.Shipping)
                    return $"Drone {drone.Id} is not shipping any parcel right now.";
                else if (parcel != null && parcel.Value.PickedUp != null)
                    return $"Parcel {parcel.Value.Id} was already picked up by drone {drone.Id}";
                return null;
            }
        }
        [Serializable]
        internal class DroneCannotSupplyParcelException : Exception
        {
            private DroneForList drone;
            private IDAL.DO.Parcel? parcel;

            public DroneCannotSupplyParcelException()
            {
            }

            public DroneCannotSupplyParcelException(string message) : base(message)
            {
            }

            public DroneCannotSupplyParcelException(DroneForList drone, IDAL.DO.Parcel? parcel = null)
            {
                this.drone = drone;
                this.parcel = parcel;
            }

            public DroneCannotSupplyParcelException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected DroneCannotSupplyParcelException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }

            public override string ToString()
            {
                if (drone.Status != DroneStatuses.Shipping)
                    return $"Drone {drone.Id} is not shipping any parcel right now.";
                else if (parcel != null && parcel.Value.Delivered != null)
                    return $"Parcel {parcel.Value.Id} was already delivered by drone {drone.Id}";
                return null;
            }
        }
    }
}

