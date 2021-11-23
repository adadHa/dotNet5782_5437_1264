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
                return Message;
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
    }
}

