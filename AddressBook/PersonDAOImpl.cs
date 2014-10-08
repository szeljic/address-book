using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    class PersonDAOImpl : PersonDAO
    {
        public List<Person> getAllPersons()
        {
            DBConnection connection = new DBConnection();
            return connection.Select("*", "addressbook", null);
        }

        public List<Person> getPerson(string fullName)
        {
            DBConnection connection = new DBConnection();
            return connection.Select("fullName='" + fullName + "'", "addressbook", null);
        }

        public void updatePerson(string oldPersonsFullName, Person person)
        {
            DBConnection connection = new DBConnection();
            connection.Update(oldPersonsFullName, person, "addressbook");
        }

        public void deletePerson(string fullName)
        {
            DBConnection connection = new DBConnection();
            connection.Delete(fullName, "addressbook");
        }

        public void insertPerson(Person person)
        {
            DBConnection connection = new DBConnection();
            connection.Insert(person, "addressbook");
        }

    }
}
