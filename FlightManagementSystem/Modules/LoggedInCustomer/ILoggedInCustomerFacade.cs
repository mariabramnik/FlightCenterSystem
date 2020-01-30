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
       IList<Flight> GetAllMyFlights(LoginToken<Customer> token);
       Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight);
       Flight GetFlightByIdFlight(LoginToken<Customer> token,int id);     
       Ticket GetTicketByAllParametrs(LoginToken<Customer> token,int flightId, int customerId);
       void RemoveTicket(LoginToken<Customer> token, Ticket ticket);
    }
}
