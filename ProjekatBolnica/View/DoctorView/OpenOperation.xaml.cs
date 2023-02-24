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
using Controller.ManagerController;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.PatientModel;
using ProjekatBolnica.Backend.Controller.PatientController;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.Backend.Model.PatientModel;
using ProjekatBolnica.DoctorView;
using Repository.PatientRepository;

namespace ProjekatBolnica.View.DoctorView
{
    /// <summary>
    /// Interaction logic for OpenOperation.xaml
    /// </summary>
    public partial class OpenOperation : Window
    {

        public Operation Operation { get; set; }
        public ObservableCollection<Medicine> Medicines { get; set; }
        public ObservableCollection<Medicine> PerscriptionMedicines { get; set; }
        private readonly IMedicineController _medicineController;
        private readonly IMedicalRecordsController _medicalRecordsController;
        public OpenOperation(Operation operation)
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;
            _medicineController = app.MedicineController;
            _medicalRecordsController = app.MedicalRecordController;
            Operation = operation;
            Medicines = new ObservableCollection<Medicine>(_medicineController.GetAll());
            PerscriptionMedicines = new ObservableCollection<Medicine>();
            if ((Application.Current.MainWindow as MainWindow).Theme == Theme.Dark)
            {
                dialogGrid.Background = Brushes.LightGray;
            }
        }

        private void selectMedicine(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                Medicine medicine = (Medicine)allMedicine.Items[allMedicine.SelectedIndex];
                if (!PerscriptionMedicines.Contains(medicine))
                {
                    PerscriptionMedicines.Add(medicine);
                }
            }
        }

        private void removeMedicine(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                Medicine medicine = (Medicine)perscribedMedicine.Items[perscribedMedicine.SelectedIndex];
                PerscriptionMedicines.Remove(medicine);
            }
        }

        private void selectFirst(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListBox list = (ListBox)sender;
            list.SelectedItem = list.Items[0];
        }
        private void openCalendar(object sender, KeyEventArgs e)
        {
            DatePicker picker = (DatePicker)sender;
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                picker.IsDropDownOpen = true;
            }
        }

        private void closeOperationDialog(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void confirmOperation(object sender, RoutedEventArgs e)
        {
            if (reportTextBox.Text.Length == 0)
            {
                string errSrb = "Unesite izveštaj pregleda.";
                string errEng = "Enter exam report.";
                printErr(errSrb, errEng);
                return;
            }
            if (PerscriptionMedicines.Count != 0)
            {
                if (!perscriptionEndDate.SelectedDate.HasValue)
                {
                    string errSrb = "Unesite period validnosti recepta.";
                    string errEng = "Enter perscription validity period.";
                    printErr(errSrb, errEng);
                    return;
                }
            }
            MedicalRecord record = new MedicalRecord();
            Perscription perscription = new Perscription();
            perscription.Medicine = PerscriptionMedicines.ToList();
            perscription.PeriodOfValidity = new Model.UtilityModel.TimePeriod
            {
                StartTime = DateTime.Now,
                EndTime = (!perscriptionEndDate.SelectedDate.HasValue) ? DateTime.Now : perscriptionEndDate.SelectedDate.Value
            };
            record.Perscription = perscription;
            record.Patient = Operation.Patient;
            record.Doctor = (Application.Current.MainWindow as MainWindow).LoggedInDoctor;
            record.ExamReport = reportTextBox.Text;
            record.ExamDate = DateTime.Now;
            record.TypeOfRecord = TypeOfRecord.Operation;
            _medicalRecordsController.New(record);
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
    }
}
