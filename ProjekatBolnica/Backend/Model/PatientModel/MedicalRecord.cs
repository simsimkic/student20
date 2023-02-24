
using Model.DoctorModel;
using ProjekatBolnica.Backend.Model.PatientModel;
using ProjekatBolnica.Backend.Repository;
using System;
using System.Text;

namespace Model.PatientModel
{
    public class MedicalRecord : IIdentifiable<long>
    {
        

        public long Id { get; set; }
        public Patient Patient { get; set; }
        public Perscription Perscription { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime ExamDate { get; set; }
        public String ExamReport { get; set; }
        public TypeOfRecord TypeOfRecord { get; set; }

        public MedicalRecord() { }
        public MedicalRecord(long id, Perscription perscription, Doctor doctor,  Patient patient, DateTime examDate, string examReport, TypeOfRecord typeOfRecord)
        {
            Id = id;
            Patient = patient;
            Perscription = perscription;
            Doctor = doctor;
            ExamDate = examDate;
            ExamReport = examReport;
            TypeOfRecord = typeOfRecord;
        }

        public MedicalRecord(long id, Perscription perscription, Doctor doctor, DateTime examDate, string examReport)
        {
            Id = id;
            Perscription = perscription;
            Doctor = doctor;
            ExamDate = examDate;
            ExamReport = examReport;
        }

        public long GetId() => Id;

        public void SetId(long id) => Id = id;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ExamDate.ToShortDateString() + Environment.NewLine);
            sb.Append(TypeOfRecord + Environment.NewLine);
            sb.Append(ExamReport + Environment.NewLine);
            return sb.ToString();
        }
    }
}