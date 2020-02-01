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
    public class TestLoginService
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


    }
}
