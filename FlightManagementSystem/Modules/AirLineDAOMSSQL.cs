using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementSystem.Models;

namespace FlightManagementSystem.Modules
{
    class AirLineDAOMSSQL : IAirLineDAO
    {
        public Dictionary<int, AirLineCompany> idAiLineCompanyDict = new Dictionary<int, AirLineCompany>();
        public Dictionary<string, AirLineCompany> userNameCompanyDict = new Dictionary<string, AirLineCompany>();
        static SqlConnection con = new SqlConnection(@"Data Source=BRAMNIK-PC;Initial Catalog=FlightManagementSystem;Integrated Security=True");
        public void SQLConnectionOpen()
        {
            con.Open();
            DictionarysFilling();

        }

        private void DictionarysFilling()
        {
            string str = "SELECT * FROM AirlineCompanies";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        AirLineCompany comp = new AirLineCompany
                        {
                            id = (int)reader["ID"],
                            airLineName = (string)reader["AIRLINE_NAME"],
                            userName = (string)reader["USER_NAME"],
                            password = (string)reader["PASSWORD"],
                            countryCode = (int)reader["COUNTRY_CODE"]
                        };
                        idAiLineCompanyDict.Add(comp.id,comp);
                        userNameCompanyDict.Add(comp.userName, comp);
                    }
                }
            }
        }

        public void SQLConnectionClose()
        {
            con.Close();
        }
       
        public int Add(AirLineCompany ob)
        {
            int res = 0;
            int id = ob.id;
            string airLineName = ob.airLineName;
            string userName = ob.userName;
            string password = ob.password;
            int countryCode = ob.countryCode;
            AirLineCompany comp = GetAirLineCompanyByName(airLineName);
            if (comp is null)
            {
                string str = $"INSERT INTO AirlineCompanies VALUES({id},'{airLineName}','{userName}','{password}',{countryCode});SELECT SCOPE_IDENTITY()";
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    res  =(int) cmd.ExecuteScalar();
                }
            }
            else
            {
                throw new AirLineCompanyAlreadyExistException("Such Airline Company already exist");
            }
            // idAiLineCompanyDict.Add(id,ob);
            // userNameCompanyDict.Add(userName, ob);
            return res;
            
        }

        public AirLineCompany Get(int id)
        {
            AirLineCompany comp = null;
            string str = $"SELECT * FROM AirlineCompanies WHERE ID = {id}";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    {
                        comp = new AirLineCompany
                        {
                            id = (int)reader["ID"],
                            airLineName = (string)reader["AIRLINE_NAME"],
                            userName = (string)reader["USER_NAME"],
                            password = (string)reader["PASSWORD"],
                            countryCode = (int)reader["COUNTRY_CODE"]
                        };
                    }
                }
            }
            
            return comp;

        }
        public AirLineCompany GetAirLineCompanyByName(string airLineName)
        {
            AirLineCompany comp = null;
            string str = $"SELECT * FROM AirlineCompanies WHERE AIRLINE_NAME = '{airLineName}'";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    {
                        comp = new AirLineCompany
                        {
                            id = (int)reader["ID"],
                            airLineName = (string)reader["AIRLINE_NAME"],
                            userName = (string)reader["USER_NAME"],
                            password = (string)reader["PASSWORD"],
                            countryCode = (int)reader["COUNTRY_CODE"]
                        };
                    }
                }
            }

            return comp;
        }

        public AirLineCompany GetAirLineByUserName(string userName)
        {          
            AirLineCompany comp = null;
            string str = $"SELECT * FROM AirlineCompanies WHERE USER_NAME = '{userName}'";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    {
                        comp = new AirLineCompany
                        {
                            id = (int)reader["ID"],
                            airLineName = (string)reader["AIRLINE_NAME"],
                            userName = (string)reader["USER_NAME"],
                            password = (string)reader["PASSWORD"],
                            countryCode = (int)reader["COUNTRY_CODE"]
                        };
                    }
                }
            }
            return comp;

        }

        public List<AirLineCompany> GetAll()
        {
            List<AirLineCompany> companies = new List<AirLineCompany>();
            foreach (KeyValuePair<int,AirLineCompany> pair in idAiLineCompanyDict)
            {
                companies.Add(pair.Value);
            }
            return companies;
        }

        public List<AirLineCompany> GetAllAirLinesCompanyByCountry(Country country)
        {
            
            List<AirLineCompany> compList = new List<AirLineCompany>() ;
            string str = $"SELECT * FROM AirlineCompanies WHERE COUNTRY_CODE = '{country.id}'";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    {
                       AirLineCompany comp = new AirLineCompany
                        {
                            id = (int)reader["ID"],
                            airLineName = (string)reader["AIRLINE_NAME"],
                            userName = (string)reader["USER_NAME"],
                            password = (string)reader["PASSWORD"],
                            countryCode = (int)reader["COUNTRY_CODE"]
                        };
                        compList.Add(comp);
                    }
                }
            }
           
            return compList;
        }

        public void Remove(AirLineCompany ob)
        {
            int id = ob.id;
            if (!idAiLineCompanyDict.ContainsKey(id))
            {
                throw new AirLineCompanyNotExistException("Such AirLine Company not exist");
            }
            string str = $"DELETE FROM AirlineCompanies WHERE ID = {id}";
           
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            idAiLineCompanyDict.Remove(id);
            
        }

        public void Update(AirLineCompany ob)
        {
            int id = ob.id;
            if (!idAiLineCompanyDict.ContainsKey(id))
            {
                throw new AirLineCompanyNotExistException("Such AirLine Company not exist");
            }
            AirLineCompany oldAirLineComp = Get(id);
            string oldCompUserName = oldAirLineComp.userName;
            string airLineName = ob.airLineName;
            string userName = ob.userName;
            string password = ob.password;
            int countryCode = ob.countryCode;
            string str = $"UPDATE AirlineCompanies SET AIRLINE_NAME = '{airLineName}',USER_NAME = '{userName}',PASSWORD = '{password}',COUNTRY_CODE = {countryCode} WHERE ID = {id}";
            
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }

            idAiLineCompanyDict.Remove(id);
            userNameCompanyDict.Remove(oldCompUserName);
            idAiLineCompanyDict.Add(id, ob);
            userNameCompanyDict.Add(userName, ob);

        }

        public AirLineCompany GetCompanyByPassword(string password)
        {
            AirLineCompany comp = null;
            string str = $"SELECT * FROM AirlineCompanies WHERE PASSWORD = '{password}'";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    {
                        comp = new AirLineCompany
                        {
                            id = (int) reader["ID"],
                            airLineName = (string)reader["AIRLINE_NAME"],
                            userName = (string)reader["USER_NAME"],
                            password = (string)reader["PASSWORD"],
                            countryCode = (int)reader["COUNTRY_CODE"]
                        };
                    }
                }
            }
           
            return comp;
        }
        public void RemoveAllFromAirLines()
        {
            string str = "delete from AirlineCompanies";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public bool IfTableAirlinesIsEmpty()
        {
            bool res = false;
            string str = $"SELECT COUNT(*) FROM AirlineCompanies";
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
