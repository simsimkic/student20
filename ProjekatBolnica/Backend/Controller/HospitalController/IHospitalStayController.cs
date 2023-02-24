/***********************************************************************
 * Module:  IStationaryTreatmentController.cs
 * Purpose: Definition of the Interface Controller.HospitalController.IStationaryTreatmentController
 ***********************************************************************/

using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using System;
using System.Collections.Generic;

namespace Controller.HospitalController
{
   public interface IHospitalStayController : IController<HospitalStay,long>
   {
        bool relocatePatient(Patient patient, Bed bed);
        TimePeriod DurationOfLayingInBed(Bed bed);
        bool isHospitalized(Patient patient);
        HospitalStay GetByPatient(Patient patient);
        List<HospitalStay> GetByBed(Bed bed);
    }
}