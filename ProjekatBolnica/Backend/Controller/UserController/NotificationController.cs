using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DoctorModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Service.UserService;

namespace ProjekatBolnica.Backend.Controller.UserController
{
    public class NotificationController : INotificationController
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public void Delete(Notification notification) => _notificationService.Delete(notification);
        public Notification Get(long id) => _notificationService.Get(id);
        public IEnumerable<Notification> GetAll() => _notificationService.GetAll();
        public List<Notification> GetNotificationsForUser(User user) => _notificationService.GetNotificationsForUser(user);
        public void New(Notification notification) => _notificationService.New(notification);
        public void Set(Notification notification) => _notificationService.Set(notification);
        public void SecretarySendNotification(User sender, Appointment appointment) => _notificationService.SecretarySendNotification( sender, appointment);
    }
}
