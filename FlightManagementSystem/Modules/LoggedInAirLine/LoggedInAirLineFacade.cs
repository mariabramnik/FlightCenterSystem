using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementSystem.Models;

namespace FlightManagementSystem.Modules
{
    public class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirLineFacade
    {
        public void CancelFlight(LoginToken<AirLineCompany> token, Flight flight)
        {
            _flightDAO.Remove(flight);
            List<Ticket>listTicket  = _ticketDAO.GetTicketsByFlight(flight.id);
            foreach(Ticket t in listTicket)
            {
                _ticketDAO.Remove(t);
            }
        }

        public void ChangeMyPassword(LoginToken<AirLineCompany> token, string oldPassword, string newPassword)
        {
            token.User.password = newPassword;        
            _airLineDAO.Update(token.User);
        }

        public int CreateFlight(LoginToken<AirLineCompany> token, Flight flight)
        {
           int res =  _flightDAO.Add(flight);
            return res;
        }

        public IList<Flight> GetAllAirLineCompaniesFlights(LoginToken<AirLineCompany> token)
       {
            return _flightDAO.GetFlightsByAirLineCompany(token.User);
       }

        public List<Ticket> GetAllTicketsByAirLine(LoginToken<AirLineCompany> token)
        {
            List<Ticket> ticketList = new List<Ticket>();
            ticketList =  _ticketDAO.GetTicketsByAirLineCompany(token.User);
            return ticketList;
        }

        public void ModifyAirLineDetails(LoginToken<AirLineCompany> token, AirLineCompany airline)
        {
            _airLineDAO.Update(airline);

        }

        public void UpdateFlight(LoginToken<AirLineCompany> token, Flight flight)
        {
            _flightDAO.Update(flight);
        }

        public Flight GetFlightByID(LoginToken<AirLineCompany> token,int id)
        {
           return  _flightDAO.GetFlightById(id);
        }

        public Country GetCountryByName(LoginToken<AirLineCompany> token,string countryName)
        {
           return  _countryDAO.GetByName(countryName);
        }

        public FlightStatus GetFlightStatusByName(LoginToken<AirLineCompany> token, string statusName)
        {
            return _flightStatusDAO.GetFlightStatusByName(statusName);
        }
        public List<Ticket> GetAllTicketByFlight(LoginToken<AirLineCompany> token, Flight flight)
        {
            List<Ticket> listTickets = _ticketDAO.GetTicketsByFlight(flight.id);
            return listTickets;
        }

        public void RemoveTicket(LoginToken<AirLineCompany> token, Ticket ticket)
        {
            _ticketDAO.Remove(ticket);
        }

        public FlightStatus GetFlightstatusById(LoginToken<AirLineCompany> token, int id)
        {
           return _flightStatusDAO.Get(id);
        }

        public List<FlightStatus> GetAllFlightStatus(LoginToken<AirLineCompany> token)
        {
            List<FlightStatus> flightstatusList = new List<FlightStatus>();
            flightstatusList =  _flightStatusDAO.GetAll();
            return flightstatusList;
        }
    }
}
