using Controller.UserController;
using Model;
using Model.UserModel;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ProjekatBolnica.ManagerView
{
    /// <summary>
    /// Interaction logic for RegistrationOfUsers.xaml
    /// </summary>
    public partial class RegistrationOfUser : Window
    {
        private int ID = 0;
        private ManagerMainPage mp = null;

        private UserController userController;

        public RegistrationOfUser(ManagerMainPage managerPage, int count)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mp = managerPage;
            ID = count;
        }

        private void clickRegisterUser(object sender, RoutedEventArgs e)
        {
            Regex expression = new Regex(@"[0-9]+");

            if (inputName.Text.Equals("") || inputSurname.Text.Equals("") || inputCountry.Text.Equals("") || inputCity.Text.Equals("")
                || inputAdrress.Text.Equals("") || inputPersonalIdNumber.Text.Equals("") || inputTelephone.Text.Equals("")
                || inputEmail.Text.Equals("") || inputUsername.Text.Equals("") || inputPassword.Text.Equals("")
                || inputCountryCode.Text.Equals("") || inputCityPostalCode.Text.Equals("") || inputAdrressNumber.Text.Equals("")
                || inputDateOfBirth.SelectedDate == null)
            {
                String message = "Warning! Some fields are empty!";
                MessageBox.Show(message);
                return;
            }
            else if (inputPassword.Text.Length < 6)
            {
                String message = "Warning! The password must be at least 6 characters long!";
                MessageBox.Show(message);
                return;
            }
            else if (inputPersonalIdNumber.Text.Length != 13)
            {
                String message = "Warning! The personal number must have 13 characters!";
                MessageBox.Show(message);
                return;
            }
            else if (!expression.IsMatch(inputPersonalIdNumber.Text))
            {
                String message = "Warning! The personal number must have 13 characters (digit)!";
                MessageBox.Show(message);
                return;
            }
            else if (isPasswordExistant(inputPassword.Text))
            {
                String message = "Warning! Password already exists!";
                MessageBox.Show(message);
                return;
            }
            else if (isUsernameExistant(inputUsername.Text))
            {
                String message = "Warning! Username already exists!";
                MessageBox.Show(message);
                return;
            }
            else if (isPersonalIdNumberExistant(inputPersonalIdNumber.Text))
            {
                String message = "Warning! Personal number already exists!";
                MessageBox.Show(message);
                return;
            }
            else if (inputTelephone.Text.Length < 10)
            {
                String message = "Warning! Telephone number doesn't have enough numbers!";
                MessageBox.Show(message);
                return;
            }

            String country = inputCountry.Text;
            String countryCode = inputCountryCode.Text;
            String city = inputCity.Text;
            int cityPostalCode = int.Parse(inputCityPostalCode.Text);
            String adrress = inputAdrress.Text;
            int addressNumber = int.Parse(inputAdrressNumber.Text);
            String name = inputName.Text;
            String surname = inputSurname.Text;
            String username = inputUsername.Text;
            String email = inputEmail.Text;
            String password = inputPassword.Text;
            String personalIdNumber = inputPersonalIdNumber.Text;
            DateTime dateOfBirth = (DateTime)inputDateOfBirth.SelectedDate;
            String telephone = inputTelephone.Text;
            bool genderFemale = (bool)Female.IsChecked;
            Gender gender = whichGender(genderFemale);
            TypeOfUser t = TypeOfUser.Doctor;
            if ((bool)(checkBoxType.IsChecked))
            {
                t = TypeOfUser.Doctor;
            }

            Doctor doctor = new Doctor
            {
                Id = ID,
                Name = name,
                Surname = surname,
                Username = username,
                Email = email,
                Password = password,
                PersonalIDnumber = personalIdNumber,
                DateOfBirth = dateOfBirth,
                Telephone = telephone,
                Address = new Address(adrress, addressNumber, new City(city, cityPostalCode, new Country(country, countryCode))),
                Gender = gender,
                Role = t
            };

            var app = Application.Current as App;
            userController = (UserController)app.UserController;
            userController.New(doctor);

            this.Close();
            mp.Show();
        }

        private Gender whichGender(bool genderFemale)
        {
            if (genderFemale) { return Gender.Female; }
            else { return Gender.Male; }
        }

        private Boolean isPasswordExistant(String password)
        {
            foreach (Doctor doctor in ManagerMainPage.Doctors)
            {
                if (doctor.Password.Equals(password)) { return true; }
            }
            return false;
        }

        private Boolean isUsernameExistant(String username)
        {
            foreach (Doctor doctor in ManagerMainPage.Doctors)
            {
                if (doctor.Username.Equals(username)) { return true; }
            }
            return false;
        }

        private Boolean isPersonalIdNumberExistant(String pid)
        {
            foreach (Doctor doctor in ManagerMainPage.Doctors)
            {
                if (doctor.PersonalIDnumber.Equals(pid)) { return true; }
            }
            return false;
        }

        public RegistrationOfUser()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void OpenHelp(object sender, RoutedEventArgs e)
        {
            Help help = new Help();
            this.Close();
            help.Show();
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
        }

        private void HandleCheckType(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
        }

        private void OpenResources(object sender, RoutedEventArgs e)
        {
            Resources resources = new Resources();
            this.Close();
            resources.Show();
        }

        private void OpenAddingANewMedicine(object sender, RoutedEventArgs e)
        {
            AddingANewMedicine medicine = new AddingANewMedicine();
            this.Close();
            medicine.Show();
        }

        private void OpenChoosingADoctorForOvertimeWork(object sender, RoutedEventArgs e)
        {
            ChoosingADoctorForOvertimeWork overtime = new ChoosingADoctorForOvertimeWork();
            this.Close();
            overtime.Show();
        }

        private void OpenManagerView(object sender, RoutedEventArgs e)
        {
            ManagerLogIn manager = new ManagerLogIn();
            this.Close();
            manager.Show();
        }

        private void OpenWorkHoursForDoctors(object sender, RoutedEventArgs e)
        {
            WorkHoursForDoctors hours = new WorkHoursForDoctors();
            this.Close();
            hours.Show();
        }

        private void OpenListOfHalls(object sender, RoutedEventArgs e)
        {
            ListOfHalls halls = new ListOfHalls();
            this.Close();
            halls.Show();
        }

        public void OpenKeyboardShortcuts(object sender, RoutedEventArgs e)
        {
            KeyboardShortcuts ks = new KeyboardShortcuts();
            this.Close();
            ks.Show();
        }

        private void OpenManagerProfile(object sender, RoutedEventArgs e)
        {
            ManagerProfile profile = new ManagerProfile();
            this.Close();
            profile.Show();
        }

        private void OpenNotifications(object sender, RoutedEventArgs e)
        {
            Notifications notifications = new Notifications();
            this.Close();
            notifications.Show();
        }
        private void OpenListOfEmployees(object sender, RoutedEventArgs e)
        {
            this.Close();
            mp.Show();
        }

        private void OpenOccupationOfDoctors(object sender, RoutedEventArgs e)
        {
            OccupationOfDoctors occupation = new OccupationOfDoctors();
            this.Close();
            occupation.Show();
        }

        private void OpenActivityStatistics(object sender, RoutedEventArgs e)
        {
            ActivityStatistics statistics = new ActivityStatistics();
            this.Close();
            statistics.Show();
        }

        private void RegisterUser(object sender, RoutedEventArgs e)
        {
            RegistrationOfUser user = new RegistrationOfUser();
            this.Close();
            user.Show();
        }
    }
}
