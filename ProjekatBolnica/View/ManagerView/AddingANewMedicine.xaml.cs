using Controller.ManagerController;
using Controller.UserController;
using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ProjekatBolnica.ManagerView
{
    /// <summary>
    /// Interaction logic for AddingANewMedicine.xaml
    /// </summary>
    public partial class AddingANewMedicine : Window
    {
        private readonly UserController userController;
        private readonly RoomsController roomController;

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

        Manager manager = new Manager();
        public Manager Manager { get => manager; set => manager = value; }

        public static ObservableCollection<Manager> Managers
        {
            get;
            set;
        }

        public static ObservableCollection<Ingredient> Ingredients
        {
            get;
            set;
        }

        public static ObservableCollection<Medicine> ApprovedMed
        {
            get;
            set;
        }

        public static ObservableCollection<Medicine> UnapprovedMed
        {
            get;
            set;
        }

        private readonly IngredientController ingredientController;
        private readonly MedicineController medicineController;
        private int counter = 0;

        public AddingANewMedicine()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            var app = Application.Current as App;
            ingredientController = (IngredientController)app.IngredientController;
            medicineController = (MedicineController)app.MedicineController;
            userController = (UserController)app.UserController;
            roomController = (RoomsController)app.RoomsController;
            Doctors = new ObservableCollection<Doctor>(userController.GetAllDoctors());
            Specialists = new ObservableCollection<Doctor>(userController.GetAllSpecialists());
            Managers = new ObservableCollection<Manager>(userController.GetAllManagers());
            Manager = Managers[0];
            Ingredients = new ObservableCollection<Ingredient>(ingredientController.GetAll());
            ApprovedMed = new ObservableCollection<Medicine>(medicineController.GetAll());
            UnapprovedMed = new ObservableCollection<Medicine>(medicineController.GetAllUnapproved());

            counter = Ingredients.Count;
        }

        private void OpenHelp(object sender, RoutedEventArgs e)
        {
            Help help = new Help();
            this.Close();
            help.Show();
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
        }

        private void OpenResources(object sender, RoutedEventArgs e)
        {
            Resources resources = new Resources();
            this.Close();
            resources.Show();
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

        private void OpenAddingANewMedicine(object sender, RoutedEventArgs e)
        {
            AddingANewMedicine medicine = new AddingANewMedicine();
            this.Close();
            medicine.Show();
        }

        private void clickAddIngredient(object sender, RoutedEventArgs e)
        {
            String name = inputIngredient.Text;
            Ingredient ingredient = new Ingredient
            {
                Id = 0,
                Name = name
            };
            ingredientController.New(ingredient);
        }

        private void clickRemoveIngredient(object sender, RoutedEventArgs e)
        {
            Ingredient ingredient = (Ingredient)inputIngredients.SelectedItem;
            ingredientController.Delete(ingredient);
        }

        private void clickAddMedicine(object sender, RoutedEventArgs e)
        {
            Regex expression = new Regex(@"[0-9]+");

            if (inputName.Text.Equals("") || inputExpiryDate.Text.Equals("") || inputMethod.Text.Equals("") || inputProtective.Text.Equals(""))
            {
                String message = "Warning! Some fields are empty!";
                MessageBox.Show(message);
                return;
            }

            String name = inputName.Text;
            String expiryDate = inputExpiryDate.Text;
            String method = inputMethod.Text;
            String protective = inputProtective.Text;
            List<Ingredient> ingredients = new List<Ingredient>();

            for (int i = 0; i < inputIngredients.SelectedItems.Count; i++)
            {
                Ingredient ing = (Ingredient)inputIngredients.SelectedItems[i];
                ingredients.Add(ing);
            }
            Medicine med = new Medicine()
            {
                Ingredient = ingredients,
                Name = name,
                ExpiryDate = expiryDate,
                MethodOfUse = method,
                ProtectiveMeasures = protective
            };

            medicineController.New(med);
        }

        private void removeUnapproved(object sender, RoutedEventArgs e)
        {
            Medicine un = (Medicine)inputUn.SelectedItem;
            medicineController.DeleteUnapproved(un);
        }

        private void SendRequest(object sender, RoutedEventArgs e)
        {
            if (inputName.Text == "")
            {
                String message = "Warning! Yoo need to insert name of the medicine!";
                MessageBox.Show(message);
                return;
            }
            String desc = "I need approval for " + inputName.Text.ToString() + ".";

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
        }
    }
}
