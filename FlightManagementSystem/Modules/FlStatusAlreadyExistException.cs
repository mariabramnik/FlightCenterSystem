using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    class FlStatusAlreadyExistException : Exception
    {
        public FlStatusAlreadyExistException(string message) : base(message)
        {

        }
    }
}
