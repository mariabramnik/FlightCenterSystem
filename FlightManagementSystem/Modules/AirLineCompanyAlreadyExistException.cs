using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    class AirLineCompanyAlreadyExistException : Exception
    {
        public AirLineCompanyAlreadyExistException(string message) : base(message)
        {

        }
    }
}
