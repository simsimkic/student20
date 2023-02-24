/***********************************************************************
 * Module:  IResourceRepository.cs
 * Purpose: Definition of the Interface Repository.ManagerRepository.IResourceRepository
 ***********************************************************************/

using System.Collections.Generic;
using Model.ManagerModel;
using Repository.HospitalRepository;
using Syncfusion.XPS;

namespace Repository.ManagerRepository
{
    public interface IResourceRepository : IRepository<Resource, long>
    {
        List<Bed> GetBedsInRoom(Room room);
    }
}