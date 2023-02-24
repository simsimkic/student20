using Controller.ManagerController;
using Model.ManagerModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProjekatBolnica.ManagerView
{
    /// <summary>
    /// Interaction logic for Resources.xaml
    /// </summary>
    public partial class Resources : Window
    {
        private readonly ResourceController resourceController;
        private readonly RoomsController roomController;

        public static ObservableCollection<Resource> AllResources
        {
            get;
            set;
        }

        public Resource SAllResources
        {
            get;
            set;
        }


        public static ObservableCollection<Room> Rooms
        {
            get;
            set;
        }

        public Resources()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            var app = Application.Current as App;
            resourceController = (ResourceController)app.ResourceController;
            AllResources = new ObservableCollection<Resource>(resourceController.GetAll());
            roomController = (RoomsController)app.RoomsController;
            Rooms = new ObservableCollection<Room>(roomController.GetAll());
        }

        private void OpenAddingANewMedicine(object sender, RoutedEventArgs e)
        {
            AddingANewMedicine medicine = new AddingANewMedicine();
            this.Close();
            medicine.Show();
        }

        private void OpenHelp(object sender, RoutedEventArgs e)
        {
            Help help = new Help();
            this.Close();
            help.Show();
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

        private void OpenResources(object sender, RoutedEventArgs e)
        {
            Resources resources = new Resources();
            this.Close();
            resources.Show();
        }

        private void clickRemoveResource(object sender, RoutedEventArgs e)
        {
            Resource resource = (Resource)inputResource.SelectedItem;
            resourceController.Delete(resource);
        }

        private void clickAddResource(object sender, RoutedEventArgs e)
        {
            long id = long.Parse(inputId.Text);
            String name = inputName.Text;
            Room room = (Room)inputRoom.SelectedItem;

            Resource resource = new Resource
            {
                Id = id,
                Name = name,
                Room = room
            };
            resourceController.New(resource);
        }

        private void clickDecreaseResource(object sender, RoutedEventArgs e)
        {
            long id = Convert.ToInt64(inputResource.SelectedItem);
            int quantity = Int32.Parse(inputQuantity.Text);
            Resource r = resourceController.Get(id);

            if ((r.Amount -= quantity) < 0)
            {
                String message = "Warning! There can not be negative quantities!";
                MessageBox.Show(message);
                return;
            }

            resourceController.DecreaseQuantity(id, quantity);
        }

        private void clickIncreaseResource(object sender, RoutedEventArgs e)
        {
            long id = Convert.ToInt64(inputResource.SelectedItem);
            int quantity = Int32.Parse(inputQuantity.Text);
            resourceController.IncreaseQuantity(id, quantity);
        }
    }
}
