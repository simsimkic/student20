using Model;
using Model.DoctorModel;
using Model.UserModel;
using ProjekatBolnica.Backend.Model.UserModel;
using Service.HospitalServices;
using System;
using System.Collections.Generic;

namespace Service.UserService
{
   public interface IUserService : IService<User, long>
    {
        List<Patient> GetAllPatients();
        List<Doctor> GetAllDoctors();
        List<Secretary> GetAllSecretaries();
        List<Specialist> GetAllSpecialists();
        List<Manager> GetAllManagers();
        void AddFeedback(Feedback message);
        Language ChangeLanguage(Language language);
        User Login(String username, String password, Boolean stayLoggedIn);
        Boolean IsUsernameValid(String username);
        Boolean IsPasswordValid(String password);
   }
}