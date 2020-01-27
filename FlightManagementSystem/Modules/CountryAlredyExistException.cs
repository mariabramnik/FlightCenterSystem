using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public class CountryAlredyExistException :  Exception
    {
        public CountryAlredyExistException(string message) : base(message)
        {

        }
    }
}
