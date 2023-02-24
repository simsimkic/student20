using Model;
using Model.DoctorModel;
using Model.UserModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using System.Collections.Generic;

namespace Repository.UserRepository
{
    public class UserRepository : CSVRepository<User, long>,
          IUserRepository,
          IEagerCSVRepository<User, long>
    {

        private const string ENTITY_NAME = "User";
        private readonly IEagerCSVRepository<User, long> _repository;
        private readonly CSVRepository<Specialty, long> _specialtyRepository;

        public UserRepository(ICSVStream<User> stream, ISequencer<long> sequencer,
           CSVRepository<Specialty, long> specialtyRepository) : base(ENTITY_NAME, stream, sequencer)
        {
            _specialtyRepository = specialtyRepository;
        }



        public IEnumerable<User> GetAllEager()
        {
            List<User> users = new List<User>();
            foreach (User user in GetAll())
            {
                users.Add(GetEager(user.Id));
            }
            return users;
        }

        public User GetEager(long id)
        {
            User user = Get(id);
            if (user.Role == TypeOfUser.Specialist)
            {
                return GetSpecialistEager(user);
            }
            return user;
        }

        public Specialist GetSpecialistEager(User user)
        {
            Specialist specialist = (Specialist)user;
            specialist.Specialty = _specialtyRepository.Get(specialist.Specialty.Id);
            return specialist;
        }

        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            foreach (User user in GetAllEager())
            {
                if (user.Role == TypeOfUser.Doctor) { doctors.Add((Doctor)user); }
            }
            return doctors;
        }

        public List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();
            foreach (User user in GetAllEager())
            {
                if (user.Role == TypeOfUser.Patient) { patients.Add((Patient)user); }
            }
            return patients;
        }

        public List<Secretary> GetAllSecretaries()
        {
            List<Secretary> secretaries = new List<Secretary>();
            foreach (User user in GetAllEager())
            {
                if (user.Role == TypeOfUser.Secretary) { secretaries.Add((Secretary)user); }
            }
            return secretaries;
        }

        public List<Specialist> GetAllSpecialists()
        {
            List<Specialist> specialists = new List<Specialist>();
            foreach (User user in GetAllEager())
            {
                if (user.Role == TypeOfUser.Specialist) { specialists.Add((Specialist)user); }
            }
            return specialists;
        }

        public List<Manager> GetAllManagers()
        {
            List<Manager> managers = new List<Manager>();
            foreach (User user in GetAllEager())
            {
                if (user.Role == TypeOfUser.Manager) { managers.Add((Manager)user); }
            }
            return managers;
        }
    }
}