using Model;
using Model.DoctorModel;
using Repository.HospitalRepository;
using System.Collections.Generic;

namespace Repository.UserRepository
{
    public interface IUserRepository : IRepository<User, long>
    {
        List<Doctor> GetAllDoctors();
        List<Secretary> GetAllSecretaries();
        List<Patient> GetAllPatients();
        List<Specialist> GetAllSpecialists();
        List<Manager> GetAllManagers();
    }
}