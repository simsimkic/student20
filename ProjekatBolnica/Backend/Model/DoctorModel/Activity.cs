using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ManagerModel;
using ProjekatBolnica.Backend.Repository;

namespace ProjekatBolnica.Backend.Model.DoctorModel
{
    public abstract class Activity : IIdentifiable<long>
    {
        public Room Room { get; set; }
        public Patient Patient { get; set; }
        public DateTime DateAndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public long Id { get; set; }

        public Activity()
        {

        }

        public Activity(long id, Patient patient, Room room, DateTime dateAndTime, TimeSpan duration)
        {
            Id = id;
            Patient = patient;
            Room = room;
            DateAndTime = dateAndTime;
            Duration = duration;
        }

        protected Activity(Patient patient, DateTime dateAndTime)
        {
            Patient = patient;
            DateAndTime = dateAndTime;
        }

        public long GetId()
        {
            return Id;
        }

        public void SetId(long id)
        {
            Id = id;
        }
    }
}
