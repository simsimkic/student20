using Controller.HospitalController;
using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Controller.UserController;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProjekatBolnica.SecretaryView
{
    /// <summary>
    /// Interaction logic for EditAppointment.xaml
    /// </summary>
    public partial class EditAppointment : Window
    {
        private AppointmentController appointmentController;
        private NotificationController notificationController;

        private bool isExistCombo;
        private Room sobaPomocna;
        Patient patientPomocni = new Patient();
        Doctor doctorPomocni = new Doctor();
        ComboBox FreeAppointments = new ComboBox();

        Appointment appointmentPomocni = new Appointment();


        public bool IsExistCombo { get => isExistCombo; set => isExistCombo = value; }
        public Room SobaPomocna { get => sobaPomocna; set => sobaPomocna = value; }
        public Patient PatientPomocni { get => patientPomocni; set => patientPomocni = value; }
        public Doctor DoctorPomocni { get => doctorPomocni; set => doctorPomocni = value; }
        public Appointment AppointmentPomocni { get => appointmentPomocni; set => appointmentPomocni = value; }


        public EditAppointment(Appointment a)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            IsExistCombo = false;

            var app = Application.Current as App;
            appointmentController = (AppointmentController)app.AppointmentController;
            notificationController = (NotificationController)app.NotificationController;

            appointmentPomocni = a;
            PatientPomocni = a.Patient;
            DoctorPomocni = a.Doctor;
            SobaPomocna = a.Room;
        }

        private void CancelAppointment(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void ConfirmAppointment(object sender, RoutedEventArgs e)
        {
            if ((dateForAppointment.SelectedDate == null) || (FreeAppointments.SelectedItem == null))
            {
                string message = "Warning! You must select date and time!";
                System.Windows.MessageBox.Show(message);
                return;
            }

            DateTime confirmAppointment = (DateTime)FreeAppointments.SelectedItem;
            appointmentPomocni.DateAndTime = confirmAppointment;

            appointmentController.Set(appointmentPomocni);
            Notification notificationForDoctor = new Notification();
            notificationForDoctor.Date = DateTime.Now;
            notificationForDoctor.Sender = SecretaryMainPage.secretary;
            notificationForDoctor.Receiver = appointmentPomocni.Doctor;
            notificationForDoctor.Description = "Edit appointment - " + appointmentPomocni.ToDescription();
            notificationController.New(notificationForDoctor);
            Notification notificationForPatient = new Notification();
            notificationForPatient.Date = DateTime.Now;
            notificationForPatient.Sender = SecretaryMainPage.secretary;
            notificationForPatient.Receiver = appointmentPomocni.Patient;
            notificationForPatient.Description = "Edit appointment - " + appointmentPomocni.ToDescription();
            notificationController.New(notificationForPatient);

            this.Close();
        }


        private void ClickFindFreeAppointmentsAndRoom(object sender, RoutedEventArgs e)
        {
            if (dateForAppointment.SelectedDate == null)
            {
                string message = "Warning! You must select a date!";
                MessageBox.Show(message);
                return;
            }
            else if ((DateTime.Compare(DateTime.Now, (DateTime)dateForAppointment.SelectedDate) > 0)
                && (!DateTime.Now.Day.Equals(dateForAppointment.SelectedDate.Value.Day)))
            {
                string message = "Warning! Date is not valid!";
                MessageBox.Show(message);
                return;
            }

            if (isExistCombo == true)
            {
                stackPanelAddAppointment.Children.RemoveAt(2);
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
            lista = appointmentController.GetAvailableTermsForRoomAndDoctorAndDate(DoctorPomocni, dateAndTimeAppointment, SobaPomocna);

            FreeAppointments.ItemsSource = lista;
            stackPanelAddAppointment.Children.Add(FreeAppointments);
            IsExistCombo = true;
        }


    }
}
