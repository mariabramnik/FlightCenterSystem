using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Models
{
    public class Customer : IPoco
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string phoneNo{ get; set; }
        public string creditCardNumber { get; set; }

        public Customer() { }

        public Customer(int id, string firstname, string lastname, string userName, string password,
            string address, string phoneNo, string creditCardNumber)
        {
            this.id = id;
            this.firstName = firstname;
            this.lastName = lastname;
            this.userName = userName;
            this.password = password;
            this.address = address;
            this.phoneNo = phoneNo;
            this.creditCardNumber = creditCardNumber;
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null &&
                   id == customer.id;
        }

        public override int GetHashCode()
        {
            return this.id;
        }

        public override string ToString()
        {
            return ($"{id} ,{firstName} ,{lastName} ,{userName} ,{password} ,{address},{phoneNo} ,{creditCardNumber}");
        }
        public static bool operator==(Customer c1 ,Customer c2)
        {
            bool res = false;
            if (c1.id == c2.id)
            {
                res = true;
            }
            return res;
        }
        public static bool operator !=(Customer c1, Customer c2)
        { 
            return !(c1==c2);
        }
    }
}
