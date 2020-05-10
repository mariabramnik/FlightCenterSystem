using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public interface IFlightDAO : IBasic<Flight>
    {
        Dictionary<Flight, int> GetAllFlightsVacancy();
        Flight GetFlightById(int id);
        List<Flight> GetFlightsByCustomer(Customer customer);
        List<Flight> GetFlightsByDepartureTime(DateTime datetime);
        List<Flight> GetFlightsByDestinationCountry(Country country);
        List<Flight> GetFlightsByLandingTime(DateTime datetime);
        List<Flight> GetFlightsByOriginCountry(Country country);
        void RemoveAllFromFlights();
        bool IfTableFlightsIsEmpty();
        Flight GetFlightByAllParametrs(Flight flight);
        List<Flight> SelectElapsedFlightsToHistory();
        void InsertElapsedFlightsToHistory(List<Flight> flightList);
        void AddFlightToHistoryTable(Flight ob);
        void DeleteElapsedFlightsFromFlights(List<Flight> flightList);
        List<Flight> GetFlightsByAirLineCompany(AirLineCompany company);
        void RemoveAllFromFlights_History();
        bool IfTableFlights_HistoryIsEmpty();
        List<Flight> SelectAllFromFlights_History();
        FlightStatus GetFlightStatusByFlightStatusName(string statusName);
        List<Flight> GetFlightsByLandingTime12Hours();
        List<Flight> GetFlightsByDepartureTime12Hours();
        void ChangeFlightStatusToLanded();


    }
}
