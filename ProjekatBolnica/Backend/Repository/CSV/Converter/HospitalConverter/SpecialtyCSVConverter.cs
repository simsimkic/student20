using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DoctorModel;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.HospitalConverter
{
    public class SpecialtyCSVConverter : ICSVConverter<Specialty>
    {
        private readonly string _delimiter;
        public SpecialtyCSVConverter(string delimiter)
        {
            _delimiter = delimiter;
        }
        public Specialty ConvertCSVFormatToEntity(string specialtyCSVFormat)
        {
            string[] tokens = specialtyCSVFormat.Split(_delimiter.ToCharArray());
            Specialty retVal = new Specialty();
            retVal.Id = long.Parse(tokens[0]);
            retVal.SpecialtyName = tokens[1];
            return retVal;

        }

        public string ConvertEntityToCSVFormat(Specialty specialty)
            => string.Join(_delimiter,
                specialty.GetId(),
                specialty.SpecialtyName);
    }
}
