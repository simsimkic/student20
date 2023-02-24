using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DoctorModel;
using Model.UtilityModel;
using Service.HospitalServices;

namespace ProjekatBolnica.Backend.Service.UserService
{
    public interface INotificationService : IService<Notification, long>
    {
        List<Notification> GetNotificationsForUser(User user);
        void SecretarySendNotification(User sender, Appointment appointment);
    }
}
