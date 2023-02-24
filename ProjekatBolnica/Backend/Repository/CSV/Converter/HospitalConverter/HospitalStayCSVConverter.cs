using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.DoctorModel;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.HospitalConverter
{
    class HospitalStayCSVConverter : ICSVConverter<HospitalStay>
    {
        private readonly string _delimiter;
        private readonly string _datetimeFormat;

        public HospitalStayCSVConverter(string delimiter, string datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
        }
        public HospitalStay ConvertCSVFormatToEntity(string hospitalStayCSVFormat)
        {
            string[] tokens = hospitalStayCSVFormat.Split(_delimiter.ToCharArray());
            HospitalStay retVal = new HospitalStay();
            retVal.Id = long.Parse(tokens[0]);
            retVal.Patient = new Patient(long.Parse(tokens[1]));
            retVal.Bed = new Bed(long.Parse(tokens[2]));
            TimePeriod duration = new TimePeriod();
            duration.StartTime = DateTime.ParseExact(tokens[3], _datetimeFormat, null);
            duration.EndTime = DateTime.ParseExact(tokens[4], _datetimeFormat, null);
            retVal.Duration = duration;
            return retVal;
        }

        public string ConvertEntityToCSVFormat(HospitalStay hospitalStay)
            => string.Join(_delimiter,
                hospitalStay.GetId(),
                hospitalStay.Patient.GetId(),
                hospitalStay.Bed.GetId(),
                hospitalStay.Duration.StartTime.ToString(TimePeriod.DATE_FORMAT),
                hospitalStay.Duration.EndTime.ToString(TimePeriod.DATE_FORMAT));
    }
}
