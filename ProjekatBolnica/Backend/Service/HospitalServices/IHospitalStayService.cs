/***********************************************************************
 * Module:  IStationaryTreatmentService.cs
 * Purpose: Definition of the Interface Service.HospitalServices.IStationaryTreatmentService
 ***********************************************************************/

using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using System;
using System.Collections.Generic;

namespace Service.HospitalServices
{
   public interface IHospitalStayService : IService<HospitalStay, long>
    {
        bool relocatePatient(Patient patient, Bed bed);
        TimePeriod DurationOfLayingInBed(Bed bed);
        bool isHospitalized(Patient patient);
        bool isBedOccupied(Bed bed, TimePeriod period);
        bool IsRelocationAllowed(Patient patient, Bed bed);
        HospitalStay changeBed(HospitalStay hospitalStay, Bed bed);
        HospitalStay GetByPatient(Patient patient);
        List<HospitalStay> GetByBed(Bed bed);
    }
}