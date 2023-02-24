/***********************************************************************
 * Module:  IMedicineRepository.cs
 * Purpose: Definition of the Interface Repository.ManagerRepository.IMedicineRepository
 ***********************************************************************/

using Model.ManagerModel;
using Repository.HospitalRepository;
using System;

namespace Repository.ManagerRepository
{
    public interface IMedicineRepository : IRepository<Medicine, long>
    {
        Medicine GetMedicineByName(String name);
    }
}