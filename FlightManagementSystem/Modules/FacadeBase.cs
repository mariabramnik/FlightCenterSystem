using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public abstract class FacadeBase
    {
        //in constructor creating DAO objects and DBConnection.Open in destructor DBConnectin.Close
        protected IAirLineDAO _airLineDAO;
        private AirLineDAOMSSQL __airLineDAO;
        protected ICountryDAO _countryDAO;
        private CountryDAOMSSQL __countryDAO;
        protected ICustomerDAO _customerDAO;
        private CustomerDAOMSSQL __customerDAO;
        protected IFlightDAO _flightDAO;
        private FlightDAOMSSQL __flightDAO;
        protected ITicketDAO _ticketDAO;
        private TicketDAOMSSQL __ticketDAO;
        protected IFlightStatusDAO _flightStatusDAO;
        private FlightStatusDAOMSSQL __flightStatusDAO;
        public FacadeBase()
        {
            __airLineDAO = new AirLineDAOMSSQL();
            _airLineDAO = __airLineDAO;

            __countryDAO = new CountryDAOMSSQL();
            _countryDAO = __countryDAO;

            __customerDAO = new CustomerDAOMSSQL();
            _customerDAO = __customerDAO;

            __flightDAO = new FlightDAOMSSQL();
            _flightDAO = __flightDAO;

            __ticketDAO = new TicketDAOMSSQL();
            _ticketDAO = __ticketDAO;

            __flightStatusDAO = new FlightStatusDAOMSSQL();
            _flightStatusDAO = __flightStatusDAO;

        }

    }
}
