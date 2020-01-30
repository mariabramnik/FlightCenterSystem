
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementSystem.Models;

namespace FlightManagementSystem.Modules
{
    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {
        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            List<Flight> listFlightByCustomer = new List<Flight>();
            listFlightByCustomer = _flightDAO.GetFlightsByCustomer(token.User);
            return listFlightByCustomer;

        }

        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            Ticket ticket = new Ticket(0,flight.id, token.User.id);
            int id = _ticketDAO.Add(ticket);          
            flight.remainingTickets = flight.remainingTickets - 1;
            _flightDAO.Update(flight);
            ticket.id = id;
            return ticket;
        }
        public Country GetCountryByName(LoginToken<Customer> token,string name)
       {
            return _countryDAO.GetByName(name);
       }
        public Flight GetFlightByIdFlight(LoginToken<Customer> token,int id)
        {
           return _ticketDAO.GetFlightById(id);
        }
        
        public Dictionary<Flight, int> GetAllFlightsVacancy(LoginToken<Customer> token)
        {
            Dictionary<Flight, int> dictFlightIntVac = new Dictionary<Flight, int>();
            dictFlightIntVac =  _flightDAO.GetAllFlightsVacancy();
            return dictFlightIntVac;
        }
        public List<Flight> GetFlightsByDepartureTime(LoginToken<Customer> token,DateTime datetime)
        {
            List<Flight> listFlightByDepartTime = new List<Flight>();
            listFlightByDepartTime = _flightDAO.GetFlightsByDepartureTime(datetime);
            return listFlightByDepartTime;
        }
        public List<Flight> GetFlightsByDestinationCountry(LoginToken<Customer> token,Country country)
        {
            List<Flight> listFlightByDestCountry = new List<Flight>();
            listFlightByDestCountry = _flightDAO.GetFlightsByDestinationCountry(country);
            return listFlightByDestCountry;
        }
        public List<Flight> GetFlightsByLandingTime(LoginToken<Customer> token,DateTime datetime)
        {
            List<Flight> listFlightByLandingTime = new List<Flight>();
            listFlightByLandingTime = _flightDAO.GetFlightsByLandingTime(datetime);
            return listFlightByLandingTime;
        }
        public List<Flight> GetFlightsByOriginCountry(LoginToken<Customer> token,Country country)
        {
            List<Flight> listFlightByOriginCountry = new List<Flight>();
            listFlightByOriginCountry = _flightDAO.GetFlightsByOriginCountry(country);
            return listFlightByOriginCountry;
        }
        public Ticket GetTicketByAllParametrs(LoginToken<Customer> token,int flightId,int customerId)
        {
           return _ticketDAO.GetTicketByAllParametrs(flightId, customerId);
        }
        public void RemoveTicket(LoginToken<Customer> token, Ticket ticket)
        {
            _ticketDAO.Remove(ticket);
            Flight flight = _ticketDAO.GetFlightById(ticket.flightId);
            flight.remainingTickets = flight.remainingTickets + 1;
            _flightDAO.Update(flight);

        }
    }
    
}
