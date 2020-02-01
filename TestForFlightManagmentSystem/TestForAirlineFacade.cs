using System;
using FlightManagementSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightManagementSystem.Modules.Login;
using FlightManagementSystem.Modules;
using FlightManagementSystem.Modules.FlyingCenterSystem;
using System.Collections.Generic;
using System.Linq;
namespace TestForFlightManagmentSystem
{
    [TestClass]
    public class TestForAirlineFacade
    {

        //Airline create Flight
        [TestMethod]
        public void CREATE_FLIGHT()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("kramnik", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                DateTime deppartTime = new DateTime(2020, 2, 20, 10, 20, 00);
                DateTime landingTime = new DateTime(2020, 2, 20, 18, 10, 00);
                Country country1 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Israel");
                Country country2 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Latvia");
                FlightStatus flStatus = iAirlineCompanyFS.GetFlightStatusByName(ltAirLine, "panding");
                Flight flight = new Flight(0, ltAirLine.User.id, country1.id, country2.id, deppartTime, landingTime, 100);
                flight.flightStatusId = flStatus.id;
                int id = iAirlineCompanyFS.CreateFlight(ltAirLine, flight);
                flight.id = id;
                if (flight == iAirlineCompanyFS.GetFlightByID(ltAirLine, id))
                {
                    actual = true;
                }

                Assert.IsTrue(actual);
            }

        }
        // airLineFacade create flight
        [TestMethod]
        public void UPADATE_FLIGHT()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("kramnik", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                IList<Flight> listFlight = iAirlineCompanyFS.GetAllAirLineCompaniesFlights(ltAirLine);
                if (listFlight.Count > 0)
                {
                    Flight flight = listFlight[0];
                    int flightId = flight.id;
                    DateTime newDepartTime = new DateTime(2020, 2, 21, 17, 15, 00);
                    flight.departureTime = newDepartTime;
                    iAirlineCompanyFS.UpdateFlight(ltAirLine, flight);
                    if (iAirlineCompanyFS.GetFlightByID(ltAirLine, flightId) == flight)
                    {
                        actual = true;
                    }
                }
            }
            Assert.IsTrue(actual);
        }

        //Airline create Flight
        [TestMethod]
        public void CREATE_FLIGHT2()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("kramnik", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                DateTime departTime = new DateTime(2020, 2, 21, 16, 15, 00);
                DateTime landingTime = new DateTime(2020, 2, 21, 08, 05, 00);
                Country country1 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Israel");
                Country country2 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Latvia");
                FlightStatus flStatus = iAirlineCompanyFS.GetFlightStatusByName(ltAirLine, "panding");
                Flight flight = new Flight(0, ltAirLine.User.id, country2.id, country1.id, departTime, landingTime, 120);
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
        // airLineFacade create flight
        [TestMethod]
        public void CREATE_FLIGHT3()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("bramnik", "BramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                DateTime departTime = new DateTime(2020, 2, 10, 19, 15, 00);
                DateTime landingTime = new DateTime(2020, 2, 11, 08, 05, 00);
                Country country1 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Israel");
                Country country2 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Latvia");
                FlightStatus flStatus = iAirlineCompanyFS.GetFlightStatusByName(ltAirLine, "panding");
                Flight flight = new Flight(0, ltAirLine.User.id, country2.id, country1.id, departTime, landingTime, 120);
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
        // airLineFacade create flight
        [TestMethod]
        public void CREATE_FLIGHT4()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("kramnik", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                DateTime departTime = new DateTime(2020, 3, 20, 21, 15, 00);
                DateTime landingTime = new DateTime(2020, 3, 21, 05, 05, 00);
                Country country1 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Israel");
                Country country2 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Latvia");
                FlightStatus flStatus = iAirlineCompanyFS.GetFlightStatusByName(ltAirLine, "panding");
                Flight flight = new Flight(0, ltAirLine.User.id, country1.id, country2.id, departTime, landingTime, 120);
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
        // airLineFacade create flight
        [TestMethod]
        public void CREATE_FLIGHT5()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("kramnik", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                DateTime departTime = new DateTime(2020, 4, 02, 21, 15, 00);
                DateTime landingTime = new DateTime(2020, 4, 03, 05, 05, 00);
                Country country1 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Israel");
                Country country2 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Latvia");
                FlightStatus flStatus = iAirlineCompanyFS.GetFlightStatusByName(ltAirLine, "panding");
                Flight flight = new Flight(0, ltAirLine.User.id, country1.id, country2.id, departTime, landingTime, 0);
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

        //airLineFacade get all flights 
        [TestMethod]
        public void GET_ALL_FLIGHTS_BY_AIRLINE()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("kramnik", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                if (iAirlineCompanyFS is IAnonymousUserFacade)
                {
                    IAnonymousUserFacade iAnonimFS = (IAnonymousUserFacade)iAirlineCompanyFS;
                    IList<Flight> flightsListByAirLine = iAnonimFS.GetAllFlightsByAirLineCompanies(ltAirLine.User);
                    IList<Flight> allFlightsList = iAnonimFS.GetAllFlights();
                    List<Flight> controllList = new List<Flight>();
                    foreach (Flight flight in allFlightsList)
                    {
                        if (flight.airLineCompanyId == ltAirLine.User.id)
                        {
                            controllList.Add(flight);
                        }
                    }
                    if (controllList.SequenceEqual(flightsListByAirLine))
                    {
                        actual = true;
                    }
                }
            }
            Assert.IsTrue(actual);
        }
        //airlineFacade get all flights 
        [TestMethod]
        public void GET_ALL_FLIGHTS_BY_AIRLINE2()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("bramnik", "BramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                if (iAirlineCompanyFS is IAnonymousUserFacade)
                {
                    IAnonymousUserFacade iAnonimFS = (IAnonymousUserFacade)iAirlineCompanyFS;
                    IList<Flight> flightsListByAirLine = iAnonimFS.GetAllFlightsByAirLineCompanies(ltAirLine.User);
                    IList<Flight> allFlightsList = iAnonimFS.GetAllFlights();
                    List<Flight> controllList = new List<Flight>();
                    foreach (Flight flight in allFlightsList)
                    {
                        if (flight.airLineCompanyId == ltAirLine.User.id)
                        {
                            controllList.Add(flight);
                        }
                    }
                    if (controllList.SequenceEqual(flightsListByAirLine))
                    {
                        actual = true;
                    }
                }
            }
            Assert.IsTrue(actual);
        }
        //airlineFacade get all tickets 
        [TestMethod]
        public void GET_ALL_TICKETS_BY_AIRLINE()
        {
            bool actual = true;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("kramnik", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                List<Ticket> listTickets = iAirlineCompanyFS.GetAllTicketsByAirLine(ltAirLine);
                foreach (Ticket ticket in listTickets)
                {
                    Flight fl = iAirlineCompanyFS.GetFlightByID(ltAirLine, ticket.flightId);
                    if (fl.airLineCompanyId != ltAirLine.User.id)
                    {
                        actual = false;
                    }
                }
            }
            Assert.IsTrue(actual);
        }

        //Airline remove Flight
        [TestMethod]
        public void REMOVE_FLIGHT()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("kramnik", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                IList<Flight> flList = iAirlineCompanyFS.GetAllAirLineCompaniesFlights(ltAirLine);
                Flight flight = flList[0];
                iAirlineCompanyFS.CancelFlight(ltAirLine, flight);
                Flight flightCheck = iAirlineCompanyFS.GetFlightByID(ltAirLine, flight.id);
                if (!(flight is null) && (flightCheck is null))
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }
        // AirLineFacade Change password
        [TestMethod]
        public void AIRLINE_CHANGE_PASSWORD_ACTUAL_TRUE()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("kramnik", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                iAirlineCompanyFS.ChangeMyPassword(ltAirLine, ltAirLine.User.password, "newPassword");
                if (ltAirLine.User.password == "newPassword")
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }
        //AirLineFacade Change password
        [TestMethod]
        public void AIRLINE_CHANGE_PASSWORD_BACK_ACTUAL_TRUE()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("newPassword", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                iAirlineCompanyFS.ChangeMyPassword(ltAirLine, ltAirLine.User.password, "kramnik");
                if (ltAirLine.User.password == "kramnik")
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }
        //airline get flightStatus by flightStatus_name
        [TestMethod]
        public void GET_FLIGHT_STATUS_BY_NAME()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("kramnik", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                FlightStatus flStatus = iAirlineCompanyFS.GetFlightStatusByName(ltAirLine, "panding");
                if (flStatus.statusName == "panding")
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }
        //airline get flightStatus by flightStatus_id
        [TestMethod]
        public void GET_FLIGHT_STATUS_BY_ID()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<AirLineCompany> ltAirLine = null;
            bool res = ls.TryAirLineLogin("kramnik", "KramnikAdmin", out ltAirLine);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                List<FlightStatus> listFlightStatus = iAirlineCompanyFS.GetAllFlightStatus(ltAirLine);
                FlightStatus flStatus = listFlightStatus[0];
                int id = flStatus.id;
                string name = flStatus.statusName;
                FlightStatus myFlightSt = iAirlineCompanyFS.GetFlightstatusById(ltAirLine, id);
                if (id == myFlightSt.id)
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }
    }
}
