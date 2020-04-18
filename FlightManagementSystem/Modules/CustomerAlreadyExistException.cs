using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.Modules
{
    [Serializable]
    public class CustomerAlreadyExistException : Exception
    {
        public CustomerAlreadyExistException()
        {
        }

        public CustomerAlreadyExistException(string message) : base("this customer is already exist")
        {
        }

        public CustomerAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}