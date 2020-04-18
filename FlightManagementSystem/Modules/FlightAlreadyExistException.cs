using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.Modules
{
    [Serializable]
    public class FlightAlreadyExistException : Exception
    {
        public FlightAlreadyExistException()
        {
        }

        public FlightAlreadyExistException(string message) : base("this flight is already exist")
        {
        }

        public FlightAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}