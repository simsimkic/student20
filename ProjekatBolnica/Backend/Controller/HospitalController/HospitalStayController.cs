/***********************************************************************
 * Module:  StationaryTreatmentController.cs
 * Purpose: Definition of the Class Controller.HospitalController.StationaryTreatmentController
 ***********************************************************************/

using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using Service.HospitalServices;
using System;
using System.Collections.Generic;

namespace Controller.HospitalController
{
    public class HospitalStayController : IHospitalStayController
    {

        private readonly IHospitalStayService _service;
        public HospitalStayController(IHospitalStayService service)
        {
            _service = service;
        }
        public void Delete(HospitalStay hospitalStay) => _service.Delete(hospitalStay);
        public TimePeriod DurationOfLayingInBed(Bed bed)
        {
            throw new NotImplementedException();
        }
        public HospitalStay Get(long id) => _service.Get(id);
        public IEnumerable<HospitalStay> GetAll() => _service.GetAll();
        public List<HospitalStay> GetByBed(Bed bed) => _service.GetByBed(bed);
        public HospitalStay GetByPatient(Patient patient) => _service.GetByPatient(patient);
        public bool isHospitalized(Patient patient) => _service.isHospitalized(patient);
        public void New(HospitalStay hospitalStay) => _service.New(hospitalStay);
        public bool relocatePatient(Patient patient, Bed bed) => _service.relocatePatient(patient, bed);
        public void Set(HospitalStay hospitalStay) => _service.Set(hospitalStay);

    }
}