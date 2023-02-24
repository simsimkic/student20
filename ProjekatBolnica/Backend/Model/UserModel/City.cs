
using System;

namespace Model.UserModel
{
    public class City
    {
        public String Name { get; set; }
        public int PostalCode { get; set; }
        public Country country { get; set; }


        public City(String name)
        {
            Name = name;
        }


        public City(String name, int postalCode, Country country)
        {
            Name = name;
            PostalCode = postalCode;
            this.country = country;
        }

        public City() { }

        /// <pdGenerated>default parent getter</pdGenerated>
        public Country GetCountry()
        {
            return country;
        }

        /// <pdGenerated>default parent setter</pdGenerated>
        /// <param>newCountry</param>
        public void SetCountry(Country newCountry)
        {
            if (this.country != newCountry)
            {
                if (this.country != null)
                {
                    Country oldCountry = this.country;
                    this.country = null;
                    oldCountry.RemoveCity(this);
                }
                if (newCountry != null)
                {
                    this.country = newCountry;
                    this.country.AddCity(this);
                }
            }
        }

    }
}