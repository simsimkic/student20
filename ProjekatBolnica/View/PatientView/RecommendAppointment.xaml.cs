using Controller.HospitalController;
using Controller.PatientController;
using Controller.UserController;
using Model;
using Model.DoctorModel;
using Model.UserModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.PatientModel;
using ProjekatBolnica.View.PatientView.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ProjekatBolnica.View.PatientView
{
    /// <summary>
    /// Interaction logic for RecommendAppointment.xaml
    /// </summary>
    public partial class RecommendAppointment : UserControl
    {

        private  Patient patient;
        private readonly IAppointmentController _appointmentController;
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

        public RecommendAppointment()
        {
            InitializeComponent();
            DataContext = this;

            

            var app = Application.Current as App;
            _appointmentController = app.AppointmentController;
            _doctorController = app.UserController;




            LoadDoctors();
            setDatePicker();

        }

        public void LoadRecommendationForm(Patient p) 
        {
            patient = p;
            setDatePicker();
        }

        private void setDatePicker()
        {
            appointmentBeginDatePicker.DisplayDateStart = DateTime.Now.AddHours(24);
            appointmentEndDatePicker.DisplayDateStart = DateTime.Now.AddHours(24);
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

        private bool IsDoctorSelected() => doctorComboBox.SelectedItem != null;
        private bool IsBeginDateSelected() => appointmentBeginDatePicker.SelectedDate.HasValue;

        private bool IsEndDateSelected() => appointmentEndDatePicker.SelectedDate.HasValue;

        private TimePeriod MakeDatePeriod() 
        {
            if (appointmentBeginDatePicker.SelectedDate.Value.AddHours(24) < appointmentEndDatePicker.SelectedDate.Value)
                return new TimePeriod()
                {
                    StartTime = appointmentBeginDatePicker.SelectedDate.Value,
                    EndTime = appointmentEndDatePicker.SelectedDate.Value
                };
            else
                MessageBox.Show("End date has to be at least 24h after begin date ");
            return new TimePeriod() { StartTime=DateTime.Now};
        }

        private Priority getPriority() 
        {
            if (Doctor.IsChecked.Value)
                return Priority.Doctor;
            else
                return Priority.Date;
        }

        

        private bool IsDateIntervalvalid() => MakeDatePeriod().StartTime!= DateTime.Now;
        private bool IsAppointmentFound() => appointment != null;
        private void SubmitAppointment_btn_Click(object sender, RoutedEventArgs e)
        {
            TimePeriod timePeriod = new TimePeriod() { StartTime = DateTime.Now };
            if (IsDoctorSelected() && IsBeginDateSelected() && IsEndDateSelected() && IsDateIntervalvalid())
            {
                doctor = (DoctorsNames)doctorComboBox.SelectedItem;
                appointment =_appointmentController.RecommendAppointment((Doctor)_doctorController.Get(doctor.ID), MakeDatePeriod(), getPriority());
                if (IsAppointmentFound())
                {
                    string message = "Our recommendation: Doctor :" + appointment.Doctor.Name + " " + appointment.Doctor.Surname + "" +
                        "\n\t Term:" + appointment.DateAndTime;
                    MessageBoxResult messageBoxResult = MessageBox.Show(message, "Appointment recommendation", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        appointment.Patient = patient;
                        _appointmentController.New(appointment);
                        this.Visibility = Visibility.Collapsed;
                    }

                }
                else
                    MessageBox.Show("No available terms!");
                
            }
            
        }



        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void DoctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
