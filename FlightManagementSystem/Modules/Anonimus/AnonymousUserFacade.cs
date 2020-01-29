using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementSystem.Models;

namespace FlightManagementSystem.Modules
{
    public class AnonymousUserFacade : FacadeBase, IAnonymousUserFacade
    {

        public IList<AirLineCompany> GetAllAirLineCompanies()
        {
            return _airLineDAO.GetAll(); 
        }

        public IList<Flight> GetAllFlights()
        {
           return  _flightDAO.GetAll();
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
           return _flightDAO.GetAllFlightsVacancy();
        }

        public Flight GetFlightById(int id)
        {
           return _flightDAO.Get(id);
        }

        public IList<Flight> GetFlightsByDepartureDate(DateTime dateTimeDeparture)
        {
            return _flightDAO.GetFlightsByDepartureTime(dateTimeDeparture);
        }

        public IList<Flight> GetFlightsByDesinationCountry(int countryCode)
        {
            Country country = _countryDAO.Get(countryCode);
            return _flightDAO.GetFlightsByDestinationCountry(country);
        }

        public IList<Flight> GetFlightsByLandingDate(DateTime dateTimeLanding)
        {
            return _flightDAO.GetFlightsByLandingTime(dateTimeLanding);
        }

        public IList<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            Country country = _countryDAO.Get(countryCode);
            return _flightDAO.GetFlightsByOriginCountry(country);
        }
        public Country GetCountryByName(string name)
        {
            return _countryDAO.GetByName(name);
        }
       
    }
}
