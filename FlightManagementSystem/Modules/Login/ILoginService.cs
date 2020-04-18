using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementSystem.Models;

namespace FlightManagementSystem.Modules
{
    interface ILoginService
    {
        bool TryAdminLogin(string password, string loginName, out LoginToken<Administrator> loginToken);
        bool TryAirLineLogin(string password, string loginName, out LoginToken<AirLineCompany> loginToken);
        bool TryCustomerLogin(string password, string loginName, out LoginToken<Customer> loginToken);
      

    }
}
