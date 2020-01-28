using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    class FlightStatusDAOMSSQL : IFlightStatusDAO
    {
       // public Dictionary<int, FlightStatus> flightStatusDict = new Dictionary<int, FlightStatus>();
        static SqlConnection con = new SqlConnection(@"Data Source=BRAMNIK-PC;Initial Catalog=FlightManagementSystem;Integrated Security=True");
        public void SQLConnectionOpen()
        {
            con.Open();
          //  DictionaryFilling();
        }
        public void SQLConnectionClose()
        {
            con.Close();
        }

        private void DictionaryFilling()
        {
            string str = "SELECT * FROM FlightStatus";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            FlightStatus flSt = new FlightStatus
                            {
                                id = (int)reader["ID"],
                                statusName = (string)reader["STATUS_NAME"],
                            };
                            //  flightStatusDict.Add(flSt.id,flSt);
                        }
                    }
                }
            }
        }

        public int Add(FlightStatus ob)
        {
            int res = 0;
            int id = ob.id;
            string statusName = ob.statusName;
            FlightStatus flightSt = GetFlightStatusByFlightStatusName(ob.statusName);
            if (flightSt is null)
            {
                string str = string.Format($"INSERT INTO FlightStatus VALUES({id},'{statusName}');SELECT SCOPE_IDENTITY()");
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    res = (int)cmd.ExecuteScalar();
                }
            }
            return res;
        }

        private FlightStatus GetFlightStatusByFlightStatusName(string statusName)
        {
            FlightStatus flStatus = null;
            string str = $"SELECT * FROM FlightStatus WHERE STATUS_NAME = '{statusName}'";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    flStatus = new FlightStatus
                    {
                        id = (int)reader["ID"],
                        statusName = (string)reader["STATUS_NAME"]
                    };
                }
            }
            return flStatus;
        }


        public FlightStatus Get(int id)
        {
            FlightStatus flStatus = null;
            string str = $"SELECT * FROM FlightStatus WHERE ID = '{id}'";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        flStatus = new FlightStatus
                        {
                            id = (int)reader["ID"],
                            statusName = (string)reader["STATUS_NAME"]
                        };
                    }
                }
            }
            return flStatus;
        }

        public List<FlightStatus> GetAll()
        {
            List<FlightStatus> flightStatusList = new List<FlightStatus>();
            string str = $"SELECT * FROM FlightStatus";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FlightStatus flightStatus = new FlightStatus
                        {
                            id = (int)reader["ID"],
                            statusName = (string)reader["STATUS_NAME"]
                        };
                        flightStatusList.Add(flightStatus);
                    }
                }
            }
            return flightStatusList;
        }

        public void Remove(FlightStatus ob)
        {
            int id = ob.id;
            FlightStatus flStatus = Get(id);
            if (flStatus is null)
            {
                throw new FlightStatusNotExistException("Such FlightStatus not exist");
            }
            string str = $"DELETE FROM FlightStatus WHERE ID = {id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(FlightStatus ob)
        {
            FlightStatus flStatus = Get(ob.id);
            if (flStatus is null)
            {
                throw new FlightStatusNotExistException("Such FlightStatus not exist");
            }
            string statusName = ob.statusName;
            string str = string.Format($"UPDATE FlightStatus SET STATUS_NAME = '{statusName}' WHERE ID = {ob.id}");
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
           
        }
        public void RemoveAllFromFlightStatus()
        {
            string str = "delete from FlightStatus";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public bool IfTableFlightStatusIsEmpty()
        {
            bool res = false;
            string str = $"SELECT COUNT(*) FROM FlightStatus";
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
