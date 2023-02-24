using Controller.HospitalController;
using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Controller.UserController;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ProjekatBolnica.SecretaryView
{
    /// <summary>
    /// Interaction logic for EditOperation.xaml
    /// </summary>
    public partial class EditOperation : Window
    {
        private OperationController operationController;
        private NotificationController notificationController;

        private bool isExistCombo;
        private Room roomPomocni;
        Patient patientPomocni = new Patient();
        Specialist doctorPomocni = new Specialist();
        ComboBox FreeAppointments = new ComboBox();

        Operation operationPomocni = new Operation();

        public bool IsExistCombo { get => isExistCombo; set => isExistCombo = value; }
        public Room RoomPomocni { get => roomPomocni; set => roomPomocni = value; }
        public Patient PatientPomocni { get => patientPomocni; set => patientPomocni = value; }
        public Specialist DoctorPomocni { get => doctorPomocni; set => doctorPomocni = value; }
        public Operation AppointmentPomocni { get => operationPomocni; set => operationPomocni = value; }



        public EditOperation(Operation o)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            IsExistCombo = false;

            var app = Application.Current as App;
            operationController = (OperationController)app.OperationController;
            notificationController = (NotificationController)app.NotificationController;

            operationPomocni = o;
            PatientPomocni = o.Patient;
            DoctorPomocni = o.Specialist;
            RoomPomocni = o.Room;          
        }


        private void cancelOperation(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void confirmOperation(object sender, RoutedEventArgs e)
        {
            if ((dateForAppointment.SelectedDate == null) || (FreeAppointments.SelectedItem == null))
            {
                string message = "Warning! You must select date and time!";
                System.Windows.MessageBox.Show(message);
                return;
            }

            DateTime confirmAppointment = (DateTime)FreeAppointments.SelectedItem;
            operationPomocni.DateAndTime = confirmAppointment;

            operationController.Set(operationPomocni);
            Notification notificationForDoctor = new Notification();
            notificationForDoctor.Date = DateTime.Now;
            notificationForDoctor.Sender = SecretaryMainPage.secretary;
            notificationForDoctor.Receiver = operationPomocni.Specialist;
            notificationForDoctor.Description = "Edit operation - " + operationPomocni.ToDescription();
            notificationController.New(notificationForDoctor);
            Notification notificationForPatient = new Notification();
            notificationForPatient.Date = DateTime.Now;
            notificationForPatient.Sender = SecretaryMainPage.secretary;
            notificationForPatient.Receiver = operationPomocni.Patient;
            notificationForPatient.Description = "Edit operation - " + operationPomocni.ToDescription();
            notificationController.New(notificationForPatient);

            this.Close();
        }


        private void clickFindFreeAppointmentsAndRoom(object sender, RoutedEventArgs e)
        {
            if (dateForAppointment.SelectedDate == null)
            {
                string message = "Warning! You must select a date!";
                System.Windows.MessageBox.Show(message);
                return;
            }
            else if ((DateTime.Compare(DateTime.Now, (DateTime)dateForAppointment.SelectedDate) > 0)
                && (!DateTime.Now.Day.Equals(dateForAppointment.SelectedDate.Value.Day)))
            {
                string message = "Warning! Date is not valid!";
                System.Windows.MessageBox.Show(message);
                return;
            }

            if (isExistCombo == true)
            {
                stackPanelAddAppointment.Children.RemoveAt(1);
            }

            FreeAppointments = new ComboBox
            {
                Height = 33,
                Width = 300,
                FontSize = 18,
                IsEditable = true,
                Margin = new Thickness(10, 30, 1, 10),
                HorizontalAlignment = HorizontalAlignment.Center,
                Name = "comboFreeAppointments"
            };

            DateTime dateAndTimeAppointment = (DateTime)dateForAppointment.SelectedDate;
            //poziv metode za trazenje slobodnih termina za datog doktora, datum i sobu...
            List<DateTime> lista = new List<DateTime>();
            lista = operationController.GetAvailableTermsForRoomAndDoctorAndDate(DoctorPomocni, dateAndTimeAppointment, RoomPomocni);

            FreeAppointments.ItemsSource = lista;
            stackPanelAddAppointment.Children.Add(FreeAppointments);
            IsExistCombo = true;
        }


    }
}
