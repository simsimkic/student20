
using Model.PatientModel;
using Repository.HospitalRepository;
using System.Collections.Generic;

namespace Repository.PatientRepository
{
    public interface IMedicalRecordRepository : IRepository<MedicalRecord, long>
    {
         List<MedicalRecord> GetMedicalRecordsByPatient(long patientId);
   }
}