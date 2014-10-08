using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    interface PersonDAO
    {
        List<Person> getAllPersons();
        List<Person> getPerson(string fullName);
        void updatePerson(string oldPersonsFullName, Person person);
        void deletePerson(string fullName);
        void insertPerson(Person person);
    }
}
