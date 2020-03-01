using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.Modules.Login
{
    [Serializable]
    internal class FunnyException : Exception
    {
        public FunnyException()
        {
        }

        public FunnyException(string message) : base(message)
        {
        }

        public FunnyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FunnyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}