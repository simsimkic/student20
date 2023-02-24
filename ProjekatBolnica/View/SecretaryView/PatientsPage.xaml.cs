
using Controller.HospitalController;
using Controller.UserController;
using Model;
using ProjekatBolnica.View.SecretaryView;
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
    /// Interaction logic for PatientsPage.xaml
    /// </summary>
    public partial class PatientsPage : Window
    {
        private UserController userController;

        public static ObservableCollection<Patient> PatientsListView
        {
            get;
            set;
        }

        private ICollectionView defaultView;


        List<Patient> PatientsList;


        public PatientsPage()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            var app = System.Windows.Application.Current as App;
            userController = (UserController)app.UserController;

            PatientsListView = new ObservableCollection<Patient>(userController.GetAllPatients());

            // ZA FILTER:
            this.defaultView = CollectionViewSource.GetDefaultView(PatientsListView);
            this.defaultView.Filter =
                w => (istrueCont((Patient)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }


        //ADD GUEST PATIENT
        private void clickAddGuestPatient(object sender, RoutedEventArgs e)
        {
            AddGuestPatient addGuestPatient = new AddGuestPatient();
            addGuestPatient.ShowDialog();
        }


        //ADD PATIENT
        private void addNewPatient(object sender, RoutedEventArgs e)
        {
            AddPatient addNewPatientWindow = new AddPatient();
            addNewPatientWindow.ShowDialog();
        }


        //EDIT PATIENT
        private void editSelectedPatient(object sender, RoutedEventArgs e)
        {
            Patient p = (Patient)defaultViewTable.SelectedItem;
            if (p == null)
            {
                string message = "Warning! You did not choose a patient!";
                System.Windows.MessageBox.Show(message);
            }
            else
            {
                EditPatient editPatient = new EditPatient(p);
                editPatient.ShowDialog();
            }
        }


        //DELETE PATIENT
        private void deleteSelectedPatient(object sender, RoutedEventArgs e)
        {
            Patient p = (Patient)defaultViewTable.SelectedItem;
            if (p != null)
            {
                string message = "Do you want to delete the patient?";
                string title = "Delete patient";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = (DialogResult)System.Windows.MessageBox.Show(message, title, (MessageBoxButton)buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    userController.Delete(p);
                    PatientsListView = new ObservableCollection<Patient>(userController.GetAllPatients());

                    // ZA FILTER:
                    this.defaultView = CollectionViewSource.GetDefaultView(PatientsListView);
                    this.defaultView.Filter = w => (istrueCont((Patient)w));

                    defaultViewTable.ItemsSource = this.defaultView;
                }
                else { }
            }
            else
            {
                string message = "Warning! You did not choose a patient!";
                System.Windows.MessageBox.Show(message);
            }

        }


        //ZA PRETRAZIVANJE
        private void refreshPage(object sender, RoutedEventArgs e)
        {
            PatientsListView = new ObservableCollection<Patient>(userController.GetAllPatients());

            // ZA FILTER:
            this.defaultView = CollectionViewSource.GetDefaultView(PatientsListView);
            this.defaultView.Filter =
                w => (istrueCont((Patient)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }


        //OSVEZI TABELU
        private void clickRefreshTablePatient(object sender, RoutedEventArgs e)
        {
            PatientsListView = new ObservableCollection<Patient>(userController.GetAllPatients());

            // ZA FILTER:
            this.defaultView = CollectionViewSource.GetDefaultView(PatientsListView);
            this.defaultView.Filter =
                w => (istrueCont((Patient)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }


        //PRETRAGA PREKO POLJA
        private void refreshPageWithTextBox(object sender, System.Windows.Input.KeyEventArgs e)
        {
            PatientsListView = new ObservableCollection<Patient>(userController.GetAllPatients());

            // ZA FILTER:
            this.defaultView = CollectionViewSource.GetDefaultView(PatientsListView);
            this.defaultView.Filter =
                w => (istrueCont((Patient)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }





        //DODATNE METODE
        private bool istrueCont(Patient w)
        {
            if (w.Name.Contains(forsearch.Text) && w.Surname.Contains(forsearchSurname.Text)
                && w.PersonalIDnumber.Contains(forsearchPersonal.Text)) { return true; }
            else { return false; }
        }


        private void filterForTable()
        {
            // ZA FILTER:
            this.defaultView = CollectionViewSource.GetDefaultView(PatientsListView);
            this.defaultView.Filter =
                w => (istrueCont((Patient)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }


    }
}
