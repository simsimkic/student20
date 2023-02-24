/***********************************************************************
 * Module:  Room.cs
 * Purpose: Definition of the Class Model.ManagerModel.Room
 ***********************************************************************/

using Model.UtilityModel;
using ProjekatBolnica.Backend.Repository;
using System;

namespace Model.ManagerModel
{
    public class Room : IIdentifiable<long>
    {
        public long Id { get; set; }

        public TimePeriod Renovation { get; set; }

        public String Name { get; set; }

        public FunctionOfRoom Function { get; set; }

        public Room(long id, TimePeriod renovation, String name, FunctionOfRoom function)
        {
            Id = id;
            Renovation = renovation;
            Name = name;
            Function = function;
        }

        public Room(long id)
        {
            Id = id;
        }

        public Room()
        {
        }

        public long GetId() => Id;

        public void SetId(long id) => Id = id;

        public override string ToString()
        {
            return Name;
        }
    }
}