using Controller.HospitalController;
using ProjekatBolnica.Backend.Model.DoctorModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for HospitalStayPage.xaml
    /// </summary>
    public partial class HospitalStayPage : Window
    {
        private IController<HospitalStay, long> hospitalStayController;

        public static ObservableCollection<HospitalStay> HospitalStayListView
        {
            get;
            set;
        }

        private ICollectionView defaultView;



        public HospitalStayPage()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            var app = System.Windows.Application.Current as App;
            hospitalStayController = app.HospitalStayController;

            HospitalStayListView = new ObservableCollection<HospitalStay>(hospitalStayController.GetAll());

            //filter...
            this.defaultView = CollectionViewSource.GetDefaultView(HospitalStayListView);
            this.defaultView.Filter =
                w => (istrueCont((HospitalStay)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }


        private bool isNumberOfRoom(HospitalStay w)
        {
            Regex expression = new Regex(@"[0-9]+");
            if (forsearchRoom.Text.Equals("")) { return true; }
            else if (!(expression.IsMatch(forsearchRoom.Text))) { return false; }
            else if (w.Bed.Room.Id.Equals(int.Parse(forsearchRoom.Text))) { return true; }
            return false;
        }

        private void clickAddHospitalStay(object sender, RoutedEventArgs e)
        {
            AddHospitalStay addHospitalStay = new AddHospitalStay();
            addHospitalStay.ShowDialog();
        }


        private void deleteSelectedHospitalStay(object sender, RoutedEventArgs e)
        {
            HospitalStay a = (HospitalStay)defaultViewTable.SelectedItem;
            if (a != null)
            {
                string message = "Do you want to delete the hospital stay?";
                string title = "Delete";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = (DialogResult)System.Windows.MessageBox.Show(message, title, (MessageBoxButton)buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    hospitalStayController.Delete(a);
                    HospitalStayListView = new ObservableCollection<HospitalStay>(hospitalStayController.GetAll());

                    // ZA FILTER:
                    this.defaultView = CollectionViewSource.GetDefaultView(HospitalStayListView);
                    this.defaultView.Filter = w => (istrueCont((HospitalStay)w));

                    defaultViewTable.ItemsSource = this.defaultView;
                }
                else { }
            }
            else
            {
                string message = "Warning! You did not choose a hospital stay!";
                System.Windows.MessageBox.Show(message);
            }
        }

        private void clickRefreshTableHospitalStay(object sender, RoutedEventArgs e)
        {
            HospitalStayListView = new ObservableCollection<HospitalStay>(hospitalStayController.GetAll());

            // ZA FILTER:
            this.defaultView = CollectionViewSource.GetDefaultView(HospitalStayListView);
            this.defaultView.Filter = w => (istrueCont((HospitalStay)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }


        private void refreshPageWithTextBox(object sender, System.Windows.Input.KeyEventArgs e)
        {
            HospitalStayListView = new ObservableCollection<HospitalStay>(hospitalStayController.GetAll());

            // ZA FILTER:
            this.defaultView = CollectionViewSource.GetDefaultView(HospitalStayListView);
            this.defaultView.Filter = w => (istrueCont((HospitalStay)w));

            defaultViewTable.ItemsSource = this.defaultView;
        }


        private bool istrueCont(HospitalStay w)
        {
            if (w.Patient.Name.Contains(forsearch.Text) && w.Patient.Surname.Contains(forsearchSurname.Text) && w.Patient.PersonalIDnumber.Contains(forsearchPersonal.Text)
                && isNumberOfRoom(w)) { return true; }
            else { return false; }
            //return true;
        }



    }
}
