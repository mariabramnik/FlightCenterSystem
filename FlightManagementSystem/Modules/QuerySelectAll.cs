using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    class QuerySelectAll : QueryTemplateBase
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
                throw new ArgumentException($"Poco {type_of_record.FullName} does not contain MyTableNameAttribute");

            cmd.CommandText = $"SELECT * FROM {tableName}";

            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

            List<T> list = new List<T>();
            while (reader.Read() == true)
            {
                T one_record = new T();

                // short and sweet
                //type_of_record.GetProperties().ToList().ForEach(p => p.SetValue(one_record, reader[p.Name]));
                foreach (var oneProperty in type_of_record.GetProperties())
                {
                    string columnName = oneProperty.Name;

                    var customFieldAttributes = (MyFieldNameAttribute[])oneProperty.GetCustomAttributes(typeof(MyFieldNameAttribute), true);
                    if (customFieldAttributes.Length > 0)
                    {
                        columnName = customFieldAttributes[0].ColumnName;
                    }

                    var value = reader[columnName];
                    oneProperty.SetValue(one_record, value);
                }
                list.Add(one_record);
            }

            return list;
        }
    }
}
