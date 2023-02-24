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
using Controller.UserController;
using Model;
using Model.UserModel;
using ProjekatBolnica.View.PatientView;

namespace ProjekatBolnica.PatientView
{
    /// <summary>
    /// Interaction logic for EditPatetion.xaml
    /// </summary>
    public partial class EditPatient : Window
    {
        Patient patient;
        private readonly IUserController _patientController;
        private static readonly Regex _addressRegex = new Regex("[a-zA-Z]+ [0-9]+$");


        private static String[] _addressAndNumber;
        private static String _houseNumber;
        private static String _street;


        public EditPatient(Patient p)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Bar.Visibility = Visibility.Collapsed;
            patient = p;
            NotificationPanel.Children.Add(new NotificationView(patient));
            var app = Application.Current as App;
            _patientController = app.UserController;

            LoadPatient();
        }


        private void LoadPatient() 
        {
            lblUsername.Content = patient.Username;
            lblJMBG.Content = patient.PersonalIDnumber;
            txtName.Text = patient.Name;
            txtSurname.Text = patient.Surname;
            txtNumber.Text = patient.Telephone;
            txtEmail.Text = patient.Email;
            txtAddress.Text = patient.Address.Street.Trim() + " " + patient.Address.Number;
            txtCity.Text = patient.Address.City.Name;
            ptxtPassword.Password = patient.Password;
            lblDate.Content = patient.DateOfBirth.Date;
            countryTxt.Text = patient.Address.City.country.Name;
        }

        private void btnSaveClc(object sender, RoutedEventArgs e)
        {

            if (txtName.Text == "" || txtSurname.Text == "" || ptxtPassword.Password == "" ||
                 txtNumber.Text == "" || txtEmail.Text == "" || txtAddress.Text == "" || txtCity.Text == "" || countryTxt.Text == "")
            {
                MessageBox.Show("Warning! Some fields are empty!");
            }
            else if (ptxtPassword.Password.Length < 6)
                MessageBox.Show("Warning! Password must be at least 6 characters long!");
            else if (IsAddressValid(txtAddress.Text))
                MessageBox.Show("Warning! Please enter valid address and house number!");
            else
            {
                patient.Name = txtName.Text;
                patient.Surname = txtSurname.Text;
                patient.Telephone = txtNumber.Text;
                patient.Email = txtEmail.Text;
                patient.Password = ptxtPassword.Password;



                Address address = MakeAdress();
                patient.Address = address;

                _patientController.Set(patient);

                PatientProfile pp = new PatientProfile(patient);
                this.Close();
                pp.Show();
            }

        }


            private Address MakeAdress()
            {
                _addressAndNumber = txtAddress.Text.Split(' ');
                _houseNumber = _addressAndNumber.Last();
                _street = txtAddress.Text.Substring(0, (txtAddress.Text.Length - _houseNumber.Length));
                City city = new City(txtCity.Text, 123456, new Country(countryTxt.Text, ""));

                return new Address(_street, int.Parse(_houseNumber), city);

            }

            private bool IsAddressValid(string input) => !_addressRegex.IsMatch(input);

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
    }
}
