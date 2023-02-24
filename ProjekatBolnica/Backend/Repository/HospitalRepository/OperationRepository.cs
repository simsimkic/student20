/***********************************************************************
 * Module:  OperationRepository.cs
 * Purpose: Definition of the Class Repository.HospitalRepository.OperationRepository
 ***********************************************************************/

using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using ProjekatBolnica.SecretaryView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace Repository.HospitalRepository
{
    public class OperationRepository :
        CSVRepository<Operation, long>,
        IOperationRepository

    {

        private const String ENTITY_NAME = "Operation";
        private readonly IEagerCSVRepository<Room, long> _roomRepository;
        private readonly IEagerCSVRepository<User, long> _userRepository;

        public OperationRepository(ICSVStream<Operation> stream,
            ISequencer<long> sequencer,
            IEagerCSVRepository<Room, long> roomRepository,
            IEagerCSVRepository<User, long> userRepository) : base(ENTITY_NAME, stream, sequencer)
        {
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }


        public new IEnumerable<Operation> Find(Func<Operation, bool> predicate)
            => GetAllEager().Where(predicate);



        public IEnumerable<Operation> GetAllEager()
        {
            List<Operation> operations = new List<Operation>();
            foreach(Operation o in GetAll())
            {
                operations.Add(GetEager(o.Id));
            }
            return operations;
        }


        public Operation GetEager(long id)
        {
            var operation = Get(id);
            operation.Room = _roomRepository.GetEager(operation.Room.Id);
            operation.Patient = (Patient)_userRepository.GetEager(operation.Patient.Id);
            operation.Specialist = (Specialist)_userRepository.GetEager(operation.Specialist.Id);
            return operation;
        }

        public List<Operation> GetActivityByDate(DateTime date)
        {
            List<Operation> operations = new List<Operation>();
            foreach (Operation o in GetAllEager())
            {
                if (o.DateAndTime.Year == date.Year && o.DateAndTime.Month == date.Month && o.DateAndTime.Day == date.Day) { operations.Add(o); }
            }
            return operations;
        }
      
        public List<Operation> GetActivityByPatient(Patient patient)
        {
            List<Operation> operations = new List<Operation>();
            foreach (Operation o in GetAllEager())
            {
                if (o.Patient.Id == patient.Id) { operations.Add(o); }
            }
            return operations;
        }


        public List<Operation> GetActivityByRoom(Room room)
        {
            List<Operation> operations = new List<Operation>();
            foreach (Operation o in GetAllEager())
            {
                if (o.Room.Id == room.Id) { operations.Add(o); }
            }
            return operations;
        }
        public List<Operation> GetActivityByDoctor(Doctor doctor)
        {
            List<Operation> operations = new List<Operation>();
            foreach(Operation o in GetAllEager())
            {
                if(o.Specialist.Id == doctor.Id) { operations.Add(o); }
            }
            return operations;
        }
    }
}