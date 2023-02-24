using Controller.HospitalController;
using Model;
using Model.DoctorModel;
using Model.UserModel;
using ProjekatBolnica.Backend.Model.UserModel;
using System;
using System.Collections.Generic;

namespace Controller.UserController
{
    public interface IUserController : IController<User, long>
    {
        void AddFeedback(Feedback message);
        Language ChangeLanguage(Language language);
        User Login(String username, String password, Boolean stayLoggedIn);

        List<Patient> GetAllPatients();
        List<Doctor> GetAllDoctors();
        List<Secretary> GetAllSecretaries();
        List<Specialist> GetAllSpecialists();
        List<Manager> GetAllManagers();
    }
}