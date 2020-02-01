
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

        public List<Ticket> GetAllMyTickets(LoginToken<Customer> token)
        {
            List<Ticket> allMyTickets = new List<Ticket>();
            allMyTickets = _ticketDAO.GetAllMyTickets(token.User.id);
            return allMyTickets;
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
        public void ChangeMyPassword(LoginToken<Customer> token, string oldPassword, string newPassword)
        {
            _customerDAO.ChangeMyPassword(token.User, oldPassword, newPassword);
        }

        public List<Ticket> GetAllTicketsFromTickets_HistoryByCustomer(LoginToken<Customer> token,Customer customer)
        {
            List<Ticket> ticketsList = new List<Ticket>();
            ticketsList =  _ticketDAO.GetAllTicketsFromTickets_HistoryByCustomer(customer);
            return ticketsList;

        }
    }
    
}
