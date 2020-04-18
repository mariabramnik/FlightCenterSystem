using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.Modules
{
    [Serializable]
    public class TicketNotExistException : Exception
    {
        public TicketNotExistException()
        {
        }

        public TicketNotExistException(string message) : base("this ticket is not exist")
        {
        }

        public TicketNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TicketNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}