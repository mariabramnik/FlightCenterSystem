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
            if (con.State != System.Data.ConnectionState.Open)
                con.Open();
          //  DictionaryFilling();
        }
        public void SQLConnectionClose()
        {
            if (con.State != System.Data.ConnectionState.Closed)
                con.Close();
        }

        private void DictionaryFilling()
        {
            SQLConnectionOpen();
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
            SQLConnectionClose();
        }

        public int Add(FlightStatus ob)
        {       
            int res = 0;
            int id = ob.id;
            string statusName = ob.statusName;
            FlightStatus flightSt = GetFlightStatusByFlightStatusName(ob.statusName);
            if (flightSt is null)
            {
                SQLConnectionOpen();
                string str = string.Format($"INSERT INTO FlightStatus VALUES('{statusName}');SELECT SCOPE_IDENTITY()");
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    res = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            SQLConnectionClose();
            return res;
        }

        private FlightStatus GetFlightStatusByFlightStatusName(string statusName)
        {
            SQLConnectionOpen();
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
            SQLConnectionClose();
            return flStatus;
        }


        public FlightStatus Get(int id)
        {
            SQLConnectionOpen();
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
            SQLConnectionClose();
            return flStatus;
        }

        public IList<FlightStatus> GetAll()
        {
            SQLConnectionOpen();
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
            SQLConnectionClose();
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
            SQLConnectionOpen();
            string str = $"DELETE FROM FlightStatus WHERE ID = {id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }

        public void Update(FlightStatus ob)
        {    
            FlightStatus flStatus = Get(ob.id);
            if (flStatus is null)
            {
                throw new FlightStatusNotExistException("Such FlightStatus not exist");
            }
            SQLConnectionOpen();
            string statusName = ob.statusName;
            string str = string.Format($"UPDATE FlightStatus SET STATUS_NAME = '{statusName}' WHERE ID = {ob.id}");
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }
        public void RemoveAllFromFlightStatus()
        {
            SQLConnectionOpen();
            string str = "delete from FlightStatus";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }
        public bool IfTableFlightStatusIsEmpty()
        {
            SQLConnectionOpen();
            bool res = false;
            string str = $"SELECT COUNT(*) FROM FlightStatus";
            SqlCommand cmd = new SqlCommand(str, con);
            int num = (int)cmd.ExecuteScalar();
            if (num == 0)
            {
                res = true;
            }
            SQLConnectionClose();
            return res;
        }
        public FlightStatus GetFlightStatusByName(string statusName)
        {
            SQLConnectionOpen();
            FlightStatus flStatus = null;
            string str = $"SELECT * FROM FlightStatus WHERE STATUS_NAME = '{statusName}'";
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
            SQLConnectionClose();
            return flStatus;
        }
    }
}
