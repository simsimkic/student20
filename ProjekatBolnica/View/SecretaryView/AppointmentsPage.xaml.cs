using Controller.HospitalController;
using Model.DoctorModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Controller.UserController;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjekatBolnica.SecretaryView
{
    /// <summary>
    /// Interaction logic for AppointmentsPage.xaml
    /// </summary>
    public partial class AppointmentsPage : Window
    {
        private IController<Appointment, long> appointmentController;
        private NotificationController notificationController;

        public static ObservableCollection<Appointment> AppointmentListView
        {
            get;
            set;
        }

        private ICollectionView defaultView;



        public AppointmentsPage()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            var app = System.Windows.Application.Current as App;
            appointmentController = app.AppointmentController;
            notificationController = (NotificationController)app.NotificationController;

            AppointmentListView = new ObservableCollection<Appointment>(appointmentController.GetAll());

            //FILTER...
            this.defaultView = CollectionViewSource.GetDefaultView(AppointmentListView);
            this.defaultView.Filter =
                w => (istrueCont((Appointment)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }




        private void clickAddAppointment(object sender, RoutedEventArgs e)
        {
            AddAppointment addAppointment = new AddAppointment();
            addAppointment.ShowDialog();
        }

        private void editSelectedAppointment(object sender, RoutedEventArgs e)
        {
            Appointment a = (Appointment)defaultViewTable.SelectedItem;
            if (a == null)
            {
                string message = "Warning! You did not choose a appointment!";
                System.Windows.MessageBox.Show(message);
            }
            else
            {
                EditAppointment editAppointment = new EditAppointment(a);
                editAppointment.ShowDialog();
            }
        }


        private void deleteSelectedAppointment(object sender, RoutedEventArgs e)
        {
            Appointment a = (Appointment)defaultViewTable.SelectedItem;
            if (a != null)
            {
                string message = "Do you want to delete the appointment?";
                string title = "Delete appointment";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = (DialogResult)System.Windows.MessageBox.Show(message, title, (MessageBoxButton)buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Notification notificationForDoctor = new Notification();
                    notificationForDoctor.Date = DateTime.Now;
                    notificationForDoctor.Sender = SecretaryMainPage.secretary;
                    notificationForDoctor.Receiver = a.Doctor;
                    notificationForDoctor.Description = "the appointment was canceled - " + a.ToDescription();
                    notificationController.New(notificationForDoctor);
                    Notification notificationForPatient = new Notification();
                    notificationForPatient.Date = DateTime.Now;
                    notificationForPatient.Sender = SecretaryMainPage.secretary;
                    notificationForPatient.Receiver = a.Patient;
                    notificationForPatient.Description = "the appointment was canceled - " + a.ToDescription();
                    notificationController.New(notificationForPatient);

                    appointmentController.Delete(a);
                    AppointmentListView = new ObservableCollection<Appointment>(appointmentController.GetAll());

                    //FILTER...
                    this.defaultView = CollectionViewSource.GetDefaultView(AppointmentListView);
                    this.defaultView.Filter =
                        w => (istrueCont((Appointment)w));

                    defaultViewTable.ItemsSource = this.defaultView;
                }
                else { }
            }
            else
            {
                string message = "Warning! You did not choose a appointment!";
                System.Windows.MessageBox.Show(message);
            }
        }

        private void clickRefreshTableAppointment(object sender, RoutedEventArgs e)
        {
            AppointmentListView = new ObservableCollection<Appointment>(appointmentController.GetAll());

            //FILTER...
            this.defaultView = CollectionViewSource.GetDefaultView(AppointmentListView);
            this.defaultView.Filter =
                w => (istrueCont((Appointment)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }

        private void forsearchDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            AppointmentListView = new ObservableCollection<Appointment>(appointmentController.GetAll());

            //FILTER...
            this.defaultView = CollectionViewSource.GetDefaultView(AppointmentListView);
            this.defaultView.Filter =
                w => (istrueCont((Appointment)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }

        private void refreshPageWithTextBox(object sender, System.Windows.Input.KeyEventArgs e)
        {
            AppointmentListView = new ObservableCollection<Appointment>(appointmentController.GetAll());

            //FILTER...
            this.defaultView = CollectionViewSource.GetDefaultView(AppointmentListView);
            this.defaultView.Filter =
                w => (istrueCont((Appointment)w));

            defaultViewTable.ItemsSource = this.defaultView; ;
        }





        //DODATNE METODE
        private bool isDate(Appointment w)
        {
            if (forsearchDate.SelectedDate == null) { return true; }
            else if (w.DateAndTime.Year.Equals(forsearchDate.SelectedDate.Value.Year)
                && w.DateAndTime.Month.Equals(forsearchDate.SelectedDate.Value.Month)
                && w.DateAndTime.Day.Equals(forsearchDate.SelectedDate.Value.Day)) { return true; }
            else { return false; }
        }

        private bool istrueCont(Appointment w)
        {
            if (w.Patient.Name.Contains(forsearch.Text) && w.Patient.Surname.Contains(forsearchSurname.Text) && w.Patient.PersonalIDnumber.Contains(forsearchPersonal.Text)
                && w.Doctor.Name.Contains(forsearchNameDoctor.Text) && w.Doctor.Surname.Contains(forsearchSurnameDoctor.Text)
                && isDate(w) ) { return true; }
            else { return false; }
        }



    }
}
