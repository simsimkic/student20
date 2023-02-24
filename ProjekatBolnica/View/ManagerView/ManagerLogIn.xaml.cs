using Controller.UserController;
using Model;
using Model.UserModel;
using System;
using System.Windows;

namespace ProjekatBolnica.ManagerView
{
    /// <summary>
    /// Interaction logic for ManagerLogIn.xaml
    /// </summary>
    public partial class ManagerLogIn : Window
    {
        private UserController userController;

        public ManagerLogIn()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            String username = inputUsername.Text;
            String password = passwordBox.Password;

            var app = Application.Current as App;
            userController = (UserController)app.UserController;

            if (username.Equals("") || password.Equals(""))
            {
                String message = "Warning! You must enter a password and username!";
                MessageBox.Show(message);
                return;
            }
            else if (userController.Login(username, password, false) == null)
            {
                String message = "Warning! Incorrect password or username!";
                MessageBox.Show(message);
                return;
            }
            else if (!(userController.Login(username, password, false).Role.Equals(TypeOfUser.Manager)))
            {
                String message = "Warning! Incorrect user! User must be manager.";
                MessageBox.Show(message);
                return;
            }
            else
            {
                User manager = userController.Login(username, password, false);
                ManagerMainPage mp = new ManagerMainPage();
                this.Close();
                mp.Show();
            }
        }

        private void OpenManagerView(object sender, RoutedEventArgs e)
        {
            ManagerLogIn manager = new ManagerLogIn();
            this.Close();
            manager.Show();
        }

    }
}
