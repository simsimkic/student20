using Controller.HospitalController;
using Model;
using Model.UserModel;
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

namespace ProjekatBolnica.SecretaryView
{
    /// <summary>
    /// Interaction logic for EditAccountPage.xaml
    /// </summary>
    public partial class EditAccountPage : Window
    {
        User secretary = new User();
        public User Secretary { get => secretary; set => secretary = value; }


        private IController<User, long> userController;


        public EditAccountPage(User user)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Secretary = user;  

            //popunjavanje polja
            inputCountry.Text = Secretary.Address.City.country.Name;
            inputCountryCode.Text = Secretary.Address.City.country.Code;
            inputCity.Text = Secretary.Address.City.Name;
            inputCityPostalCode.Text = Secretary.Address.City.PostalCode.ToString();
            inputAdrress.Text = Secretary.Address.Street;
            inputAdrressNumber.Text = Secretary.Address.Number.ToString();
            inputTelephone.Text = Secretary.Telephone;
            inputEmail.Text = Secretary.Email;
            inputUsername.Text = Secretary.Username;
            inputPassword.Password = Secretary.Password;
        }


        private void clickConfirm(object sender, RoutedEventArgs e)
        {
            Regex expression = new Regex(@"[0-9]+");

            if (inputCountry.Text.Equals("") || inputCity.Text.Equals("")
                            || inputAdrress.Text.Equals("") || inputTelephone.Text.Equals("")
                            || inputEmail.Text.Equals("") || inputUsername.Text.Equals("") || inputPassword.Password.Equals(""))
            {
                string message = "Warning! Some fields are empty!";
                System.Windows.MessageBox.Show(message);
                return;
            }
            else if (inputPassword.Password.Length < 6)
            {
                string message = "Warning! The password must be at least 6 characters long!";
                System.Windows.MessageBox.Show(message);
                return;
            }
            else if (!expression.IsMatch(inputAdrressNumber.Text))
            {
                string message = "Warning! Street number is invalid!";
                System.Windows.MessageBox.Show(message);
                return;
            }
            else if (!expression.IsMatch(inputCityPostalCode.Text))
            {
                string message = "Warning! Postal code is invalid!";
                System.Windows.MessageBox.Show(message);
                return;
            }


            //popunjavanje polja ako je sve ok
            String countrySecretary = inputCountry.Text;
            String countryCode = inputCountryCode.Text;
            String city = inputCity.Text;
            int cityPostalCode = int.Parse(inputCityPostalCode.Text);
            String adrress = inputAdrress.Text;
            int addressNumber = int.Parse(inputAdrressNumber.Text);
            String telephone = inputTelephone.Text;
            String email = inputEmail.Text;
            String username = inputUsername.Text;

            secretary.Role = TypeOfUser.Secretary;
            secretary.Telephone = telephone;
            secretary.Email = email;
            secretary.Username = username;
            secretary.Password = inputPassword.Password;
            secretary.Address = new Address(adrress, addressNumber, new City(city, cityPostalCode, new Country(countrySecretary, countryCode)));
            
            var app = Application.Current as App;
            userController = app.UserController;
            userController.Set(secretary);

            this.Close();
        }




        private void clickCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
