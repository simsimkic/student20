
using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.PatientModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.PatientRepository
{
   public class MedicalRecordRepository : CSVRepository<MedicalRecord, long>,
        IMedicalRecordRepository,
        IEagerCSVRepository<MedicalRecord, long>
    {

        private const string ENTITY_NAME = "MedicalRecord";
        private readonly IEagerCSVRepository<User, long> _userRepository;
        private readonly IEagerCSVRepository<Medicine, long> _medicineRepository;

        public MedicalRecordRepository(ICSVStream<MedicalRecord> stream, ISequencer<long> sequencer
          , IEagerCSVRepository<User, long> userRepository, IEagerCSVRepository<Medicine, long> medicineRepository) : base(ENTITY_NAME, stream, sequencer)
        {
            _userRepository = userRepository;
            _medicineRepository = medicineRepository;
        }

        public new IEnumerable<MedicalRecord> Find(Func<MedicalRecord, bool> predicate)
        => GetAllEager().Where(predicate);

        public MedicalRecord GetEager(long id)
        {
            var medicalRecord = Get(id);
            medicalRecord.Patient = (Patient)_userRepository.GetEager(medicalRecord.Patient.Id);
            medicalRecord.Doctor = (Doctor)_userRepository.GetEager(medicalRecord.Doctor.Id);
            medicalRecord.Perscription = GetPerscriptionEager(medicalRecord);
            return medicalRecord;
        }

        private Perscription GetPerscriptionEager(MedicalRecord medicalRecord)
        {
            Perscription perscription = new Perscription();
            foreach(Medicine medicine in medicalRecord.Perscription.Medicine)
            {
                perscription.Medicine.Add(_medicineRepository.GetEager(medicine.Id));
            }
            perscription.PeriodOfValidity = medicalRecord.Perscription.PeriodOfValidity;
            return perscription;
        }

        public List<MedicalRecord> GetMedicalRecordsByPatient(long patientId) 
            =>Find(record => record.Patient.Id == patientId).ToList();
       

        public IEnumerable<MedicalRecord> GetAllEager()
        {
            List<MedicalRecord> medicalRecords = new List<MedicalRecord>();
            foreach(MedicalRecord medicalRecord in GetAll())
            {
                medicalRecords.Add(GetEager(medicalRecord.Id));
            }
            return medicalRecords;
        }
    }
}