using Model;
using Model.DoctorModel;
using Model.UserModel;
using System;
using System.Linq;
using System.Threading;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.UserConverter
{
    class UserCSVConverter : ICSVConverter<User>
    {
        private readonly string _delimiter;
        private readonly string _datetimeFormat;
        private object entityCSVFormat;

        public UserCSVConverter(string delimiter, string datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
        }

        public string ConvertEntityToCSVFormat(User entity)
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
                entity.Address.City.country.Code);
            if(entity.Role == TypeOfUser.Specialist)
            {
                Specialist temp = (Specialist)entity;
                retVal += "," + temp.Specialty.Id;
            }
            if(entity.Role == TypeOfUser.Patient)
            {
                Patient temp = (Patient)entity;
                retVal += "," + temp.Allergy;
                retVal += "," + temp.IsGuest;
            }
          
            return retVal;
        }

        User ICSVConverter<User>.ConvertCSVFormatToEntity(string entityCSVFormat)
        {
            string[] tokens = entityCSVFormat.Split(_delimiter.ToCharArray());
            
            TypeOfUser role = (TypeOfUser)Enum.Parse(typeof(TypeOfUser), tokens[3]);
            if (role == TypeOfUser.Specialist) { return ConvertCSVFormatToSpecilist(entityCSVFormat); }
            if (role == TypeOfUser.Patient) { return ConvertCSVFormatToPatient(entityCSVFormat); }

            User retVal = getUserType(role);

            retVal.Id = long.Parse(tokens[0]);
            retVal.Username = tokens[1];
            retVal.Active = bool.Parse(tokens[2]);
            retVal.Role = role;
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

            return retVal;
        }


        private Specialist ConvertCSVFormatToSpecilist(string entityCSVFormat)
        {
            string[] tokens = entityCSVFormat.Split(_delimiter.ToCharArray());
            Specialist retVal = new Specialist();

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
            retVal.DateOfBirth = DateTime.ParseExact(tokens[11], _datetimeFormat, null);
            Country country = new Country(tokens[16], tokens[17]);
            City city = new City(tokens[14], int.Parse(tokens[15]), country);
            Address address = new Address(tokens[12], int.Parse(tokens[13]), city);
            retVal.Address = address;
            retVal.Specialty = new Specialty(long.Parse(tokens[18]));

            return retVal;
        }

        private Patient ConvertCSVFormatToPatient(string entityCSVFormat)
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
            retVal.DateOfBirth = DateTime.ParseExact(tokens[11], _datetimeFormat, null);
            Country country = new Country(tokens[16], tokens[17]);
            City city = new City(tokens[14], int.Parse(tokens[15]), country);
            Address address = new Address(tokens[12], int.Parse(tokens[13]), city);
            retVal.Address = address;
            retVal.Allergy = tokens[18];
            retVal.IsGuest = Boolean.Parse(tokens[19]);

            return retVal;
        }

        private User getUserType(TypeOfUser role)
        {
            switch (role)
            {
                case TypeOfUser.Doctor:
                    return new Doctor();
                case TypeOfUser.Manager:
                    return new Manager();
                case TypeOfUser.Secretary:
                    return new Secretary();
                default:
                    return new User();
            }
        }

    }
}



