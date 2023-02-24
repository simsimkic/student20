
using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.PatientModel;
using System;
using System.Collections.Generic;

namespace Controller.HospitalController
{
    public interface IAppointmentController : IController<Appointment, long>
    {
        List<DateTime> GetAvailableTerms(Doctor doctor, DateTime term);
        int CountAppointmentsInRoom(Room room, Model.UtilityModel.TimePeriod timePeriod);
        Appointment RecommendAppointment(Doctor doctor, TimePeriod timePeriod, Priority priority);
        List<Appointment> GetActivityByDoctor(Doctor doctor);
        List<Appointment> GetActivityByDate(DateTime date);
        List<Appointment> GetActivityByPatient(Patient patient);
        List<Appointment> GetActivityByRoom(Room room);
        List<DateTime> GetAvailableTermsForRoomAndDoctorAndDate(Doctor doctor, DateTime term, Room room);
    }
}