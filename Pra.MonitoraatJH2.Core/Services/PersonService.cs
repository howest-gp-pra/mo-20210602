using System;
using System.Collections.Generic;
using System.Text;
using Pra.MonitoraatJH2.Core.Entities;
using Pra.MonitoraatJH2.Core.Enums;
using Pra.MonitoraatJH2.Core.Interfaces;
using System.Linq;

namespace Pra.MonitoraatJH2.Core.Services
{
    public class PersonService : IPersonService
    {
        private List<Person> persons;
        public List<Person> Persons
        {
            get { return persons; }
        }
        //// alternatief
        //public List<Person> Persons { get; private set; }

        public PersonService()
        {
            persons = new List<Person>();
            Seeding();
        }
        private void Seeding()
        {
            Student student = new Student("Taer Guy", new DateTime(2000, 5, 1), "0497123456", Diploma.Niveau4, "Nederlands", true);
            student.Emails.Add("guy.taer@student.howest.be");
            student.Emails.Add("guytaer@hotmail.com");
            student.Addresses.Add(new Address("Steenstraat", "5A", "8000", "Brugge", "België", AddressType.Domicilie));
            student.Addresses.Add(new Address("Beekweg", "312", "9000", "Gent", "België", AddressType.Vader));
            persons.Add(student);

            student = new Student("Tumas Marie", new DateTime(2000, 6, 1), "0497112233", Diploma.Niveau5, "Frans", false);
            student.Emails.Add("marie.tumas@student.howest.be");
            student.Emails.Add("marie.tumas@gmail.com");
            student.Addresses.Add(new Address("Klaverstraat", "1", "8000", "Brugge", "België", AddressType.Domicilie));
            persons.Add(student);

            student = new Student("Tura Will", new DateTime(1900,5,1), "0497979204", Diploma.Niveau1, "Nederlands");
            student.Emails.Add("will.tura@student.howest.be");
            student.Emails.Add("will@tura.be");
            student.Emails.Add("will.tura@hotmail.be");
            student.Addresses.Add(new Address("Hootkaai", "666", "8630", "Veurne", "België", AddressType.Domicilie));
            student.Addresses.Add(new Address("Place Patat", "1", "06000", "Nice", "France", AddressType.Vrijetijd));
            persons.Add(student);


            Address address = new Address("Grote markt", "1", "1000", "Brussel", "België", AddressType.Domicilie);

            Staff staff = new Staff("Merckx Eddy", new DateTime(1985, 1, 1), "0497515151", Function.Lector, Diploma.Niveau7, "Nederlands", true);
            staff.Emails.Add("eddy.merckx@howest.be");
            staff.Addresses.Add(address);
            persons.Add(staff);

            staff = new Staff("Wild Kim", new DateTime(1987, 11, 1), "0497666666", Function.Administratie, Diploma.Niveau5, "Engels", false);
            staff.Emails.Add("kim.wild@howest.be");
            staff.Addresses.Add(address);
            persons.Add(staff);

            staff = new Staff("Degrande Els", new DateTime(1961, 11, 27), "0497951753", Function.Kader, Diploma.Niveau8, "Nederlands", false);
            staff.Emails.Add("els.degrande@howest.be");
            staff.Emails.Add("elsdegrande@telenet.be");
            staff.Addresses.Add(new Address("Grote markt", "1", "8000", "Brugge", "België", AddressType.Domicilie));
            staff.Addresses.Add(new Address("Grote markt", "5", "9000", "Gent", "België", AddressType.Vrijetijd));
            persons.Add(staff);

            persons = persons.OrderBy(p => p.Name).ToList();
        }
        public List<Type> GetPersonTypes()
        {
            List<Type> types = new List<Type>();
            foreach(Person person in persons)
            {
                Type type = person.GetType();
                bool found = false;
                foreach(Type zoektype in types)
                {
                    if(zoektype == type)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    types.Add(type);
            }
            return types;
        }
        public List<Person> GetPersonsPerType(Type type)
        {
            List<Person> filteredPersons = new List<Person>();
            foreach(Person person in Persons)
            {
                if(person.GetType() == type)
                {
                    filteredPersons.Add(person);
                }
            }
            return filteredPersons;
        }
    }
}
