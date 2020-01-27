using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    class CustomerNotExistException : Exception
    {
        public CustomerNotExistException(string message) : base(message)
        {

        }
    }
}
