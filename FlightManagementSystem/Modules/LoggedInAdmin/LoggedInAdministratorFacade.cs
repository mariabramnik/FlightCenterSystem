using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementSystem.Models;

namespace FlightManagementSystem.Modules
{
   public class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade
   {
        public void CreateNewairLine(LoginToken<Administrator> token, AirLineCompany comp)
        {
            _airLineDAO.Add(comp);
        }

        public void CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            _customerDAO.Add(customer);
        }

        public void RemoveAirLine(LoginToken<Administrator> token, AirLineCompany comp)
        {
            _airLineDAO.Remove(comp);
        }

        public void RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        {
            _customerDAO.Remove(customer);
        }

        public void UpdateAirLineDetails(LoginToken<Administrator> token, AirLineCompany comp)
        {
            _airLineDAO.Update(comp);
        }

        public void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
        {
            _customerDAO.Update(customer);
        }
        public void RemoveAllFromDB(LoginToken<Administrator> token)
        {
            _ticketDAO.RemoveAllFromTickets();
            _flightDAO.RemoveAllFromFlights();
            _airLineDAO.RemoveAllFromAirLines();
            _flightDAO.RemoveAllFromFlights();
            _customerDAO.RemoveAllFromCustomers();
            _countryDAO.RemoveAllFromCountries();

        }
        public bool CheckIfDBIsEmpty(LoginToken<Administrator> token)
        {
            bool res = true;
            res = _ticketDAO.IfTableTicketsIsEmpty();
            if (res == true)
            {
                res = _flightDAO.IfTableFlightsIsEmpty();
                if (res == true)
                {
                    res = _airLineDAO.IfTableAirlinesIsEmpty();
                    if (res == true)
                    {
                        res = _flightDAO.IfTableFlightsIsEmpty();
                        if (res == true)
                        {
                            res = _customerDAO.IfTableCustomersIsEmpty();
                            if (res == true)
                            {
                                res = _countryDAO.IfTableCountriesIsEmpty();
                            }
                        }
                    }
                }
            }
            return res;
            
        }
        public AirLineCompany GetAirLineCompanyByName(LoginToken<Administrator> token,string name)
        {
            return _airLineDAO.GetAirLineCompanyByName(name);
        }
        public void CreateCountry(LoginToken<Administrator> token,Country country)
        {
            _countryDAO.Add(country);
        }

        public Country GetCountryByName(LoginToken<Administrator> token, string name)
        {
            Country country = _countryDAO.GetByName(name);
            return country;
        }
        public AirLineCompany GetAirLineCompanyById(LoginToken<Administrator> token, int id)
        {
            return _airLineDAO.Get(id);
        }
        public Customer GetCustomerByid(LoginToken<Administrator> token,int id)
        {
           return  _customerDAO.Get(id);
        }
        public bool CheckIfCustomersTableIsEmpty(LoginToken<Administrator> token)
        {
          bool res =  _customerDAO.IfTableCustomersIsEmpty();
            return res;
        }
        public bool CheckIfAirlinesTableIsEmpty(LoginToken<Administrator> token)
        {
            bool res = _airLineDAO.IfTableAirlinesIsEmpty();
            return res;
        }
        public void CreateFlightStatus(LoginToken<Administrator> token, FlightStatus flStatus)
        {
            _flightStatusDAO.Add(flStatus);
        }
        public FlightStatus GetFlightStatusById(LoginToken<Administrator> token, int id)
        {
            return _flightStatusDAO.Get(id);
        }
    }
}
