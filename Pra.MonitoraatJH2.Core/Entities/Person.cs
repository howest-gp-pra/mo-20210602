using System;
using System.Collections.Generic;
using System.Text;
using Pra.MonitoraatJH2.Core.Interfaces;


namespace Pra.MonitoraatJH2.Core.Entities
{
    public abstract class Person:IPerson
    {
        private string name;
        private DateTime dateOfBirth;

        public string Name
        {
            get { return name; }
            set 
            {
                value = value.Trim();
                if (string.IsNullOrEmpty(value))
                    throw new Exception("De naam kan niet leeg zijn");
                name = value; 
            }
        }
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set 
            {
                if (value < new DateTime(1920, 1, 1))
                    value = new DateTime(1920, 1, 1);
                if (value > DateTime.Now)
                    value = DateTime.Now;
                dateOfBirth = value; 
            }
        }
        public bool? Gender { get; set; }  // true = male, false = female, null = unknown
        public int AgeInYears
        {
            get
            {
                // vandaag = 15/05/2021
                // geboren = 18/05/2000 ==> leeftijd = 20 jaar
                // geboren = 10/05/2000 ==> leeftijd = 21 jaar
                // geboren = 15/05/2000 ==> leeftijd = 21 jaar
                int age = DateTime.Now.Year - dateOfBirth.Year;
                if (dateOfBirth.AddYears(age) < DateTime.Now)
                    age--;
                return age;
            }
        }
        public string NativeLanguage { get; set; }
        public string Phone { get; set; }
        public List<string> Emails { get;}
        public List<Address> Addresses { get; }

        public Person(string name, DateTime dateOfBirth, string phone, bool? gender = null )
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Phone = phone;
            Gender = gender;
            Emails = new List<string>();
            Addresses = new List<Address>();
        }
        public Person(string name, DateTime dateOfBirth, string phone, string nativeLanguage, bool? gender = null ) 
            : this(name, dateOfBirth, phone, gender )
        {
            NativeLanguage = nativeLanguage;
        }

        public override string ToString()
        {
            return name;
        }
        public virtual string ShowDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(name);
            sb.AppendLine($"Geboortedatum : {dateOfBirth.ToString("dd/MM/yyyy")}");
            sb.AppendLine($"Leeftijd : {AgeInYears} jaar");
            string strGender;
            if (Gender == null)
                strGender = "onbekend";
            else
            {
                strGender = (bool)Gender ? "Man" : "Vrouw";
            }
            sb.AppendLine($"Geslacht : {strGender}");
            sb.AppendLine($"Moedertaal : {NativeLanguage}");
            sb.AppendLine($"Telefoon : {Phone}");
            int counter = 1;
            foreach(string email in Emails)
            {
                sb.AppendLine($"Email #{counter} : {email}");
                counter++;
            }
            counter = 1;
            foreach(Address address in Addresses)
            {
                sb.AppendLine($"Adres #{counter} : {address.ToString()}");
                counter++;
            }
            return sb.ToString();
        }
    }
}
