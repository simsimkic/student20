using Model.ManagerModel;
using System;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.ManagerConverter
{
    class IngredientsCSVConverter : ICSVConverter<Ingredient>

    {
        private readonly String _delimiter;

        public IngredientsCSVConverter(String delimiter)
        {
            _delimiter = delimiter;
        }

        public Ingredient ConvertCSVFormatToEntity(String entityCSVFormat)
        {
            String[] tokens = entityCSVFormat.Split(_delimiter.ToCharArray());

            return new Ingredient(
                long.Parse(tokens[0]),
                tokens[1]);
        }

        public String ConvertEntityToCSVFormat(Ingredient entity)
            => String.Join(_delimiter,
                   entity.Id,
                   entity.Name);
    }
}
