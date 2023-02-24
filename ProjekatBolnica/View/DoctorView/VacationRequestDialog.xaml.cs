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
using Controller.UserController;
using Model;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Controller.UserController;
using ProjekatBolnica.Backend.Model.DoctorModel;
using Xceed.Wpf.Toolkit;

namespace ProjekatBolnica.View.DoctorView
{
    /// <summary>
    /// Interaction logic for VacationRequestDialog.xaml
    /// </summary>
    public partial class VacationRequestDialog : Window
    {
        public ObservableCollection<Manager> Managers { get; set; }
        private readonly IUserController _userController;
        private readonly INotificationController _notificationController;
        private const string NOTIFICATION_DESCRIPTION = "Vacation request for period {0} - {1}";
        private const string DATE_FORMAT = "dd.MM.yyyy.";
        public VacationRequestDialog()
        {
            InitializeComponent();
            if ((Application.Current.MainWindow as MainWindow).Theme == Theme.Dark) { dialogGrid.Background = Brushes.LightGray; }
            this.DataContext = this;
            var app = Application.Current as App;
            _userController = app.UserController;
            _notificationController = app.NotificationController;
            Managers = new ObservableCollection<Manager>(_userController.GetAllManagers());
        }

        private void closeVacationDialog(object sender, RoutedEventArgs e)
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
            if(e.Key == Key.Space || e.Key == Key.Enter)
            {
                picker.IsDropDownOpen = true;
            }
        }

        private void requestVacation(object sender, RoutedEventArgs e)
        {
            if(managerComboBox.SelectedItem == null)
            {
                string errSrb = "Izaberite menadžera.";
                string errEng = "Choose a manager.";
                printErr(errSrb, errEng);
                return;
            }
            if (!startDate.SelectedDate.HasValue || !endDate.SelectedDate.HasValue)
            {
                string errSrb = "Izaberite period godišnjeg odmora.";
                string errEng = "Choose vacation period.";
                printErr(errSrb, errEng);
                return;
            }
            if (endDate.SelectedDate.Value.Date < startDate.SelectedDate.Value.Date)
            {
                string errSrb = "Izaberite validan period.";
                string errEng = "Choose a valid period.";
                printErr(errSrb, errEng);
                return;
            }
            var app = Application.Current as App;
            Notification notification = new Notification();
            notification.Sender = (Application.Current.MainWindow as MainWindow).LoggedInDoctor;
            notification.Receiver = (Manager)managerComboBox.SelectedItem;
            notification.Date = DateTime.Now;
            notification.Description = string.Format(NOTIFICATION_DESCRIPTION, startDate.SelectedDate.Value.ToString(DATE_FORMAT), endDate.SelectedDate.Value.ToString(DATE_FORMAT));
            _notificationController.New(notification);
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
