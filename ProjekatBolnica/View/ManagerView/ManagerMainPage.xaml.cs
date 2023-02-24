using Controller.UserController;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace ProjekatBolnica.ManagerView
{
    /// <summary>
    /// Interaction logic for ManagerMainPage.xaml
    /// </summary>
    public partial class ManagerMainPage : Window
    {
        private ICollectionView defaultView;
        public int nbrDoctors = 0;

        private readonly UserController userController;

        public static ObservableCollection<Doctor> Doctors
        {
            get;
            set;
        }

        public static ObservableCollection<Doctor> Specialists
        {
            get;
            set;
        }

        public static ObservableCollection<Secretary> Secretaries
        {
            get;
            set;
        }

        public ManagerMainPage()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            var app = Application.Current as App;
            userController = (UserController)app.UserController;

            Doctors = new ObservableCollection<Doctor>(userController.GetAllDoctors());
            Specialists = new ObservableCollection<Doctor>(userController.GetAllSpecialists());
            Secretaries = new ObservableCollection<Secretary>(userController.GetAllSecretaries());

            nbrDoctors = Doctors.Count + Specialists.Count + Secretaries.Count + 1;
            this.defaultView = CollectionViewSource.GetDefaultView(Doctors);
            this.defaultView.Filter =
                d => (IsTrueContent((Doctor)d));

            dataGridDoctors.ItemsSource = this.defaultView;
        }

        private bool IsTrueContent(Doctor d)
        {
            if (d.Name.Contains(inputSearch.Text) || d.Surname.Contains(inputSearch.Text) || d.Username.Contains(inputSearch.Text) ||
            d.Email.Contains(inputSearch.Text) || d.Password.Contains(inputSearch.Text) ||
            d.PersonalIDnumber.Contains(inputSearch.Text) || d.Telephone.Contains(inputSearch.Text) || d.Address.City.Name.Contains(inputSearch.Text))

            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void RefreshPageWithTextBox(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Doctors = new ObservableCollection<Doctor>(Doctors);

            // ZA FILTER:
            this.defaultView = CollectionViewSource.GetDefaultView(Doctors);
            this.defaultView.Filter =
               d => (IsTrueContent((Doctor)d));

            dataGridDoctors.ItemsSource = this.defaultView;
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

        public void OpenKeyboardShortcuts(object sender, RoutedEventArgs e)
        {
            KeyboardShortcuts ks = new KeyboardShortcuts();
            this.Close();
            ks.Show();
        }

        private void OpenOccupationOfDoctors(object sender, RoutedEventArgs e)
        {
            OccupationOfDoctors occupation = new OccupationOfDoctors();
            this.Close();
            occupation.Show();
        }

        private void RegisterUser(object sender, RoutedEventArgs e)
        {
            RegistrationOfUser user = new RegistrationOfUser(this, ++nbrDoctors);
            this.Hide();
            user.Show();
        }

        private void ChangeUser(object sender, RoutedEventArgs e)
        {
            Doctor d = (Doctor)dataGridDoctors.SelectedItem;
            Doctor spec = (Doctor)dataGridSpecialists.SelectedItem;
            Secretary sec = (Secretary)dataGridSecretaries.SelectedItem;
            if (d != null && sec == null && spec == null)
            {
                ChangeOfUserAccount user = new ChangeOfUserAccount(this, d);
                this.Hide();
                user.Show();
            }
            else if (sec != null && d == null && spec == null)
            {
                ChangeOfUserAccount user = new ChangeOfUserAccount(this, sec);
                this.Hide();
                user.Show();
            }
            else if (spec != null && d == null && sec == null)
            {
                ChangeOfUserAccount user = new ChangeOfUserAccount(this, spec);
                this.Hide();
                user.Show();
            }
        }

        private void RemoveUser(object sender, RoutedEventArgs e)
        {
            Doctor d = (Doctor)dataGridDoctors.SelectedItem;
            Doctor spec = (Doctor)dataGridSpecialists.SelectedItem;
            Secretary s = (Secretary)dataGridSecretaries.SelectedItem;
            if (d != null && s == null && spec == null)
            {
                RemovingOfUserAccount user = new RemovingOfUserAccount(this, d);
                this.Hide();
                user.Show();
            }
            else if (s != null && d == null && spec == null)
            {
                RemovingOfUserAccount user = new RemovingOfUserAccount(this, s);
                this.Hide();
                user.Show();
            }
            else if (spec != null && d == null && s == null)
            {
                RemovingOfUserAccount user = new RemovingOfUserAccount(this, spec);
                this.Hide();
                user.Show();
            }
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

        private void OpenActivityStatistics(object sender, RoutedEventArgs e)
        {
            ActivityStatistics statistics = new ActivityStatistics();
            this.Close();
            statistics.Show();
        }

        private void OpenListOfHalls(object sender, RoutedEventArgs e)
        {
            ListOfHalls halls = new ListOfHalls();
            this.Close();
            halls.Show();
        }

    }
}
