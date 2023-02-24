using Model;
using Model.DoctorModel;
using Model.UserModel;
using ProjekatBolnica.Backend.Model.UserModel;
using Repository.UserRepository;
using Service.HospitalServices;
using System.Collections.Generic;

namespace Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IService<User, long> _service;

        private readonly INotificationRepository _iNotificationRepository;
        private readonly IFeedbackRepository _iFeedbackRepository;

        private int MinPasswordLenght;

        public UserService(IUserRepository repository /*, IService<User, long> service, 
            INotificationRepository iNotificationRepository*/, IFeedbackRepository iFeedbackRepository)
        {
            _iFeedbackRepository = iFeedbackRepository;
            //_iNotificationRepository = iNotificationRepository;
            _repository = repository;
            //_service = service;
        }

        public void Delete(User user) => _repository.Delete(user);
        public User Get(long id) => _repository.Get(id);

        public IEnumerable<User> GetAll() => _repository.GetAll();

        public void New(User user) => _repository.New(user);

        public void Set(User user) => _repository.Set(user);

        public void AddFeedback(Feedback message) => _iFeedbackRepository.New(message);

        public Language ChangeLanguage(Language language)
        {
            throw new System.NotImplementedException();
        }

        public User Login(string username, string password, bool stayLoggedIn)
        {
            foreach (User user in _repository.GetAll())
            {
                if (user.Username.Equals(username) && user.Password.Equals(password))
                {
                    user.Active = stayLoggedIn;
                    return user;
                }
            }
            return null;
        }

        public bool IsUsernameValid(string username)
        {
            throw new System.NotImplementedException();
        }

        public bool IsPasswordValid(string password)
        {
            throw new System.NotImplementedException();
        }

        public List<Patient> GetAllPatients() => _repository.GetAllPatients();

        public List<Doctor> GetAllDoctors() => _repository.GetAllDoctors();
        public List<Secretary> GetAllSecretaries() => _repository.GetAllSecretaries();
        public List<Specialist> GetAllSpecialists() => _repository.GetAllSpecialists();
        public List<Manager> GetAllManagers() => _repository.GetAllManagers();
    }
}