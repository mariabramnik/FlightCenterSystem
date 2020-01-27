using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Models
{
   public class AirLineCompany : IPoco
    {
        public int id;
        public string airLineName;
        public string userName;
        public string password;
        public int countryCode;

        public AirLineCompany() { }
        public AirLineCompany( int id,string airlineName, string userName, string password, int countryCode)
        {
            this.id = id;
            this.airLineName = airlineName;
            this.userName = userName;
            this.password = password;
            this.countryCode = countryCode;
        }

        public override bool Equals(object obj)
        {
            var company = obj as AirLineCompany;
            return !(company is null) &&
                 id == company.id;
        }

        public override int GetHashCode()
        {
            return this.id;
        }

        public override string ToString()
        {
            return ($"{id} ,{airLineName} ,{userName}, {password} ,{countryCode}");

        }
        public static bool operator==(AirLineCompany com1, AirLineCompany com2)
        {
            bool res = false;
            if(com1.id == com2.id)
            {
                res = true;
            }
            return res;
        }
        public static bool operator !=(AirLineCompany com1, AirLineCompany com2)
        {
            return !(com1 == com2);
        }

    }
  
    
}
