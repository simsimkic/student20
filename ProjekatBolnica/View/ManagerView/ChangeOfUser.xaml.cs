using Controller.HospitalController;
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
    /// Interaction logic for ChangeOfUserAccount.xaml
    /// </summary>
    public partial class ChangeOfUserAccount : Window
    {
        private readonly ManagerMainPage mp = null;
        private readonly ManagerProfile manp = null;
        public Object o = null;
        Manager manager = new Manager();
        public Manager Manager { get => manager; set => manager = value; }

        Doctor doctor = new Doctor();
        public Doctor Doctor { get => doctor; set => doctor = value; }

        Secretary secretary = new Secretary();
        public Secretary Secretary { get => secretary; set => secretary = value; }

        private IController<User, long> userController;

        public ChangeOfUserAccount(ManagerProfile managerPage, Manager mPro)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            manp = managerPage;
            Manager = mPro;
            o = Manager;

            inputName.Text = Manager.Name;
            inputSurname.Text = Manager.Surname;
            inputUsername.Text = Manager.Username;
            inputEmail.Text = Manager.Email;
            inputPassword.Text = Manager.Password;
            inputTelephone.Text = Manager.Telephone;
            inputCountry.Text = Manager.Address.City.country.Name;
            inputCountryCode.Text = Manager.Address.City.country.Code;
            inputCity.Text = Manager.Address.City.Name;
            inputCityPostalCode.Text = Manager.Address.City.PostalCode.ToString();
            inputAdrress.Text = Manager.Address.Street;
            inputAdrressNumber.Text = Manager.Address.Number.ToString();
        }

        public ChangeOfUserAccount(ManagerMainPage managerPage, Doctor doc)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mp = managerPage;
            Doctor = doc;
            o = Doctor;

            inputName.Text = Doctor.Name;
            inputSurname.Text = Doctor.Surname;
            inputUsername.Text = Doctor.Username;
            inputEmail.Text = Doctor.Email;
            inputPassword.Text = Doctor.Password;
            inputTelephone.Text = Doctor.Telephone;
            inputCountry.Text = Doctor.Address.City.country.Name;
            inputCountryCode.Text = Doctor.Address.City.country.Code;
            inputCity.Text = Doctor.Address.City.Name;
            inputCityPostalCode.Text = Doctor.Address.City.PostalCode.ToString();
            inputAdrress.Text = Doctor.Address.Street;
            inputAdrressNumber.Text = Doctor.Address.Number.ToString();
        }

        public ChangeOfUserAccount(ManagerMainPage managerPage, Secretary sec)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mp = managerPage;
            Secretary = sec;
            o = Secretary;

            inputName.Text = Secretary.Name;
            inputSurname.Text = Secretary.Surname;
            inputUsername.Text = Secretary.Username;
            inputEmail.Text = Secretary.Email;
            inputPassword.Text = Secretary.Password;
            inputTelephone.Text = Secretary.Telephone;
            inputCountry.Text = Secretary.Address.City.country.Name;
            inputCountryCode.Text = Secretary.Address.City.country.Code;
            inputCity.Text = Secretary.Address.City.Name;
            inputCityPostalCode.Text = Secretary.Address.City.PostalCode.ToString();
            inputAdrress.Text = Secretary.Address.Street;
            inputAdrressNumber.Text = Secretary.Address.Number.ToString();
        }

        public ChangeOfUserAccount()
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

        private void OpenResources(object sender, RoutedEventArgs e)
        {
            Resources resources = new Resources();
            this.Close();
            resources.Show();
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
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

        private void OpenWorkHoursForDoctors(object sender, RoutedEventArgs e)
        {
            WorkHoursForDoctors hours = new WorkHoursForDoctors();
            this.Close();
            hours.Show();
        }

        private void OpenManagerView(object sender, RoutedEventArgs e)
        {
            ManagerLogIn manager = new ManagerLogIn();
            this.Close();
            manager.Show();
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

            if (mp != null)
            {
                mp.Show();
            }

            if (manp != null)
            {
                manp.Show();
            }
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
        private void ChangeUser(object sender, RoutedEventArgs e)
        {
            ChangeOfUserAccount user = new ChangeOfUserAccount();
            this.Close();
            user.Show();
        }

        private void ClickChangeOfUser(object sender, RoutedEventArgs e)
        {
            Regex expression = new Regex(@"[0-9]+");

            if (inputName.Text.Equals("") || inputSurname.Text.Equals("") || inputCountry.Text.Equals("") || inputCity.Text.Equals("")
               || inputAdrress.Text.Equals("") || inputTelephone.Text.Equals("")
               || inputEmail.Text.Equals("") || inputUsername.Text.Equals("") || inputPassword.Text.Equals("")
               || inputCountryCode.Text.Equals("") || inputCityPostalCode.Text.Equals("") || inputAdrressNumber.Text.Equals(""))
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
            else if (inputTelephone.Text.Length < 10)
            {
                String message = "Warning! Telephone number doesn't have enough numbers!";
                MessageBox.Show(message);
                return;
            }

            String name = inputName.Text;
            String surname = inputSurname.Text;
            String username = inputUsername.Text;
            String email = inputEmail.Text;
            String password = inputPassword.Text;
            String telephone = inputTelephone.Text;
            String country = inputCountry.Text;
            String countryCode = inputCountryCode.Text;
            String city = inputCity.Text;
            int cityPostalCode = int.Parse(inputCityPostalCode.Text);
            String adrress = inputAdrress.Text;
            int addressNumber = int.Parse(inputAdrressNumber.Text);

            var app = Application.Current as App;
            userController = (UserController)app.UserController;

            if (o.Equals(Doctor))
            {
                doctor.Name = name;
                doctor.Surname = surname;
                doctor.Username = username;
                doctor.Email = email;
                doctor.Password = password;
                doctor.Telephone = telephone;
                doctor.Address = new Address(adrress, addressNumber, new City(city, cityPostalCode, new Country(country, countryCode)));

                userController.Set(doctor);
                this.Close();
                if (mp != null)
                    mp.Show();
            }
            else if (o.Equals(Secretary))
            {
                secretary.Name = name;
                secretary.Surname = surname;
                secretary.Username = username;
                secretary.Email = email;
                secretary.Password = password;
                secretary.Telephone = telephone;
                secretary.Address = new Address(adrress, addressNumber, new City(city, cityPostalCode, new Country(country, countryCode)));

                userController.Set(secretary);
                this.Close();
                if (mp != null)
                    mp.Show();
            }
            else if (o.Equals(Manager))
            {
                manager.Name = name;
                manager.Surname = surname;
                manager.Username = username;
                manager.Email = email;
                manager.Password = password;
                manager.Telephone = telephone;
                manager.Address = new Address(adrress, addressNumber, new City(city, cityPostalCode, new Country(country, countryCode)));

                userController.Set(manager);
                this.Close();
                manp.Show();
            }
        }
    }
}
