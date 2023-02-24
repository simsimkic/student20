/***********************************************************************
 * Module:  ResourceRepository.cs
 * Purpose: Definition of the Class Repository.ManagerRepository.ResourceRepository
 ***********************************************************************/

using Model.ManagerModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.ManagerRepository
{
    public class ResourceRepository : CSVRepository<Resource, long>,
        IResourceRepository,
        IEagerCSVRepository<Resource, long>
    {
        private const String ENTITY_NAME = "Resource";
        private readonly IEagerCSVRepository<Room, long> _roomRepository;

        public ResourceRepository(ICSVStream<Resource> stream, ISequencer<long> sequencer,
            IEagerCSVRepository<Room, long> roomRepository)
            : base(ENTITY_NAME, stream, sequencer)
        {
            _roomRepository = roomRepository;
        }

        public new IEnumerable<Resource> Find(Func<Resource, bool> predicate)
             => GetAllEager().Where(predicate);

        public new void New(Resource resource) => base.New(resource);

        public IEnumerable<Resource> GetAllEager()
        {
            var rooms = _roomRepository.GetAllEager();
            var resources = GetAll();
            BindRoomsWithResources(rooms, resources);
            return resources;
        }

        public Resource GetEager(long id)
        {
            var resource = Get(id);
            resource.Room = _roomRepository.GetEager(resource.Room.Id);
            return resource;
        }

        public List<Bed> GetBedsInRoom(Room room)
        {
            List<Bed> beds = new List<Bed>();
            foreach (Resource resource in GetAllEager())
            {
                if (resource is Bed && resource.Room.Id == room.Id) { beds.Add((Bed)resource); }
            }
            return beds;
        }

        private void BindRoomsWithResources(IEnumerable<Room> rooms, IEnumerable<Resource> resources)
            => resources.ToList().ForEach(resource => resource.Room = FindRoomById(rooms, resource.Room.Id));

        private Room FindRoomById(IEnumerable<Room> rooms, long id)
            => rooms.SingleOrDefault(room => room.Id == id);

    }
}