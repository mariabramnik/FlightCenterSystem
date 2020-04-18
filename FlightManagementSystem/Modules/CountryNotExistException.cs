using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.Modules
{
    [Serializable]
    public class CountryNotExistException : Exception
    {
        public CountryNotExistException()
        {
        }

        public CountryNotExistException(string message) : base("this country is not exist")
        {
        }

        public CountryNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CountryNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}