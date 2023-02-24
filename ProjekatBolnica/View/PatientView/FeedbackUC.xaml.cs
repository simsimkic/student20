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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjekatBolnica.View.PatientView
{
   
    public partial class FeedbackUC : UserControl
    {
        
        String message;
        private readonly IUserController _patientController;
        public FeedbackUC()
        {
            InitializeComponent();
            DataContext = this;
            var app = Application.Current as App;
            _patientController = app.UserController;
        }


        private void btnSubmitFeedback_Click(object sender, RoutedEventArgs e)
        {
            if (txtFeedback.Text != "")
            {

                _patientController.AddFeedback(CreateFeedback()); 
                this.Visibility = Visibility.Collapsed;
                MessageBox.Show("You have successfully submitted your feedback .");
            }
            else {
                MessageBox.Show("Please enter your feedback before you send it ");
            }
        }

        private Feedback CreateFeedback() => new Feedback(FormatFeedback());

        private String FormatFeedback()=> message=txtFeedback.Text.Replace("\r\n", " ");
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            txtFeedback.Text = "";
        }
    }
}
