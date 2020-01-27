using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    class FlightNotExistException : Exception
    {
        public FlightNotExistException(string message) : base(message)
        {

        }
    }
}
