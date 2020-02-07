using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementSystem.Models;

namespace FlightManagementSystem.Modules
{
    class CustomerDAOMSSQL : ICustomerDAO
    {
        static SqlConnection con = new SqlConnection(@"Data Source=BRAMNIK-PC;Initial Catalog=FlightManagementSystem;Integrated Security=True");
        public void SQLConnectionOpen()
        {
            if (con.State != System.Data.ConnectionState.Open)
                con.Open();
        }
        public void SQLConnectionClose()
        {
            if (con.State != System.Data.ConnectionState.Closed)
                con.Close();
        }
        public int Add(Customer ob)
        {        
            int res = 0;
            Customer customer =  GetCustomerByUserName(ob.userName);
            if (customer is null)
            {
                SQLConnectionOpen();
                string str = $"INSERT INTO Customers VALUES('{ob.firstName}','{ob.lastName}','{ob.userName}','{ob.password}','{ob.address}','{ob.phoneNo}','{ob.creditCardNumber}');SELECT SCOPE_IDENTITY()";
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    res = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            else
            {
                throw new CustomerAlreadyExistException("This customer is already exist");
            }
            SQLConnectionClose();
            return res;        
        }

        public Customer Get(int id)
        {
            SQLConnectionOpen();
            Customer customer = null;
            string str = $"SELECT * FROM Customers WHERE ID = {id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        customer = new Customer
                        {
                            id = (int)reader["ID"],
                            firstName = (string)reader["FIRST_NAME"],
                            lastName = (string)reader["LAST_NAME"],
                            userName = (string)reader["USER_NAME"],
                            password = (string)reader["PASSWORD"],
                            address = (string)reader["ADDRESS"],
                            phoneNo = (string)reader["PHONE_NO"],
                            creditCardNumber = (string)reader["CREDIT_CARD_NUMBER"]
                        };                        
                    }
                }
            }
            SQLConnectionClose();
            return customer;
        }

        public List<Customer> GetAll()
        {
            SQLConnectionOpen();
            List<Customer> customersList = new List<Customer>();
            string str = $"SELECT * FROM Customers";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            id = (int)reader["ID"],
                            firstName = (string)reader["FIRST_NAME"],
                            lastName = (string)reader["LAST_NAME"],
                            userName = (string)reader["USER_NAME"],
                            password = (string)reader["PASSWORD"],
                            address = (string)reader["ADDRESS"],
                            phoneNo = (string)reader["PHONE_NO"],
                            creditCardNumber = (string)reader["CREDIT_CARD_NUMBER"]
                        };
                        customersList.Add(customer);
                    }

                }
            }
            SQLConnectionClose();
            return customersList;
        }

        public Customer GetCustomerByUserName(string userName)
        {
            SQLConnectionOpen();
            Customer customer = null;
            string str = $"SELECT * FROM Customers WHERE USER_NAME = '{userName}'";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();                        
                        customer = new Customer
                        {
                             id = (int)reader["ID"],
                             firstName = (string)reader["FIRST_NAME"],
                             lastName = (string)reader["LAST_NAME"],
                             userName = (string)reader["USER_NAME"],
                             password = (string)reader["PASSWORD"],
                             address = (string)reader["ADDRESS"],
                             phoneNo = (string)reader["PHONE_NO"],
                             creditCardNumber = (string)reader["CREDIT_CARD_NUMBER"]
                        };                       
                    }
                }
            }
            SQLConnectionClose();
            return customer;         
        }

        public void Remove(Customer ob)
        { 
            int id = ob.id;
            Customer customer = Get(id);
            if (customer is null)
            {
                throw new CustomerNotExistException("This customer is not exist");
            }
            SQLConnectionOpen();
            string str = $"DELETE FROM Customers WHERE ID = {id}";         
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }

        public void Update(Customer ob)
        {
            Customer customer = Get(ob.id);
            if (customer is null)
            {
                throw new CustomerAlreadyExistException("This customer is already exist");
            }
            SQLConnectionOpen();
            string str = string.Format($"UPDATE Customers SET FIRST_NAME = '{ob.firstName}',LAST_NAME = '{ob.lastName}'," +
                $"USER_NAME = '{ob.userName}', PASSWORD = '{ob.password}',Address = '{ob.address}',PHONE_NO = '{ob.phoneNo}'," +
                $"CREDIT_CARD_NUMBER = '{ob.creditCardNumber}' WHERE ID = {ob.id}");          
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }
        public void RemoveAllFromCustomers()
        {
            SQLConnectionOpen();
            string str = "delete from Customers";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }
        public bool IfTableCustomersIsEmpty()
        {
            SQLConnectionOpen();
            bool res = false;
            string str = $"SELECT COUNT(*) FROM Customers";
            SqlCommand cmd = new SqlCommand(str, con);
            int num = (int)cmd.ExecuteScalar();
            if (num == 0)
            {
                res = true;
            }
            SQLConnectionClose();
            return res;
        }
        public void ChangeMyPassword(Customer customer, string oldPassword, string newPassword)
        {
            customer.password = newPassword;
            Update(customer);
        }

    }
}
