
using Model;
using Model.PatientModel;
using ProjekatBolnica.Backend.Repository.PatientRepository;
using ProjekatBolnica.Backend.Service.PatientServices;
using Repository.PatientRepository;
using Repository.UserRepository;
using Service.HospitalServices;
using System;
using System.Collections.Generic;

namespace Service.PatientServices
{
    public class MedicalRecordService : IMedicalRecordServices
    {

        private readonly IMedicalRecordRepository _repository;
        private readonly IUserRepository _userRepository;




        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository)
        {
            _repository = medicalRecordRepository;

        }

        public void Delete(MedicalRecord record)=>_repository.Delete(record);


        public MedicalRecord Get(long id) => _repository.Get(id);

        public IEnumerable<MedicalRecord> GetAll() => _repository.GetAll();

        public void New(MedicalRecord record) => _repository.New(record);

        public void Set(MedicalRecord record) => _repository.Set(record);

        public List<MedicalRecord> GetMedicalRecordByPatient(Patient patient)
        => _repository.GetMedicalRecordsByPatient(patient.GetId());
    }
}