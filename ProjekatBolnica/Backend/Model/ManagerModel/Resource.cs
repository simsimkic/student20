/***********************************************************************
 * Module:  Resource.cs
 * Purpose: Definition of the Class Model.ManagerModel.Resource
 ***********************************************************************/

using ProjekatBolnica.Backend.Repository;
using System;

namespace Model.ManagerModel
{
    public class Resource : IIdentifiable<long>
    {
        public long Id { get; set; }

        public String Name { get; set; }

        public int Amount { get; set; }

        public Room Room { get; set; }

        public Resource(long id, String name, int amount, Room room)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Room = room;
        }

        public Resource()
        {
        }

        public Resource(long id)
        {
            Id = id;
        }

        public long GetId() => Id;

        public void SetId(long id) => Id = id;

    }
}