using Controller.ManagerController;
using Controller.UserController;
using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Controller.UserController;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProjekatBolnica.ManagerView
{
    /// <summary>
    /// Interaction logic for ChoosingADoctorForOvertimeWork.xaml
    /// </summary>
    public partial class ChoosingADoctorForOvertimeWork : Window
    {
        private readonly UserController userController;
        private readonly WorkHoursForDoctorController workHoursForDoctorController;
        private readonly NotificationController notificationController;

        Manager manager = new Manager();
        public Manager Manager { get => manager; set => manager = value; }

        public static ObservableCollection<Manager> Managers
        {
            get;
            set;
        }

        public static ObservableCollection<Notification> AllNotifications
        {
            get;
            set;
        }

        public static ObservableCollection<Doctor> Doctors
        {
            get;
            set;
        }

        public static List<double> Procents
        {
            get;
            set;
        }

        public static ObservableCollection<WorkHoursForDoctor> AllWorkHours
        {
            get;
            set;
        }

        public ChoosingADoctorForOvertimeWork()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            var app = Application.Current as App;
            userController = (UserController)app.UserController;
            Doctors = new ObservableCollection<Doctor>(userController.GetAllDoctors());
            workHoursForDoctorController = (WorkHoursForDoctorController)app.WorkHoursForDoctorController;
            AllWorkHours = new ObservableCollection<WorkHoursForDoctor>(workHoursForDoctorController.GetAll());
            notificationController = (NotificationController)app.NotificationController;
            AllNotifications = new ObservableCollection<Notification>(notificationController.GetAll());
            Managers = new ObservableCollection<Manager>(userController.GetAllManagers());
            Manager = Managers[0];

            List<int> annual = new List<int>();
            List<int> weekly = new List<int>();
            for (int i = 0; i < AllWorkHours.Count; i++)
            {
                annual.Add(AllWorkHours[i].OvertimeAnnualWork);
                weekly.Add(AllWorkHours[i].OvertimeWeeklyWork);
            }
            Procents = new List<double>(workHoursForDoctorController.CalculateOvertimeWorkInPercantage(annual, weekly));
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
        private void OpenActivityStatistics(object sender, RoutedEventArgs e)
        {
            ActivityStatistics statistics = new ActivityStatistics();
            this.Close();
            statistics.Show();
        }

        private void OpenChoosingADoctorForOvertimeWork(object sender, RoutedEventArgs e)
        {
            ChoosingADoctorForOvertimeWork overtime = new ChoosingADoctorForOvertimeWork();
            this.Close();
            overtime.Show();
        }

        private void ClickInform(object sender, RoutedEventArgs e)
        {
            if (inputNewFrom.Value == null || inputNewTo.Value == null)
            {
                String message = "Warning! Yoo need to insert time!";
                MessageBox.Show(message);
                return;
            }

            if (comboBox.SelectedItem == null)
            {
                String message = "Warning! Yoo need to pick a doctor!";
                MessageBox.Show(message);
                return;
            }

            String desc = "You are selected for addional work that";
            desc += " starts at " + inputNewFrom.Value.ToString() + " and ends at " + inputNewTo.Value.ToString();

            Notification notification = new Notification
            {
                Sender = Manager,
                Receiver = (Doctor)comboBox.SelectedItem,
                Date = DateTime.Now,
                Description = desc
            };

            notificationController.New(notification);
        }
    }
}
