using Controller.ManagerController;
using Controller.UserController;
using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProjekatBolnica.ManagerView
{
    /// <summary>
    /// Interaction logic for AddingOfRoom.xaml
    /// </summary>
    public partial class AddingOfRoom : Window
    {
        private readonly ListOfHalls lh = null;
        private readonly int ID = 0;
        private readonly RoomsController roomController;
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

        Manager manager = new Manager();
        public Manager Manager { get => manager; set => manager = value; }

        public static ObservableCollection<Manager> Managers
        {
            get;
            set;
        }

        public static ObservableCollection<FunctionOfRoom> FunctionOfRooms
        {
            get;
            set;
        }

        public AddingOfRoom(ListOfHalls listOfHalls, int nbrRooms)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;
            lh = listOfHalls;
            ID = nbrRooms;

            var app = Application.Current as App;
            roomController = (RoomsController)app.RoomsController;
            userController = (UserController)app.UserController;
            Doctors = new ObservableCollection<Doctor>(userController.GetAllDoctors());
            Specialists = new ObservableCollection<Doctor>(userController.GetAllSpecialists());
            Secretaries = new ObservableCollection<Secretary>(userController.GetAllSecretaries());

            FunctionOfRooms = new ObservableCollection<FunctionOfRoom>
            {
                FunctionOfRoom.ControlRoom,
                FunctionOfRoom.OperationRoom,
                FunctionOfRoom.RoomForLaying,
                FunctionOfRoom.Storage
            };

            Managers = new ObservableCollection<Manager>(userController.GetAllManagers());
            Manager = Managers[0];
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

        private void OpenAddingOfRoom(object sender, RoutedEventArgs e)
        {
            AddingOfRoom room = new AddingOfRoom(lh, ID);
            this.Close();
            room.Show();
        }

        private void OpenListOfHalls(object sender, RoutedEventArgs e)
        {
            this.Close();
            lh.Show();
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

        private void ClickAddRoom(object sender, RoutedEventArgs e)
        {

            if (inputName.Text.Equals("") || inputBeginning.Value == null || inputEnding.Value == null)
            {
                String message = "Warning! Some fields are empty!";
                MessageBox.Show(message);
                return;
            }

            String name = inputName.Text;

            String beginningStr = inputBeginning.Value.ToString();
            DateTime beginning = Convert.ToDateTime(beginningStr);

            String endingStr = inputEnding.Value.ToString();
            DateTime ending = Convert.ToDateTime(endingStr);

            TimePeriod renovation = new TimePeriod
            {
                StartTime = beginning,
                EndTime = ending
            };

            FunctionOfRoom function = (FunctionOfRoom)inputFunction.SelectedItem;

            Room room = new Room
            {
                Id = ID,
                Renovation = renovation,
                Name = name,
                Function = function
            };

            roomController.New(room);
            this.Close();
            lh.Show();
        }

        private void ClickInform(object sender, RoutedEventArgs e)
        {
            if (inputBeginning.Value == null || inputEnding.Value == null)
            {
                String message = "Warning! Yoo need to insert time!";
                MessageBox.Show(message);
                return;
            }

            String desc = inputMessage.Text;
            desc += "Start is at " + inputBeginning.Value.ToString() + " and end is at " + inputEnding.Value.ToString();

            Notification notification;
            for (int i = 0; i < Doctors.Count; i++)
            {
                notification = new Notification
                {
                    Sender = Manager,
                    Receiver = Doctors[i],
                    Date = DateTime.Now,
                    Description = desc
                };
                roomController.AnnounceAction(notification);
            }

            for (int i = 0; i < Specialists.Count; i++)
            {
                notification = new Notification
                {
                    Sender = Manager,
                    Receiver = Specialists[i],
                    Date = DateTime.Now,
                    Description = desc
                };
                roomController.AnnounceAction(notification);
            }

            notification = new Notification
            {
                Sender = Manager,
                Receiver = Secretaries[0],
                Date = DateTime.Now,
                Description = desc
            };
            roomController.AnnounceAction(notification);
        }
    }
}
