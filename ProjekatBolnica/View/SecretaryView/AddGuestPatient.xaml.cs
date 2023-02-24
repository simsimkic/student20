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

namespace ProjekatBolnica.View.SecretaryView
{
    /// <summary>
    /// Interaction logic for AddGuestPatient.xaml
    /// </summary>
    public partial class AddGuestPatient : Window
    {
        private UserController userController;

        public AddGuestPatient()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }


        //ADD guest PATIENT
        private void clickConfirmPatient(object sender, RoutedEventArgs e)
        {
            Regex expression = new Regex(@"[0-9]+");

            if (inputName.Text.Equals("") || inputSurname.Text.Equals("") || inputCountry.Text.Equals("") || inputCity.Text.Equals("")
                || inputAdrress.Text.Equals("") || inputPersonalNumber.Text.Equals("") || inputTelephone.Text.Equals("")
                || inputEmail.Text.Equals("")
                || inputCountryCode.Text.Equals("") || inputCityPostalCode.Text.Equals("") || inputAdrressNumber.Text.Equals("")
                || inputDateOfBirth.SelectedDate == null)
            {
                string message = "Warning! Some fields are empty!";
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
            DateTime dateOfBirthPatient = (DateTime)inputDateOfBirth.SelectedDate;
            bool genderFemale = (bool)female.IsChecked; Gender genderPatient = whichGender(genderFemale);


            Patient newPatient = new Patient
            {
                Active = false,
                Name = namePatient,
                Surname = surnamePatient,
                Gender = genderPatient,
                Role = TypeOfUser.Patient,
                //Password = "-1",
                PersonalIDnumber = personalNumberPatient,
                Telephone = telephonePatient,
                Email = emailPatient,
                //Username = "-1",
                IsGuest = true,
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
