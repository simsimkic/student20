using Controller.HospitalController;
using Controller.ManagerController;
using Controller.UserController;
using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.View.SecretaryView;
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
    /// Interaction logic for AddHospitalStay.xaml
    /// </summary>
    public partial class AddHospitalStay : Window
    {
        private UserController userController;
        private RoomsController roomsController;
        private HospitalStayController hospitalStayController;
        private ResourceController resourceController;

        private bool isExistCombo;
        private Room roomTemp;
        ComboBox FreeRoomAndBed = new ComboBox();

        private Patient patientPmocni = new Patient();
        private TimePeriod timePeriodPomocni = new TimePeriod();

        public bool IsExistCombo { get => isExistCombo; set => isExistCombo = value; }
        public Patient PatientPmocni { get => patientPmocni; set => patientPmocni = value; }
        public TimePeriod TimePeriodPomocni { get => timePeriodPomocni; set => timePeriodPomocni = value; }
        public Room RoomTemp { get => roomTemp; set => roomTemp = value; }



        public AddHospitalStay()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            IsExistCombo = false;

            var app = Application.Current as App;
            userController = (UserController)app.UserController;
            roomsController = (RoomsController)app.RoomsController;
            hospitalStayController = (HospitalStayController)app.HospitalStayController;
            resourceController = (ResourceController)app.ResourceController;

            //za combobox od pacijenata
            for (int i = 0; i < userController.GetAllPatients().Count; i++)
            {
                comboPatient.Items.Add(userController.GetAllPatients().ElementAt(i).Name + " "
                    + userController.GetAllPatients().ElementAt(i).Surname + " "
                    + userController.GetAllPatients().ElementAt(i).PersonalIDnumber);
            }
            //za combobox od soba
            for (int i = 0; i < roomsController.GetAllRoomsForLaying().Count; i++)
            {
                comboRoom.Items.Add(roomsController.GetAllRoomsForLaying().ElementAt(i).Name + " " +
                    roomsController.GetAllRoomsForLaying().ElementAt(i).Id);
            }
        }


        private void cancelHospitalStay(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void confirmHospitalStay(object sender, RoutedEventArgs e)
        {
            if ((dateForStart.SelectedDate == null) || (dateForEnd.SelectedDate == null) || comboPatient.SelectedItem == null || comboRoom.SelectedItem == null
                 || comboPatient.SelectedItem.Equals("") || comboRoom.SelectedItem.Equals("") || (FreeRoomAndBed.SelectedItem == null))
            {
                string message = "Warning! You must select the patient, date-start, date-end and bed!";
                System.Windows.MessageBox.Show(message);
                return;
            }

            PatientPmocni = userController.GetAllPatients().ElementAt(comboPatient.SelectedIndex);

            //int confirmIdBed = (int)FreeRoomAndBed.SelectedItem;
            HospitalStay hs = new HospitalStay();
            hs.Bed = (Bed)FreeRoomAndBed.SelectedItem;
            //hs.Bed.Room = RoomTemp;
            hs.Patient = PatientPmocni;
            hs.Duration = timePeriodPomocni;

            hospitalStayController.New(hs);
            this.Close();
        }

        private void clickFindFreeBedAndRoom(object sender, RoutedEventArgs e)
        {
            if ((dateForStart.SelectedDate == null) || (dateForEnd.SelectedDate == null) || comboPatient.SelectedItem == null || comboRoom.SelectedItem == null
                 || comboPatient.SelectedItem.Equals("") || comboRoom.SelectedItem.Equals(""))
            {
                string message = "Warning! You must select the room, patient, date-start and date-end!";
                System.Windows.MessageBox.Show(message);
                return;
            }
            else if (DateTime.Compare((DateTime)dateForStart.SelectedDate, (DateTime)dateForEnd.SelectedDate) >= 0)
            {
                string message = "Warning! Dates are invalid!";
                System.Windows.MessageBox.Show(message);
                return;
            }
            else if ((DateTime.Compare(DateTime.Now, (DateTime)dateForStart.SelectedDate) > 0)
                && (!DateTime.Now.Day.Equals(dateForStart.SelectedDate.Value.Day)))
            {
                string message = "Warning! Date is not valid!";
                System.Windows.MessageBox.Show(message);
                return;
            }

            timePeriodPomocni.StartTime = (DateTime)dateForStart.SelectedDate;
            timePeriodPomocni.EndTime = (DateTime)dateForEnd.SelectedDate;
            RoomTemp = roomsController.GetAllRoomsForLaying().ElementAt(comboRoom.SelectedIndex);

            if (IsExistCombo == true) { stackPanelAddAppointment.Children.RemoveAt(2); }

            FreeRoomAndBed = new ComboBox
            {
                Height = 33,
                Width = 300,
                FontSize = 18,
                IsEditable = true,
                Margin = new Thickness(30, 30, 1, 10),
                HorizontalAlignment = HorizontalAlignment.Center,
                Name = "comboFreeRoomAndBed"
            };

            //ovde treba da budu kreveti iz odabrane sobe za combobox...
            List<Bed> lista = new List<Bed>();
            lista = resourceController.GetBedsInRoom(roomTemp);
            FreeRoomAndBed.ItemsSource = lista;
            stackPanelAddAppointment.Children.Add(FreeRoomAndBed);
            IsExistCombo = true;
        }



    }
}
