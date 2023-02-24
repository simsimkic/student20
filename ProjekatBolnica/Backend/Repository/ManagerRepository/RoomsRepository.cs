/***********************************************************************
 * Module:  RoomsRepository.cs
 * Purpose: Definition of the Class Repository.ManagerRepository.RoomsRepository
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
    public class RoomsRepository : CSVRepository<Room, long>,
        IRoomsRepository,
        IEagerCSVRepository<Room, long>
    {
        private const String ENTITY_NAME = "Room";
        private const String NOT_UNIQUE_ERROR = "Room name {0} is not unique!";

        public RoomsRepository(ICSVStream<Room> stream, ISequencer<long> sequencer)
            : base(ENTITY_NAME, stream, sequencer)
        {
        }

        public new IEnumerable<Room> Find(Func<Room, bool> predicate)
           => GetAllEager().Where(predicate);

        public new void New(Room room)
        {
            if (IsNameUnique(room.Name))
                base.New(room);
            else
                throw new NotUniqueException(String.Format(NOT_UNIQUE_ERROR, room.Name));
        }

        public IEnumerable<Room> GetAllEager() => GetAll();

        public Room GetEager(long id) => Get(id);

        public List<Room> GetAllControlRooms()
        {
            List<Room> controlRooms = new List<Room>();
            foreach (Room room in GetAllEager())
            {
                if (room.Function == FunctionOfRoom.ControlRoom) { controlRooms.Add(room); }
            }
            return controlRooms;
        }

        public List<Room> GetAllOperationRooms()
        {
            List<Room> operationRooms = new List<Room>();
            foreach (Room room in GetAllEager())
            {
                if (room.Function == FunctionOfRoom.OperationRoom) { operationRooms.Add(room); }
            }
            return operationRooms;
        }

        public List<Room> GetAllRoomsForLaying()
        {
            List<Room> roomsForLaying = new List<Room>();
            foreach (Room room in GetAllEager())
            {
                if (room.Function == FunctionOfRoom.RoomForLaying) { roomsForLaying.Add(room); }
            }
            return roomsForLaying;
        }

        private bool IsNameUnique(String name)
           => GetRoomByName(name) == null;

        private Room GetRoomByName(String name)
           => _stream.ReadAll().SingleOrDefault(room => room.Name.Equals(name));
    }
}