/***********************************************************************
 * Module:  Medicine.cs
 * Purpose: Definition of the Class Utility.Medicine
 ***********************************************************************/

using ProjekatBolnica.Backend.Repository;
using System;
using System.Collections.Generic;

namespace Model.ManagerModel
{
    public class Medicine : Resource, IIdentifiable<long>
    {
        public List<Ingredient> Ingredient { get; set; }
        public String ExpiryDate { get; set; }
        public String MethodOfUse { get; set; }
        public String ProtectiveMeasures { get; set; }

        public Medicine(List<Ingredient> ingredient, String expiryDate, String methodOfUse, String protectiveMeasures)
        {
            Ingredient = ingredient;
            ExpiryDate = expiryDate;
            MethodOfUse = methodOfUse;
            ProtectiveMeasures = protectiveMeasures;
        }

        public Medicine()
        {
        }

        public Medicine(long id)
        {
            Id = id;
        }

        /// <pdGenerated>default getter</pdGenerated>
        public List<Ingredient> GetIngredient()
        {
            if (Ingredient == null)
                Ingredient = new List<Ingredient>();
            return Ingredient;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetIngredient(List<Ingredient> newIngredient)
        {
            RemoveAllIngredient();
            foreach (Ingredient oIngredient in newIngredient)
                AddIngredient(oIngredient);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddIngredient(Ingredient newIngredient)
        {
            if (newIngredient == null)
                return;
            if (this.Ingredient == null)
                this.Ingredient = new List<Ingredient>();
            if (!this.Ingredient.Contains(newIngredient))
                this.Ingredient.Add(newIngredient);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveIngredient(Ingredient oldIngredient)
        {
            if (oldIngredient == null)
                return;
            if (this.Ingredient != null)
                if (this.Ingredient.Contains(oldIngredient))
                    this.Ingredient.Remove(oldIngredient);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllIngredient()
        {
            if (Ingredient != null)
                Ingredient.Clear();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}