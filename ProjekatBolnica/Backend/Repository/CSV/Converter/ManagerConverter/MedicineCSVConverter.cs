using Model.ManagerModel;
using System;
using System.Collections.Generic;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.ManagerConverter
{
    class MedicineCSVConverter : ICSVConverter<Medicine>
    {
        private readonly String _delimiter;

        public MedicineCSVConverter(String delimiter)
        {
            _delimiter = delimiter;
        }

        public Medicine ConvertCSVFormatToEntity(String entityCSVFormat)
        {
            String[] tokens = entityCSVFormat.Split(_delimiter.ToCharArray());
            List<Ingredient> ingredient = new List<Ingredient>();
            ingredient.Add(new Ingredient(tokens[0]));

            Medicine retVal = new Medicine();
            retVal.Ingredient = ingredient;
            retVal.Name = tokens[1];
            retVal.ExpiryDate = tokens[2];
            retVal.MethodOfUse = tokens[3];
            retVal.ProtectiveMeasures = tokens[4];

            return retVal;
        }

        public String ConvertEntityToCSVFormat(Medicine entity)
            => String.Join(_delimiter,
                   entity.Ingredient,
                   entity.Name,
                   entity.ExpiryDate,
                   entity.MethodOfUse,
                   entity.ProtectiveMeasures);
    }
}
