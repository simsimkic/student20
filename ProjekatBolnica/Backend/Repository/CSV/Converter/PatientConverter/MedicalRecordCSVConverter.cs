using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.PatientModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.PatientConverter
{
    class MedicalRecordCSVConverter : ICSVConverter<MedicalRecord>
    {
        private readonly string _delimiter;
        private readonly string _datetimeFormat;



        public MedicalRecordCSVConverter(string delimiter, string datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
        }

        public MedicalRecord ConvertCSVFormatToEntity(string entityCSVFormat)
        {
            string[] tokens = entityCSVFormat.Split(_delimiter.ToCharArray());
            Perscription perscription = ConvertCSVFormatToPerscription(tokens[1]);
            Doctor doctor = new Doctor(long.Parse(tokens[2]));
            Patient patient = new Patient(long.Parse(tokens[3]));
            TypeOfRecord type= (TypeOfRecord)Enum.Parse(typeof(TypeOfRecord), tokens[6]);

            return new MedicalRecord(long.Parse(tokens[0]), perscription, doctor, patient, DateTime.ParseExact(tokens[4], _datetimeFormat, null),tokens[5],type);
        }

        private Perscription ConvertCSVFormatToPerscription(string perscription)
        {
            Perscription retVal = new Perscription();
            retVal.Medicine = new List<Medicine>();
            string[] tokens = perscription.Split("|".ToCharArray());
            string[] medicine = tokens[0].Split(";".ToCharArray());
            string[] period = tokens[1].Split(";".ToCharArray());
            foreach (string s in medicine)
            {
                if(s != string.Empty) { retVal.Medicine.Add(new Medicine(long.Parse(s))); }
            }
            retVal.PeriodOfValidity.StartTime = DateTime.ParseExact(period[0], TimePeriod.DATE_FORMAT, null);
            retVal.PeriodOfValidity.EndTime = DateTime.ParseExact(period[1], TimePeriod.DATE_FORMAT, null);
            return retVal;
        }

        public string ConvertEntityToCSVFormat(MedicalRecord entity)
        {
            string perscriptionMedicine = string.Join(";", entity.Perscription.Medicine.Select(x => x.Id));
            string perscriptionValidity = string.Join(";",
                                            entity.Perscription.PeriodOfValidity.StartTime.ToString(_datetimeFormat),
                                            entity.Perscription.PeriodOfValidity.EndTime.ToString(_datetimeFormat));
            string perscription = string.Join("|", perscriptionMedicine, perscriptionValidity);

            return string.Join(_delimiter,
                                entity.Id,
                                perscription,
                                entity.Doctor.Id,
                                entity.Patient.Id,
                                entity.ExamDate.ToString(_datetimeFormat),
                                entity.ExamReport,
                                entity.TypeOfRecord.ToString());
        }
    }
}
