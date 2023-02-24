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
using Model;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.View.DoctorView;

namespace ProjekatBolnica.DoctorView
{
    /// <summary>
    /// Interaction logic for DoctorLogIn.xaml
    /// </summary>
    public partial class DoctorLogIn : Window
    {
        public DoctorLogIn()
        {
            InitializeComponent();
        }

        public void LogIn(object sender, RoutedEventArgs e)
        {
            Language language;

            var app = Application.Current as App;

            if(languageComboBox.SelectedIndex == 0)
            {
                language = Backend.Model.DoctorModel.Language.English;
            } else
            {
                language = Backend.Model.DoctorModel.Language.Serbian;
            }

            Doctor LoggedInDoctor = (Doctor)app.UserController.Login(usernameTextBox.Text, passwordBox.Password, false);
            
            if(LoggedInDoctor != null)
            { 
                (Application.Current.MainWindow as MainWindow).LoggedInDoctor = LoggedInDoctor;
                (Application.Current.MainWindow as MainWindow).Theme = Theme.Light;
                (Application.Current.MainWindow as MainWindow).Lang = language;
                LoggedInDoctor.Language = language;
                LoggedInDoctor.Theme = Theme.Light;
                DoctorMainPage mainPage = new DoctorMainPage(LoggedInDoctor);
                this.Visibility = Visibility.Hidden;
                mainPage.Show();
            } else
            {
                errorMessage.Visibility = Visibility.Visible;
            }
            
        }
        private void openComboBox(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                comboBox.IsDropDownOpen = true;
            }
        }
    }
}
