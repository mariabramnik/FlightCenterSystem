using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules.Login
{
    public class LoginService : ILoginService
    {
        AirLineDAOMSSQL _airLineDAO;
        CustomerDAOMSSQL _customerDAO;
        public bool TryAdminLogin(string password, string userName, out LoginToken<Administrator> loginToken)
        {
            bool res = false;
            loginToken = null;
            if (password == "9999" && userName == "admin")
            {
                loginToken = new LoginToken<Administrator>();
                loginToken.User = new Administrator();
                res = true;
            }
            return res;
        }

        public bool TryAirLineLogin(string password, string userName, out LoginToken<AirLineCompany> loginToken)
        {
            bool res = false;
            loginToken = null;
            AirLineCompany comp = null;
            _airLineDAO = new AirLineDAOMSSQL();
             comp = _airLineDAO.GetAirLineByUserName(userName);
             if(!(comp is null))
            { 
                if (comp.password != password)
                {
                    throw new WrongPasswordException("entered password is not correct");
                }
                else
                {
                    loginToken = new LoginToken<AirLineCompany>();
                    loginToken.User = comp;
                    res = true;
                }
            }
            return res;

        }

        public bool TryCustomerLogin(string password, string userName, out LoginToken<Customer> loginToken)
        {
            bool res = false;
            loginToken = null;
            _customerDAO = new CustomerDAOMSSQL();
            Customer cust = _customerDAO.GetCustomerByUserName(userName);
            if(!(cust is null))
            {
                if(cust.password != password)
                {
                    throw new WrongPasswordException("entered password is not correct");
                }
                else
                {
                    res = true;
                    loginToken = new LoginToken<Customer>();
                    loginToken.User = cust;
                }
            }
            return res;
        }

        public bool TryLogin(string password, string userName, out ILoginToken token)
        {
            bool res = false;
            LoginToken<Administrator> ltAdmin = null;
            res = TryAdminLogin(password, userName, out ltAdmin);
  
            //throw new FunnyException("HA HA");
            if (res == false)
            {
                LoginToken<AirLineCompany> ltAirLineCompany = null;
                res = TryAirLineLogin(password, userName, out ltAirLineCompany);
                if (res == false)
                {
                    LoginToken<Customer> ltCustomer = null;
                    res = TryCustomerLogin(password, userName,out ltCustomer);
                    if (res == false)
                    {
                        throw new UserNotFoundException("Not Found");
                    }
                    else
                    {
                        token = ltCustomer;
                    }
                }
                else
                {
                    token = ltAirLineCompany;
                }
            }
            else
            {
                token = ltAdmin;
            }
 
            return res;
        }

    }
}
