using System;
using System.Collections.Generic;
using System.Text;
using Pra.MonitoraatJH2.Core.Enums;

namespace Pra.MonitoraatJH2.Core.Entities
{
    public class Address
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public AddressType AddressType { get; set; } 

        public Address(string street, string houseNumber, string postalCode, string city, string country, AddressType addressType)
        {
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
            City = city;
            Country = country;
            AddressType = addressType;
        }
        public override string ToString()
        {
            return $"{AddressType.ToString()} : {Street}, {HouseNumber} - {PostalCode} {City} - {Country}";
        }
    }
}
