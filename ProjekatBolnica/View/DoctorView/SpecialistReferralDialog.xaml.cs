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
using System.Windows.Shapes;
using Controller.HospitalController;
using Controller.ManagerController;
using Controller.UserController;
using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.DoctorView;
using Xceed.Wpf.Toolkit;

namespace ProjekatBolnica.View.DoctorView
{
    /// <summary>
    /// Interaction logic for SpecialistReferralDialog.xaml
    /// </summary>
    public partial class SpecialistReferralDialog : Window
    {
        public Patient SelectedPatient { get; set; }
        public ObservableCollection<Specialist> Specialists { get; set; }
        public ObservableCollection<Room> ControlRooms { get; set; }
        public ObservableCollection<DateTime> AvailableTerms { get; set; }
        private readonly IUserController _userController;
        private readonly IRoomsController _roomsController;
        private readonly IAppointmentController _appointmentController;
        public SpecialistReferralDialog(Patient patient)
        {
            InitializeComponent();
            if ((Application.Current.MainWindow as MainWindow).Theme == Theme.Dark) { dialogGrid.Background = Brushes.LightGray; }
            this.DataContext = this;
            var app = Application.Current as App;
            _userController = app.UserController;
            _roomsController = app.RoomsController;
            _appointmentController = app.AppointmentController;
            SelectedPatient = patient;
            Specialists = new ObservableCollection<Specialist>(_userController.GetAllSpecialists());
            ControlRooms = new ObservableCollection<Room>(_roomsController.GetAllControlRooms());
        }

        private void closeSpecialistRefferalDialog(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void openComboBox(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                comboBox.IsDropDownOpen = true;
            }
        }
        private void openCalendar(object sender, KeyEventArgs e)
        {
            DateTimePicker picker = (DateTimePicker)sender;
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                picker.IsOpen = true;
            }
        }

        private void referToSpecialist(object sender, RoutedEventArgs e)
        {
            if (specialistComboBox.SelectedItem == null)
            {
                string errSrb = "Izaberite specijalistu.";
                string errEng = "Choose a specialist.";
                printErr(errSrb, errEng);
                return;
            }
            if (officeComboBox.SelectedItem == null)
            {
                string errSrb = "Izaberite ordinaciju.";
                string errEng = "Choose an office.";
                printErr(errSrb, errEng);
                return;
            }
            if (!datePicker.SelectedDate.HasValue)
            {
                string errSrb = "Izaberite datum.";
                string errEng = "Choose date.";
                printErr(errSrb, errEng);
                return;
            }
            if (availableTermsComboBox.SelectedItem == null)
            {
                string errSrb = "Izaberite termin.";
                string errEng = "Choose a term.";
                printErr(errSrb, errEng);
                return;
            }
            Appointment appointment = new Appointment();
            appointment.DateAndTime = (DateTime)availableTermsComboBox.SelectedItem;
            appointment.Doctor = (Doctor)specialistComboBox.SelectedItem;
            appointment.Patient = SelectedPatient;
            appointment.Room = (Room)officeComboBox.SelectedItem;
            _appointmentController.New(appointment);
            this.Close();
        }
        private void printErr(string errSrb, string errEng)
        {
            if ((Application.Current.MainWindow as MainWindow).Lang == Backend.Model.DoctorModel.Language.English)
            {
                System.Windows.MessageBox.Show(errEng);
            }
            else
            {
                System.Windows.MessageBox.Show(errSrb);
            }
        }
        private void getAvailableTerms(object sender, SelectionChangedEventArgs e)
        {
            if (specialistComboBox.SelectedItem != null && officeComboBox.SelectedItem != null && datePicker.SelectedDate.HasValue)
            {
                AvailableTerms = new ObservableCollection<DateTime>(_appointmentController.GetAvailableTermsForRoomAndDoctorAndDate((Specialist)specialistComboBox.SelectedItem, datePicker.SelectedDate.Value, (Room)officeComboBox.SelectedItem));
                availableTermsComboBox.ItemsSource = AvailableTerms;
            }
        }
    }
}
