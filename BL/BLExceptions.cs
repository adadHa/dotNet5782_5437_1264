using IDAL.DO;
using System;
using System.Runtime.Serialization;

namespace BL
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
}