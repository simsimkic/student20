
using System;

namespace Model.UserModel
{
    public class Address
    {
        public City City { get; set; }
        public String Street { get; set; }
        public int Number { get; set; }
        private string _token;

        public Address(String streat, int number, City city)
        {
            Street = streat;
            Number = number;
            City = city;
        }

        public Address() { }

    }
}