using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public abstract class QueryTemplateBase
    {
        public string StoredProcedureName { get; set; }
        public Dictionary<string, object> StoredProcedureParams { get; set; }

        protected virtual SqlCommand Connect(string storedProcedureName = null, Dictionary<string, object> storedProcedureParams = null)
        {
            SqlCommand cmd = new SqlCommand();
            //cmd.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["flightsystemdb"].ConnectionString);
            cmd.Connection = new SqlConnection(AppConfig.flightsystemdb);
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
        abstract protected List<T> ExecuteQuery<T>(SqlCommand cmd, T item = default(T)) where T : new();

        protected void Close(SqlCommand cmd)
        {
            cmd.Connection.Close();
        }

        public List<T> Run<T>(T item = default(T)) where T : new()
        {
            SqlCommand cmd = Connect(StoredProcedureName, StoredProcedureParams);
            List<T> result = ExecuteQuery<T>(cmd, item);
            Close(cmd);
            return result;
        }
    }
}
