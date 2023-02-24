using Controller.HospitalController;
using Controller.UserController;
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
    /// Interaction logic for EditPatient.xaml
    /// </summary>
    public partial class EditPatient : Window
    {
        private UserController userController;

        private Patient tempPatient;

        public Patient TempPatient { get => tempPatient; set => tempPatient = value; }

        public EditPatient(Patient p)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            TempPatient = p;

            //popunjavanje polja
            inputCountry.Text = p.Address.City.country.Name;
            inputCountryCode.Text = p.Address.City.country.Code;
            inputCity.Text = p.Address.City.Name;
            inputCityPostalCode.Text = p.Address.City.PostalCode.ToString();
            inputAdrress.Text = p.Address.Street;
            inputAdrressNumber.Text = p.Address.Number.ToString();
            inputTelephone.Text = p.Telephone;
            inputEmail.Text = p.Email;

            if (p.IsGuest)
            {
                inputUsername.Text = "";
                inputPasswordPatient.Password = "";
            }
            else
            {
                inputUsername.Text = p.Username;
                inputPasswordPatient.Password = p.Password;
            }

        }


        private void clickConfirmPatient(object sender, RoutedEventArgs e)
        {
            Regex expression = new Regex(@"[0-9]+");

            if (inputCountry.Text.Equals("") || inputCity.Text.Equals("")
                || inputAdrress.Text.Equals("") || inputTelephone.Text.Equals("")
                || inputEmail.Text.Equals("") || inputUsername.Text.Equals("") || inputPasswordPatient.Password.Equals("")
                || inputCountryCode.Text.Equals("") || inputCityPostalCode.Text.Equals("") || inputAdrressNumber.Text.Equals(""))
            {
                string message = "Warning! Some fields are empty!";
                System.Windows.MessageBox.Show(message);
                return;
            }
            else if (inputPasswordPatient.Password.Length < 6)
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


            String countryPatient = inputCountry.Text;
            String countryCodePatient = inputCountryCode.Text;
            String cityPatient = inputCity.Text;
            int cityPostalCodePatient = int.Parse(inputCityPostalCode.Text);
            String adrressPatient = inputAdrress.Text;
            int addressNumberPatient = int.Parse(inputAdrressNumber.Text);
            String telephonePatient = inputTelephone.Text;
            String emailPatient = inputEmail.Text;
            String usernamePatient = inputUsername.Text;
            String passwordPatient = inputPasswordPatient.Password;

            var app = Application.Current as App;
            userController = (UserController)app.UserController;

            tempPatient.IsGuest = false;
            tempPatient.Password = passwordPatient;
            tempPatient.Telephone = telephonePatient;
            tempPatient.Email = emailPatient;
            tempPatient.Username = usernamePatient;
            tempPatient.Address = new Address(adrressPatient, addressNumberPatient, new City(cityPatient, cityPostalCodePatient, new Country(countryPatient, countryCodePatient)));

            userController.Set(tempPatient);

            this.Close();
        }



        private void clickCancelPatient(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
