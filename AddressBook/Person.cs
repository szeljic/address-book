using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    class Person
    {
        private string fullName;
        private string address;
        private string phone;
        private string email;

        public Person(string fullName, string address, string phone, string email)
        {
            this.fullName = fullName;
            this.address = address;
            this.phone = phone;
            this.email = email;
        }

        public string getFullName()
        {
            return this.fullName;
        }

        public string getAddress()
        {
            return this.address;
        }

        public string getPhone()
        {
            return this.phone;
        }

        public string getEmail()
        {
            return this.email;
        }
    }
}
