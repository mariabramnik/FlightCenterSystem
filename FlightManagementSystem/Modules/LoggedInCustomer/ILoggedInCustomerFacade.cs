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
       Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight);
       Flight GetFlightByIdFlight(LoginToken<Customer> token,int id);
       Dictionary<Flight, int> GetAllFlightsVacancy(LoginToken<Customer> token);
       List<Flight> GetFlightsByCustomer(LoginToken<Customer> token, Customer customer);
       List<Flight> GetFlightsByDepartureTime(LoginToken<Customer> token, DateTime datetime);
       List<Flight> GetFlightsByDestinationCountry(LoginToken<Customer> token, Country country);
       List<Flight> GetFlightsByLandingTime(LoginToken<Customer> token, DateTime datetime);
       List<Flight> GetFlightsByOriginCountry(LoginToken<Customer> token, Country country);
       Country GetCountryByName(LoginToken<Customer> token, string name);
       Ticket GetTicketByAllParametrs(LoginToken<Customer> token,int flightId, int customerId);
    }
}
