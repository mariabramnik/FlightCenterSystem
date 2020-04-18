using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.Modules
{
    [Serializable]
    public class TicketAlreadyExistException : Exception
    {
        public TicketAlreadyExistException()
        {
        }

        public TicketAlreadyExistException(string message) : base("this ticket is already exist")
        {
        }

        public TicketAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TicketAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}