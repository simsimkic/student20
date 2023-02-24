using Controller.PatientController;
using Controller.UserController;
using Model;
using Model.UserModel;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace ProjekatBolnica.PatientView
{
    public partial class PatientLogin : Window
    {


        private String _username;
        private String _password;
        private readonly IUserController _patientController;

        public PatientLogin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            DataContext = this;
            _username = "Username";

            var app = Application.Current as App;
            _patientController = app.UserController;
        }

        private void OpenAppointment(object sender, RoutedEventArgs e)
        {
            if (usernameTxt.Text == "" || txtPassword.Password == "")
            {
                MessageBox.Show("Please fill in all boxes");
            }
            else
            {
                _password = txtPassword.Password;
                Patient p = (Patient)_patientController.Login(_username, _password, true);

                if (p != null && p.Role == TypeOfUser.Patient)
                {
                    MakeAnAppointment appointment = new MakeAnAppointment(p);
                    appointment.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username and password don't match");
                }
            }

        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Password = "";
        }

        private void LblSignUpClc(object sender, RoutedEventArgs e)
        {

            PatientRegistration pr = new PatientRegistration();
            pr.Show();
            this.Close();
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = "";
        }




        public String Username
        {
            get { return _username; }
            set
            {
                if (
                   _username != value)
                    _username = value;
                OnPropertyChanged();
            }
        }






        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
