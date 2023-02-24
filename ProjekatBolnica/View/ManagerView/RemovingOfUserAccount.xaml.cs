using Controller.UserController;
using Model;
using System.Windows;
using System.Windows.Controls;

namespace ProjekatBolnica.ManagerView
{
    /// <summary>
    /// Interaction logic for RemovingOfUserAccount.xaml
    /// </summary>
    public partial class RemovingOfUserAccount : Window
    {
        private ManagerMainPage mp = null;
        private Doctor oldDoctor = null;
        private Secretary oldSecretary = null;
        private UserController userController;

        public RemovingOfUserAccount(ManagerMainPage managerPage, Doctor d)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mp = managerPage;
            oldDoctor = d;

            inputName.Text = oldDoctor.Name;
            inputSurname.Text = oldDoctor.Surname;
            inputUsername.Text = oldDoctor.Username;
            inputEmail.Text = oldDoctor.Email;
            inputPassword.Text = oldDoctor.Password;
            inputPersonalIdNumber.Text = oldDoctor.PersonalIDnumber;
            inputDateOfBirth.SelectedDate = oldDoctor.DateOfBirth;
            inputTelephone.Text = oldDoctor.Telephone;
            inputCountry.Text = oldDoctor.Address.City.country.Name;
            inputCountryCode.Text = oldDoctor.Address.City.country.Code;
            inputCity.Text = oldDoctor.Address.City.Name;
            inputCityPostalCode.Text = oldDoctor.Address.City.PostalCode.ToString();
            inputAdrress.Text = oldDoctor.Address.Street;
            inputAdrressNumber.Text = oldDoctor.Address.Number.ToString();
        }

        public RemovingOfUserAccount(ManagerMainPage managerPage, Secretary s)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mp = managerPage;
            oldSecretary = s;

            inputName.Text = oldSecretary.Name;
            inputSurname.Text = oldSecretary.Surname;
            inputUsername.Text = oldSecretary.Username;
            inputEmail.Text = oldSecretary.Email;
            inputPassword.Text = oldSecretary.Password;
            inputPersonalIdNumber.Text = oldSecretary.PersonalIDnumber;
            inputDateOfBirth.SelectedDate = oldSecretary.DateOfBirth;
            inputTelephone.Text = oldSecretary.Telephone;
            inputCountry.Text = oldSecretary.Address.City.country.Name;
            inputCountryCode.Text = oldSecretary.Address.City.country.Code;
            inputCity.Text = oldSecretary.Address.City.Name;
            inputCityPostalCode.Text = oldSecretary.Address.City.PostalCode.ToString();
            inputAdrress.Text = oldSecretary.Address.Street;
            inputAdrressNumber.Text = oldSecretary.Address.Number.ToString();
        }

        public RemovingOfUserAccount()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
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

        private void RemoveUser(object sender, RoutedEventArgs e)
        {
            RemovingOfUserAccount user = new RemovingOfUserAccount();
            this.Close();
            user.Show();
        }

        private void clickRemoveUser(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            userController = (UserController)app.UserController;

            if (oldDoctor != null)
            {
                userController.Delete(oldDoctor);
            }
            else if (oldSecretary != null)
            {
                userController.Delete(oldSecretary);
            }

            this.Close();
            mp.Show();
        }
    }
}
