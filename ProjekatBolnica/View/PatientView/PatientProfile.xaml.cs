using Model;
using ProjekatBolnica.View.PatientView;
using System;
using System.Windows;

namespace ProjekatBolnica.PatientView
{

    public partial class PatientProfile : Window
    {
        Patient patient;
        public PatientProfile(Patient p)
        {

            InitializeComponent();
            patient = p;
            NotificationPanel.Children.Add(new NotificationView(patient));
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Bar.Visibility = Visibility.Collapsed;
            
            

            lblName.Content = patient.Name;
            lblSurname.Content = patient.Surname;
            lblUsername.Content = patient.Username;
            string pass="";
            for (int i = 0; i < patient.Password.Length; i++)
            { pass += "*"; }

            lblPassword.Content = pass;
            lblNumber.Content = patient.Telephone;
            lblJMBG.Content = patient.PersonalIDnumber;
            lblEmail.Content = patient.Email;
            lblAddress.Content = "Adresa";
            lblCity.Content = "Grad";
            lblDate.Content = patient.DateOfBirth.ToString(" dd / MM / yyyy");
        }

        public PatientProfile()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Bar.Visibility = Visibility.Collapsed;
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

        private void btnEditClc(object sender, RoutedEventArgs e)
        {
            EditPatient edit = new EditPatient(patient);
            this.Close();
            edit.Show();
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
    }
}
