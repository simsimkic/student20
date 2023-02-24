using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.HospitalConverter
{
    class OperationCSVConverter : ICSVConverter<Operation>
    {
        private readonly string _delimiter;
        private readonly string _datetimeFormat;
        public OperationCSVConverter(string delimiter, string datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
        }
        public Operation ConvertCSVFormatToEntity(string operationCSVFormat)
        {
            string[] tokens = operationCSVFormat.Split(_delimiter.ToCharArray());
            Operation retVal = new Operation();
            retVal.Id = long.Parse(tokens[0]);
            retVal.Room = new Room(long.Parse(tokens[1]));
            retVal.Patient = new Patient(long.Parse(tokens[2]));
            retVal.DateAndTime = DateTime.ParseExact(tokens[3], _datetimeFormat, null);
            retVal.Duration = new TimeSpan(0, int.Parse(tokens[4]), 0);
            retVal.Specialist = new Specialist(long.Parse(tokens[5]));
            return retVal;
        }
        public string ConvertEntityToCSVFormat(Operation operation)
            => string.Join(_delimiter,
                operation.GetId(),
                operation.Room.GetId(),
                operation.Patient.GetId(),
                operation.DateAndTime.ToString(_datetimeFormat),
                operation.Duration.TotalMinutes.ToString(),
                operation.Specialist.GetId());
    }   
}
