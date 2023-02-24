using Model;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjekatBolnica.SecretaryView
{
    /// <summary>
    /// Interaction logic for SecretaryMainPage.xaml
    /// </summary>
    public partial class SecretaryMainPage : Window
    {
        public static User secretary;

        public User Secretary { get => secretary; set => secretary = value; }

        public SecretaryMainPage(User user)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Secretary = user;
            nameAndSurname.Content = Secretary.Name + " " + Secretary.Surname;
        }



        private void clickAddGuestPatient(object sender, RoutedEventArgs e)
        {
            AddGuestPatient addGuestPatient = new AddGuestPatient();
            addGuestPatient.ShowDialog();
        }

        private void AddPatient(object sender, RoutedEventArgs e)
        {
            AddPatient addPatientWindow = new AddPatient();
            addPatientWindow.ShowDialog();
        }

        private void patientsPage(object sender, RoutedEventArgs e)
        {
            PatientsPage patientsPageList = new PatientsPage();
            patientsPageList.Show();
        }

        private void editAccount(object sender, RoutedEventArgs e)
        {
            EditAccountPage editAccountSecretary = new EditAccountPage(secretary);
            editAccountSecretary.ShowDialog();
        }

        private void clickFeedbackPage(object sender, RoutedEventArgs e)
        {
            FeedbackPage feedbackPage = new FeedbackPage();
            feedbackPage.ShowDialog();
        }

        private void secretaryLogOut(object sender, RoutedEventArgs e)
        {
            string message = "Do you want to log out?";
            string title = "LogOut";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = (DialogResult)System.Windows.MessageBox.Show(message, title, (MessageBoxButton)buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                SecretaryLogIn secretaryLogIn = new SecretaryLogIn();
                this.Close();
                secretaryLogIn.ShowDialog();
            }
        }

        private void clickAddAppointment(object sender, RoutedEventArgs e)
        {
            AddAppointment addAppointment = new AddAppointment();
            addAppointment.ShowDialog();
        }

        private void clickAddOperation(object sender, RoutedEventArgs e)
        {
            AddOperation addOperation = new AddOperation();
            addOperation.ShowDialog();
        }

        private void clickAddHospitalStay(object sender, RoutedEventArgs e)
        {
            AddHospitalStay addHospitalStay = new AddHospitalStay();
            addHospitalStay.ShowDialog();
        }

        private void clickAppointmentsPage(object sender, RoutedEventArgs e)
        {
            AppointmentsPage appointmentsPage = new AppointmentsPage();
            appointmentsPage.ShowDialog();
        }

        private void clickOperationsPage(object sender, RoutedEventArgs e)
        {
            OperationsPage operationsPage = new OperationsPage();
            operationsPage.ShowDialog();
        }

        private void clickHospitalStayPage(object sender, RoutedEventArgs e)
        {
            HospitalStayPage hospitalStayPage = new HospitalStayPage();
            hospitalStayPage.ShowDialog();
        }

        private void clickHelp(object sender, RoutedEventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }

     

       

        
    }
}
