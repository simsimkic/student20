using Controller.HospitalController;
using Controller.ManagerController;
using Controller.UserController;
using Model;
using Model.DoctorModel;
using Model.ManagerModel;
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
    /// Interaction logic for AddAppointment.xaml
    /// </summary>
    public partial class AddAppointment : Window
    {
        private UserController userController;
        private AppointmentController appointmentController;
        private RoomsController roomsController;
        private NotificationController notificationController;

        private bool isExistCombo;
        private Room sobaPomocna;
        private Doctor doctorPomocni = new Doctor();

        ComboBox FreeAppointments = new ComboBox();

        public bool IsExistCombo { get => isExistCombo; set => isExistCombo = value; }
        public Doctor DoctorPomocni { get => doctorPomocni; set => doctorPomocni = value; }
        public Room SobaPomocna { get => sobaPomocna; set => sobaPomocna = value; }

        public AddAppointment()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            IsExistCombo = false;

            var app = Application.Current as App;
            userController = (UserController)app.UserController;
            roomsController = (RoomsController)app.RoomsController;
            appointmentController = (AppointmentController)app.AppointmentController;
            notificationController = (NotificationController)app.NotificationController;

            //za combobox od pacijenata
            for (int i = 0; i < userController.GetAllPatients().Count; i++)
            {
                comboPatient.Items.Add(userController.GetAllPatients().ElementAt(i).Name + " "
                    + userController.GetAllPatients().ElementAt(i).Surname + " "
                    + userController.GetAllPatients().ElementAt(i).PersonalIDnumber);
            }
            //za combobox doktora
            for (int i = 0; i < userController.GetAllDoctors().Count; i++)
            {
                comboDoctor.Items.Add(userController.GetAllDoctors().ElementAt(i).Name + " "
                    + userController.GetAllDoctors().ElementAt(i).Surname + " "
                    + userController.GetAllDoctors().ElementAt(i).Username);
            }
            //za combobox sobe
            for (int i = 0; i < roomsController.GetAllControlRooms().Count; i++)
            {
                comboRoom.Items.Add(roomsController.GetAllControlRooms().ElementAt(i).Name + " "
                    + roomsController.GetAllControlRooms().ElementAt(i).Id);
            }
        }


        private void cancelAppointment(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void clickFindFreeAppointmentsAndRoom(object sender, RoutedEventArgs e)
        {
            //PROVERE
            if ((dateForAppointment.SelectedDate == null) || comboDoctor.SelectedItem == null || comboPatient.SelectedItem == null || comboRoom.SelectedItem == null
                || comboDoctor.SelectedItem.Equals("") || comboPatient.SelectedItem.Equals("") || comboRoom.SelectedItem.Equals(""))
            {
                string message = "Warning! You must select the patient, room, doctor and date!";
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
                stackPanelAddAppointment.Children.RemoveAt(2);
            }

            //IZABRAN DOKTOR i SOBA
            DoctorPomocni = userController.GetAllDoctors().ElementAt(comboDoctor.SelectedIndex);
            SobaPomocna = roomsController.GetAllControlRooms().ElementAt(comboRoom.SelectedIndex);

            //Margin="30, 2, 1, 10" Height="33" Width="300" VerticalAlignment="Top" FontSize="18" IsEditable="True"
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
            //poziv metode za trazenje slobodnih termina za datog doktora i datum i sobu...
            List<DateTime> lista = new List<DateTime>();
            lista = appointmentController.GetAvailableTermsForRoomAndDoctorAndDate(DoctorPomocni, dateAndTimeAppointment, SobaPomocna);

            FreeAppointments.ItemsSource = lista;
            stackPanelAddAppointment.Children.Add(FreeAppointments);
            IsExistCombo = true;
        }




        private void confirmAppointment(object sender, RoutedEventArgs e)
        {
            if ((dateForAppointment.SelectedDate == null) || comboDoctor.SelectedItem == null || comboPatient.SelectedItem == null || comboRoom.SelectedItem == null
                || comboDoctor.SelectedItem.Equals("") || comboPatient.SelectedItem.Equals("") || comboRoom.SelectedItem.Equals("") || (FreeAppointments.SelectedItem == null))
            {
                string message = "Warning! You must select the patient, doctor, room, date and time!";
                System.Windows.MessageBox.Show(message);
                return;
            }

            DateTime confirmAppointment = (DateTime)FreeAppointments.SelectedItem;
            Appointment a = new Appointment { DateAndTime = confirmAppointment};
            a.Doctor = doctorPomocni;
            a.Room = sobaPomocna;
            a.Patient = userController.GetAllPatients().ElementAt(comboPatient.SelectedIndex);

            appointmentController.New(a);
            notificationController.SecretarySendNotification( SecretaryMainPage.secretary, a);

            this.Close();
        }






    }
}
