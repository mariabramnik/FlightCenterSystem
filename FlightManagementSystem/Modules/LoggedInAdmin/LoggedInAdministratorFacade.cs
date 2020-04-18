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
        public int CreateNewairLine(LoginToken<Administrator> token, AirLineCompany comp)
        {
            int res = _airLineDAO.Add(comp);
            return res;
        }

        public int CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            int res = _customerDAO.Add(customer);
            return res;
        }

        public Customer GetCustomerByUserName(LoginToken<Administrator> token,string userName)
        {
           return _customerDAO.GetCustomerByUserName(userName);
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
            _flightStatusDAO.RemoveAllFromFlightStatus();
            _customerDAO.RemoveAllFromCustomers();
            _countryDAO.RemoveAllFromCountries();
            _flightDAO.RemoveAllFromFlights_History();
            _ticketDAO.RemoveAllFromTickets_History();

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
                                if(res ==  true)
                                {
                                    res = _flightDAO.IfTableFlights_HistoryIsEmpty();
                                    if(res == true)
                                    {
                                        res = _ticketDAO.IfTableTickets_HistoryIsEmpty();
                                    }
                                }
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

        public int CreateCountry(LoginToken<Administrator> token,Country country)
        {
            int res = _countryDAO.Add(country);
            return res;
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

        public int CreateFlightStatus(LoginToken<Administrator> token, FlightStatus flStatus)
        {
            int res = _flightStatusDAO.Add(flStatus);
            return res;
        }

        public FlightStatus GetFlightStatusById(LoginToken<Administrator> token, int id)
        {
            return _flightStatusDAO.Get(id);
        }

       public void TransferElapsedFlightsToHistory(LoginToken<Administrator> token)
        {
            List<Flight> listFlights = _flightDAO.SelectElapsedFlightsToHistory();
            foreach(Flight fl in listFlights)
            {
                List<Ticket> listTickets = _ticketDAO.GetTicketsByFlight(fl.id);
                foreach(Ticket ticket in listTickets)
                {
                    _ticketDAO.InsertTicketToTicketHistory(ticket);
                    _ticketDAO.Remove(ticket);
                }
            }
            _flightDAO.InsertElapsedFlightsToHistory(listFlights);
            _flightDAO.DeleteElapsedFlightsFromFlights(listFlights);
        }

        public List<Flight> GetAllFromFlights_History()
        {
            List<Flight> flightsList = new List<Flight>();
            flightsList = _flightDAO.SelectAllFromFlights_History();
            return flightsList;
        }

        public IList<Customer> GetAllCustomers(LoginToken<Administrator> token)
        {
            IList<Customer> customersList = new List<Customer>();
            customersList = _customerDAO.GetAll();
            return customersList;
        }

        public Country GetCountry(LoginToken<Administrator> token, int id)
        {
            return _countryDAO.Get(id);
        }
    }
}
