
using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.PatientModel;
using Service.HospitalServices;
using System;
using System.Collections.Generic;

namespace Controller.HospitalController
{
    public class AppointmentController : IAppointmentController
    {

        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        public void Delete(Appointment appointment) => _service.Delete(appointment);

        public Appointment Get(long id) => _service.Get(id);

        public IEnumerable<Appointment> GetAll() => _service.GetAll();

        public void New(Appointment appointment) => _service.New(appointment);
        public void Set(Appointment appointment) => _service.Set(appointment);

        public List<DateTime> GetAvailableTerms(Doctor doctor, DateTime term) => _service.GetAvailableTerms(doctor, term);

        public List<DateTime> GetAvailableTermsForRoomAndDoctorAndDate(Doctor doctor, DateTime term, Room room) => _service.GetAvailableTermsForRoomAndDoctorAndDate(doctor, term, room);


        public Appointment RecommendAppointment(Doctor doctor, TimePeriod timePeriod, Priority priority) => _service.RecommendAppointment(doctor, timePeriod, priority);

        public int CountAppointmentsInRoom(Room room, TimePeriod timePeriod) => _service.CountAppointmentsInRoom(room, timePeriod);

        public List<Appointment> GetActivityByDoctor(Doctor doctor) => _service.GetActivityByDoctor(doctor);
        public List<Appointment> GetActivityByDate(DateTime date) => _service.GetActivityByDate(date);
        public List<Appointment> GetActivityByPatient(Patient patient) => _service.GetActivityByPatient(patient);
        public List<Appointment> GetActivityByRoom(Room room) => _service.GetActivityByRoom(room);

    }
}