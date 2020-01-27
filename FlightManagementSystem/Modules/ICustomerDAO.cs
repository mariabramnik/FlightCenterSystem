using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public interface ICustomerDAO : IBasic<Customer>
    {
        Customer GetCustomerByUserName(string userName);
        void RemoveAllFromCustomers();
        bool IfTableCustomersIsEmpty();
    }
}
