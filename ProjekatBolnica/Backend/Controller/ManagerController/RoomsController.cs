/***********************************************************************
 * Module:  RoomsController.cs
 * Purpose: Definition of the Class Controller.ManagerController.RoomsController
 ***********************************************************************/

using Model.ManagerModel;
using Model.UtilityModel;
using Service.ManagerService;
using System.Collections.Generic;

namespace Controller.ManagerController
{
    public class RoomsController : IRoomsController
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public void Delete(Room room) => _roomService.Delete(room);

        public Room Get(long id) => _roomService.Get(id);

        public IEnumerable<Room> GetAll() => _roomService.GetAll();

        public void New(Room room) => _roomService.New(room);

        public void Set(Room room) => _roomService.Set(room);

        public void AnnounceAction(Notification n) => _roomService.AnnounceAction(n);

        public List<Room> GetAllControlRooms() => _roomService.GetAllControlRooms();

        public List<Room> GetAllOperationRooms() => _roomService.GetAllOperationRooms();

        public List<Room> GetAllRoomsForLaying() => _roomService.GetAllRoomsForLaying();
    }
}