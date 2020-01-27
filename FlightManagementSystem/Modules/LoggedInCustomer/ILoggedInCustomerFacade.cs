using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public interface ILoggedInCustomerFacade
    {
       
       void CancelTicket(LoginToken<Customer>token,Ticket ticket);
       IList<Flight>GetAllMyFlights(LoginToken<Customer>token);
        Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight, int ticket_id);
        Flight GetFlightByIdFlight(LoginToken<Customer> token,int id);
    }
}
