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
    /// Interaction logic for OperationsPage.xaml
    /// </summary>
    public partial class OperationsPage : Window
    {
        private IController<Operation, long> operationController;
        private NotificationController notificationController;

        public static ObservableCollection<Operation> OperationListView
        {
            get;
            set;
        }

        private ICollectionView defaultView;



        public OperationsPage()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            var app = System.Windows.Application.Current as App;
            operationController = app.OperationController;
            notificationController = (NotificationController)app.NotificationController;

            OperationListView = new ObservableCollection<Operation>(operationController.GetAll());

            //FILTER...
            this.defaultView = CollectionViewSource.GetDefaultView(OperationListView);
            this.defaultView.Filter =
                w => (istrueCont((Operation)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }


        private bool isDate(Operation w)
        {
            if (forsearchDate.SelectedDate == null) { return true; }
            else if (w.DateAndTime.Year.Equals(forsearchDate.SelectedDate.Value.Year)
                && w.DateAndTime.Month.Equals(forsearchDate.SelectedDate.Value.Month)
                && w.DateAndTime.Day.Equals(forsearchDate.SelectedDate.Value.Day)) { return true; }
            else { return false; }
        }

        private bool istrueCont(Operation w)
        {
            if (w.Patient.Name.Contains(forsearch.Text) && w.Patient.Surname.Contains(forsearchSurname.Text) && w.Patient.PersonalIDnumber.Contains(forsearchPersonal.Text)
                && w.Specialist.Name.Contains(forsearchNameDoctor.Text) && w.Specialist.Surname.Contains(forsearchSurnameDoctor.Text)
               && isDate(w) /*&& w.DateAndTime.Equals(forsearchDate.SelectedDate) */) { return true; }
            else { return false; }
            //return true;
        }



        private void clickAddOperation(object sender, RoutedEventArgs e)
        {
            AddOperation addOperation = new AddOperation();
            addOperation.ShowDialog();
        }

        private void editSelectedOperation(object sender, RoutedEventArgs e)
        {
            Operation o = (Operation)defaultViewTable.SelectedItem;
            if (o == null)
            {
                string message = "Warning! You did not choose a operation!";
                System.Windows.MessageBox.Show(message);
            }
            else
            {
                EditOperation editOperation = new EditOperation(o);
                editOperation.ShowDialog();
            }
        }


        private void deleteSelectedOperation(object sender, RoutedEventArgs e)
        {
            Operation o = (Operation)defaultViewTable.SelectedItem;
            if (o != null)
            {
                string message = "Do you want to delete the operation?";
                string title = "Delete operation";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = (DialogResult)System.Windows.MessageBox.Show(message, title, (MessageBoxButton)buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Notification notificationForDoctor = new Notification();
                    notificationForDoctor.Date = DateTime.Now;
                    notificationForDoctor.Sender = SecretaryMainPage.secretary;
                    notificationForDoctor.Receiver = o.Specialist;
                    notificationForDoctor.Description = "The operation was canceled - " + o.ToDescription();
                    notificationController.New(notificationForDoctor);
                    Notification notificationForPatient = new Notification();
                    notificationForPatient.Date = DateTime.Now;
                    notificationForPatient.Sender = SecretaryMainPage.secretary;
                    notificationForPatient.Receiver = o.Patient;
                    notificationForPatient.Description = "The operation was canceled - " + o.ToDescription();
                    notificationController.New(notificationForPatient);

                    operationController.Delete(o);
                    OperationListView = new ObservableCollection<Operation>(operationController.GetAll());

                    // ZA FILTER:
                    this.defaultView = CollectionViewSource.GetDefaultView(OperationListView);
                    this.defaultView.Filter = w => (istrueCont((Operation)w));

                    defaultViewTable.ItemsSource = this.defaultView;
                }
                else { }
            }
            else
            {
                string message = "Warning! You did not choose a operation!";
                System.Windows.MessageBox.Show(message);
            }
        }


        private void clickRefreshTableOperation(object sender, RoutedEventArgs e)
        {
            OperationListView = new ObservableCollection<Operation>(operationController.GetAll());

            // ZA FILTER:
            this.defaultView = CollectionViewSource.GetDefaultView(OperationListView);
            this.defaultView.Filter = w => (istrueCont((Operation)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }

        private void refreshPageWithTextBox(object sender, System.Windows.Input.KeyEventArgs e)
        {
            OperationListView = new ObservableCollection<Operation>(operationController.GetAll());

            // ZA FILTER:
            this.defaultView = CollectionViewSource.GetDefaultView(OperationListView);
            this.defaultView.Filter = w => (istrueCont((Operation)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }

        private void forsearchDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            OperationListView = new ObservableCollection<Operation>(operationController.GetAll());

            // ZA FILTER:
            this.defaultView = CollectionViewSource.GetDefaultView(OperationListView);
            this.defaultView.Filter = w => (istrueCont((Operation)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }

    }
}
