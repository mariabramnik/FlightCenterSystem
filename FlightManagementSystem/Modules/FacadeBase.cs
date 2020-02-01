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
            __airLineDAO.SQLConnectionOpen();
            _airLineDAO = __airLineDAO;

            __countryDAO = new CountryDAOMSSQL();
            __countryDAO.SQLConnectionOpen();
            _countryDAO = __countryDAO;

            __customerDAO = new CustomerDAOMSSQL();
            __customerDAO.SQLConnectionOpen();
            _customerDAO = __customerDAO;

            __flightDAO = new FlightDAOMSSQL();
            __flightDAO.SQLConnectionOpen();
            _flightDAO = __flightDAO;

            __ticketDAO = new TicketDAOMSSQL();
            __ticketDAO.SQLConnectionOpen();
            _ticketDAO = __ticketDAO;

            __flightStatusDAO = new FlightStatusDAOMSSQL();
            __flightStatusDAO.SQLConnectionOpen();
            _flightStatusDAO = __flightStatusDAO;

        }

        ~FacadeBase()
        {
            __airLineDAO.SQLConnectionClose();
            __countryDAO.SQLConnectionClose();
            __customerDAO.SQLConnectionClose();
            __flightDAO.SQLConnectionClose();
            __ticketDAO.SQLConnectionClose();
        }
    }
}
