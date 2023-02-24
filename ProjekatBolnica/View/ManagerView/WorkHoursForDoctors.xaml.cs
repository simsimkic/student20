using Controller.ManagerController;
using Controller.UserController;
using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;

namespace ProjekatBolnica.ManagerView
{
    /// <summary>
    /// Interaction logic for WorkHoursForDoctors.xaml
    /// </summary>
    public partial class WorkHoursForDoctors : Window
    {
        private readonly WorkHoursForDoctorController workHoursForDoctorController;
        private readonly UserController userController;

        public static ObservableCollection<WorkHoursForDoctor> AllWorkHours
        {
            get;
            set;
        }

        public static ObservableCollection<Doctor> Doctors
        {
            get;
            set;
        }

        public static List<int> OnVacation
        {
            get;
            set;
        }

        public static List<int> OnDuty
        {
            get;
            set;
        }

        public static List<TimePeriod> OneShift
        {
            get;
            set;
        }

        public static List<DateTime> Start
        {
            get;
            set;
        }

        public static List<DateTime> End
        {
            get;
            set;
        }

        public WorkHoursForDoctors()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            var app = Application.Current as App;
            userController = (UserController)app.UserController;
            workHoursForDoctorController = (WorkHoursForDoctorController)app.WorkHoursForDoctorController;
            AllWorkHours = new ObservableCollection<WorkHoursForDoctor>(workHoursForDoctorController.GetAll());
            Doctors = new ObservableCollection<Doctor>(userController.GetAllDoctors());

            OnVacation = new List<int>();
            OnDuty = new List<int>();
            OnVacation.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnVacation(0));
            OnDuty.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnDuty(0));
            OnVacation.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnVacation(1));
            OnDuty.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnDuty(1));
            OnVacation.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnVacation(2));
            OnDuty.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnDuty(2));
            OnVacation.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnVacation(3));
            OnDuty.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnDuty(3));
            OnVacation.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnVacation(4));
            OnDuty.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnDuty(4));
            OnVacation.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnVacation(5));
            OnDuty.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnDuty(5));
            OnVacation.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnVacation(6));
            OnDuty.Add(workHoursForDoctorController.CalculateNumberOfDoctorsOnDuty(6));

            OneShift = AllWorkHours[2].Shift;

            Start = new List<DateTime>();
            End = new List<DateTime>();

            for (int j = 0; j < OneShift.Count; j++)
            {
                Start.Add(OneShift[j].StartTime);
                End.Add(OneShift[j].EndTime);
            }
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

        private void clickChangeWorkHours(object sender, RoutedEventArgs e)
        {
            if (inputNewFrom.Value == null || inputNewTo.Value == null)
            {
                String message = "Warning! Yoo need to insert time!";
                MessageBox.Show(message);
                return;
            }

            String fromStr = inputNewFrom.Value.ToString();
            DateTime from = Convert.ToDateTime(fromStr);

            String toStr = inputNewTo.Value.ToString();
            DateTime to = Convert.ToDateTime(toStr);

            DateTime fromTable = (DateTime)inputFrom.SelectedItem;
            DateTime toTable = (DateTime)inputTo.SelectedItem;


            WorkHoursForDoctor wh = AllWorkHours[2];
            // wh.Shift[0].StartTime = from;
            // wh.Shift[0].EndTime = to;
            // workHoursForDoctorController.Delete(AllWorkHours[2]);
            //  workHoursForDoctorController.New(wh);
        }
    }
}
