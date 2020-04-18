using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
   public interface ILoggedInAirLineFacade
    {
        IList<Ticket> GetAllTicketsByAirLine(LoginToken<AirLineCompany> token);
        IList<Flight> GetAllAirLineCompaniesFlights(LoginToken<AirLineCompany> token);
        void CancelFlight(LoginToken<AirLineCompany> token, Flight flight);
        int CreateFlight(LoginToken<AirLineCompany>token, Flight flight);
        void UpdateFlight(LoginToken<AirLineCompany> token, Flight flight);
        void ChangeMyPassword(LoginToken<AirLineCompany> token, string oldPassword, string newPassword);
        void ModifyAirLineDetails(LoginToken<AirLineCompany> token, AirLineCompany airline);
        Flight GetFlightByID(LoginToken<AirLineCompany> token, int id);
        Country GetCountryByName(LoginToken<AirLineCompany> token,string countryName);
        FlightStatus GetFlightStatusByName(LoginToken<AirLineCompany> token, string statusName);
        IList<Ticket> GetAllTicketByFlight(LoginToken<AirLineCompany> token, Flight flight);
        void RemoveTicket(LoginToken<AirLineCompany> token, Ticket ticket);
        FlightStatus GetFlightstatusById(LoginToken<AirLineCompany> token, int id);
        IList<FlightStatus> GetAllFlightStatus(LoginToken<AirLineCompany> token);
        


    }
}
