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
    public class TestForAnanimousFacade
    {
        //anonimous get all airlineCompanies
        [TestMethod]
        public void GET_ALL_AIRLINECOMPANIES()
        {
            bool actual = false;
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            IAnonymousUserFacade iAnonym = fs.GetFacade<IAnonymousUserFacade>();
            IList<AirLineCompany> list = iAnonym.GetAllAirLineCompanies();
            List<string> compNameList = new List<string>();
            foreach (AirLineCompany comp in list)
            {
                compNameList.Add(comp.airLineName);
            }
            if (compNameList.Contains("KramnikAirLine") && compNameList.Contains("BramnikAirLine"))
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        //Anonimous Get all Flights by airlineCompany
        [TestMethod]
        public void GET_ALL_FLIGHTS_BY_AIRLINECOMPANY()
        {
            bool actual = false;
            int res = 0;
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            IAnonymousUserFacade iAnonym = fs.GetFacade<IAnonymousUserFacade>();
            IList<AirLineCompany> list = iAnonym.GetAllAirLineCompanies();
            AirLineCompany comp = list[0];
            IList<Flight> listFlights = iAnonym.GetAllFlightsByAirLineCompanies(comp);
            if (list[0].airLineName == "KramnikAirLine" && listFlights.Count == 4)
            {
                actual = true;
            }
            if (list[0].airLineName == "BramnikAirLine" && listFlights.Count == 1)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        //Anonimous Get all Flights
        [TestMethod]
        public void GET_ALL_FLIGHTS_FOR_ANONYMOUS()
        {
            bool actual = false;
            int res = 0;
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            IAnonymousUserFacade iAnonym = fs.GetFacade<IAnonymousUserFacade>();
            IList<Flight> listAllFlights = iAnonym.GetAllFlights();
            if (listAllFlights.Count == 5)
            {
                actual = true;
            }

            Assert.IsTrue(actual);
        }
        //Anonimous Get all Flights Vacancy
        [TestMethod]
        public void GET_ALL_FLIGHTS_VACANCY()
        {
            bool actual = false;
            int res = 0;
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            IAnonymousUserFacade iAnonym = fs.GetFacade<IAnonymousUserFacade>();
            Dictionary<Flight, int> dictAllFlightsVacancy = iAnonym.GetAllFlightsVacancy();
            if (dictAllFlightsVacancy.Count == 4)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        //Anonimous Get Flights by departure time
        [TestMethod]
        public void GET_ALL_FLIGHTS_BY_DEPARTURE_TIME()
        {
            bool actual = true;
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            IAnonymousUserFacade iAnonym = fs.GetFacade<IAnonymousUserFacade>();
            IList<Flight> listAllFlights = iAnonym.GetAllFlights();
            DateTime dateTime = listAllFlights[0].departureTime;
            IList<Flight> listFlights = iAnonym.GetFlightsByDepartureDate(dateTime);
            foreach (Flight flight in listFlights)
            {
                if (flight.departureTime != dateTime)
                {
                    actual = false;
                }
            }
            Assert.IsTrue(actual);
        }
        //Anonimous Get Flights by landing time
        [TestMethod]
        public void GET_ALL_FLIGHTS_BY_LANDING_TIME()
        {
            bool actual = true;
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            IAnonymousUserFacade iAnonym = fs.GetFacade<IAnonymousUserFacade>();
            IList<Flight> listAllFlights = iAnonym.GetAllFlights();
            DateTime dateTime = listAllFlights[0].landingTime;
            IList<Flight> listFlights = iAnonym.GetFlightsByLandingDate(dateTime);
            foreach (Flight flight in listFlights)
            {
                if (flight.landingTime != dateTime)
                {
                    actual = false;
                }
            }
            Assert.IsTrue(actual);
        }
        //Anonimous Get Flights by origin country 
        [TestMethod]
        public void GET_ALL_FLIGHTS_BY_ORIGIN_COUNTRY_CODE()
        {
            bool actual = true;
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            IAnonymousUserFacade iAnonym = fs.GetFacade<IAnonymousUserFacade>();
            IList<Flight> listAllFlights = iAnonym.GetAllFlights();
            int id = listAllFlights[0].originCountryCode;
            IList<Flight> listFlights = iAnonym.GetFlightsByOriginCountry(id);
            foreach (Flight flight in listFlights)
            {
                if (flight.originCountryCode != id)
                {
                    actual = false;
                }
            }
            Assert.IsTrue(actual);
        }
        //Anonimous Get Flights by origin country 
        [TestMethod]
        public void GET_ALL_FLIGHTS_BY_DESTINATION_COUNTRY_CODE()
        {
            bool actual = true;
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            IAnonymousUserFacade iAnonym = fs.GetFacade<IAnonymousUserFacade>();
            IList<Flight> listAllFlights = iAnonym.GetAllFlights();
            int id = listAllFlights[0].destinationCountryCode;
            IList<Flight> listFlights = iAnonym.GetFlightsByDesinationCountry(id);
            foreach (Flight flight in listFlights)
            {
                if (flight.destinationCountryCode != id)
                {
                    actual = false;
                }
            }
            Assert.IsTrue(actual);
        }
        //Anonimous Get Flights by origin country 
        [TestMethod]
        public void GET_ALL_COUNTRIES()
        {
            bool actual = true;
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            IAnonymousUserFacade iAnonym = fs.GetFacade<IAnonymousUserFacade>();
            List<Country> countryList = iAnonym.GetAllCountries();
            if (countryList.Count == 3)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }

        //Anonimous Get Country by Country_name
        [TestMethod]
        public void ANONYMOUS_GET_COUNTRY_BY_NAME()
        {
            bool actual = true;
            FlyingCenterSystem fs = FlyingCenterSystem.Instance;
            IAnonymousUserFacade iAnonym = fs.GetFacade<IAnonymousUserFacade>();
            Country country = iAnonym.GetCountryByName("Israel");
            if (country.countryName == "Israel")
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }


    }
}
