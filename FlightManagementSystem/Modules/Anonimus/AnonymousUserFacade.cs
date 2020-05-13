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

        public  IList<Country> GetAllCountries()
        {
            IList<Country> list = _countryDAO.GetAll();
            return list;
        }

        public IList<Flight> GetAllFlightsByAirLineCompanies(AirLineCompany comp)
        {
            IList<Flight> allFlightsByAirLine = new List<Flight>();
            allFlightsByAirLine = _flightDAO.GetFlightsByAirLineCompany(comp);
            return allFlightsByAirLine;
        }

        public IList<Flight> GetAllFlightsByFlights_History()
        {
            IList<Flight> listFlights = new List<Flight>();
            listFlights = _flightDAO.SelectAllFromFlights_History();
            return listFlights;
        }

        public IList<FlightStatus> GetFlightStatuses()
        {
            IList<FlightStatus> listFlightStatuses = new List<FlightStatus>();
            listFlightStatuses = _flightStatusDAO.GetAll();
            return listFlightStatuses;
        }

        public IList<Flight> GetFlightsByLandingTime12Hours()
        {
            return _flightDAO.GetFlightsByLandingTime12Hours();
        }

        public IList<Flight> GetFlightsByDepartureTime12Hours()
        {
            return _flightDAO.GetFlightsByDepartureTime12Hours();
        }

        public Country GetCountryById(int id)
        {
            return _countryDAO.Get(id);
        }

        public IList<City> GetAllByCountry(string countryName)
        {
            IList<City> listCities = new List<City>();
            return _countryDAO.GetAllByCountry(countryName);
        }

        public AirLineCompany GetAirLineById(int id)
        {
            return _airLineDAO.Get(id);
        }

        public IList<Flight> GetFlightsAlreadyTakenOff()
        {
            IList<Flight> listFlights = new List<Flight>();
            listFlights = _flightDAO.GetFlightsAlreadyTakenOff();
            return listFlights;
        }

        public IList<Flight> GetFlightsNotTakenOffYet()
        {
            IList<Flight> listFlights = new List<Flight>();
            listFlights = _flightDAO.SearchNotTakenOffYet();
            return listFlights;
        }
    }
}
