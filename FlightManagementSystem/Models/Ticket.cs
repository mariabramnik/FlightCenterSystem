using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Models
{
    public class Ticket : IPoco
    {
        public int id;
        public int flightId;
        public int customerId;
        public Ticket() { }

        public Ticket(int id, int flightId, int customerId)
        {
            this.id = id;
            this.flightId = flightId;
            this.customerId = customerId;
        }

        public override bool Equals(object obj)
        {
            var ticket = obj as Ticket;
            return ticket != null &&
                   id == ticket.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public override string ToString()
        {
            return ($"{id} ,{flightId} ,{customerId}");
        }
        public static bool operator ==(Ticket t1, Ticket t2)
        {
            bool res = false;
            if (t1.id == t2.id)
            {
                res = true;
            }
            return res;
        }
        public static bool operator !=(Ticket t1, Ticket t2)
        {
            return !(t1 == t2);
        }
    }
}
