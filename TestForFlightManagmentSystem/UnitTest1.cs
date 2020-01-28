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
                airLineComp.countryCode = 1;
                iAdminFS.CreateNewairLine(ltAdmin, airLineComp);
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
                airLineComp.id = 2;
                airLineComp.countryCode = 1;
                iAdminFS.CreateNewairLine(ltAdmin, airLineComp);
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
                AirLineCompany airLineComp = new AirLineCompany();
                airLineComp.airLineName = "BramnikAirLineCompany";
                airLineComp.userName = "BramnikAdmin";
                airLineComp.password = "bramnik";
                airLineComp.id = 1;
                airLineComp.countryCode = 1;
                iAdminFS.UpdateAirLineDetails(ltAdmin, airLineComp);
                if (airLineComp == iAdminFS.GetAirLineCompanyById(ltAdmin, 1))
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
                Customer customer = new Customer(1, "Naty", "kazman", "Naty", "121212", "Riga", "+3713456473", "1234567876543212");
                iAdminFS.CreateNewCustomer(ltAdmin, customer);
                if (customer == iAdminFS.GetCustomerByid(ltAdmin, 1))
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
                Customer customer = new Customer(2, "Inna", "Kibetz", "inna34", "0021", "oooooo1", "+9723488473", "11133355577767");
                iAdminFS.CreateNewCustomer(ltAdmin, customer);
                if (customer == iAdminFS.GetCustomerByid(ltAdmin, 2))
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
                Customer customer = new Customer(1, "Naty", "kazman", "Naty", "121212", "Riga", "+371555555", "1234567876543212");
                iAdminFS.UpdateCustomerDetails(ltAdmin, customer);
                if (customer == iAdminFS.GetCustomerByid(ltAdmin, 1))
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
                Customer customer = new Customer(1, "Naty", "kazman", "Naty", "121212", "Riga", "+371555555", "1234567876543212");
                iAdminFS.RemoveCustomer(ltAdmin, customer);
                if (iAdminFS.CheckIfCustomersTableIsEmpty(ltAdmin))
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
                AirLineCompany airLineComp = new AirLineCompany();
                airLineComp.airLineName = "BramnikAirLineCompany";
                airLineComp.userName = "BramnikAdmin";
                airLineComp.password = "bramnik";
                airLineComp.id = 1;
                airLineComp.countryCode = 1;
                iAdminFS.RemoveAirLine(ltAdmin, airLineComp);
                if (iAdminFS.CheckIfAirlinesTableIsEmpty(ltAdmin))
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
                FlightStatus flStatus = new FlightStatus(1, "panding");
                iAdminFS.CreateFlightStatus(ltAdmin, flStatus);
                if (flStatus == iAdminFS.GetFlightStatusById(ltAdmin, 1))
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
                FlightStatus flStatus = new FlightStatus(2, "landing");
                iAdminFS.CreateFlightStatus(ltAdmin, flStatus);
                if (flStatus == iAdminFS.GetFlightStatusById(ltAdmin, 2))
                {
                    actual = true;
                }

                Assert.IsTrue(actual);
            }
        }


        // Testing AnonimousFacade
        [TestMethod]
        public void GetAllAirLineCompanies()
        {
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            IAnonymousUserFacade iAnonym = fs.GetFacade<IAnonymousUserFacade>();
            IList<AirLineCompany> list = iAnonym.GetAllAirLineCompanies();
            AirLineCompany comp1 = new AirLineCompany(1, "BramnikAirLineCompany", "BramnikAdmin", "bramnik", 1);
            AirLineCompany comp2 = new AirLineCompany(2, "KramnikAirLineCompany", "KramnikAdmin", "kramnik", 1);
            List<AirLineCompany> originList = new List<AirLineCompany>() { comp1, comp2 };
            bool actual = list.SequenceEqual(originList);
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
                DateTime dt1 = new DateTime(2020, 2, 20, 10, 20, 00);
                DateTime dt2 = new DateTime(2020, 2, 20, 18, 10, 00);
                Flight flight = new Flight(1, 1, 1, 2, dt1, dt2, 100);
                iAirlineCompanyFS.CreateFlight(ltAirLine, flight);
                if (flight == iAirlineCompanyFS.GetFlightByID(ltAirLine, 1))
                {
                    actual = true;
                }

                Assert.IsTrue(actual);
            }

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
                DateTime dt1 = new DateTime(2020, 2, 21, 16, 15, 00);
                DateTime dt2 = new DateTime(2020, 2, 21, 08, 05, 00);
                Flight flight = new Flight(2, 1, 2, 1, dt1, dt2, 120);
                iAirlineCompanyFS.CreateFlight(ltAirLine, flight);
                if (flight == iAirlineCompanyFS.GetFlightByID(ltAirLine, 2))
                {
                    actual = true;
                }

                Assert.IsTrue(actual);
            }

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
      
                Ticket ticket = new Ticket(1,1,2);
                Flight flight = iCustomerFS.GetFlightByIdFlight(ltCustomer,1);
                iCustomerFS.PurchaseTicket(ltCustomer,flight);
                IList<Flight> listFlights = new List<Flight>();
                IList<Flight> originFlights = new List<Flight>();
                originFlights.Add(flight);
                listFlights = iCustomerFS.GetAllMyFlights(ltCustomer);
                actual = originFlights.SequenceEqual(listFlights);

                Assert.IsTrue(actual);
            }

        }
    }
}
    

