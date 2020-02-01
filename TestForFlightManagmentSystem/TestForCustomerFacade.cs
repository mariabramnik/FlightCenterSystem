using System;
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
    public class TestForCustomerFacade
    {
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
                if (iCustomerFS is IAnonymousUserFacade)
                {
                    IAnonymousUserFacade iAnonimFS = (IAnonymousUserFacade)iCustomerFS;
                    Country country1 = iAnonimFS.GetCountryByName("Israel");
                    IList<Flight> list = iAnonimFS.GetFlightsByOriginCountry(country1.id);
                    Flight fl = list[0];
                    // Ticket ticket = new Ticket(0, fl.id,ltCustomer.User.id));               
                    Ticket ticket = iCustomerFS.PurchaseTicket(ltCustomer, fl);
                    if (ticket == iCustomerFS.GetTicketByAllParametrs(ltCustomer, fl.id, ltCustomer.User.id))
                    {
                        actual = true;
                    }
                }

                Assert.IsTrue(actual);
            }
        }
        [TestMethod]
        public void CREATE_TICKET2()
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
                    Country country1 = iAnonimFS.GetCountryByName("Latvia");
                    IList<Flight> list = iAnonimFS.GetFlightsByOriginCountry(country1.id);
                    Flight fl = list[0];
                    Ticket ticket = iCustomerFS.PurchaseTicket(ltCustomer, fl);
                    if (ticket == iCustomerFS.GetTicketByAllParametrs(ltCustomer, fl.id, ltCustomer.User.id))
                    {
                        actual = true;
                    }
                }

                Assert.IsTrue(actual);
            }
        }
        [TestMethod]
        public void CREATE_TICKET3()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Customer> ltCustomer = null;
            bool res = ls.TryCustomerLogin("121212", "Naty", out ltCustomer);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInCustomerFacade iCustomerFS = fs.GetFacade<ILoggedInCustomerFacade>();
                if (iCustomerFS is IAnonymousUserFacade)
                {
                    IAnonymousUserFacade iAnonimFS = (IAnonymousUserFacade)iCustomerFS;
                    Country country1 = iAnonimFS.GetCountryByName("Israel");
                    IList<Flight> list = iAnonimFS.GetFlightsByOriginCountry(country1.id);
                    Flight fl = list[0];
                    // Ticket ticket = new Ticket(0, fl.id,ltCustomer.User.id));               
                    Ticket ticket = iCustomerFS.PurchaseTicket(ltCustomer, fl);
                    if (ticket == iCustomerFS.GetTicketByAllParametrs(ltCustomer, fl.id, ltCustomer.User.id))
                    {
                        actual = true;
                    }
                }

                Assert.IsTrue(actual);
            }
        }


        //Remove Ticket
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
                Ticket ticket = iCustomerFS.GetTicketByAllParametrs(ltCustomer, flightId, ltCustomer.User.id);
                iCustomerFS.RemoveTicket(ltCustomer, ticket);
                Ticket ticketCheck = iCustomerFS.GetTicketByAllParametrs(ltCustomer, flightId, ltCustomer.User.id);
                if (!(ticket is null) && ticketCheck is null)
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }

        // CustomerFacade Change password
        [TestMethod]
        public void CUSTOMER_CHANGE_PASSWORD_ACTUAL_TRUE()
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
                if (ltCustomer.User.password == "newPassword")
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }
        //CustomerFacade Change password
        [TestMethod]
        public void CUSTOMER_CHANGE_PASSWORD_BACK_ACTUAL_TRUE()
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
        //CustomerFacade get all my flights
        [TestMethod]
        public void GET_ALL_MY_FLIGHTS()
        {
            bool actual = true;
            LoginService ls = new LoginService();
            LoginToken<Customer> ltCustomer = null;
            bool res = ls.TryCustomerLogin("0021", "inna34", out ltCustomer);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInCustomerFacade iCustomerFS = fs.GetFacade<ILoggedInCustomerFacade>();
                IList<Flight> allMyFlights = iCustomerFS.GetAllMyFlights(ltCustomer);
                foreach (Flight flight in allMyFlights)
                {
                    Ticket ticket = iCustomerFS.GetTicketByAllParametrs(ltCustomer, flight.id, ltCustomer.User.id);
                    if (ticket is null)
                    {
                        actual = false;
                    }
                }

            }
            Assert.IsTrue(actual);
        }
        //CustomerFacade get all my flights
        [TestMethod]
        public void GET_ALL_MY_TICKETS()
        {
            bool actual = false;
            LoginService ls = new LoginService();
            LoginToken<Customer> ltCustomer = null;
            bool res = ls.TryCustomerLogin("0021", "inna34", out ltCustomer);
            if (res == true)
            {
                FlyingCenterSystem fs = FlyingCenterSystem.Instance;
                ILoggedInCustomerFacade iCustomerFS = fs.GetFacade<ILoggedInCustomerFacade>();
                IList<Ticket> allMyTicket = iCustomerFS.GetAllMyTickets(ltCustomer);
                IList<Flight> allMyFlights = iCustomerFS.GetAllMyFlights(ltCustomer);
                if (allMyTicket.Count == allMyFlights.Count)
                {
                    actual = true;
                }
            }
            Assert.IsTrue(actual);
        }
    }
}
