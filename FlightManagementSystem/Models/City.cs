using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Models
{
    public class City : IPoco
    {
        public int id { get; set; }
        public string city { get; set; }
        public string countryName { get; set; }

        public City()
        {

        }

        public City(int id, string city)
        {
            this.id = id;
            this.city = city;
            this.countryName = countryName;

        }

        public override bool Equals(object obj)
        {
            var city = obj as City;
            return city != null &&
                  id == city.id;
        }

        public override int GetHashCode()
        {
            return this.id;
        }

        public override string ToString()
        {
            return ($"Id = {id} , CountryName = {countryName}, City = {city }");
        }
        public static bool operator ==(City c1, City c2)
        {
            bool res = false;
            if (c1.id == c2.id)
            {
                res = true;
            }
            return res;
        }
        public static bool operator !=(City c1, City c2)
        {

            return !(c1 == c2);
        }
    }
}
