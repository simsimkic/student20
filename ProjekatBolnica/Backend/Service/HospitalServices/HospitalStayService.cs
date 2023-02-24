/***********************************************************************
 * Module:  StationaryTreatmentService.cs
 * Purpose: Definition of the Class Service.HospitalServices.StationaryTreatmentService
 ***********************************************************************/

using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using Repository.HospitalRepository;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Service.HospitalServices
{
   public class HospitalStayService : IHospitalStayService
   {
        public HospitalStayRepository _repository;

        public HospitalStayService(HospitalStayRepository repository)
        {
            _repository = repository;
        }

        public void Delete(HospitalStay hospitalStay) => _repository.Delete(hospitalStay);

        public TimePeriod DurationOfLayingInBed(Bed bed)
        {
            throw new NotImplementedException();
        }

        public HospitalStay Get(long id) => _repository.Get(id);

        public IEnumerable<HospitalStay> GetAll() => _repository.GetAllEager();

        public bool isBedOccupied(Bed bed, TimePeriod period)
        {
            foreach(HospitalStay hs in GetByBed(bed))
            {    
                if((period.StartTime > hs.Duration.StartTime && period.StartTime > hs.Duration.EndTime) ||
                    (period.EndTime < hs.Duration.StartTime && period.EndTime < hs.Duration.EndTime))
                {
                    continue;
                } else { return true; }
            }
            return false;
        }

        public bool isHospitalized(Patient patient)
        {
            if(GetByPatient(patient) == null) { return false; } else { return true; }
        }

        public bool IsRelocationAllowed(Patient patient, Bed bed)
        {
            HospitalStay hospitalStay = GetByPatient(patient);
            TimePeriod period = new TimePeriod
            {
                StartTime = DateTime.Now.Date,
                EndTime = hospitalStay.Duration.EndTime
            };
            return !isBedOccupied(bed, period);
        }

        public void New(HospitalStay hospitalStay)
        {
            if(!isBedOccupied(hospitalStay.Bed, hospitalStay.Duration))
            {
                _repository.New(hospitalStay);
            }
        }

        public bool relocatePatient(Patient patient, Bed bed)
        {
            if(IsRelocationAllowed(patient, bed))
            {
                Set(changeBed(GetByPatient(patient), bed));
                return true;
            } else { return false; }
        }

        public HospitalStay changeBed(HospitalStay hospitalStay, Bed bed)
        {
            hospitalStay.Bed = bed;
            return hospitalStay;
        }

        public void Set(HospitalStay hospitalStay) => _repository.Set(hospitalStay);

        public HospitalStay GetByPatient(Patient patient) => _repository.GetByPatient(patient);

        public List<HospitalStay> GetByBed(Bed bed) => _repository.GetByBed(bed);
    }
}