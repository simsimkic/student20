using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.UserConverter
{
    class LanguageCSVConverter : ICSVConverter<Language>
    {
        private readonly string _delimiter;

        public LanguageCSVConverter(string delimiter)
        {
            _delimiter = delimiter;
        }

        public Language ConvertCSVFormatToEntity(string entityCSVFormat)
        {
            throw new NotImplementedException();
        }

        public string ConvertEntityToCSVFormat(Language entity)
        {
            throw new NotImplementedException();
        }
    }
}
