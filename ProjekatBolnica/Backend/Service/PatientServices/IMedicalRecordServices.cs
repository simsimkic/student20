using Model;
using Model.PatientModel;
using Service.HospitalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatBolnica.Backend.Service.PatientServices
{
    public interface IMedicalRecordServices : IService<MedicalRecord, long>
    {
         List<MedicalRecord> GetMedicalRecordByPatient(Patient patient);
    }
}
