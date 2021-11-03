using System;
using System.Runtime.Serialization;

namespace DalObject
{
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
}