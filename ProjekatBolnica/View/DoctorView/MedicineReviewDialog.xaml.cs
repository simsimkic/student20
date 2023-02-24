using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Controller.HospitalController;
using Controller.UserController;
using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Controller.UserController;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.View.DoctorView;

namespace ProjekatBolnica.DoctorView
{
    /// <summary>
    /// Interaction logic for MedicineReviewDialog.xaml
    /// </summary>
    public partial class MedicineReviewDialog : Window
    {
        public Medicine SelectedMedicine { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public ObservableCollection<Manager> Managers { get; set; }
        private readonly IApprovalOfMedicineController _approvalOfMedicineController;
        private readonly IUserController _userController;
        public MedicineReviewDialog(Medicine medicine)
        {
            InitializeComponent();
            if ((Application.Current.MainWindow as MainWindow).Theme == Theme.Dark) { dialogGrid.Background = Brushes.LightGray; }
            this.DataContext = this;
            var app = App.Current as App;
            _approvalOfMedicineController = app.ApprovalOfMedicineController;
            _userController = app.UserController;
            SelectedMedicine = medicine;
            Ingredients = new ObservableCollection<Ingredient>(SelectedMedicine.Ingredient);
            Managers = new ObservableCollection<Manager>(_userController.GetAllManagers());
        }

        private void closeMedicineReviewDialog(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void approveMedicine(object sender, RoutedEventArgs e)
        {
            if(managerComboBox.SelectedItem == null)
            {
                string errSrb = "Odaberite menadzera";
                string errEng = "Choose a manager";
                printErr(errSrb, errEng);
                return;
            }
            if(medicineComment.Text.Equals(""))
            {
                string errSrb = "Napišite komentar.";
                string errEng = "Write a comment.";
                printErr(errSrb, errEng);
                return;
            }
            Notification notification = new Notification();
            notification.Date = DateTime.Now;
            notification.Description = "Medicine: " + SelectedMedicine.Name + " " + "Comment: " + medicineComment.Text;
            notification.Sender = (Application.Current.MainWindow as MainWindow).LoggedInDoctor;
            notification.Receiver = (Manager)managerComboBox.SelectedItem;
            _approvalOfMedicineController.ApproveMedicine(notification);
            this.Close();
        }
        private void disapproveMedicine(object sender, RoutedEventArgs e)
        {
            if (managerComboBox.SelectedItem == null)
            {
                string errSrb = "Odaberite menadzera";
                string errEng = "Choose a manager";
                printErr(errSrb, errEng);
                return;
            }
            if (medicineComment.Text == string.Empty)
            {
                string errSrb = "Napišite komentar.";
                string errEng = "Write a comment.";
                printErr(errSrb, errEng);
                return;
            }
            Notification notification = new Notification();
            notification.Date = DateTime.Now;
            notification.Description = "Medicine: " + SelectedMedicine.Name + " " + "Comment: " + medicineComment.Text;
            notification.Sender = (Application.Current.MainWindow as MainWindow).LoggedInDoctor;
            notification.Receiver = (Manager)managerComboBox.SelectedItem;
            _approvalOfMedicineController.DisapproveMedicine(notification);
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
