using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public interface IAnonymousUserFacade
    {
        IList<AirLineCompany>GetAllAirLineCompanies();
        IList<Flight> GetAllFlights();
        Dictionary<Flight,int>GetAllFlightsVacancy();
        Flight GetFlightById(int id);
        IList<Flight>GetFlightsByDepartureDate(DateTime dateTimeDeparture);
        IList<Flight>GetFlightsByDesinationCountry(int countryCode);
        IList<Flight>GetFlightsByLandingDate(DateTime dateTimeLanding);
        IList<Flight>GetFlightsByOriginCountry(int countryCode);
        Country GetCountryByName(string name);
        List<Country> GetAllCountries();
    }

}
