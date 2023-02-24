/***********************************************************************
 * Module:  StationaryTreatmentService.cs
 * Purpose: Definition of the Class Service.DoctorService.StationaryTreatmentService
 ***********************************************************************/

using Model;
using Model.ManagerModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using System;
using System.Collections.Generic;

namespace Repository.HospitalRepository
{
   public class HospitalStayRepository : 
        CSVRepository<HospitalStay, long>,
        IHospitalStayRepository,
        IEagerCSVRepository<HospitalStay, long>
   {
        private const String ENTITY_NAME = "HospitalStay";
        private readonly IEagerCSVRepository<User, long> _userRepository;
        private readonly IEagerCSVRepository<Resource, long> _resourceRepository;

        public HospitalStayRepository(ICSVStream<HospitalStay> stream,
            ISequencer<long> sequencer,
            IEagerCSVRepository<User, long> userRepository,
            IEagerCSVRepository<Resource, long> resourceRepository) : base(ENTITY_NAME, stream, sequencer)
        {
            _userRepository = userRepository;
            _resourceRepository = resourceRepository;
        }

        public IEnumerable<HospitalStay> GetAllEager()
        {
            List<HospitalStay> hospitalStays = new List<HospitalStay>();
            foreach(HospitalStay hs in GetAll())
            {
                hospitalStays.Add(GetEager(hs.Id));
            }
            return hospitalStays;
        }

        public List<HospitalStay> GetByBed(Bed bed)
        {
            List<HospitalStay> hospitalStays = new List<HospitalStay>();
            foreach(HospitalStay hs in GetAllEager())
            {
                if(hs.Bed.Id == bed.Id) { hospitalStays.Add(hs); }
            }
            return hospitalStays;
        }

        public HospitalStay GetByPatient(Patient patient)
        {
            HospitalStay hospitalStay = null;
            foreach(HospitalStay hs in GetAllEager())
            {
                if(hs.Patient.Id == patient.Id) { hospitalStay = hs; break; }
            }
            return hospitalStay;
        }

        public HospitalStay GetEager(long id)
        {
            HospitalStay hospitalStay = Get(id);
            hospitalStay.Bed = (Bed)_resourceRepository.GetEager(hospitalStay.Bed.Id);
            hospitalStay.Patient = (Patient)_userRepository.GetEager(hospitalStay.Patient.Id);
            return hospitalStay;
        }
    }
}