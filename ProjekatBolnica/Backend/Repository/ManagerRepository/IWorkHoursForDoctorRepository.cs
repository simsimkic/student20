/***********************************************************************
 * Module:  IWorkHoursForDoctorRepository.cs
 * Purpose: Definition of the Interface Repository.ManagerRepository.IWorkHoursForDoctorRepository
 ***********************************************************************/

using Model.ManagerModel;
using Repository.HospitalRepository;

namespace Repository.ManagerRepository
{
    public interface IWorkHoursForDoctorRepository : IRepository<WorkHoursForDoctor, long>
    {
    }
}