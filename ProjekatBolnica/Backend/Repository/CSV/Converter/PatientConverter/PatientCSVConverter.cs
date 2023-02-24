using Model;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.PatientConverter
{
    class PatientCSVConverter : ICSVConverter<Patient>
    {
        private readonly string _delimiter;
        private readonly string _datetimeFormat;
        private object entityCSVFormat;

        public PatientCSVConverter(string delimiter, string datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
        }


        public Patient ConvertCSVFormatToEntity(string entityCSVFormat)
        {
            string[] tokens = entityCSVFormat.Split(_delimiter.ToCharArray());

            Patient retVal = new Patient();

            retVal.Id = long.Parse(tokens[0]);
            retVal.Username = tokens[1];
            retVal.Active = bool.Parse(tokens[2]);
            retVal.Role = (TypeOfUser)Enum.Parse(typeof(TypeOfUser), tokens[3]);
            retVal.Password = tokens[4];
            retVal.Name = tokens[5];
            retVal.Surname = tokens[6];
            retVal.Email = tokens[7];
            retVal.PersonalIDnumber = tokens[8];
            retVal.Telephone = tokens[9];
            retVal.Gender = (Gender)Enum.Parse(typeof(Gender), tokens[10]);
            retVal.DateOfBirth = DateTime.Parse(tokens[11]);

            Country country = new Country(tokens[16], tokens[17]);
            City city = new City(tokens[14], int.Parse(tokens[15]), country);
            Address address = new Address(tokens[12], int.Parse(tokens[13]), city);
            retVal.Address = address;

            retVal.Allergy = tokens[18];
            retVal.IsGuest = bool.Parse(tokens[19]);

            return retVal;
        }

        public string ConvertEntityToCSVFormat(Patient entity)
        {
            string retVal = string.Join(_delimiter,
                entity.Id,
                entity.Username,
                entity.Active,
                entity.Role.ToString(),
                entity.Password,
                entity.Name,
                entity.Surname,
                entity.Email,
                entity.PersonalIDnumber,
                entity.Telephone,
                entity.Gender,
                entity.DateOfBirth.ToString(_datetimeFormat),
                entity.Address.Street,
                entity.Address.Number,
                entity.Address.City.Name,
                entity.Address.City.PostalCode,
                entity.Address.City.country.Name,
                entity.Address.City.country.Code,
                entity.Allergy,
                entity.IsGuest);
            
            return retVal;
        }
    }
}
