using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller.HospitalController;
using Model;
using Model.DoctorModel;
using Model.UtilityModel;

namespace ProjekatBolnica.Backend.Controller.UserController
{
    public interface INotificationController : IController<Notification, long>
    {
        List<Notification> GetNotificationsForUser(User user);

        void SecretarySendNotification(User sender, Appointment appointment);
    }
}
