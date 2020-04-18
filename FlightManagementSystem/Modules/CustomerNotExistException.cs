using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.Modules
{
    [Serializable]
    public class CustomerNotExistException : Exception
    {
        public CustomerNotExistException()
        {
        }

        public CustomerNotExistException(string message) : base("this customer is not exist")
        {
        }

        public CustomerNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}