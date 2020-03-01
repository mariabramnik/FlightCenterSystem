using System;
using FlightManagementSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightManagementSystem.Modules.Login;
using FlightManagementSystem.Modules;
using FlightManagementSystem.Modules.FlyingCenterSystem;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TestForFlightManagmentSystem
{
    [TestClass]
    public class UnitTest1
    {
  
        //Clean all database from data
        [TestMethod]
        public void REMOVE_ALLDATA_FROM_DB()
        {
            bool actual = true;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                iAdminFS.RemoveAllFromDB(ltAdmin);
                actual = iAdminFS.CheckIfDBIsEmpty(ltAdmin);
                Assert.IsTrue(actual);
            }

        }
        //Airline create Flight with departTime = current time - 8 hours and landingTime = current time - 3 hours;
        [TestMethod]
        public void CREATE_FLIGHT_TO_TEST_RECORDS_TO_HISTORY()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("kramnik", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                DateTime dtNow = DateTime.Now;
                DateTime dtCurr = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour, 00, 00);
                DateTime departTime = dtCurr.AddHours(-8.0);
                DateTime landingTime = dtCurr.AddHours(-4.0);
                Country country1 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Israel");
                Country country2 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Latvia");
                FlightStatus flStatus = iAirlineCompanyFS.GetFlightStatusByName(ltAirLine, "landing");
                Flight flight = new Flight(0, ltAirLine.User.id, country1.id, country2.id, departTime, landingTime, 100);
                flight.flightStatusId = flStatus.id;
                int id = iAirlineCompanyFS.CreateFlight(ltAirLine, flight);
                flight.id = id;
                if (flight == iAirlineCompanyFS.GetFlightByID(ltAirLine, id))
                {
                    actual = true;
                }    
            }
            Assert.IsTrue(actual);
        }
        //Airline create Flight with departTime = current time - 8 hours and landingTime = current time - 3 hours;
        [TestMethod]
        public void CREATE_FLIGHT_TO_TEST_RECORDS_TO_HISTORY2()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("kramnik", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                DateTime dtNow = DateTime.Now;
                DateTime dtCurr = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour, 00, 00);
                DateTime departTime = dtCurr.AddHours(-8.0);
                DateTime landingTime = dtCurr.AddHours(-4.0);
                Country country1 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Israel");
                Country country2 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Germany");
                FlightStatus flStatus = iAirlineCompanyFS.GetFlightStatusByName(ltAirLine, "landing");
                Flight flight = new Flight(0, ltAirLine.User.id, country1.id, country2.id, departTime, landingTime, 100);
                flight.flightStatusId = flStatus.id;
                int id = iAirlineCompanyFS.CreateFlight(ltAirLine, flight);
                flight.id = id;
                if (flight == iAirlineCompanyFS.GetFlightByID(ltAirLine, id))
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }
        //Airline create Flight with departTime = current time - 8 hours and landingTime = current time - 3 hours;
        [TestMethod]
        public void CREATE_2_TICKET_TO_TEST_RECORDS_TO_HISTORY()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Customer> ltCustomer = null;
            bool res = ls.TryCustomerLogin("0021", "inna34", out ltCustomer);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInCustomerFacade iCustomerFS = fs.GetFacade<ILoggedInCustomerFacade>();
                if (iCustomerFS is IAnonymousUserFacade)
                {
                    IAnonymousUserFacade iAnonimFS = (IAnonymousUserFacade)iCustomerFS;
                    DateTime dtNow = DateTime.Now;
                    DateTime dtCurr = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour, 00, 00);
                    DateTime departTime = dtCurr.AddHours(-8.0);
                    IList<Flight> list = iAnonimFS.GetFlightsByDepartureDate(departTime);
                    Flight fl1 = list[0];
                    Flight fl2 = list[1];
                    // Ticket ticket = new Ticket(0, fl.id,ltCustomer.User.id));               
                    Ticket ticket1 = iCustomerFS.PurchaseTicket(ltCustomer, fl1);
                    Ticket ticket2 = iCustomerFS.PurchaseTicket(ltCustomer, fl2);
                    if ((ticket1 == iCustomerFS.GetTicketByAllParametrs(ltCustomer, fl1.id, ltCustomer.User.id)) &&
                       (ticket2 == iCustomerFS.GetTicketByAllParametrs(ltCustomer, fl2.id, ltCustomer.User.id)))
                    {
                        Monitor.Exit(fs);
                        Thread.Sleep(2000);
                        Monitor.Enter(fs);
                        Flight myfl1 = iCustomerFS.GetFlightByIdFlight(ltCustomer, fl1.id);
                        Flight myfl2 = iCustomerFS.GetFlightByIdFlight(ltCustomer, fl2.id);
                        Ticket mytick1 = iCustomerFS.GetTicketByAllParametrs(ltCustomer, fl1.id, ltCustomer.User.id);
                        Ticket mytick2 = iCustomerFS.GetTicketByAllParametrs(ltCustomer, fl2.id, ltCustomer.User.id);
                        IList<Ticket> myListHist = iCustomerFS.GetAllTicketsFromTickets_HistoryByCustomer(ltCustomer,ltCustomer.User);
                        IList<Flight>FlightsHistory = iAnonimFS.GetAllFlightsByFlights_History();
                        if (myfl1 is null && myfl2 is null && mytick1 is null && mytick2 is null && myListHist.Count == 2 && FlightsHistory.Count == 2)
                        {
                            actual = true;
                        }
                    }
                }

                Assert.IsTrue(actual);
            }
        
    }
        // THIS METHOD RANS ALL THE TESTS IN THE CORRECT ORDER
        [TestMethod]
        public void TESTS_RUNNING()
        {
            // TestLoginService tls = new TestLoginService();
            // TestForCustomerFacade tcustf = new TestForCustomerFacade();
            //TestForAnanimousFacade tananimf = new TestForAnanimousFacade();
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            lock (fs)
            {
                REMOVE_ALLDATA_FROM_DB();
                UnitTestAdministratorFacade utad = new UnitTestAdministratorFacade();
                utad.ADD_COUNTRY();
                utad.ADD_COUNTRY2();
                utad.ADD_COUNTRY3();
                utad.GET_COUNTRY_BY_NAME();
                utad.ADD_AIRLINECOMPANY();
                utad.ADD_AIRLINECOMPANY2();
                utad.UPDATE_AIRLINECOMPANY();
                utad.REMOVE_AIRLINECOMPANY();
                utad.ADD_AIRLINECOMPANY();
                utad.CREATE_CUSTOMER();
                utad.CREATE_CUSTOMER2();
                utad.UPDATE_CUSTOMER();
                utad.UPDATE_CUSTOMER2();
                utad.REMOVE_CUSTOMER();
                utad.CREATE_CUSTOMER2();
                utad.CREATE_FLIGHT_STATUS();
                utad.CREATE_FLIGHT_STATUS2();
                utad.CREATE_FLIGHT_STATUS3();
                utad.GET_AIRLINE_BY_NAME();
                utad.GET_AIRLINE_BY_ID();
                TestForAirlineFacade tairf = new TestForAirlineFacade();
                tairf.CREATE_FLIGHT();
                tairf.CREATE_FLIGHT2();
                tairf.CREATE_FLIGHT3();
                tairf.CREATE_FLIGHT4();
                tairf.CREATE_FLIGHT5();
                tairf.UPADATE_FLIGHT();
                tairf.REMOVE_FLIGHT();
                tairf.CREATE_FLIGHT();
                tairf.AIRLINE_CHANGE_PASSWORD_ACTUAL_TRUE();
                tairf.AIRLINE_CHANGE_PASSWORD_BACK_ACTUAL_TRUE();
                tairf.GET_FLIGHT_STATUS_BY_ID();
                tairf.GET_FLIGHT_STATUS_BY_NAME();
                tairf.GET_ALL_FLIGHTS_BY_AIRLINE();
                tairf.GET_ALL_FLIGHTS_BY_AIRLINE2();
                tairf.GET_ALL_TICKETS_BY_AIRLINE();
                TestForCustomerFacade tcustf = new TestForCustomerFacade();
                tcustf.CREATE_TICKET();
                tcustf.CREATE_TICKET2();
                tcustf.CREATE_TICKET3();
                tcustf.REMOVE_TICKET_ACTUAL_TRUE();
                tcustf.CREATE_TICKET();
                tcustf.GET_ALL_MY_FLIGHTS();
                tcustf.GET_ALL_MY_TICKETS();
                tcustf.CUSTOMER_CHANGE_PASSWORD_ACTUAL_TRUE();
                tcustf.CUSTOMER_CHANGE_PASSWORD_BACK_ACTUAL_TRUE();
                TestForAnanimousFacade tanonimf = new TestForAnanimousFacade();
                tanonimf.ANONYMOUS_GET_COUNTRY_BY_NAME();
                tanonimf.GET_ALL_AIRLINECOMPANIES();
                tanonimf.GET_ALL_COUNTRIES();
                tanonimf.GET_ALL_FLIGHTS_BY_AIRLINECOMPANY();
                tanonimf.GET_ALL_FLIGHTS_BY_DEPARTURE_TIME();
                tanonimf.GET_ALL_FLIGHTS_BY_DESTINATION_COUNTRY_CODE();
                tanonimf.GET_ALL_FLIGHTS_BY_LANDING_TIME();
                tanonimf.GET_ALL_FLIGHTS_BY_ORIGIN_COUNTRY_CODE();
                tanonimf.GET_ALL_FLIGHTS_FOR_ANONYMOUS();
                tanonimf.GET_ALL_FLIGHTS_VACANCY();
                TestLoginService tls = new TestLoginService();
                tls.TryLogin_FALSE_Returned();
                tls.TryLogin_FALSE_Returned2();
                tls.TryLogin_TRUE_Returned();
                tls.TryLogin_TRUE_Returned2();
            }
            Monitor.Enter(fs);
            //Test  thread for records Flights and Tickrts to History
            CREATE_FLIGHT_TO_TEST_RECORDS_TO_HISTORY();
            CREATE_FLIGHT_TO_TEST_RECORDS_TO_HISTORY2();
            CREATE_2_TICKET_TO_TEST_RECORDS_TO_HISTORY();
            Monitor.Exit(fs);
            //record to the tables Flight_history and Tickets_History is expected after 2 sec




        }

    }
}
    

