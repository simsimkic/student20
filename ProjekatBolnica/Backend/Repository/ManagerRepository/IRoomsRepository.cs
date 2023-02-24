/***********************************************************************
 * Module:  IRoomsRepository.cs
 * Purpose: Definition of the Interface Repository.ManagerRepository.IRoomsRepository
 ***********************************************************************/

using System.Collections.Generic;
using Model.ManagerModel;
using Repository.HospitalRepository;
using Syncfusion.XPS;

namespace Repository.ManagerRepository
{
    public interface IRoomsRepository : IRepository<Room, long>
    {
        List<Room> GetAllControlRooms();
        List<Room> GetAllOperationRooms();
        List<Room> GetAllRoomsForLaying();
    }
}