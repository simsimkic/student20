/***********************************************************************
 * Module:  IOperationRepository.cs
 * Purpose: Definition of the Interface Repository.HospitalRepository.IOperationRepository
 ***********************************************************************/

using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using ProjekatBolnica.Backend.Repository.CSV;
using System;
using System.Collections.Generic;

namespace Repository.HospitalRepository
{
    public interface IOperationRepository : IActivity<Operation, long>, IRepository<Operation, long>, IEagerCSVRepository<Operation, long>
    {
    }
}