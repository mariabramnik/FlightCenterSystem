using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.Modules
{
    [Serializable]
    public class AirLineCompanyNotExistException : Exception
    {
        public AirLineCompanyNotExistException()
        {
        }

        public AirLineCompanyNotExistException(string message) : base(" this company is not exist")
        {
        }

        public AirLineCompanyNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AirLineCompanyNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}