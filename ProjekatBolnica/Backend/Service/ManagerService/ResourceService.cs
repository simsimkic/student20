/***********************************************************************
 * Module:  ResourceService.cs
 * Purpose: Definition of the Class Service.ManagerService.ResourceService
 ***********************************************************************/

using Model.ManagerModel;
using Repository.ManagerRepository;
using System.Collections.Generic;
using System.Linq;

namespace Service.ManagerService
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IRoomService _roomService;

        public ResourceService(IResourceRepository resourceRepository, IRoomService roomService)
        {
            _resourceRepository = resourceRepository;
            _roomService = roomService;
        }

        public void Delete(Resource resource) => _resourceRepository.Delete(resource);

        public Resource Get(long id)
        {
            var resource = _resourceRepository.Get(id);
            resource.Room = _roomService.Get(resource.Room.Id);
            return resource;
        }

        public IEnumerable<Resource> GetAll()
        {
            var rooms = _roomService.GetAll();
            var resources = _resourceRepository.GetAll();
            BindRoomsWithResources(rooms, resources);
            return resources;
        }

        public void New(Resource resource)
        {
            Room room = resource.Room;
            SetMissingValues(resource);

            _roomService.Set(room);
            _resourceRepository.New(resource);
            resource.Room = room;
        }

        public void Set(Resource resource)
        {
            _roomService.Set(resource.Room);
            _resourceRepository.Set(resource);
        }

        public void IncreaseQuantity(long id, int quantity)
        {
            var resource = _resourceRepository.Get(id);
            resource.Amount += quantity;
        }

        public void DecreaseQuantity(long id, int quantity)
        {
            var resource = _resourceRepository.Get(id);
            var amount = resource.Amount;
            if (amount + quantity >= quantity)
                resource.Amount -= quantity;
        }

        public int GetAmountOfResource(long id)
        {
            var resource = _resourceRepository.Get(id);
            return resource.Amount;
        }

        public int CountNumberOfBedsInRoom(Room room)
        {
            List<Bed> beds = GetBedsInRoom(room);
            return beds.Count;
        }

        public List<Bed> GetBedsInRoom(Room room) => _resourceRepository.GetBedsInRoom(room);

        private void BindRoomsWithResources(IEnumerable<Room> rooms, IEnumerable<Resource> resources)
           => resources.ToList().ForEach(resource => resource.Room = FindRoomById(rooms, resource.Room.Id));

        private Room FindRoomById(IEnumerable<Room> rooms, long id)
            => rooms.SingleOrDefault(room => room.Id == id);

        private void SetMissingValues(Resource resource) => resource.Amount = 0;
    }
}
