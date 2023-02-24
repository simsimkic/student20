/***********************************************************************
 * Module:  IRoomService.cs
 * Purpose: Definition of the Interface Service.ManagerService.IRoomService
 ***********************************************************************/

using Model.ManagerModel;
using Model.UtilityModel;
using Service.HospitalServices;
using System.Collections.Generic;

namespace Service.ManagerService
{
    public interface IRoomService : IService<Room, long>
    {
        void AnnounceAction(Notification n);
        List<Room> GetAllControlRooms();
        List<Room> GetAllOperationRooms();
        List<Room> GetAllRoomsForLaying();
    }
}