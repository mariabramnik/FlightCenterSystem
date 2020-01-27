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
        public Dictionary<int, FlightStatus> flightStatusDict = new Dictionary<int, FlightStatus>();
        static SqlConnection con = new SqlConnection(@"Data Source=BRAMNIK-PC;Initial Catalog=FlightManagementSystem;Integrated Security=True");
        public void SQLConnectionOpen()
        {
            con.Open();
            DictionaryFilling();
        }

        private void DictionaryFilling()
        {
            string str = "SELECT * FROM FlightStatus";
            SqlCommand cmd = new SqlCommand(str, con);
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
                        flightStatusDict.Add(flSt.id,flSt);
                    }
                }
            }
        }

        public void SQLConnectionClose()
        {
            con.Close();
        }
        public void Add(FlightStatus ob)
        {
            int id = ob.id;
            string statusName = ob.statusName;
            if (flightStatusDict.ContainsKey(id))
            {
                throw new FlStatusAlreadyExistException("Such Flight Status already exist");
            }
            string str = string.Format($"INSERT INTO FlightStatus VALUES({id},'{statusName}')");
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            flightStatusDict.Add(id,ob);
        }

        public FlightStatus Get(int id)
        {
            FlightStatus flightStatus = null;
            if (flightStatusDict.ContainsKey(id))
            {
                flightStatus = flightStatusDict[id];
            }

            return flightStatus;
        }

 

        public List<FlightStatus> GetAll()
        {
            /*
            List<FlightStatus> flightStatusList = new List<FlightStatus>();
            string str = $"SELECT * FROM FlightStatus";
            SqlCommand cmd = new SqlCommand(str, con);
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
            return flightStatusList;
            */
            List<FlightStatus> flightStatusList = new List<FlightStatus>();
            foreach(FlightStatus flSt in flightStatusDict.Values)
            {
                flightStatusList.Add(flSt);
            }
            return flightStatusList;
        }

        public void Remove(FlightStatus ob)
        {
            int id = ob.id;
            if (!flightStatusDict.ContainsKey(id))
            {
                throw new FlightStatusNotExistException("Such FlightStatus not exist");
            }
            string str = string.Format($"DELETE FROM FlightStatus WHERE ID = {id})");
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            flightStatusDict.Remove(id);
        }

        public void Update(FlightStatus ob)
        {
            int id = ob.id;
            if (!flightStatusDict.ContainsKey(id))
            {
                throw new FlightStatusNotExistException("Such FlightStatus not exist");
            }
            string statusName = ob.statusName;
            string str = string.Format($"UPDATE FlightStatus SET STATUS_NAME = '{statusName}' WHERE ID = {id}");
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            flightStatusDict.Remove(id);
            flightStatusDict.Add(id,ob);
           
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
