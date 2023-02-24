/***********************************************************************
 * Module:  IStationaryTreatmentRepository.cs
 * Purpose: Definition of the Interface Repository.HospitalRepository.IStationaryTreatmentRepository
 ***********************************************************************/

using Model;
using Model.ManagerModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using System;
using System.Collections.Generic;

namespace Repository.HospitalRepository
{
   public interface IHospitalStayRepository : IRepository<HospitalStay, long>
   {
        HospitalStay GetByPatient(Patient patient);
        List<HospitalStay> GetByBed(Bed bed);
   }
}