using Controller.UserController;
using Model;
using Model.UserModel;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ProjekatBolnica.PatientView
{
    /// <summary>
    /// Interaction logic for PatientRegistration.xaml
    /// </summary>
    public partial class PatientRegistration : Window
    {

        private readonly IUserController _patientController;
        private static readonly Regex _jmbgRegex = new Regex("[0-9]{13}");
        private static readonly Regex _addressRegex = new Regex("[a-zA-Z]+ [0-9]+$");

        private static String[] _addressAndNumber;
        private static String _houseNumber;
        private static String _street;




        public PatientRegistration()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Male.IsChecked = true;
            var app = Application.Current as App;

            _patientController = app.UserController;


        }



        private void btnSignUp(object sender, RoutedEventArgs e)
        {
            if (firstNameTxt.Text == "" || surnameTxt.Text == "" || usernameTxt.Text == "" || passBox.Password == "" ||
                jmbgTxt.Text == "" || countryTxt.Text == "" || dateBirthPicker.SelectedDate == null || phoneTxt.Text == "" || emailTxt.Text == "" || adresseTxt.Text == "" || cityTxt.Text == "")
                MessageBox.Show("Warning! Some fields are empty!");
            else if(IsUsernameAvailable())
                MessageBox.Show("Warning! Username already in use!");
            else if (passBox.Password.Length < 6)
                MessageBox.Show("Warning! Password must be at least 6 characters long!");
            else if (IsJMBGValid())
                MessageBox.Show("Warning! JMBG must be 13 digits long (numbers 0-9)! ");
            else if (IsEmailValid())
                MessageBox.Show("Warning! JMBG must be 13 digits long (numbers 0-9)! ");
            else if (IsAddressValid(adresseTxt.Text))
                MessageBox.Show("Warning! Please enter valid address and house number!");
            else
            {
                _patientController.New(CreatePatient());
                PatientLogin login = new PatientLogin();
                this.Close();
                login.Show();
            }

        }



        private Patient CreatePatient() =>
            new Patient(usernameTxt.Text.Trim(),
                true,
                passBox.Password.Trim(),
                firstNameTxt.Text.Trim(),
                surnameTxt.Text.Trim(),
                emailTxt.Text.Trim(),
                jmbgTxt.Text.Trim(),
                phoneTxt.Text.Trim(),
                WhichGender(),
                dateBirthPicker.SelectedDate.Value,
                MakeAdress(),
                null,
                "",
                false
                );


        private bool IsUsernameAvailable() => _patientController.GetAll().Where(user => user.Username.Trim().Equals(usernameTxt.Text.Trim())) != null;
        private bool IsEmailValid()=> Regex.IsMatch(emailTxt.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

        private Gender WhichGender()
        {
            bool genderOption = (bool)Female.IsChecked;
            if (genderOption)
            {
                return Gender.Female;
            }
            else
            {
                return Gender.Male;
            }
        }

        private Address MakeAdress()
        {
            _addressAndNumber = adresseTxt.Text.Split(' ');
            _houseNumber = _addressAndNumber.Last();
            _street = adresseTxt.Text.Substring(0, (adresseTxt.Text.Length - _houseNumber.Length));
            City city = new City(cityTxt.Text.Trim(), 123456, new Country(countryTxt.Text.Trim(), ""));

            return new Address(_street.Trim(), int.Parse(_houseNumber.Trim()), city);

        }

        private bool IsJMBGValid() => !_jmbgRegex.IsMatch(jmbgTxt.Text.Trim());
        private bool IsAddressValid(string input) => !_addressRegex.IsMatch(input);

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = "";
        }


    }
}
