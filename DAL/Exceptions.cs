using System;
using System.Runtime.Serialization;

namespace DalObject
{
    [Serializable]
    public class IdIsNotExistException : Exception
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
            if (Message != null)
            {
                return Message;
            }
            else
            {
                return $"{Type} with Id = {Id} is not exist. please try another id \n";
            }
        }
    }

    [Serializable]
    public class IdIsAlreadyExistException : Exception
    {
        private int Id { get; set; }
        private string Type { get; set; }
        public IdIsAlreadyExistException(int id, string type)
        {
            Id = id;
            Type = type;
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
        {   if (Message != null)
            {
                return Message;
            }
            else
            {
                return $"{Type} with Id = {Id} is already exist. please try another id \n";
            }
        }
    }
}

    

