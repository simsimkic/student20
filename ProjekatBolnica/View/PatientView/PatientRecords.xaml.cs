using Controller.HospitalController;
using Controller.PatientController;
using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.PatientModel;
using ProjekatBolnica.Backend.Controller.PatientController;
using ProjekatBolnica.Backend.Model.PatientModel;
using ProjekatBolnica.View.PatientView;
using ProjekatBolnica.View.PatientView.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using Xamarin.Forms.Internals;

namespace ProjekatBolnica.PatientView
{
    /// <summary>
    /// Interaction logic for PatientRecords.xaml
    /// </summary>
    public partial class PatientRecords : Window
    {
        Patient patient;
        private readonly IMedicalRecordsController _controllerRecords;
        private GridData recordRow;

        public ObservableCollection<GridData> Records
        {
            get;
            set;
        }

        public PatientRecords(Patient patient)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Bar.Visibility = Visibility.Collapsed;
            var app = Application.Current as App;
            _controllerRecords = app.MedicalRecordController;


            DataContext = this;
            this.patient = patient;

            NotificationPanel.Children.Add(new NotificationView(patient));

            loadRecord();

        }

        private void loadRecord()
        {
            ObservableCollection<GridData> rec = new ObservableCollection<GridData>();
            List<MedicalRecord> list =  _controllerRecords.GetMedicalRecordByPatient(patient);
            for (int i = 0; i < list.ToArray().Length; i++)
            {
                rec.Add(new GridData
                {
                    ID = list[i].Id,
                    Date = list[i].ExamDate,
                    Doctor = list[i].Doctor.Name + " " + list[i].Doctor.Surname
                }
                );
            }
            Records = rec;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           if(recordsDataGrid.Items.Count != 0)
            {
                if (recordsDataGrid.SelectedItems.Count == 1)
                {
                    MedicalRecord m;
                    recordRow = (GridData)recordsDataGrid.SelectedItems[0];
                    long id = (long)recordRow.ID;
                    m = _controllerRecords.Get(id);
                    medicalRecordUC.LoadRecorda(m);
                    medicalRecordUC.Visibility = Visibility.Visible;
                }
                else
                    MessageBox.Show("Warning!Please select one row");
                
            }
        }

        private void recordsDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
