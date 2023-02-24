using Controller.HospitalController;
using Controller.UserController;
using Model;
using ProjekatBolnica.Backend.Model.UserModel;
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
    /// Interaction logic for FeedbackPage.xaml
    /// </summary>
    public partial class FeedbackPage : Window
    {
        private UserController userController;

        public FeedbackPage()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void cancelFeedback(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sendFeedback(object sender, RoutedEventArgs e)
        {
            String fb = impressions.Text;
            if (fb.Equals(""))
            {
                string message = "Warning! You did not enter impressions!";
                System.Windows.MessageBox.Show(message);
            }
            else
            {
                var app = Application.Current as App;
                userController = (UserController)app.UserController;

                Feedback feedback = new Feedback(fb);
                userController.AddFeedback(feedback);
                this.Close();
                string message = "Successfully!";
                System.Windows.MessageBox.Show(message);
                return;
            }
        }


    }
}
