/***********************************************************************
 * Module:  Ingredients.cs
 * Purpose: Definition of the Class Utility.Ingredients
 ***********************************************************************/

using ProjekatBolnica.Backend.Repository;
using System;

namespace Model.ManagerModel
{
    public class Ingredient : IIdentifiable<long>
    {
        public long Id { get; set; }

        public String Name { get; set; }

        public Ingredient(long id, String name)
        {
            Id = id;
            Name = name;
        }

        public Ingredient(long id)
        {
            Id = id;
        }

        public Ingredient(String name)
        {
            Name = name;
        }

        public Ingredient()
        {
        }

        public long GetId() => Id;

        public void SetId(long id) => Id = id;

        public override string ToString()
        {
            return Name;
        }
    }
}

