using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    class CountryNotExistException : Exception
    {
        public CountryNotExistException(string message) : base(message)
        {

        }
    }
}
