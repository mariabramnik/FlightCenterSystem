﻿using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public interface ILoggedInAdministratorFacade : IAnonymousUserFacade
    {
        void CreateNewairLine(LoginToken<Administrator>token,AirLineCompany comp);
        void CreateNewCustomer(LoginToken<Administrator> token, Customer customer);
        void RemoveAirLine(LoginToken<Administrator> token, AirLineCompany comp);
        void RemoveCustomer(LoginToken<Administrator> token, Customer customer);
        void UpdateAirLineDetails(LoginToken<Administrator> token, AirLineCompany comp);
        void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer);
        void RemoveAllFromDB(LoginToken<Administrator> token);
        bool CheckIfDBIsEmpty(LoginToken<Administrator> token);
        AirLineCompany GetAirLineCompanyByName(LoginToken<Administrator> token,string name);
        void CreateCountry(LoginToken<Administrator> token,Country country);
        Country GetCountryByName(LoginToken<Administrator> token, string name);
        AirLineCompany GetAirLineCompanyById(LoginToken<Administrator> token, int id);
        Customer GetCustomerByid(LoginToken<Administrator> token, int id);
        bool CheckIfCustomersTableIsEmpty(LoginToken<Administrator> token);
        bool CheckIfAirlinesTableIsEmpty(LoginToken<Administrator> token);
        void CreateFlightStatus(LoginToken<Administrator> token, FlightStatus flStatus);
        FlightStatus GetFlightStatusById(LoginToken<Administrator> token,int id);
    }
}