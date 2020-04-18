using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.Modules
{
    [Serializable]
    public class FlightNotExistException : Exception
    {
        public FlightNotExistException()
        {
        }

        public FlightNotExistException(string message) : base("this flight is not exist")
        {
        }

        public FlightNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}