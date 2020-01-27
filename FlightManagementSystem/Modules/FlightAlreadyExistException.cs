using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    class FlightAlreadyExistException : Exception
    {
        public FlightAlreadyExistException(string message):base(message)
        {

        }
    }
}
