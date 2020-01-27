using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    class FlightStatusNotExistException : Exception
    {
        public FlightStatusNotExistException(string message):base(message)
        {

        }
    }
}
