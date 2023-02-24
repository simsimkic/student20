/***********************************************************************
 * Module:  OperationService.cs
 * Purpose: Definition of the Class Service.HospitalServices.OperationService
 ***********************************************************************/

using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.UtilityModel;
using Repository.HospitalRepository;
using Repository.ManagerRepository;
using Service.ManagerService;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Service.HospitalServices
{
   public class OperationService : IOperationService
   {
        private readonly IOperationRepository _repository;
        private readonly IWorkHoursForDoctorService _workHoursForDoctorService;
        private readonly TimeSpan duration = new TimeSpan(0, 60, 0);
        public OperationService(IOperationRepository repository, IWorkHoursForDoctorService workHoursForDoctorService)
        {
            _repository = repository;
            _workHoursForDoctorService = workHoursForDoctorService;
        }
        public int CountOperationsInRoom(Room room, TimePeriod timePeriod)
        {
            int counter = 0;
            foreach(Operation o in _repository.GetActivityByRoom(room))
            {
                if(o.DateAndTime.Date >= timePeriod.StartTime && o.DateAndTime.Date <= timePeriod.EndTime) { counter++; }
            }
            return counter;
        }
        public bool IsAvailableDoctor(Doctor doctor, DateTime term)
        {
            List<Operation> operations = _repository.GetActivityByDoctor(doctor);

            if (IsAvailableTerm(term, operations)) { return true; }
            return false;
        }
        public bool IsAvailableTerm(DateTime term, List<Operation> operations)
        {
            foreach (Operation o in operations)
            {
                if (o.DateAndTime.Equals(term)) { return false; }
            }
            return true;
        }
        public bool IsAvailableRoom(Room room, DateTime term)
        {
            if (DateTime.Compare(term, room.Renovation.StartTime) > 0 && DateTime.Compare(room.Renovation.EndTime, term) > 0) { return false; }
            List<Operation> operations = _repository.GetActivityByRoom(room);
            if (IsAvailableTerm(term, operations)) { return true; }
            return false;
        }
        public bool IsValidAppointment(Operation operation)
        {
            if (IsAvailableDoctor(operation.Specialist, operation.DateAndTime))
            {
                if (IsAvailableRoom(operation.Room, operation.DateAndTime))
                { return true; }
            }
            return false;
        }
        public void Set(Operation operation) => _repository.Set(operation);
        public Operation Get(long id) => _repository.Get(id);
        IEnumerable<Operation> IService<Operation, long>.GetAll() => _repository.GetAllEager();
        public void New(Operation operation)
        {
            if (IsValidAppointment(operation))
            {
                operation.Duration = duration;
                _repository.New(operation);
            } else
            {
                MessageBox.Show("Invalid operation.");
            }
        }
        public void Delete(Operation operation) => _repository.Delete(operation);
        public List<Operation> GetActivityByDoctor(Doctor doctor) => _repository.GetActivityByDoctor(doctor);
        public List<Operation> GetActivityByDate(DateTime date) => _repository.GetActivityByDate(date);
        public List<Operation> GetActivityByPatient(Patient patient) => _repository.GetActivityByPatient(patient);
        public List<Operation> GetActivityByRoom(Room room) => _repository.GetActivityByRoom(room);

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
        private List<DateTime> GetAvailableTerms(Doctor doctor, DateTime term)
        {
            List<DateTime> free = new List<DateTime>();
            List<DateTime> taken = new List<DateTime>();

            TimePeriod shift = _workHoursForDoctorService.GetDoctorsShift(doctor, term);
            DateTime d = new DateTime(shift.StartTime.Year, shift.StartTime.Month, shift.StartTime.Day, shift.StartTime.Hour, shift.StartTime.Minute, shift.StartTime.Second);
            TimeSpan durationOfShift = new TimeSpan();
            durationOfShift = shift.EndTime - shift.StartTime;
            taken = GetTakenTerms(doctor, term);
            int numberOfOperations = (int)(durationOfShift.TotalMinutes / duration.TotalMinutes);
            for (int i = 0; i < numberOfOperations; i++)
            {
                free.Add(d);
                var temp = d.AddMinutes(duration.TotalMinutes);
                d = temp;
            }
            foreach (DateTime date in taken)
            {
                if (taken.Contains(date))
                    free.Remove(date);
            }
            return free;
        }
        public List<DateTime> GetTakenTerms(Doctor doctor, DateTime term)
        {
            List<DateTime> times = new List<DateTime>();
            List<Operation> doctorsTerms = _repository.GetActivityByDoctor(doctor);
            foreach (Operation operation in doctorsTerms)
                if (operation.DateAndTime.Date.Equals(term.Date))
                    times.Add(operation.DateAndTime);
            return times;
        }
    }
}