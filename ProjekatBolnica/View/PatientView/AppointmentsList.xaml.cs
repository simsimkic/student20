using Controller.HospitalController;
using Model;
using Model.DoctorModel;
using ProjekatBolnica.View.PatientView;
using ProjekatBolnica.View.PatientView.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProjekatBolnica.PatientView
{
    /// <summary>
    /// Interaction logic for AppointmentsList.xaml
    /// </summary>
    public partial class AppointmentsList : Window
    {

        Patient patient;
        private readonly IAppointmentController _appointmentController;
        private GridData appointmentRow;

        public ObservableCollection<GridData> Apppontments
        {
            get;
            set;
        }

        public AppointmentsList(Patient p)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Bar.Visibility = Visibility.Collapsed;
            patient = p;
            NotificationPanel.Children.Add(new NotificationView(patient));
            var app = Application.Current as App;
            _appointmentController = app.AppointmentController;


            DataContext = this;
            loadAppointments();
        }

        private void loadAppointments()
        {
            ObservableCollection<GridData> pom = new ObservableCollection<GridData>();
            List<Appointment> list = _appointmentController.GetActivityByPatient(patient);
            for (int i = 0; i < list.ToArray().Length; i++)
            {
                pom.Add(new GridData
                {
                    ID = list[i].Id,
                    Date = list[i].DateAndTime,
                    Doctor = list[i].Doctor.Name + " " + list[i].Doctor.Surname
                }
                );
            }
            Apppontments = pom;
        }

        private void btnBarClk(object sender, RoutedEventArgs e)
        {

            if (Bar.Visibility == Visibility.Visible)
                Bar.Visibility = Visibility.Collapsed;
            else
                Bar.Visibility = Visibility.Visible;

        }

        private void btnAppointment(object sender, RoutedEventArgs e)
        {
            MakeAnAppointment appointment = new MakeAnAppointment(patient);
            appointment.Show();
            this.Close();

        }

        private void btnProfile(object sender, RoutedEventArgs e)
        {
            PatientProfile pp = new PatientProfile(patient);

            pp.Show();
            this.Close();
        }

        private void btnSingOut(object sender, RoutedEventArgs e)
        {
            PatientLogin login = new PatientLogin();
            login.Show();
            this.Close();

        }

        private void btnEditAppointments(object sender, RoutedEventArgs e)
        {
            AppointmentsList al = new AppointmentsList(patient);
            al.Show();
            this.Close();

        }


        private void btnRecords(object sender, RoutedEventArgs e)
        {
            PatientRecords pr = new PatientRecords(patient);
            pr.Show();
            this.Close();

        }

        private void btnSurvey(object sender, RoutedEventArgs e)
        {

            PatientSurvey ps = new PatientSurvey(patient);
            ps.Show();
            this.Close();
        }


        private void notificationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!(NotificationPanel.Visibility == Visibility.Visible))
            {

                NotificationPanel.Visibility = Visibility.Visible;
            }
            else
                NotificationPanel.Visibility = Visibility.Collapsed;
        }

        private void btnFeedback_Click(object sender, RoutedEventArgs e)
        {
            feedbackUC.Visibility = Visibility.Visible;
            feedbackPanel.Visibility = Visibility.Collapsed;
        }

        private void moreBtn_Click(object sender, RoutedEventArgs e)
        {
            if (feedbackPanel.Visibility != Visibility.Visible)
                feedbackPanel.Visibility = Visibility.Visible;
            else
                feedbackPanel.Visibility = Visibility.Collapsed;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsDataGrid.Items.Count != 0)
            {
                if (appointmentsDataGrid.SelectedItems.Count == 1)
                {
                    appointmentRow = (GridData)appointmentsDataGrid.SelectedItems[0];

                    if (IsChangeAllowed(appointmentRow))
                    {
                        if (RemoveConfirmation())
                        {
                            appointmentRow = (GridData)appointmentsDataGrid.SelectedItems[0];
                            _appointmentController.Delete(_appointmentController.Get(appointmentRow.ID));
                            loadAppointments();
                            appointmentsDataGrid.ItemsSource = Apppontments;
                        }
                    }
                    else {
                        MessageBox.Show("Change not allowed !");
                    }

                }
            }
        }

        private bool IsChangeAllowed(GridData appointmentRow)
        {
            TimeSpan ts = new TimeSpan();
            ts = appointmentRow.Date - DateTime.Now;
            return ts.TotalHours > 26;
        }

        private bool RemoveConfirmation()
        {
            return MessageBox.Show("Are you sure you want to cancel your appointment? ", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }

        private void AppointmentEdit_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsDataGrid.Items.Count != 0)
            {
                if (appointmentsDataGrid.SelectedItems.Count == 1)
                {
                    appointmentRow = (GridData)appointmentsDataGrid.SelectedItems[0];
                    if (IsChangeAllowed(appointmentRow))
                    {
                        EditAppointment editAppointment = new EditAppointment(_appointmentController.Get(appointmentRow.ID));
                        editAppointment.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Change not allowed !");
                    }

                }
            }
        }
    }
}