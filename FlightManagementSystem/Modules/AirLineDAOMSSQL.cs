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
       // public Dictionary<int, AirLineCompany> idAiLineCompanyDict = new Dictionary<int, AirLineCompany>();
       // public Dictionary<string, AirLineCompany> userNameCompanyDict = new Dictionary<string, AirLineCompany>();
        static SqlConnection con = new SqlConnection(@"Data Source=BRAMNIK-PC;Initial Catalog=FlightManagementSystem;Integrated Security=True");
        public void SQLConnectionOpen()
        {
            if(con.State != System.Data.ConnectionState.Open)
              con.Open();
         //   DictionarysFilling();

        }
        public void SQLConnectionClose()
        {
            if (con.State != System.Data.ConnectionState.Closed)
                con.Close();
        }
        private void DictionarysFilling()
        {
            string str = "SELECT * FROM AirlineCompanies";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
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
                            //  idAiLineCompanyDict.Add(comp.id,comp);
                            //  userNameCompanyDict.Add(comp.userName, comp);                       
                    }
                }
            }
        }
        public int Add(AirLineCompany ob)
        {
            int res = 0;
            AirLineCompany comp = GetAirLineCompanyByName(ob.airLineName);
            if (comp is null)
            {
                string str = $"INSERT INTO AirlineCompanies VALUES('{ob.airLineName}','{ob.userName}','{ob.password}',{ob.countryCode});SELECT SCOPE_IDENTITY()";
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    res  = Convert.ToInt32(cmd.ExecuteScalar());
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
            SQLConnectionOpen();

            AirLineCompany comp = null;
            string str = $"SELECT * FROM AirlineCompanies WHERE ID = {id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
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
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
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
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
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
            List<AirLineCompany> compList = new List<AirLineCompany>();
            string str = $"SELECT * FROM AirlineCompanies";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
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
                        compList.Add(comp);
                    }

                }
            }
            return compList;
        }

        public List<AirLineCompany> GetAllAirLinesCompanyByCountry(Country country)
        {           
            List<AirLineCompany> compList = new List<AirLineCompany>() ;
            string str = $"SELECT * FROM AirlineCompanies WHERE COUNTRY_CODE = '{country.id}'";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
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
                            compList.Add(comp);                     
                     }
                }
            }
           
            return compList;
        }

        public void Remove(AirLineCompany ob)
        {
            int id = ob.id;
            AirLineCompany comp = Get(ob.id);
            if(comp is null)
            {
                throw new AirLineCompanyNotExistException("This company not exist");
            }
            string str = $"DELETE FROM AirlineCompanies WHERE ID = {id}";         
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
 
        }

        public void Update(AirLineCompany ob)
        {     
            AirLineCompany comp = Get(ob.id);
            if (comp is null)
            {
                throw new AirLineCompanyNotExistException("This company not exist");
            }
            AirLineCompany oldAirLineComp = Get(ob.id);
            string str = $"UPDATE AirlineCompanies SET AIRLINE_NAME = '{ob.airLineName}',USER_NAME = '{ob.userName}',PASSWORD = '{ob.password}',COUNTRY_CODE = {ob.countryCode} WHERE ID = {ob.id}";           
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }

        }

        public AirLineCompany GetCompanyByPassword(string password)
        {
            AirLineCompany comp = null;
            string str = $"SELECT * FROM AirlineCompanies WHERE PASSWORD = '{password}'";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
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
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                int num = (int)cmd.ExecuteScalar();
                if (num == 0)
                {
                    res = true;
                }
            }
            return res;
        }
        
    }
}
