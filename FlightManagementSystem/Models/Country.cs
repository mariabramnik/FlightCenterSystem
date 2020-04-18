using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Models
{
    public class Country : IPoco
    {
        public int id { get; set; }
        public string countryName { get; set; }
        
        public Country()
        {

        }

        public Country(int id, string countryName)
        {
            this.id = id;
            this.countryName = countryName;
           
        }

        public override bool Equals(object obj)
        {
            var country = obj as Country;
            return country != null &&
                  id == country.id;
        }

        public override int GetHashCode()
        {
            return this.id;
        }

        public override string ToString()
        {
            return ($"Id = {id} , CountryName = {countryName}");
        }
        public static bool operator==(Country c1,Country c2)
        {
            bool res = false;
            if (c1.id == c2.id)
            {
                res = true;
            }
            return res;
        }
        public static bool operator !=(Country c1, Country c2)
        {
  
            return !(c1==c2);
        }
    }
}

