using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public class QueryRemoveItem : QueryTemplateBase
    {
        protected override List<T> ExecuteQuery<T>(SqlCommand cmd, T item = default(T))
        {
            Type type_of_record = typeof(T);

            string tableName = "";

            var customAttributes = (MyTableNameAttribute[])type_of_record.GetCustomAttributes(typeof(MyTableNameAttribute), true);
            if (customAttributes.Length > 0)
            {
                tableName = customAttributes[0].TableName;
            }
            else
            {
                throw new ArgumentException($"Poco {type_of_record.FullName} does not contain MyTableNameAttribute");
            }
            int idValue = GetPropertyIdValue(item, type_of_record);

            cmd.CommandText = $"DELETE FROM {tableName} WHERE ID = {idValue}";

            cmd.ExecuteNonQuery();

            return null;
        }

        private static int GetPropertyIdValue<T>(T item, Type type_of_record) where T : new()
        {
            int idValue = -1;
            string str = "";
            foreach (var oneProperty in type_of_record.GetProperties())
            {
                // let's assume id is identity (auto-increment)
                if (oneProperty.Name.ToUpper() == "ID")
                {
                    str +=  oneProperty.GetValue(item);
                    idValue = Int32.Parse(str);
                    break;
                }
            }

            return idValue;
        }
    }
}
