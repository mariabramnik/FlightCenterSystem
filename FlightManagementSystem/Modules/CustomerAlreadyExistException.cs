using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    class CustomerAlreadyExistException : Exception
    {
        public CustomerAlreadyExistException(string message) : base(message)
        {

        }
    }
}
