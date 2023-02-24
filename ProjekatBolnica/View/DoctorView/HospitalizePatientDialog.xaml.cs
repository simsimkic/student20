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
using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.DoctorView;

namespace ProjekatBolnica.View.DoctorView
{
    /// <summary>
    /// Interaction logic for HospitalizePatientDialog.xaml
    /// </summary>
    public partial class HospitalizePatientDialog : Window
    {
        public Patient SelectedPatient { get; set; }
        public ObservableCollection<Room> RoomsForLaying { get; set; }
        public ObservableCollection<Bed> Beds { get; set; }
        private readonly IHospitalStayController _hospitalStayController;
        private readonly IRoomsController _roomsController;
        private readonly IResourceController _resourceController;
        public HospitalizePatientDialog(Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;
            _hospitalStayController = app.HospitalStayController;
            _roomsController = app.RoomsController;
            _resourceController = app.ResourceController;
            SelectedPatient = patient;
            RoomsForLaying = new ObservableCollection<Room>(_roomsController.GetAllRoomsForLaying());
            if ((Application.Current.MainWindow as MainWindow).Theme == Theme.Dark)
            {
                dialogGrid.Background = Brushes.LightGray;
            }
        }

        private void closeHospitalizePatientDialog(object sender, RoutedEventArgs e)
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
            DatePicker picker = (DatePicker)sender;
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                picker.IsDropDownOpen = true;
            }
        }

        private void addHospitalStay(object sender, RoutedEventArgs e)
        {
            if(roomComboBox.SelectedItem == null)
            {
                string errSrb = "Izaberite sobu.";
                string errEng = "Choose a room.";
                printErr(errSrb, errEng);
                return;
            }
            if(bedComboBox.SelectedItem == null)
            {
                string errSrb = "Izaberite krevet.";
                string errEng = "Choose a bed.";
                printErr(errSrb, errEng);
                return;
            }
            if(!startDate.SelectedDate.HasValue || !endDate.SelectedDate.HasValue)
            {
                string errSrb = "Izaberite period hospitalizacije.";
                string errEng = "Choose hospitalization period.";
                printErr(errSrb, errEng);
                return;
            }
            if(endDate.SelectedDate.Value.Date < startDate.SelectedDate.Value.Date)
            {
                string errSrb = "Izaberite validan period.";
                string errEng = "Choose a valid period.";
                printErr(errSrb, errEng);
                return;
            }
            HospitalStay hs = new HospitalStay();
            TimePeriod period = new TimePeriod();
            period.StartTime = startDate.SelectedDate.Value;
            period.EndTime = endDate.SelectedDate.Value;
            hs.Duration = period;
            hs.Bed = (Bed)bedComboBox.SelectedItem;
            hs.Patient = SelectedPatient;
            _hospitalStayController.New(hs);
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

        private void getBedsInRoom(object sender, SelectionChangedEventArgs e)
        {
            Beds = new ObservableCollection<Bed>(_resourceController.GetBedsInRoom((Room)roomComboBox.SelectedItem));
            bedComboBox.ItemsSource = Beds;
        }
    }
}
