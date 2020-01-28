using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementSystem.Models;

namespace FlightManagementSystem.Modules
{
    class CountryDAOMSSQL : ICountryDAO
    {
       // public Dictionary<int, Country> idCountriesDict = new Dictionary<int, Country>();
       // public Dictionary<string, Country> nameCountriesDict = new Dictionary<string, Country>();
        static SqlConnection con = new SqlConnection(@"Data Source=BRAMNIK-PC;Initial Catalog=FlightManagementSystem;Integrated Security=True");
        public void SQLConnectionOpen()
        {
            con.Open();
         //   DictionaryFilling();
        }

        private void DictionaryFilling()
        {
            Country country = null;
            string str = "SELECT * FROM Countries";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {    
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        country = new Country
                        {
                                id = (int)reader["ID"],
                                countryName = (string)reader["COUNTRY_NAME"],

                        };
                          //  idCountriesDict.Add(country.id, country);
                          //  nameCountriesDict.Add(country.countryName, country);                      
                    }
                }
            }
        }

        public void SQLConnectionClose()
        {
            con.Close();
        }
        public int Add(Country ob)
        {
            int res = 0;            
            string countryName = ob.countryName;
            Country country = GetByName(ob.countryName);
            if (country is null)
            {
                string str = $"INSERT INTO Countries VALUES('{countryName}');SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    res = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            else
            {
                throw new CountryAlredyExistException("Such Country already exist");
            }
            // idCountriesDict.Add(id, ob);
            // nameCountriesDict.Add(countryName, ob);
            return res;
        }

        public Country Get(int id)
        {
            Country country = null;
            string str = $"SELECT * FROM Countries WHERE ID = {id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        country = new Country
                        {
                             id = (int)reader["ID"],
                             countryName = (string)reader["COUNTRY_NAME"],
                        };
                    }
                }
            }

            return country;
        }

        public Country GetByName(string name)
        {
            Country country = null;
            string str = $"SELECT * FROM Countries WHERE COUNTRY_NAME = '{name}'";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        reader.Read();
                        country = new Country
                        {
                            id = (int)reader["ID"],
                            countryName = (string)reader["COUNTRY_NAME"],
                        };
                        
                    }
                }
            }
            return country;
        }

        public List<Country> GetAll()
        {
            List<Country> countriesList = new List<Country>();
            string str = $"SELECT * FROM Countries";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Country country = new Country
                        {
                            id = (int)reader["ID"],
                            countryName = (string)reader["COUNTRY_NAME"],
                        };
                        countriesList.Add(country);
                    }
                }
            }       
            return countriesList;
        }
      
        public void Remove(Country ob)
        {
            string countryName = ob.countryName;
            Country country = Get(ob.id);
            if(country is null)
            {
               throw new CountryNotExistException("This Country not exist");
            }
            string str = string.Format($"DELETE FROM Countries WHERE ID = {ob.id})");                      
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
          //  idCountriesDict.Remove(id);
          //  nameCountriesDict.Remove(countryName);           
        }

        public void Update(Country ob)
        {
            Country country = Get(ob.id);
            if (country is null)
            {
                throw new CountryNotExistException("This Country not exist");
            }
            string str = $"UPDATE Countries SET COUNTRY_NAME = {ob.countryName} WHERE ID = {ob.id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }          
        }
        public void RemoveAllFromCountries()
        {
            string str = "delete from Countries";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public bool IfTableCountriesIsEmpty()
        {
            bool res = false;
            string str = $"SELECT COUNT(*) FROM Countries";
            SqlCommand cmd = new SqlCommand(str, con);
            int num = (int)cmd.ExecuteScalar();
            if (num == 0)
            {
                res = true;
            }
            return res;
        }
    }
}
