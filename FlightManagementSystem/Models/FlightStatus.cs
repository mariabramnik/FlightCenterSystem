using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Models
{
    public class FlightStatus : IPoco
    {
        public int id { get; set; }
        public string statusName { get; set; }

        public FlightStatus(int id, string statusName)
        {
            this.id = id;
            this.statusName = statusName;
        }

        public FlightStatus() { }

        public override bool Equals(object obj)
        {
            var status = obj as FlightStatus;
            return status != null &&
                   id == status.id;
        }

        public override int GetHashCode()
        {
            return this.id;
        }

        public override string ToString()
        {
            return ($"{id} , {statusName}");
        }
        public static bool operator ==(FlightStatus f1, FlightStatus f2)
        {
            bool res = false;
            if (f1.id == f2.id)
            {
                res = true;
            }
            return res;
        }
        public static bool operator !=(FlightStatus f1, FlightStatus f2)
        {
            return !(f1 == f2);
        }
    }
}
