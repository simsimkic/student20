/***********************************************************************
 * Module:  AppointmentRepository.cs
 * Purpose: Definition of the Class Repository.HospitalRepository.AppointmentRepository
 ***********************************************************************/

using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using ProjekatBolnica;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.HospitalRepository
{
   public class AppointmentRepository : 
        CSVRepository<Appointment, long>,
        IAppointmentRepository
   {
        private const String ENTITY_NAME = "Appointment";
        private readonly IEagerCSVRepository<Room, long> _roomRepository;
        private readonly IEagerCSVRepository<User, long> _userRepository;
        //treba dodati i za pacijenta i za doktora IEagerCSVRepository...

        public AppointmentRepository(ICSVStream<Appointment> stream,
            ISequencer<long> sequencer,
            IEagerCSVRepository<Room, long> roomRepository,
            IEagerCSVRepository<User, long> userRepository) : base(ENTITY_NAME, stream, sequencer)
        {
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }

        public List<Appointment> GetActivityByDate(DateTime date)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment a in GetAllEager()) 
            {
                if (a.DateAndTime.Year == date.Year && a.DateAndTime.Month == date.Month && a.DateAndTime.Day == date.Day) { appointments.Add(a); }
            }
            return appointments;
        }

        public List<Appointment> GetActivityByDoctor(Doctor doctor)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment a in GetAllEager())
            {
                if (a.Doctor.Id == doctor.Id) { appointments.Add(a); }
            }
            return appointments;
        }

        public List<Appointment> GetActivityByPatient(Patient patient)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment a in GetAllEager())
            {
                if (a.Patient.Id == patient.Id) { appointments.Add(a); }
            }
            return appointments;
        }

        public List<Appointment> GetActivityByRoom(Room room)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment a in GetAllEager())
            {
                if (a.Room != null)
                { if (a.Room.Id == room.Id) { appointments.Add(a); } }
            }
            return appointments;
        }

        public IEnumerable<Appointment> GetAllEager()
        {
            //TO DO, treba dodati za pacijenta i doktora kao sto je za sobu...
            List<Appointment> appointments = new List<Appointment>();
            foreach(Appointment a in GetAll())
            {
                appointments.Add(GetEager(a.Id));
            }
            return appointments;
            /*var rooms = _roomRepository.GetAllEager();
            var appointments = GetAll();
            BindRoomsWithAppointments(rooms, appointments);
            return appointments;*/
        }

        public Appointment GetEager(long id)
        {
            //TO DO, treba dodati za pacijenta i doktora kao sto je za sobu...
            var appointment = Get(id);
            appointment.Room = _roomRepository.GetEager(appointment.Room.Id);
            appointment.Patient = (Patient)_userRepository.GetEager(appointment.Patient.Id);
            appointment.Doctor = (Doctor)_userRepository.GetEager(appointment.Doctor.Id);
            return appointment;
        }


        private void BindRoomsWithAppointments(IEnumerable<Room> rooms, IEnumerable<Appointment> appointments)
          => appointments.ToList().ForEach(appointment =>
          {
              appointment.Room = FindRoomById(rooms, appointment.Room.Id);
          });

        private Room FindRoomById(IEnumerable<Room> rooms, long id)
           => rooms.SingleOrDefault(room => room.Id == id);


    }
}