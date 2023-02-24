/***********************************************************************
 * Module:  Notifications.cs
 * Purpose: Definition of the Class Patient.Notification
 ***********************************************************************/

using ProjekatBolnica.Backend.Repository;
using System;

namespace Model.UtilityModel
{
    public class Notification : IIdentifiable<long>
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public long Id { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public Notification() { }
        public Notification(long id, string description, DateTime date)
        {
            Date = date;
            Description = description;
            Id = id;
        }
        public long GetId() => Id;
        public void SetId(long id) => Id = id;
    }
}