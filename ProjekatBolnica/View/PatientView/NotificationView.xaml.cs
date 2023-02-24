using Model;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Controller.UserController;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjekatBolnica.View.PatientView
{
    /// <summary>
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class NotificationView : UserControl
    {

        private INotificationController notificationController;

        private List<Notification> notificationList=new List<Notification>();
        public ObservableCollection<UserControl> Data { get; set; }
        public NotificationView(Patient patient)
        {
            InitializeComponent();

            DataContext = this;

            var app = Application.Current as App;

            notificationController = app.NotificationController;

            Data = new ObservableCollection<UserControl>();

            FillNotificationList(patient);

            loadNotification();

        }

        private void FillNotificationList(Patient patient)
        {
            foreach (Notification notification in notificationController.GetNotificationsForUser(patient))
            {
                notificationList.Add(notification);
            }
        }

        private void loadNotification() 
        {
            notificationList.ForEach(notification =>
                Data.Add(new NotificationUC
                {
                    NotificationText = notification.Description
                }
            ));

        }
    }
}
