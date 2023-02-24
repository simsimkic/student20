using System;
using System.Collections.Generic;
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
using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.DoctorView;

namespace ProjekatBolnica.View.DoctorView
{
    /// <summary>
    /// Interaction logic for HospitalStayDialog.xaml
    /// </summary>
    public partial class HospitalStayDialog : Window
    {
        public Patient SelectedPatient { get; set; }
        public HospitalStay HospitalStay { get; set; }
        private readonly IHospitalStayController _hospitalStayController;
        public HospitalStayDialog(Patient patient)
        {
            InitializeComponent();
            if ((Application.Current.MainWindow as MainWindow).Theme == Theme.Dark) { dialogGrid.Background = Brushes.LightGray; }
            this.DataContext = this;
            var app = Application.Current as App;
            _hospitalStayController = app.HospitalStayController;
            SelectedPatient = patient;
            HospitalStay = _hospitalStayController.GetByPatient(SelectedPatient);
        }

        private void closeHospitalStayDialog(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void openRelocatePatientDialog(object sender, RoutedEventArgs e)
        {
            RelocatePatientDialog dialog = new RelocatePatientDialog(SelectedPatient);
            dialog.ShowDialog();
            this.Close();
        }

        private void releasePatient(object sender, RoutedEventArgs e)
        {
            _hospitalStayController.Delete(HospitalStay);
            this.Close();
        }
    }
}
