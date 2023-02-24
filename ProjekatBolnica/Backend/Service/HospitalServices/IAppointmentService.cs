/***********************************************************************
 * Module:  IAppointmentService.cs
 * Purpose: Definition of the Interface Service.HospitalServices.IAppointmentService
 ***********************************************************************/

using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.PatientModel;
using System;
using System.Collections.Generic;

namespace Service.HospitalServices
{
   public interface IAppointmentService : IActivitySevice<Appointment, long>, IService<Appointment, long>
    {
        List<DateTime> GetAvailableTerms(Doctor doctor, DateTime term);
        int CountAppointmentsInRoom(Room room, TimePeriod timePeriod);
        Appointment RecommendAppointment(Doctor doctor, TimePeriod timePeriod, Priority priority);
        List<DateTime> GetAvailableTermsForRoomAndDoctorAndDate(Doctor doctor, DateTime term, Room room);
    }
}