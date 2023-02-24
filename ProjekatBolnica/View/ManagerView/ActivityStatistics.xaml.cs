using Controller.HospitalController;
using Controller.ManagerController;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.UtilityModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProjekatBolnica.ManagerView
{
    /// <summary>
    /// Interaction logic for ActivityStatistics.xaml
    /// </summary>
    public partial class ActivityStatistics : Window
    {
        public static ObservableCollection<Room> OperationRooms
        {
            get;
            set;
        }

        public static List<int> NbrOperations
        {
            get;
            set;
        }

        public static ObservableCollection<Room> ControlRooms
        {
            get;
            set;
        }

        public static List<int> NbrControls
        {
            get;
            set;
        }

        public static ObservableCollection<Room> RoomsForLaying
        {
            get;
            set;
        }

        public static List<int> Nbrbeds
        {
            get;
            set;
        }

        public static ObservableCollection<Operation> Operations
        {
            get;
            set;
        }

        private readonly RoomsController roomController;
        private readonly OperationController operationController;
        private readonly AppointmentController appointmentController;
        private readonly ResourceController resourceController;

        public ActivityStatistics()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            var app = Application.Current as App;
            roomController = (RoomsController)app.RoomsController;
            OperationRooms = new ObservableCollection<Room>(roomController.GetAllOperationRooms());
            ControlRooms = new ObservableCollection<Room>(roomController.GetAllControlRooms());
            RoomsForLaying = new ObservableCollection<Room>(roomController.GetAllRoomsForLaying());

            operationController = (OperationController)app.OperationController;
            appointmentController = (AppointmentController)app.AppointmentController;
            resourceController = (ResourceController)app.ResourceController;

            TimePeriod tp = new TimePeriod();
            tp.StartTime = DateTime.Now;
            tp.EndTime = DateTime.Now;

            Nbrbeds = new List<int>();
            NbrOperations = new List<int>();
            NbrControls = new List<int>();

            for (int i = 0; i < OperationRooms.Count; i++)
                NbrOperations.Add(operationController.CountOperationsInRoom(OperationRooms[i], tp));
            
           for (int i = 0; i < ControlRooms.Count; i++)
               NbrControls.Add(appointmentController.CountAppointmentsInRoom(ControlRooms[i], tp));
                
            for (int i = 0; i < RoomsForLaying.Count; i++)
                Nbrbeds.Add(resourceController.CountNumberOfBedsInRoom(RoomsForLaying[i]));
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
    }
}
