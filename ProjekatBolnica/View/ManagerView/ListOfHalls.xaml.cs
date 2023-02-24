using Controller.ManagerController;
using Model.ManagerModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace ProjekatBolnica.ManagerView
{
    /// <summary>
    /// Interaction logic for ListOfHalls.xaml
    /// </summary>
    public partial class ListOfHalls : Window
    {
        private readonly RoomsController roomController;
        private ICollectionView defaultView;
        public int nbrRooms = 0;

        public static ObservableCollection<Room> Rooms
        {
            get;
            set;
        }

        public ListOfHalls()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            var app = Application.Current as App;
            roomController = (RoomsController)app.RoomsController;
            Rooms = new ObservableCollection<Room>(roomController.GetAll());
            nbrRooms = Rooms.Count;

            this.defaultView = CollectionViewSource.GetDefaultView(Rooms);
            this.defaultView.Filter =
                r => (IsTrueContent((Room)r));

            dataGridRooms.ItemsSource = this.defaultView;
        }

        private bool IsTrueContent(Room r)
        {
            if (r.Name.Contains(inputSearch.Text) || r.Id.Equals(inputSearch.Text))
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
            Rooms = new ObservableCollection<Room>(Rooms);

            this.defaultView = CollectionViewSource.GetDefaultView(Rooms);
            this.defaultView.Filter =
               r => (IsTrueContent((Room)r));

            dataGridRooms.ItemsSource = this.defaultView;
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

        private void OpenAddingOfRoom(object sender, RoutedEventArgs e)
        {
            AddingOfRoom room = new AddingOfRoom(this, nbrRooms);
            this.Hide();
            room.Show();
        }

        private void OpenChangeOfRoom(object sender, RoutedEventArgs e)
        {
            Room r = (Room)dataGridRooms.SelectedItem;
            if (r != null)
            {
                ChangeOfRoom change = new ChangeOfRoom(this, r);
                this.Hide();
                change.Show();
            }
        }

        private void OpenRemovingOfRoom(object sender, RoutedEventArgs e)
        {
            Room r = (Room)dataGridRooms.SelectedItem;
            if (r != null)
            {
                RemovingOfRoom room = new RemovingOfRoom(this, r);
                this.Hide();
                room.Show();
            }
        }
    }
}
