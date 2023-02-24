
using Controller.HospitalController;
using Model;
using Model.DoctorModel;
using Model.PatientModel;
using Model.UserModel;
using ProjekatBolnica.Backend.Controller.PatientController;
using ProjekatBolnica.Backend.Model.PatientModel;
using ProjekatBolnica.Backend.Service.PatientServices;
using Service.HospitalServices;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Controller.PatientController
{
    public class MedicalRecordController : IMedicalRecordsController
    {
        private readonly IMedicalRecordServices _service;

        public MedicalRecordController(IMedicalRecordServices service)
        {
            _service = service;
        }

        public void Delete(MedicalRecord record) => _service.Delete(record);

        public MedicalRecord Get(long id) => _service.Get(id);

        public IEnumerable<MedicalRecord> GetAll() => _service.GetAll();

        public void New(MedicalRecord record) => _service.New(record);

        public void Set(MedicalRecord record) => _service.Set(record);

        public List<MedicalRecord> GetMedicalRecordByPatient(Patient patient) => _service.GetMedicalRecordByPatient(patient);

    }
}