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
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.DoctorView;

namespace ProjekatBolnica.View.DoctorView
{
    /// <summary>
    /// Interaction logic for RelocatePatientDialog.xaml
    /// </summary>
    public partial class RelocatePatientDialog : Window
    {
        public Patient SelectedPatient { get; set; }
        public ObservableCollection<Room> RoomsForLaying { get; set; }
        public ObservableCollection<Bed> Beds { get; set; }
        private readonly IHospitalStayController _hospitalStayController;
        private readonly IRoomsController _roomsController;
        private readonly IResourceController _resourceController;
        public RelocatePatientDialog(Patient patient)
        {
            InitializeComponent();
            if ((Application.Current.MainWindow as MainWindow).Theme == Theme.Dark) { dialogGrid.Background = Brushes.LightGray; }
            this.DataContext = this;
            var app = Application.Current as App;
            _hospitalStayController = app.HospitalStayController;
            _roomsController = app.RoomsController;
            _resourceController = app.ResourceController;
            SelectedPatient = patient;
            RoomsForLaying = new ObservableCollection<Room>(_roomsController.GetAllRoomsForLaying());
        }

        private void closeRelocatePatientDialog(object sender, RoutedEventArgs e)
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

        private void relocatePatient(object sender, RoutedEventArgs e)
        {
            if (roomComboBox.SelectedItem == null)
            {
                string errSrb = "Izaberite sobu.";
                string errEng = "Choose a room.";
                printErr(errSrb, errEng);
                return;
            }
            if (bedComboBox.SelectedItem == null)
            {
                string errSrb = "Izaberite krevet.";
                string errEng = "Choose a bed.";
                printErr(errSrb, errEng);
                return;
            }
            if(_hospitalStayController.relocatePatient(SelectedPatient, (Bed)bedComboBox.SelectedItem)){
                string msgSrb = "Pacijent relociran.";
                string msgEng = "Patient relocated.";
                printErr(msgSrb, msgEng);
                return;
            } else
            {
                string msgSrb = "Relokacija nije moguca.";
                string msgEng = "Relocation not  valid.";
                printErr(msgSrb, msgEng);
                return;
            }
        }

        private void getBedsInRoom(object sender, SelectionChangedEventArgs e)
        {
            Beds = new ObservableCollection<Bed>(_resourceController.GetBedsInRoom((Room)roomComboBox.SelectedItem));
            bedComboBox.ItemsSource = Beds;
        }
    }
}
