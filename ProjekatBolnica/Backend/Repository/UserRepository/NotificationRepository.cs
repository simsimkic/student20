/***********************************************************************
 * Module:  Class8.cs
 * Purpose: Definition of the Class Repository.PatientRepository.Class8
 ***********************************************************************/

using Model;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using System;
using System.Collections.Generic;

namespace Repository.UserRepository
{
   public class NotificationRepository : CSVRepository<Notification, long>,
        INotificationRepository,
        IEagerCSVRepository<Notification, long>
    {
        private const string ENTITY_NAME = "Notification";
        private readonly IEagerCSVRepository<User, long> _userRepository;


        public NotificationRepository(ICSVStream<Notification> stream, ISequencer<long> sequencer,
           IEagerCSVRepository<User, long> userRepository) : base(ENTITY_NAME, stream, sequencer)
        {
            _userRepository = userRepository;
        }
        public Notification GetEager(long id)
        {
            var notification = Get(id);
            notification.Sender = _userRepository.GetEager(notification.Sender.Id);
            notification.Receiver = _userRepository.GetEager(notification.Receiver.Id);
            return notification;
        }

        public IEnumerable<Notification> GetAllEager()
        {
            List<Notification> notifications = new List<Notification>();
            foreach(Notification notification in GetAll())
            {
                notifications.Add(GetEager(notification.Id));
            }
            return notifications;
        }

        public List<Notification> GetNotificationsForUser(User user)
        {
            List<Notification> notifications = new List<Notification>();
            foreach(Notification notification in GetAllEager())
            {
                if(notification.Receiver.Id == user.Id)
                {
                    notifications.Add(notification);
                }
            }
            return notifications;
        }
    }
}