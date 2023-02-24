using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DoctorModel;
using Model.UtilityModel;
using Repository.HospitalRepository;
using Repository.UserRepository;

namespace ProjekatBolnica.Backend.Service.UserService
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public NotificationService(INotificationRepository notificationRepository, IAppointmentRepository appointmentRepository)
        {
            _notificationRepository = notificationRepository;
            _appointmentRepository = appointmentRepository;
        }
        public void Delete(Notification notification) => _notificationRepository.Delete(notification);

        public Notification Get(long id) => _notificationRepository.Get(id);

        public IEnumerable<Notification> GetAll() => _notificationRepository.GetAll();

        public List<Notification> GetNotificationsForUser(User user) => _notificationRepository.GetNotificationsForUser(user);

        public void New(Notification notification) => _notificationRepository.New(notification);

        public void Set(Notification notification) => _notificationRepository.Set(notification);


        public void SecretarySendNotification(User sender, Appointment appointment)
        {
            if (_appointmentRepository.GetAllEager().Where(x => x.Id == appointment.Id) != null)
            {
                Notification notificationForPatient = new Notification();
                notificationForPatient.Description = appointment.ToDescription();
                notificationForPatient.Date = DateTime.Now;
                notificationForPatient.Sender = sender;
                notificationForPatient.Receiver = appointment.Patient;
                _notificationRepository.New(notificationForPatient);
                Notification notificationForDoctor = new Notification();
                notificationForDoctor.Description = appointment.ToDescription();
                notificationForDoctor.Date = DateTime.Now;
                notificationForDoctor.Sender = sender;
                notificationForDoctor.Receiver = appointment.Doctor;
                _notificationRepository.New(notificationForDoctor);
            }          
        }

    }
}
