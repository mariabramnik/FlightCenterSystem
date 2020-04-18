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
       IList<Ticket> GetAllMyTickets(LoginToken<Customer> token);
       Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight);
       Flight GetFlightByIdFlight(LoginToken<Customer> token,int id);
       Dictionary<Flight, int> GetAllFlightsVacancy(LoginToken<Customer> token);
       Ticket GetTicketByAllParametrs(LoginToken<Customer> token,int flightId, int customerId);
       void RemoveTicket(LoginToken<Customer> token, Ticket ticket);
       void ChangeMyPassword(LoginToken<Customer> token, string oldPassword, string newPassword);
       IList<Ticket> GetAllTicketsFromTickets_HistoryByCustomer(LoginToken<Customer> token,Customer customer);


    }
}
