using Model;
using Model.DoctorModel;
using Model.UserModel;
using ProjekatBolnica.Backend.Model.UserModel;
using Service.HospitalServices;
using Service.UserService;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace Controller.UserController
{
    public class UserController : IUserController
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        public void Delete(User user) => _service.Delete(user);
        public User Get(long id) => _service.Get(id);
        public IEnumerable<User> GetAll() => _service.GetAll();
        public void New(User user) => _service.New(user);
        public void Set(User user) => _service.Set(user);

        public User Login(string username, string password, bool stayLoggedIn)=> _service.Login(username, password, stayLoggedIn);



        public void AddFeedback(Feedback message) => _service.AddFeedback(message);
        

        public Language ChangeLanguage(Language language)
        {
            throw new System.NotImplementedException();
        }

        public List<Patient> GetAllPatients() => _service.GetAllPatients();

        public List<Doctor> GetAllDoctors() => _service.GetAllDoctors();
        public List<Secretary> GetAllSecretaries() => _service.GetAllSecretaries();
        public List<Specialist> GetAllSpecialists() => _service.GetAllSpecialists();
        public List<Manager> GetAllManagers() => _service.GetAllManagers();
    }
}