/***********************************************************************
 * Module:  RoomService.cs
 * Purpose: Definition of the Class Service.ManagerService.RoomService
 ***********************************************************************/

using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Service.UserService;
using Repository.ManagerRepository;
using System.Collections.Generic;

namespace Service.ManagerService
{
    public class RoomService : IRoomService
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly INotificationService _notificationService;

        public RoomService(IRoomsRepository roomsRepository, INotificationService notificationService)
        {
            _roomsRepository = roomsRepository;
            _notificationService = notificationService;
        }

        public void Delete(Room room) => _roomsRepository.Delete(room);

        public Room Get(long id) => _roomsRepository.Get(id);

        public IEnumerable<Room> GetAll() => _roomsRepository.GetAll();

        public List<Room> GetAllControlRooms() => _roomsRepository.GetAllControlRooms();

        public List<Room> GetAllOperationRooms() => _roomsRepository.GetAllOperationRooms();

        public List<Room> GetAllRoomsForLaying() => _roomsRepository.GetAllRoomsForLaying();

        public void AnnounceAction(Notification n) => _notificationService.New(n);

        public void New(Room room) => _roomsRepository.New(room);

        public void Set(Room room) => _roomsRepository.Set(room);
    }
}