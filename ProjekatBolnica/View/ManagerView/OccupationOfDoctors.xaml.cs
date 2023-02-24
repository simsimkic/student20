using Controller.ManagerController;
using Controller.UserController;
using Model;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ProjekatBolnica.ManagerView
{
    /// <summary>
    /// Interaction logic for OccupationOfDoctors.xaml
    /// </summary>
    public partial class OccupationOfDoctors : Window
    {
        private readonly UserController userController;
        private readonly WorkHoursForDoctorController workHoursForDoctorController;
        public static ObservableCollection<Doctor> Doctors
        {
            get;
            set;
        }

        public OccupationOfDoctors()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            var app = Application.Current as App;
            userController = (UserController)app.UserController;
            workHoursForDoctorController = (WorkHoursForDoctorController)app.WorkHoursForDoctorController;

            Doctors = new ObservableCollection<Doctor>(userController.GetAllDoctors());
        }

        private void OpenHelp(object sender, RoutedEventArgs e)
        {
            Help help = new Help();
            this.Close();
            help.Show();
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (inputAll.IsChecked == true)
            {
                inputId.SelectAll();
            }
        }

        private void OpenResources(object sender, RoutedEventArgs e)
        {
            Resources resources = new Resources();
            this.Close();
            resources.Show();
        }

        private void OpenAddingANewMedicine(object sender, RoutedEventArgs e)
        {
            AddingANewMedicine medicine = new AddingANewMedicine();
            this.Close();
            medicine.Show();
        }

        private void OpenManagerView(object sender, RoutedEventArgs e)
        {
            ManagerLogIn manager = new ManagerLogIn();
            this.Close();
            manager.Show();
        }

        private void OpenChoosingADoctorForOvertimeWork(object sender, RoutedEventArgs e)
        {
            ChoosingADoctorForOvertimeWork overtime = new ChoosingADoctorForOvertimeWork();
            this.Close();
            overtime.Show();
        }

        private void OpenWorkHoursForDoctors(object sender, RoutedEventArgs e)
        {
            WorkHoursForDoctors hours = new WorkHoursForDoctors();
            this.Close();
            hours.Show();
        }

        private void OpenListOfHalls(object sender, RoutedEventArgs e)
        {
            ListOfHalls halls = new ListOfHalls();
            this.Close();
            halls.Show();
        }

        public void OpenKeyboardShortcuts(object sender, RoutedEventArgs e)
        {
            KeyboardShortcuts ks = new KeyboardShortcuts();
            this.Close();
            ks.Show();
        }

        private void OpenManagerProfile(object sender, RoutedEventArgs e)
        {
            ManagerProfile profile = new ManagerProfile();
            this.Close();
            profile.Show();
        }

        private void OpenNotifications(object sender, RoutedEventArgs e)
        {
            Notifications notifications = new Notifications();
            this.Close();
            notifications.Show();
        }
        private void OpenListOfEmployees(object sender, RoutedEventArgs e)
        {
            ManagerMainPage list = new ManagerMainPage();
            this.Close();
            list.Show();
        }

        private void OpenOccupationOfDoctors(object sender, RoutedEventArgs e)
        {
            OccupationOfDoctors occupation = new OccupationOfDoctors();
            this.Close();
            occupation.Show();
        }
        private void OpenActivityStatistics(object sender, RoutedEventArgs e)
        {
            ActivityStatistics statistics = new ActivityStatistics();
            this.Close();
            statistics.Show();
        }

        private void generateFile(object sender, RoutedEventArgs e)
        {
            if (inputDate.SelectedDate == null)
            {
                String message = "Warning! Yoo need to insert date!";
                MessageBox.Show(message);
                return;
            }

            String reportApp = generateStringOperations();

            using (PdfDocument document = new PdfDocument())
            {
                //Add a page to the document
                PdfPage page = document.Pages.Add();

                //Create PDF graphics for a page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

                //Draw the text 
                graphics.DrawString(reportApp, font, PdfBrushes.Black, new PointF(0, 0));
                //String b = bingPathToAppDir(@"JsonFiles\output.pdf");

                //Save the document
                document.Save("outputOp.pdf");
            }
            System.Diagnostics.Process.Start(@"outputOp.pdf");
        }

        private String generateStringOperations()
        {
            StringBuilder retVal = new StringBuilder("Report on occupation of doctors \n\n");

            String fromStr = inputDate.ToString();
            DateTime date = (DateTime)inputDate.SelectedDate;

            retVal.Append("On " + fromStr + "\n\n");

            List<Doctor> selDoctors = new List<Doctor>();
            for (int i = 0; i < inputId.SelectedItems.Count; i++)
            {
                Doctor d = (Doctor)inputId.SelectedItems[i];
                selDoctors.Add(d);
            }

            bool isWorking = false;
            foreach (Doctor d in selDoctors)
            {
                retVal.Append("->" + " Doctor " + d.Name + " " + d.Surname + " ");
                isWorking = workHoursForDoctorController.OccupationOfDoctors(date, d);

                if (isWorking) { retVal.Append("is working.\n"); }
                else { retVal.Append("is on vacation.\n"); }

                retVal.Append("\n");
                retVal.Append("\n");
            }

            return retVal.ToString();
        }
    }
}
