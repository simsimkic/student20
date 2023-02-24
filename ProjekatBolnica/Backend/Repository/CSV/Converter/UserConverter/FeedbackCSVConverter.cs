using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.UserConverter
{
    class FeedbackCSVConverter : ICSVConverter<Feedback>
    {
        private readonly string _delimiter;

        public FeedbackCSVConverter(string delimiter)
        {
            _delimiter = delimiter;
        }

        public Feedback ConvertCSVFormatToEntity(string entityCSVFormat)
        {
            string[] tokens = entityCSVFormat.Split(_delimiter.ToCharArray());
            return new Feedback(
                long.Parse(tokens[0]),
                tokens[1]);
        }

        public string ConvertEntityToCSVFormat(Feedback entity)
            => string.Join(_delimiter,
               entity.Id,
               entity.Description);
    }
}
