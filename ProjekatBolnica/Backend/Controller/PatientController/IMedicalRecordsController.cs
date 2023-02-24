using Controller.HospitalController;
using Model;
using Model.PatientModel;
using System.Collections.Generic;

namespace ProjekatBolnica.Backend.Controller.PatientController
{
    public interface IMedicalRecordsController : IController<MedicalRecord, long>
    {
        List<MedicalRecord> GetMedicalRecordByPatient(Patient patient);
    }
}
