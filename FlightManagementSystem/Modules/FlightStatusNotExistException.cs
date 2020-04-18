using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.Modules
{
    [Serializable]
    public class FlightStatusNotExistException : Exception
    {
        public FlightStatusNotExistException()
        {
        }

        public FlightStatusNotExistException(string message) : base("this flightstatus is not exist")
        {
        }

        public FlightStatusNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightStatusNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}