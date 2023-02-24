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
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        private UserController userController;

        public AddPatient()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

     

        //ADD PATIENT
        private void clickConfirmPatient(object sender, RoutedEventArgs e)
        {
            Regex expression = new Regex(@"[0-9]+");

            if (inputName.Text.Equals("") || inputSurname.Text.Equals("") || inputCountry.Text.Equals("") || inputCity.Text.Equals("")
                || inputAdrress.Text.Equals("") || inputPersonalNumber.Text.Equals("") || inputTelephone.Text.Equals("")
                || inputEmail.Text.Equals("") || inputUsername.Text.Equals("") || inputPasswordPatient.Password.Equals("")
                || inputCountryCode.Text.Equals("") || inputCityPostalCode.Text.Equals("") || inputAdrressNumber.Text.Equals("")
                || inputDateOfBirth.SelectedDate == null)
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
            else if (inputPersonalNumber.Text.Length != 13)
            {
                string message = "Warning! The personal number must have 13 characters!";
                System.Windows.MessageBox.Show(message);
                return;
            }
            else if (!expression.IsMatch(inputPersonalNumber.Text))
            {
                string message = "Warning! The personal number must have 13 characters (digit)!";
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


            String namePatient = inputName.Text;
            String surnamePatient = inputSurname.Text;
            String countryPatient = inputCountry.Text;
            String countryCodePatient = inputCountryCode.Text;
            String cityPatient = inputCity.Text;
            int cityPostalCodePatient = int.Parse(inputCityPostalCode.Text);
            String adrressPatient = inputAdrress.Text;
            int addressNumberPatient = int.Parse(inputAdrressNumber.Text);
            String personalNumberPatient = inputPersonalNumber.Text;
            String telephonePatient = inputTelephone.Text;
            String emailPatient = inputEmail.Text;
            String usernamePatient = inputUsername.Text;
            String passwordPatient = inputPasswordPatient.Password;
            DateTime dateOfBirthPatient = (DateTime)inputDateOfBirth.SelectedDate;
            bool genderFemale = (bool)female.IsChecked; Gender genderPatient = whichGender(genderFemale);


            Patient newPatient = new Patient
            {
                Active = false,
                Name = namePatient,
                Surname = surnamePatient,
                Gender = genderPatient,
                Role = TypeOfUser.Patient,
                Password = passwordPatient,
                PersonalIDnumber = personalNumberPatient,
                Telephone = telephonePatient,
                Email = emailPatient,
                Username = usernamePatient,
                IsGuest = false,
                DateOfBirth = dateOfBirthPatient,
                Address = new Address(adrressPatient, addressNumberPatient, new City(cityPatient, cityPostalCodePatient, new Country(countryPatient, countryCodePatient)))
            };


            var app = Application.Current as App;
            userController = (UserController)app.UserController;
            userController.New(newPatient);

            this.Close();
        }


        private void clickCancelPatient(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        //DODATNE METODE:
        private Gender whichGender(bool genderFemale)
        {
            if (genderFemale) { return Model.UserModel.Gender.Female; }
            else { return Model.UserModel.Gender.Male; }
        }



    }
}
