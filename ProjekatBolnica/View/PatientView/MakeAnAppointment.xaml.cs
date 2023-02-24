using Controller.HospitalController;
using Controller.PatientController;
using Controller.UserController;
using Model;
using Model.DoctorModel;
using ProjekatBolnica.View.PatientView;
using ProjekatBolnica.View.PatientView.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ProjekatBolnica.PatientView
{
    public partial class MakeAnAppointment : Window
    {
        private readonly Patient patient;
        private readonly IAppointmentController _appointmentController;
        private readonly IUserController _doctorController;

        private DateTime _selectedAppointmentDate;
        DoctorsNames doctor;


        public DateTime SelectedAppointmentDate
        {
            get { return _selectedAppointmentDate; }
            set { if (_selectedAppointmentDate != value)
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


        public MakeAnAppointment(Patient p)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Bar.Visibility = Visibility.Collapsed;
            var app = Application.Current as App;
            _appointmentController = app.AppointmentController;
            _doctorController = app.UserController;
            patient = p;

            NotificationPanel.Children.Add(new NotificationView(patient));

            DataContext = this;
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
                ) ;
            }
            Doctors = rec;
        }


        private ObservableCollection<DateTime> LoadAppointmentTime()
        {
            List<DateTime> appoitmentTime;
            doctor = (DoctorsNames)doctorComboBox.SelectedItem;
            appoitmentTime = _appointmentController.GetAvailableTerms((Doctor)_doctorController.Get(doctor.ID), appointmentDatePicker.SelectedDate.Value);
            ObservableCollection<DateTime> temp = new ObservableCollection<DateTime>();
            foreach (DateTime app in appoitmentTime)
            {
                temp.Add(app);
            }
            AppointmentListView = temp;
            return AppointmentListView;

        }

        private Appointment CreatAppointment()
        {
            return new Appointment(patient,
                                    (Doctor)_doctorController.Get(doctor.ID),
                                    (DateTime)timeComboBox.SelectedItem
                                    );
        }

        private void AppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsDoctorSelected() && IsDateSelected() && IsTimeSelected())
            {
                _appointmentController.New(CreatAppointment());
                timeComboBox.ItemsSource = LoadAppointmentTime();
            }
        }



        private bool IsDoctorSelected() => doctorComboBox.SelectedItem != null;
        private bool IsDateSelected() => appointmentDatePicker.SelectedDate.HasValue;
        private bool IsTimeSelected() => timeComboBox.SelectedItem != null;



        

        private void DoctorComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (IsDoctorSelected() && IsDateSelected())
            {
                timeComboBox.ItemsSource = LoadAppointmentTime();
                if (IsTimeComboBoxEmpty())
                    MessageBox.Show("No available terms!");
            }
        }


        private bool IsTimeComboBoxEmpty() => timeComboBox.Items.Count == 0;

        private void RecommendBtn_Click(object sender, RoutedEventArgs e)
        {
            RecommendedUC.LoadRecommendationForm(patient);
            RecommendedUC.Visibility = Visibility.Visible;

        }


        private void NotificationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!(NotificationPanel.Visibility == Visibility.Visible))
            {

                NotificationPanel.Visibility = Visibility.Visible;
            }
            else
                NotificationPanel.Visibility = Visibility.Collapsed;
        }

        private void BtnFeedback_Click(object sender, RoutedEventArgs e)
        {
            feedbackUC.Visibility = Visibility.Visible;
            feedbackPanel.Visibility = Visibility.Collapsed;
        }

        private void MoreBtn_Click(object sender, RoutedEventArgs e)
        {
            if (feedbackPanel.Visibility != Visibility.Visible)
                feedbackPanel.Visibility = Visibility.Visible;
            else
                feedbackPanel.Visibility = Visibility.Collapsed;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BtnBarClk(object sender, RoutedEventArgs e)
        {

            if (Bar.Visibility == Visibility.Visible)
                Bar.Visibility = Visibility.Collapsed;
            else
                Bar.Visibility = Visibility.Visible;

        }

  
        
        private void BtnAppointment(object sender, RoutedEventArgs e)
        {
            MakeAnAppointment appointment = new MakeAnAppointment(patient);
            appointment.Show();
            this.Close();

        }

        private void BtnProfile(object sender, RoutedEventArgs e)
        {
            PatientProfile pp = new PatientProfile(patient);

            pp.Show();
            this.Close();
        }

        private void BtnSingOut(object sender, RoutedEventArgs e)
        {
            PatientLogin login = new PatientLogin();
            login.Show();
            this.Close();

        }

        private void BtnEditAppointments(object sender, RoutedEventArgs e)
        {
            AppointmentsList al = new AppointmentsList(patient);
            al.Show();
            this.Close();

        }


        private void BtnRecords(object sender, RoutedEventArgs e)
        {
            PatientRecords pr = new PatientRecords(patient);
            pr.Show();
            this.Close();

        }

        private void BtnSurvey(object sender, RoutedEventArgs e)
        {

            PatientSurvey ps = new PatientSurvey(patient);
            ps.Show();
            this.Close();
        }

        private void appointmentDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsDoctorSelected() && IsDateSelected())
            {
                timeComboBox.ItemsSource = LoadAppointmentTime();
                if (timeComboBox.Items.Count == 0)
                    MessageBox.Show("No available terms!");
            }
        }
    }
}
