using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DoctorModel;
using Model.ManagerModel;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.HospitalConverter
{
    class AppointmentCSVConverter : ICSVConverter<Appointment>
    {
        private readonly string _delimiter;
        private readonly string _datetimeFormat;
        public AppointmentCSVConverter(string delimiter, string datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
        }
        public Appointment ConvertCSVFormatToEntity(string appointmentCSVFormat)
        {
            string[] tokens = appointmentCSVFormat.Split(_delimiter.ToCharArray());
            Appointment retVal = new Appointment();
            retVal.Id = long.Parse(tokens[0]);
            retVal.Room = new Room(long.Parse(tokens[1]));
            retVal.Patient = new Patient(long.Parse(tokens[2]));
            retVal.DateAndTime = DateTime.ParseExact(tokens[3], _datetimeFormat, null);
            retVal.Duration = new TimeSpan(0, int.Parse(tokens[4]), 0);
            retVal.Doctor = new Doctor(long.Parse(tokens[5]));
            return retVal;
        }
        public string ConvertEntityToCSVFormat(Appointment appointment)
            => string.Join(_delimiter,
                appointment.GetId(),
                appointment.Room.GetId(),
                appointment.Patient.GetId(),
                appointment.DateAndTime.ToString(_datetimeFormat),
                appointment.Duration.TotalMinutes.ToString(),
                appointment.Doctor.GetId());
    }
}
