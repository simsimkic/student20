using Controller.UserController;
using Model;
using Model.UserModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProjekatBolnica.ManagerView
{
    /// <summary>
    /// Interaction logic for ManagerProfile.xaml
    /// </summary>
    public partial class ManagerProfile : Window
    {
        Manager manager = new Manager();
        public Manager Manager { get => manager; set => manager = value; }

        private UserController userController;
        public static ObservableCollection<Manager> Managers
        {
            get;
            set;
        }

        public ManagerProfile()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var app = Application.Current as App;
            userController = (UserController)app.UserController;

            Managers = new ObservableCollection<Manager>(userController.GetAllManagers());
            Manager = Managers[0];

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

        private void OpenHelp(object sender, RoutedEventArgs e)
        {
            Help help = new Help();
            this.Close();
            help.Show();
        }

        private void OpenListOfHalls(object sender, RoutedEventArgs e)
        {
            ListOfHalls halls = new ListOfHalls();
            this.Close();
            halls.Show();
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
            ManagerMainPage list = new ManagerMainPage();
            this.Close();
            list.Show();
        }

        private void OpenOccupationOfDoctors(object sender, RoutedEventArgs e)
        {
            OccupationOfDoctors occupation = new OccupationOfDoctors();
            this.Close();
            occupation.Show();
        }

        private void ChangeUser(object sender, RoutedEventArgs e)
        {
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

            manager.Name = name;
            manager.Surname = surname;
            manager.Username = username;
            manager.Email = email;
            manager.Password = password;
            manager.Telephone = telephone;
            manager.Address = new Address(adrress, addressNumber, new City(city, cityPostalCode, new Country(country, countryCode)));

            ChangeOfUserAccount user = new ChangeOfUserAccount(this, manager);
            this.Hide();
            user.Show();
        }

        private void OpenActivityStatistics(object sender, RoutedEventArgs e)
        {
            ActivityStatistics statistics = new ActivityStatistics();
            this.Close();
            statistics.Show();
        }

    }
}
