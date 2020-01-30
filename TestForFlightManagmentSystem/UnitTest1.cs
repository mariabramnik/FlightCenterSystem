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
    public class UnitTest1
    {
        // Successfull authorization.This User is AirlaineCompany. 
        [TestMethod]
        public void TryLogin_TRUE_Returned()
        {
            string password = "kramnik";
            string userName = "KramnikAdmin";

            LoginService ls = new LoginService();
            bool actual = ls.TryLogin(password, userName);
            Assert.IsTrue(actual);
        }
        // Successfull authorization.This User is Customer.
        [TestMethod]
        public void TryLogin_TRUE_Returned2()
        {
            string password = "0021";
            string userName = "inna34";

            LoginService ls = new LoginService();
            bool actual = ls.TryLogin(password, userName);
            Assert.IsTrue(actual);
        }
        // Not Successfull authorization.
        [TestMethod]
        public void TryLogin_FALSE_Returned()
        {
            string password = "Inna11";
            string userName = "Inna23";
            bool actual = true;
            try
            {
                LoginService ls = new LoginService();
                ls.TryLogin(password, userName);
            }
            catch (Exception ex)
            {
                if (ex is UserNotFoundException)
                {
                    actual = false;
                }
            }
            Assert.IsFalse(actual);
        }
        //User is founded.This is Customer,but the password is incorrect
        [TestMethod]
        public void TryLogin_FALSE_Returned2()
        {
            string password = "Inna";
            string userName = "inna34";
            bool actual = true;
            try
            {
                LoginService ls = new LoginService();
                ls.TryLogin(password, userName);
            }
            catch (Exception ex)
            {
                if (ex is WrongPasswordException)
                {
                    actual = false;
                }
            }
            Assert.IsFalse(actual);
        }

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
        // Testing AdminFacade
        //Admin add new Country;
        [TestMethod]
        public void ADD_COUNTRY()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                //ILoggedInAirLineFacade iAirlineCompanyFS = fs.GetFacade<ILoggedInAirLineFacade>();
                //iAirlineCompanyFS.GetAllFlights();
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                Country country = new Country(0, "Israel");
                int newCountryId = iAdminFS.CreateCountry(ltAdmin, country);
                country.id = newCountryId;
                if (country == iAdminFS.GetCountryByName(ltAdmin, "Israel"))
                {
                    actual = true;
                }
                Assert.IsTrue(actual);
            }
        }
        //Admin add new Country;
        [TestMethod]
        public void ADD_COUNTRY2()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
                {
                    FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                    ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                    Country country = new Country();
                    country.countryName = "Latvia";
                    int newCountryId = iAdminFS.CreateCountry(ltAdmin, country);
                    country.id = newCountryId;
                    if (country == iAdminFS.GetCountryByName(ltAdmin, "Latvia"))
                    {
                        actual = true;
                    }
                    Assert.IsTrue(actual);
                }
        }
        //Admin add new Country;
        [TestMethod]
        public void ADD_COUNTRY3()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                Country country = new Country();
                country.countryName = "Germany";
                int newCountryId = iAdminFS.CreateCountry(ltAdmin, country);
                country.id = newCountryId;
                if (country == iAdminFS.GetCountryByName(ltAdmin, "Germany"))
                {
                    actual = true;
                }
                Assert.IsTrue(actual);
            }
        }


        //
        [TestMethod]
        public void GET_COUNTRY_BY_NAME()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
                if (res == true)
                {
                    FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                    ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                    List<Country> listCountries = iAdminFS.GetAllCountries();
                    string name = listCountries[0].countryName;
                    if(listCountries[0] == iAdminFS.GetCountryByName(name))
                    {
                        actual = true;
                    }
                    Assert.IsTrue(actual);
                }
        }

        //Admin add new airLineCompany;
        [TestMethod]
        public void ADD_AIRLINECOMPANY()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                AirLineCompany airLineComp = new AirLineCompany();
                airLineComp.airLineName = "BramnikAirLine";
                airLineComp.userName = "BramnikAdmin";
                airLineComp.password = "bramnik";
                airLineComp.countryCode = 11;
                int id = iAdminFS.CreateNewairLine(ltAdmin, airLineComp);
                airLineComp.id = id;
                if (airLineComp == iAdminFS.GetAirLineCompanyByName(ltAdmin, "BramnikAirLine"))
                {
                    actual = true;
                }
                Assert.IsTrue(actual);
            }
        }
        //Admin add new airLineCompany;
        [TestMethod]
        public void ADD_AIRLINECOMPANY2()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                AirLineCompany airLineComp = new AirLineCompany();
                airLineComp.airLineName = "KramnikAirLine";
                airLineComp.userName = "KramnikAdmin";
                airLineComp.password = "kramnik";
                airLineComp.countryCode = 12;
                int id = iAdminFS.CreateNewairLine(ltAdmin, airLineComp);
                airLineComp.id = id;
                if (airLineComp == iAdminFS.GetAirLineCompanyByName(ltAdmin, "KramnikAirLine"))
                {
                    actual = true;
                }
                Assert.IsTrue(actual);
            }
        }


        //Admin update  airLineCompany;
        [TestMethod]
        public void UPDATE_AIRLINECOMPANY()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                AirLineCompany airLineComp = iAdminFS.GetAirLineCompanyByName(ltAdmin,"BramnikAirLine");
                airLineComp.airLineName = "BramnikAirLineCompany";
                iAdminFS.UpdateAirLineDetails(ltAdmin, airLineComp);
                if (airLineComp == iAdminFS.GetAirLineCompanyById(ltAdmin, airLineComp.id))
                {
                    actual = true;
                }
                Assert.IsTrue(actual);
            }
        }
        //Admin create customer
        [TestMethod]
        public void CREATE_CUSTOMER()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                Customer customer = new Customer(0,"Naty", "kazman", "Naty", "121212", "Riga", "+3713456473", "1234567876543212");
                int id = iAdminFS.CreateNewCustomer(ltAdmin, customer);
                customer.id = id;
                if (customer == iAdminFS.GetCustomerByid(ltAdmin, id))
                {
                    actual = true;
                }

                Assert.IsTrue(actual);
            }
        }
        //Admin create customer
        [TestMethod]
        public void CREATE_CUSTOMER2()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                Customer customer = new Customer(0, "Inna", "Kibetz", "inna34", "0021", "oooooo1", "+9723488473", "11133355577767");
                int id = iAdminFS.CreateNewCustomer(ltAdmin, customer);
                customer.id = id;
                if (customer == iAdminFS.GetCustomerByid(ltAdmin, id))
                {
                    actual = true;
                }

                Assert.IsTrue(actual);
            }
        }
        //Admin update customer
        [TestMethod]
        public void UPDATE_CUSTOMER()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                Customer customer = iAdminFS.GetCustomerByUserName(ltAdmin, "inna34");
                customer.userName = "inna35";
                iAdminFS.UpdateCustomerDetails(ltAdmin, customer);
                if (customer == iAdminFS.GetCustomerByid(ltAdmin, customer.id))
                {
                    actual = true;
                }

                Assert.IsTrue(actual);
            }
        }
        //Admin remove customer
        [TestMethod]
        public void REMOVE_CUSTOMER()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                Customer customer = iAdminFS.GetCustomerByUserName(ltAdmin,"inna34");
                iAdminFS.RemoveCustomer(ltAdmin, customer);
                Customer customerCheck = iAdminFS.GetCustomerByUserName(ltAdmin, "inna34");
                if (!(customer is null) && (customerCheck is null))
                {
                    actual = true;
                }

                Assert.IsTrue(actual);
            }
        }
        //Admin remove customer
        [TestMethod]
        public void REMOVE_AIRLINECOMPANY()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                AirLineCompany airLineComp = iAdminFS.GetAirLineCompanyByName(ltAdmin, "BramnikAirLineCompany");               
                iAdminFS.RemoveAirLine(ltAdmin, airLineComp);
                AirLineCompany airLineCompCheck = iAdminFS.GetAirLineCompanyByName(ltAdmin, "BramnikAirLineCompany");
                if (!(airLineComp is null)  &&  (airLineCompCheck is null))
                {
                    actual = true;
                }

                Assert.IsTrue(actual);
            }
        }
        //Admin create flightStatus "panding"
        [TestMethod]
        public void CREATE_FLIGHT_STATUS()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                FlightStatus flStatus = new FlightStatus(0, "panding");
                int id = iAdminFS.CreateFlightStatus(ltAdmin, flStatus);
                flStatus.id = id;
                if (flStatus == iAdminFS.GetFlightStatusById(ltAdmin, id))
                {
                    actual = true;
                }

                Assert.IsTrue(actual);
            }
        }
        //Admin create flightStatus "landing"
        [TestMethod]
        public void CREATE_FLIGHT_STATUS2()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                FlightStatus flStatus = new FlightStatus(0, "landing");
                int id = iAdminFS.CreateFlightStatus(ltAdmin, flStatus);
                flStatus.id = id;
                if (flStatus == iAdminFS.GetFlightStatusById(ltAdmin, id))
                {
                    actual = true;
                }

                Assert.IsTrue(actual);
            }
        }
        // 
        [TestMethod]
        public void GET_AIRLINE_BY_NAME()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                IList<AirLineCompany> list = iAdminFS.GetAllAirLineCompanies();
                string airLineName = list[0].airLineName;
                AirLineCompany comp = iAdminFS.GetAirLineCompanyByName(ltAdmin, airLineName);
                if (comp == list[0])
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void GET_AIRLINE_BY_ID()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                IList<AirLineCompany> list = iAdminFS.GetAllAirLineCompanies();
                int airLineId = list[0].id;
                AirLineCompany comp = iAdminFS.GetAirLineCompanyById(ltAdmin, airLineId);
                if (comp == list[0])
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }

        // Testing AnonimousFacade
        [TestMethod]
        public void GET_ALL_AIRLINECOMPANIES()
        {
            bool actual = false;
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            IAnonymousUserFacade iAnonym = fs.GetFacade<IAnonymousUserFacade>();
            IList<AirLineCompany> list = iAnonym.GetAllAirLineCompanies();
            List<string> compNameList = new List<string>();
            foreach(AirLineCompany comp in list)
            {
                compNameList.Add(comp.airLineName);
            }
            if(compNameList.Contains("KramnikAirLine") && compNameList.Contains("BramnikAirLine"))
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        //Testing AirLineFacade
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
                Country country1 = iAirlineCompanyFS.GetCountryByName(ltAirLine,"Israel");
                Country country2 = iAirlineCompanyFS.GetCountryByName(ltAirLine, "Latvia");
                FlightStatus flStatus = iAirlineCompanyFS.GetFlightStatusByName(ltAirLine,"panding");
                Flight flight = new Flight(0, ltAirLine.User.id,country1.id, country2.id, deppartTime, landingTime, 100);
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
        //
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
                if(listFlight.Count > 0)
                {
                    Flight flight = listFlight[0];
                    int flightId = flight.id;
                    DateTime newDepartTime = new DateTime(2020, 2, 21, 17, 15, 00);
                    flight.departureTime = newDepartTime;
                    iAirlineCompanyFS.UpdateFlight(ltAirLine,flight);
                    if(iAirlineCompanyFS.GetFlightByID(ltAirLine,flightId) == flight)
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
                if(flight == iAirlineCompanyFS.GetFlightByID(ltAirLine, id))
                {
                    actual = true;
                }

                Assert.IsTrue(actual);
            }

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
                Flight flightCheck = iAirlineCompanyFS.GetFlightByID(ltAirLine,flight.id);
                if (!(flight is null) && (flightCheck is null))
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }

        //Testing CustomerFacade
        //Customer create Ticket
        [TestMethod]
        public void CREATE_TICKET()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Customer> ltCustomer = null;
            bool res = ls.TryCustomerLogin("0021", "inna34", out ltCustomer);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInCustomerFacade iCustomerFS = fs.GetFacade<ILoggedInCustomerFacade>();
                Country country1 = iCustomerFS.GetCountryByName(ltCustomer, "Israel");
                List<Flight> list = iCustomerFS.GetFlightsByOriginCountry(ltCustomer, country1);
                Flight fl = list[0];
                // Ticket ticket = new Ticket(0, fl.id,ltCustomer.User.id));               
                Ticket ticket = iCustomerFS.PurchaseTicket(ltCustomer,fl);
                if(ticket == iCustomerFS.GetTicketByAllParametrs(ltCustomer,fl.id,ltCustomer.User.id))
                {
                    actual = true;
                }
                Assert.IsTrue(actual);
            }
        }//Remove Ticket
        [TestMethod]
        public void REMOVE_TICKET_ACTUAL_TRUE()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Customer> ltCustomer = null;
            bool res = ls.TryCustomerLogin("0021", "inna34", out ltCustomer);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInCustomerFacade iCustomerFS = fs.GetFacade<ILoggedInCustomerFacade>();
                IList<Flight> listCustFlights = iCustomerFS.GetAllMyFlights(ltCustomer);
                int flightId = listCustFlights[0].id;
                Ticket ticket = iCustomerFS.GetTicketByAllParametrs(ltCustomer,flightId, ltCustomer.User.id);
                iCustomerFS.RemoveTicket(ltCustomer,ticket);
                Ticket ticketCheck = iCustomerFS.GetTicketByAllParametrs(ltCustomer, flightId, ltCustomer.User.id);
                if(!(ticket is null) && ticketCheck is null)
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }
    
    // CustomerFacade Change password
    [TestMethod]
    public void CHANGE_PASSWORD_ACTUAL_TRUE()
    {
        bool actual = false;
        LoginService ls = new LoginService();
        LoginToken<Customer> ltCustomer = null;
        bool res = ls.TryCustomerLogin("0021", "inna34", out ltCustomer);
        if (res == true)
        {
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            ILoggedInCustomerFacade iCustomerFS = fs.GetFacade<ILoggedInCustomerFacade>();
            iCustomerFS.ChangeMyPassword(ltCustomer, ltCustomer.User.password, "newPassword");
            if(ltCustomer.User.password == "newPassword")
            {
                actual = true;
            }
        }
            Assert.IsTrue(actual);
     }
        //CustomerFacade Change password
        [TestMethod]
        public void CHANGE_PASSWORD_BACK_ACTUAL_TRUE()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Customer> ltCustomer = null;
            bool res = ls.TryCustomerLogin("newPassword", "inna34", out ltCustomer);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInCustomerFacade iCustomerFS = fs.GetFacade<ILoggedInCustomerFacade>();
                iCustomerFS.ChangeMyPassword(ltCustomer, ltCustomer.User.password, "0021");
                if (ltCustomer.User.password == "0021")
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }
    }
}
    

