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
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.DoctorView;

namespace ProjekatBolnica.View.DoctorView
{
    /// <summary>
    /// Interaction logic for NotificationDialog.xaml
    /// </summary>
    public partial class NotificationDialog : Window
    {
        public Notification Notification { get; set; }
        public NotificationDialog(Notification notification)
        {
            InitializeComponent();
            this.DataContext = this;
            Notification = notification;
            if ((Application.Current.MainWindow as MainWindow).Theme == Theme.Dark)
            {
                dialogGrid.Background = Brushes.LightGray;
            }
        }

        private void closeNotificationDialog(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
