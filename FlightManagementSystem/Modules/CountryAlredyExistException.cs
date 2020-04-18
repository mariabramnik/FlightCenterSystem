using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.Modules
{
    [Serializable]
    public class CountryAlredyExistException : Exception
    {
        public CountryAlredyExistException()
        {
        }

        public CountryAlredyExistException(string message) : base("this country is already exist")
        {
        }

        public CountryAlredyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CountryAlredyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}