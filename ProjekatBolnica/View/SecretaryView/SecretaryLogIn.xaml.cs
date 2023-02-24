using Controller.HospitalController;
using Controller.ManagerController;
using Controller.UserController;
using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.UserModel;
using System;
using System.Collections.Generic;
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

namespace ProjekatBolnica.SecretaryView
{
    /// <summary>
    /// Interaction logic for SecretaryLogIn.xaml
    /// </summary>
    public partial class SecretaryLogIn : Window
    {
        private UserController userController;
        private AppointmentController appointmentController;

        public SecretaryLogIn()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.MaxHeight = 800;
            this.MinHeight = 800;
            this.MaxWidth = 1280;
            this.MinWidth = 1280;

        }
        
        private void login(object sender, RoutedEventArgs e)
        {
            String pw = passwordSecretary.Password;
            String un = usernameSecretary.Text;

            var app = Application.Current as App;
            userController = (UserController)app.UserController;

            if (pw.Equals("") || un.Equals(""))
            {
                string message = "Warning! You must enter a password and username!";
                System.Windows.MessageBox.Show(message);
            }
            else if (userController.Login(un, pw, false) == null) 
            {
                string message = "Warning! Incorrect password or username!";
                System.Windows.MessageBox.Show(message);
            }
            else if (!(userController.Login(un, pw, false).Role.Equals(TypeOfUser.Secretary)))
            {
                string message = "Warning! Incorrect user! User must be Secretary";
                System.Windows.MessageBox.Show(message);
            }
            else
            {
                User secretary = userController.Login(un, pw, false);
                SecretaryMainPage mainPage = new SecretaryMainPage(secretary);
                this.Close();
                mainPage.Show();
            }
        }



    }

}
