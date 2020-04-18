using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public interface IAirLineDAO : IBasic<AirLineCompany>
    {
        AirLineCompany GetAirLineByUserName(string userName);
        List<AirLineCompany> GetAllAirLinesCompanyByCountry(Country country);
        AirLineCompany GetCompanyByPassword(string password);
        AirLineCompany GetAirLineCompanyByName(string name);
        void RemoveAllFromAirLines();
        bool IfTableAirlinesIsEmpty();

    }
}
