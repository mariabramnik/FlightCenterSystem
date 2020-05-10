using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public interface ICountryDAO : IBasic<Country>
    {
        void RemoveAllFromCountries();
        bool IfTableCountriesIsEmpty();
        Country GetByName(string name);
        List<City> GetAllByCountry(string countryName);
    }
}
