using Controller.HospitalController;
using Controller.UserController;
using Model;
using Model.DoctorModel;
using ProjekatBolnica.PatientView;
using ProjekatBolnica.View.PatientView.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ProjekatBolnica.View.PatientView
{
    /// <summary>
    /// Interaction logic for EditAppointment.xaml
    /// </summary>
    public partial class EditAppointment : Window
    {
        private AppointmentController appointmentController;
        private readonly IUserController _doctorController;
        Appointment appointment;

        private DateTime _selectedAppointmentDate;
        DoctorsNames doctor;


        public DateTime SelectedAppointmentDate
        {
            get { return _selectedAppointmentDate; }
            set
            {
                if (_selectedAppointmentDate != value)
                {
                    _selectedAppointmentDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public static ObservableCollection<DateTime> AppointmentListView
        {
            get;
            set;
        }

        public static ObservableCollection<DoctorsNames> Doctors
        {
            get;
            set;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public EditAppointment(Appointment a)
        {
            InitializeComponent();
            DataContext = this;
            appointment = a;

            var app = Application.Current as App;
            appointmentController = (AppointmentController)app.AppointmentController;
            _doctorController = app.UserController;

            appointmentDatePicker.DisplayDateStart = DateTime.Now.AddHours(24);
            LoadDoctors();
           
        }

        private void LoadDoctors()
        {
            ObservableCollection<DoctorsNames> rec = new ObservableCollection<DoctorsNames>();
            List<Doctor> list = _doctorController.GetAllDoctors();
            for (int i = 0; i < list.ToArray().Length; i++)
            {
                rec.Add(new DoctorsNames
                {
                    ID = list[i].Id,
                    DoctorName = list[i].Name + " " + list[i].Surname
                }
                );
            }
            Doctors = rec;
        }

        private ObservableCollection<DateTime> LoadAppointmentTime()
        {
            List<DateTime> appoitmentTime;
            doctor = (DoctorsNames)doctorComboBox.SelectedItem;
            appoitmentTime = appointmentController.GetAvailableTerms((Doctor)_doctorController.Get(doctor.ID), appointmentDatePicker.SelectedDate.Value);
            ObservableCollection<DateTime> temp = new ObservableCollection<DateTime>();
            foreach (DateTime app in appoitmentTime)
            {
                temp.Add(app);
            }
            AppointmentListView = temp;
            return AppointmentListView;

        }

        private bool IsDoctorSelected() => doctorComboBox.SelectedItem != null;
        private bool IsDateSelected() => appointmentDatePicker.SelectedDate.HasValue;
        private bool IsTimeSelected() => timeComboBox.SelectedItem != null;
        private bool IsTimeComboBoxEmpty() => timeComboBox.Items.Count == 0;

        private void DoctorComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (IsDoctorSelected() && IsDateSelected())
            {
                timeComboBox.ItemsSource = LoadAppointmentTime();
                if (IsTimeComboBoxEmpty())
                    MessageBox.Show("No available terms!");
            }
        }

        private void AppointmentDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsDoctorSelected() && IsDateSelected())
            {
                timeComboBox.ItemsSource = LoadAppointmentTime();
                if (timeComboBox.Items.Count == 0)
                    MessageBox.Show("No available terms!");
            }
        }

        private void EditAppointment_btn_Click(object sender, RoutedEventArgs e)
        {
            if (IsDoctorSelected() && IsDateSelected() && IsTimeSelected()) 
            {
                appointmentController.Set(appointment);
                AppointmentsList appointmentsList = new AppointmentsList(appointment.Patient);
                appointmentsList.Show();
                Close();
            }
            else
                MessageBox.Show("Warning! Some fields are empty!");
        }

        private void CancelAppointment_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
