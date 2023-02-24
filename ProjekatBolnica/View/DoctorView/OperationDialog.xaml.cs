using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Controller.HospitalController;
using Controller.ManagerController;
using Controller.UserController;
using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.DoctorView;
using Xceed.Wpf.Toolkit;

namespace ProjekatBolnica.View.DoctorView
{
    /// <summary>
    /// Interaction logic for OperationDialog.xaml
    /// </summary>
    public partial class OperationDialog : Window
    {
        public Patient SelectedPatient { get; set; }
        public List<Specialist> Specialists { get; set; }
        public List<Room> OperationRooms { get; set; }
        public ObservableCollection<DateTime> AvailableTerms { get; set; }
        private readonly IUserController _userController;
        private readonly IRoomsController _roomsController;
        private readonly IOperationController _operationController;
        public OperationDialog(Patient patient)
        {
            InitializeComponent();
            if ((Application.Current.MainWindow as MainWindow).Theme == Theme.Dark) { dialogGrid.Background = Brushes.LightGray; }
            this.DataContext = this;
            var app = Application.Current as App;
            _userController = app.UserController;
            _roomsController = app.RoomsController;
            _operationController = app.OperationController;
            SelectedPatient = patient;
            Specialists = _userController.GetAllSpecialists();
            OperationRooms = _roomsController.GetAllOperationRooms();
        }

        private void closeOperationDialog(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void openComboBox(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                comboBox.IsDropDownOpen = true;
            }
        }

        private void openCalendar(object sender, KeyEventArgs e)
        {
            DateTimePicker picker = (DateTimePicker)sender;
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                picker.IsOpen = true;
            }
        }

        private void addOperation(object sender, RoutedEventArgs e)
        {
            if (doctorComboBox.SelectedItem == null)
            {
                string errSrb = "Izaberite lekara.";
                string errEng = "Choose a doctor.";
                printErr(errSrb, errEng);
                return;
            }
            if (operationRoomComboBox.SelectedItem == null)
            {
                string errSrb = "Izaberite operacionu salu.";
                string errEng = "Choose an operating room.";
                printErr(errSrb, errEng);
                return;
            }
            if (!datePicker.SelectedDate.HasValue)
            {
                string errSrb = "Izaberite datum.";
                string errEng = "Choose date.";
                printErr(errSrb, errEng);
                return;
            }
            if(availableTermsComboBox.SelectedItem == null)
            {
                string errSrb = "Izaberite termin.";
                string errEng = "Choose a term.";
                printErr(errSrb, errEng);
                return;
            }
            Operation operation = new Operation();
            operation.DateAndTime = (DateTime)availableTermsComboBox.SelectedItem;
            operation.Specialist = (Specialist)doctorComboBox.SelectedItem;
            operation.Patient = SelectedPatient;
            operation.Room = (Room)operationRoomComboBox.SelectedItem;
            _operationController.New(operation);
            this.Close();
        }
        private void printErr(string errSrb, string errEng)
        {
            if ((Application.Current.MainWindow as MainWindow).Lang == Backend.Model.DoctorModel.Language.English)
            {
                System.Windows.MessageBox.Show(errEng);
            }
            else
            {
                System.Windows.MessageBox.Show(errSrb);
            }
        }
        private void getAvailableTerms(object sender, SelectionChangedEventArgs e)
        {
            if (doctorComboBox.SelectedItem != null && operationRoomComboBox.SelectedItem != null && datePicker.SelectedDate.HasValue)
            {
                AvailableTerms = new ObservableCollection<DateTime>(_operationController.GetAvailableTermsForRoomAndDoctorAndDate((Doctor)doctorComboBox.SelectedItem, datePicker.SelectedDate.Value, (Room)operationRoomComboBox.SelectedItem));
                availableTermsComboBox.ItemsSource = AvailableTerms;
            }
        }
    }
}
