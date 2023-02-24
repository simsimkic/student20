using Model;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.DoctorView;
using ProjekatBolnica.ManagerView;
using ProjekatBolnica.PatientView;
using ProjekatBolnica.SecretaryView;
using ProjekatBolnica.View.DoctorView;
using System.Windows;

namespace ProjekatBolnica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Language Lang { get; set; }
        public Theme Theme { get; set; }
        public Doctor LoggedInDoctor { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void OpenManagerView(object sender, RoutedEventArgs e)
        {
            ManagerLogIn manager = new ManagerLogIn();
            manager.Show();
            this.Close();
        }

        private void openDoctor(object sender, RoutedEventArgs e)
        {
            DoctorLogIn doctor = new DoctorLogIn();
            doctor.Show();
            this.Hide();
        }

        private void openPatient(object sender, RoutedEventArgs e)
        {
            PatientLogin patient = new PatientLogin();
            patient.Show();
            this.Close();
        }


        private void openSecretary(object sender, RoutedEventArgs e)
        {
            SecretaryLogIn secretary = new SecretaryLogIn();
            //this.Visibility = Visibility.Hidden;
            secretary.Show();
            this.Close();
        }


    }
}
