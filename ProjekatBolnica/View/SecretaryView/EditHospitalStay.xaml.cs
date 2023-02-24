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

namespace ProjekatBolnica.SecretaryView
{
    /// <summary>
    /// Interaction logic for EditHospitalStay.xaml
    /// </summary>
    public partial class EditHospitalStay : Window
    {
        public EditHospitalStay()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void cancelEditHospitalStay(object sender, RoutedEventArgs e)
        {
            //this.Visibility = Visibility.Hidden;
            this.Close();
        }
    }
}
