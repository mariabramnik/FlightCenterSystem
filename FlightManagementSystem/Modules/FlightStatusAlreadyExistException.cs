using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.Modules
{
    [Serializable]
    public class FlightStatusAlreadyExistException : Exception
    {
        public FlightStatusAlreadyExistException()
        {
        }

        public FlightStatusAlreadyExistException(string message) : base("this flightstatus is already exist")
        {
        }

        public FlightStatusAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightStatusAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}