using IDAL.DO;
using System;
using System.Runtime.Serialization;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        internal class NoChargeSlotsException : Exception
        {
            private Station InitialStation;

            public NoChargeSlotsException()
            {
            }

            public NoChargeSlotsException(Station initialStation)
            {
                InitialStation = initialStation;
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
                return $"{InitialStation.Name} has no more free charge slots!\n";
            }
        }

        [Serializable]
        internal class IdIsNotExistException : Exception
        {
            public IdIsNotExistException()
            {
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
        }
    }
}