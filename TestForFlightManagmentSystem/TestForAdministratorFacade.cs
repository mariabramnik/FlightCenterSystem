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
    public class UnitTestAdministratorFacade
    {
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

        //Admin get country by name
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
                IList<Country> listCountries = iAdminFS.GetAllCountries();
                string name = listCountries[0].countryName;
                if (listCountries[0] == iAdminFS.GetCountryByName(name))
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
                Country country = iAdminFS.GetCountryByName("Israel");
                airLineComp.countryCode = country.id;
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
                Country country = iAdminFS.GetCountryByName("Latvia");
                airLineComp.countryCode = country.id;
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
                AirLineCompany airLineComp = iAdminFS.GetAirLineCompanyByName(ltAdmin, "BramnikAirLine");
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
                Customer customer = new Customer(0, "Naty", "kazman", "Naty", "121212", "Riga", "+3713456473", "1234567876543212");
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
        //Admin update customer
        [TestMethod]
        public void UPDATE_CUSTOMER2()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                Customer customer = iAdminFS.GetCustomerByUserName(ltAdmin, "inna35");
                customer.userName = "inna34";
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
                Customer customer = iAdminFS.GetCustomerByUserName(ltAdmin, "inna34");
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
                if (!(airLineComp is null) && (airLineCompCheck is null))
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
        //Admin create flightStatus "flying"
        [TestMethod]
        public void CREATE_FLIGHT_STATUS3()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Administrator> ltAdmin = null;
            bool res = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInAdministratorFacade iAdminFS = fs.GetFacade<ILoggedInAdministratorFacade>();
                FlightStatus flStatus = new FlightStatus(0, "flying");
                int id = iAdminFS.CreateFlightStatus(ltAdmin, flStatus);
                flStatus.id = id;
                if (flStatus == iAdminFS.GetFlightStatusById(ltAdmin, id))
                {
                    actual = true;
                }

                Assert.IsTrue(actual);
            }
        }
        // admin get airline by name
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
        // admin get airline by id
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


    }
}
