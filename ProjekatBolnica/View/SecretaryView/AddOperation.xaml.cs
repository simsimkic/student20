using Controller.HospitalController;
using Controller.ManagerController;
using Controller.UserController;
using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Controller.UserController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddOperation.xaml
    /// </summary>
    public partial class AddOperation : Window
    {
        private UserController userController;
        private RoomsController roomsController;
        private NotificationController notificationController;
        private OperationController operationController;

        private bool isExistCombo;
        private Room roomPomocni;
        private Specialist doctorPomocni = new Specialist();

        ComboBox FreeAppointments = new ComboBox();

        private Patient patientPomocni = new Patient();
        private DateTime dateTimePomcni = new DateTime();

        public bool IsExistCombo { get => isExistCombo; set => isExistCombo = value; }
        public Patient PatientPomocni { get => patientPomocni; set => patientPomocni = value; }
        public Specialist DoctorPomocni { get => doctorPomocni; set => doctorPomocni = value; }
        public DateTime DateTimePomcni { get => dateTimePomcni; set => dateTimePomcni = value; }
        public Room RoomPomocni { get => roomPomocni; set => roomPomocni = value; }


        public AddOperation()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            IsExistCombo = false;

            var app = Application.Current as App;
            userController = (UserController)app.UserController;
            roomsController = (RoomsController)app.RoomsController;
            notificationController = (NotificationController)app.NotificationController;
            operationController = (OperationController)app.OperationController;

            //za combobox od pacijenata
            for (int i = 0; i < userController.GetAllPatients().Count; i++)
            {
                comboPatient.Items.Add(userController.GetAllPatients().ElementAt(i).Name + " "
                    + userController.GetAllPatients().ElementAt(i).Surname + " "
                    + userController.GetAllPatients().ElementAt(i).PersonalIDnumber);
            }
            //za combobox specijalista
            for (int i = 0; i < userController.GetAllSpecialists().Count; i++)
            {
                comboDoctor.Items.Add(userController.GetAllSpecialists().ElementAt(i).Name + " "
                    + userController.GetAllSpecialists().ElementAt(i).Surname + " "
                    + userController.GetAllSpecialists().ElementAt(i).Username + " "
                    + userController.GetAllSpecialists().ElementAt(i).Specialty.SpecialtyName);
            }
            //za combobox sobe
            for (int i = 0; i < roomsController.GetAllOperationRooms().Count; i++)
            {
                comboRoom.Items.Add(roomsController.GetAllOperationRooms().ElementAt(i).Name + " "
                    + roomsController.GetAllOperationRooms().ElementAt(i).Id);
            }
        }


        private void cancelOperation(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void clickFindFreeAppointments(object sender, RoutedEventArgs e)
        {        
            if ((dateForAppointment.SelectedDate == null) || comboDoctor.SelectedItem == null || comboPatient.SelectedItem == null
                || comboDoctor.SelectedItem.Equals("") || comboPatient.SelectedItem.Equals(""))
            {
                string message = "Warning! You must select the patient, doctor and date!";
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

            //IZABRAN DOKTOR
            DoctorPomocni = userController.GetAllSpecialists().ElementAt(comboDoctor.SelectedIndex);
            RoomPomocni = roomsController.GetAllControlRooms().ElementAt(comboRoom.SelectedIndex);

            FreeAppointments = new ComboBox
            {
                Height = 33,
                Width = 300,
                FontSize = 18,
                IsEditable = true,
                Margin = new Thickness(30, 30, 1, 10),
                HorizontalAlignment = HorizontalAlignment.Center,
                Name = "comboFreeAppointments"
            };

            DateTime dateAndTimeAppointment = (DateTime)dateForAppointment.SelectedDate;
            //poziv metode za trazenje slobodnih termina za datog doktora i datum i sobu...
            List<DateTime> lista = new List<DateTime>();
            lista = operationController.GetAvailableTermsForRoomAndDoctorAndDate(DoctorPomocni, dateAndTimeAppointment, RoomPomocni);
            //lista.Add(new DateTime(dateAndTimeAppointment.Year, dateAndTimeAppointment.Month, dateAndTimeAppointment.Day, 7, 0, 00));
            
            FreeAppointments.ItemsSource = lista;
            stackPanelAddAppointment.Children.Add(FreeAppointments);
            IsExistCombo = true;
        }


        private void confirmOperation(object sender, RoutedEventArgs e)
        {
            if ((dateForAppointment.SelectedDate == null) || comboDoctor.SelectedItem == null || comboPatient.SelectedItem == null || comboRoom.SelectedItem == null
                || comboDoctor.SelectedItem.Equals("") || comboPatient.SelectedItem.Equals("") || comboRoom.SelectedItem.Equals("") || (FreeAppointments.SelectedItem == null))
            {
                string message = "Warning! You must select the patient, doctor, date and time!";
                System.Windows.MessageBox.Show(message);
                return;
            }

            DateTime confirmAppointment = (DateTime)FreeAppointments.SelectedItem;
            Operation o = new Operation { DateAndTime = confirmAppointment };
            o.Specialist = doctorPomocni;
            o.Room = RoomPomocni;
            o.Patient = userController.GetAllPatients().ElementAt(comboPatient.SelectedIndex);

            operationController.New(o);

            Notification notificationForDoctor = new Notification();
            notificationForDoctor.Date = DateTime.Now;
            notificationForDoctor.Sender = SecretaryMainPage.secretary;
            notificationForDoctor.Receiver = o.Specialist;
            notificationForDoctor.Description = "Surgery is scheduled - " + o.ToDescription();
            notificationController.New(notificationForDoctor);
            Notification notificationForPatient = new Notification();
            notificationForPatient.Date = DateTime.Now;
            notificationForPatient.Sender = SecretaryMainPage.secretary;
            notificationForPatient.Receiver = o.Patient;
            notificationForPatient.Description = "Surgery is scheduled - " + o.ToDescription();
            notificationController.New(notificationForPatient);

            this.Close();
        }




    }
}
