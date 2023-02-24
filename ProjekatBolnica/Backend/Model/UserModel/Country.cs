using System;

namespace Model.UserModel
{
    public class Country
    {

        public String Name { get; set; }
        public String Code { get; set; }



        public System.Collections.ArrayList city;

        public Country(String name, String code)
        {
            Name = name;
            Code = code;
        }


        /// <pdGenerated>default getter</pdGenerated>
        public System.Collections.ArrayList GetCity()
        {
            if (city == null)
                city = new System.Collections.ArrayList();
            return city;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetCity(System.Collections.ArrayList newCity)
        {
            RemoveAllCity();
            foreach (City oCity in newCity)
                AddCity(oCity);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddCity(City newCity)
        {
            if (newCity == null)
                return;
            if (this.city == null)
                this.city = new System.Collections.ArrayList();
            if (!this.city.Contains(newCity))
            {
                this.city.Add(newCity);
                newCity.SetCountry(this);
            }
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveCity(City oldCity)
        {
            if (oldCity == null)
                return;
            if (this.city != null)
                if (this.city.Contains(oldCity))
                {
                    this.city.Remove(oldCity);
                    oldCity.SetCountry((Country)null);
                }
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllCity()
        {
            if (city != null)
            {
                System.Collections.ArrayList tmpCity = new System.Collections.ArrayList();
                foreach (City oldCity in city)
                    tmpCity.Add(oldCity);
                city.Clear();
                foreach (City oldCity in tmpCity)
                    oldCity.SetCountry((Country)null);
                tmpCity.Clear();
            }
        }



    }
}