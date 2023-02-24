using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.PatientModel;
using Repository.HospitalRepository;
using Repository.ManagerRepository;
using Repository.UserRepository;
using Service.ManagerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Service.HospitalServices
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IRoomsRepository _roomsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWorkHoursForDoctorService _workHoursForDoctorService;
        TimeSpan duration = new TimeSpan(0, 20, 0);

        public AppointmentService(IAppointmentRepository repository, IRoomsRepository roomsRepository, IUserRepository userRepository, IWorkHoursForDoctorService workHoursForDoctorService)
        {
            _repository = repository;
            _roomsRepository = roomsRepository;
            _userRepository = userRepository;
            _workHoursForDoctorService = workHoursForDoctorService;
        }

        public void Delete(Appointment appointment) => _repository.Delete(appointment);

        public Appointment Get(long id) => _repository.Get(id);

        public IEnumerable<Appointment> GetAll() => _repository.GetAllEager();

        public void New(Appointment appointment) //=> _repository.New(appointment);
        {
            if (appointment.Room == null)
                SetMissingValues(appointment);

            if (IsValidAppointment(appointment)) 
            {
                appointment.Duration = duration;
                _repository.New(appointment);
                MessageBox.Show("Appointment saved");
            }
            else
            {
                string message = "Invalid appointment.";
                MessageBox.Show(message);
            }
        }

        public Appointment RecommendAppointment(Doctor doctor, TimePeriod timePeriod, Priority priority) 
        {
            DateTime date = timePeriod.StartTime.Date;

            DateTime recommendTerm = new DateTime();

            List<DateTime> availableTerms = new List<DateTime>(); 
            do {
                availableTerms=GetAvailableTerms(doctor, date);

                if (IsAvailableTermsEmpty(availableTerms))
                {
                    recommendTerm = availableTerms.First();
                    return new Appointment()
                    {
                        Doctor = doctor,
                        DateAndTime = recommendTerm
                    };
                }
                else
                    date = date.AddDays(1);


            }while (date < timePeriod.EndTime);

            return CallPriorityRecommendation(doctor, timePeriod, priority);
        }

        

        private Appointment CallPriorityRecommendation(Doctor doctor, TimePeriod timePeriod, Priority priority)
        {
            if (IsPriorityDoctor(priority))
                return RecommendAppointmentByDoctor(doctor, timePeriod.EndTime);
            else
                return RecommendAppointmentByDate(timePeriod);
        }

        public Appointment RecommendAppointmentByDoctor(Doctor doctor, DateTime date)
        {
            List<DateTime> availableTerms = new List<DateTime>();
            for (int i = 0; i < 5; i++)
            {
                date = date.AddDays(1);
                availableTerms =GetAvailableTerms(doctor, date);
                if (IsAvailableTermsEmpty(availableTerms))
                {
                    return new Appointment()
                    {
                        Doctor = doctor,
                        DateAndTime = availableTerms.First()
                    };
                }

            }

            return null;
        }



        private Appointment RecommendAppointmentByDate(TimePeriod timePeriod)
        {
            List<DateTime> availableTerms = new List<DateTime>();
            DateTime date = timePeriod.StartTime.Date;
            do {
            foreach (Doctor doctor in _userRepository.GetAllDoctors())
            {
                availableTerms=GetAvailableTerms(doctor, date);
                if (IsAvailableTermsEmpty(availableTerms))
                {

                    return new Appointment()
                        {
                            Doctor = doctor,
                            DateAndTime = availableTerms.First()
                    };
                }
                }
                date = date.AddDays(1);
            } while (date < timePeriod.EndTime);



            return null;
        }

        private bool IsPriorityDoctor(Priority priority) => priority == Priority.Doctor;
        private bool IsAvailableTermsEmpty(List<DateTime> availableTerms) => availableTerms.Any();

        private void SetMissingValues(Appointment appointment)
        {
            List<Room> rooms = new List<Room>();
            rooms = _roomsRepository.GetAllControlRooms().ToList();
            foreach (Room r in rooms)
            {

                appointment.Room = r;
                break;
            }
            appointment.Duration = duration;
        }

        public void Set(Appointment appointment) => _repository.Set(appointment);

        public List<DateTime> GetAvailableTermsForRoomAndDoctorAndDate(Doctor doctor, DateTime term, Room room)
        {
            List<DateTime> availableTerms = new List<DateTime>();
            List<DateTime> availableTermsForDoctor = GetAvailableTerms(doctor, term);
            foreach (DateTime dateTime in availableTermsForDoctor)
            {
                if (IsAvailableRoom(room, dateTime))
                {
                    availableTerms.Add(dateTime);
                }
            }
            return availableTerms;
        }


        public List<DateTime> GetAvailableTerms(Doctor doctor, DateTime term)
        {
            List<DateTime> free = new List<DateTime>();
            List<DateTime> taken = new List<DateTime>();

            TimePeriod shift = _workHoursForDoctorService.GetDoctorsShift(doctor, term);
            DateTime d = new DateTime(shift.StartTime.Year, shift.StartTime.Month, shift.StartTime.Day, shift.StartTime.Hour, shift.StartTime.Minute, shift.StartTime.Second);
            TimeSpan durationOfShift = new TimeSpan();
            durationOfShift = shift.EndTime - shift.StartTime;
            taken = GetTakenTerms(doctor, term);
            int numberOfApp = (int)(durationOfShift.TotalMinutes / duration.TotalMinutes);
            for (int i = 0; i < numberOfApp; i++)
            {
                free.Add(d);
                d = d.AddMinutes(duration.Minutes);
            }

            foreach (DateTime date in taken)
            {
                if (taken.Contains(date))
                    free.Remove(date);
            }

            return free;
        }

        private List<DateTime> GetTakenTerms(Doctor doctor, DateTime term)
        {
            List<DateTime> times = new List<DateTime>();
            List<Appointment> doctorsTerms = _repository.GetActivityByDoctor(doctor);
            foreach (Appointment appointment in doctorsTerms)
                if (appointment.DateAndTime.Date.Equals(term.Date))
                    times.Add(appointment.DateAndTime);
            return times;
        }

        public int CountAppointmentsInRoom(Room room, TimePeriod timePeriod)
        {
            int counter = 0;
            foreach(Appointment a in _repository.GetActivityByRoom(room))
            {
                if(a.DateAndTime.Date >= timePeriod.StartTime && a.DateAndTime.Date <= timePeriod.EndTime) { counter++; }
            }
            return counter;
        }

        public bool IsAvailableDoctor(Doctor doctor, DateTime term)
        {
            
            //ovde treba provera da li doktor radi u prosledjenom terminu...
            List<Appointment> appointments = _repository.GetActivityByDoctor(doctor);      
            
            if (IsAvailableTerm(term, appointments)) { return true; }
            return false;
        }

        public bool IsAvailableRoom(Room room, DateTime term)
        {
            if (DateTime.Compare(term, room.Renovation.StartTime) > 0 && DateTime.Compare(room.Renovation.EndTime, term) > 0) { return false; }
                List<Appointment> appointments = _repository.GetActivityByRoom(room);
            if (IsAvailableTerm(term, appointments)) { return true; }
            return false;
        }

        public bool IsAvailableTerm(DateTime term, List<Appointment> appointments)
        {
            foreach (Appointment a in appointments)
            {
                if (a.DateAndTime.Equals(term)) { return false; }
            }
            return true;
        }

        public bool IsValidAppointment(Appointment appointment)
        { 
            if (IsAvailableDoctor(appointment.Doctor, appointment.DateAndTime))
            {
                if (IsAvailableRoom(appointment.Room, appointment.DateAndTime)) 
                { return true; }
            }
            return false;
        }



        public List<Appointment> GetActivityByDoctor(Doctor doctor) => _repository.GetActivityByDoctor(doctor);
        public List<Appointment> GetActivityByDate(DateTime date) => _repository.GetActivityByDate(date);
        public List<Appointment> GetActivityByPatient(Patient patient) => _repository.GetActivityByPatient(patient);
        public List<Appointment> GetActivityByRoom(Room room) => _repository.GetActivityByRoom(room);


    }
}