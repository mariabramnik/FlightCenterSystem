using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.Modules
{
    [Serializable]
    public class AirLineCompanyAlreadyExistException : Exception
    {
        public AirLineCompanyAlreadyExistException()
        {
        }

        public AirLineCompanyAlreadyExistException(string message) : base("this airline is already exist")
        {
        }

        public AirLineCompanyAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AirLineCompanyAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}