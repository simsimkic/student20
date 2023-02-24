using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Controller.HospitalController;
using Controller.ManagerController;
using Controller.UserController;
using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.PatientModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Controller.PatientController;
using ProjekatBolnica.Backend.Controller.UserController;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.View.DoctorView;
using Syncfusion.Linq;
using Xamarin.Forms.Internals;

namespace ProjekatBolnica.DoctorView
{
    /// <summary>
    /// Interaction logic for DoctorMainPage.xaml
    /// </summary>
    public partial class DoctorMainPage : Window, INotifyPropertyChanged
    {
        private const int MIN_PASS_LENGTH = 8; 
        private Doctor _loggedInDoctor;
        public Doctor LoggedInDoctor
        {
            get
            {
                return _loggedInDoctor;
            }
            set
            {
                if (value != _loggedInDoctor)
                {
                    _loggedInDoctor = value;
                    OnPropertyChanged("LoggedInDoctor");
                }
            }
        }
        public ObservableCollection<Operation> Operations { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public ObservableCollection<Notification> Notifications { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Medicine> ApprovedMedicine { get; set; }
        public ObservableCollection<Medicine> UnapprovedMedicine { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private Patient _selectedPatient;
        private ObservableCollection<MedicalRecord> _selectedPatientMedicalRecords;
        public Patient SelectedPatient
        {
            get
            {
                return _selectedPatient;
            }
            set
            {
                if (value != _selectedPatient)
                {
                    _selectedPatient = value;
                    OnPropertyChanged("SelectedPatient");
                }
            }
        }
        public ObservableCollection<MedicalRecord> SelectedPatientMedicalRecords
        {
            get
            {
                return _selectedPatientMedicalRecords;
            }
            set
            {
                if(value != _selectedPatientMedicalRecords)
                {
                    _selectedPatientMedicalRecords = value;
                    OnPropertyChanged("SelectedPatientMedicalRecords");
                }
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private readonly IAppointmentController _appointmentController;
        private readonly IOperationController _operationController;
        private readonly INotificationController _notificationController;
        private readonly IUserController _userController;
        private readonly IHospitalStayController _hospitalStayController;
        private readonly IMedicalRecordsController _medicalRecordController;
        private readonly IMedicineController _medicineController;
        public DoctorMainPage(Doctor doctor)
        {
            LoggedInDoctor = doctor;
            setUpLanguage();
            InitializeComponent();
            setUpGUI();
            this.DataContext = this;

            var app = Application.Current as App;
            _appointmentController = app.AppointmentController;
            _operationController = app.OperationController;
            _notificationController = app.NotificationController;
            _userController = app.UserController;
            _hospitalStayController = app.HospitalStayController;
            _medicalRecordController = app.MedicalRecordController;
            _medicineController = app.MedicineController;
            

            Appointments = new ObservableCollection<Appointment>(_appointmentController.GetActivityByDoctor(LoggedInDoctor));
            Operations = new ObservableCollection<Operation>(_operationController.GetActivityByDoctor(LoggedInDoctor));
            Patients = new ObservableCollection<Patient>(_userController.GetAllPatients());
            Notifications = new ObservableCollection<Notification>(_notificationController.GetNotificationsForUser(LoggedInDoctor));
            ApprovedMedicine = new ObservableCollection<Medicine>(_medicineController.GetAll());
            UnapprovedMedicine = new ObservableCollection<Medicine>(_medicineController.GetAllUnapproved());

        }
        private void setUpLanguage()
        {
            if (LoggedInDoctor.Language == Backend.Model.DoctorModel.Language.Serbian)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("sr-Latn");
            } else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            }
        }
        private void setUpGUI()
        {
            setUpTheme();
            setUpRestrictions();
        }

        private void setUpTheme()
        {
            if(LoggedInDoctor.Theme == Theme.Dark)
            {
                setDarkTheme();
                themeComboBox.SelectedIndex = 1;
            } else if(LoggedInDoctor.Theme == Theme.Light)
            {
                setLightTheme();
                themeComboBox.SelectedIndex = 0;
            }
        }

        private void setUpRestrictions()
        {
            if(LoggedInDoctor.Role == Model.UserModel.TypeOfUser.Doctor)
            {
                surgeryButton.IsEnabled = false;
                operationsPanel.Visibility = Visibility.Hidden;
            }
        }

        private void setDarkTheme()
        {
            (Application.Current.MainWindow as MainWindow).Theme = Theme.Dark;
            mainGrid.Background = Brushes.DimGray;
            basicInformation.Background = Brushes.LightGray;
            notificationsLabel.Background = Brushes.LightGray;
            generalSettingsLabel.Background = Brushes.LightGray;
            generalSettings.Background = Brushes.LightGray;
            passwordSettingsLabel.Background = Brushes.LightGray;
            passwordSettings.Background = Brushes.LightGray;
            patientSearchHeader.Background = Brushes.LightGray;
            pastMedicalRecordsLabel.Background = Brushes.LightGray;
            pastMedicalRecords.Background = Brushes.LightGray;
            patientInfo.Background = Brushes.LightGray;
            scheduleHeader.Background = Brushes.LightGray;
            appointmentsPanel.Background = Brushes.LightGray;
            operationsPanel.Background = Brushes.LightGray;
            medicineSearchHeader.Background = Brushes.LightGray;
            unapprovedMedicineHeader.Background = Brushes.LightGray;
            reportHeader.Background = Brushes.LightGray;
            reportTextBlock.Background = Brushes.LightGray;
        }

        private void setLightTheme()
        {
            (Application.Current.MainWindow as MainWindow).Theme = Theme.Light;
            mainGrid.Background = Brushes.White;
            basicInformation.Background = Brushes.AliceBlue;
            notificationsLabel.Background = Brushes.AliceBlue;
            generalSettingsLabel.Background = Brushes.AliceBlue;
            generalSettings.Background = Brushes.White;
            passwordSettingsLabel.Background = Brushes.AliceBlue;
            passwordSettings.Background = Brushes.White;
            patientSearchHeader.Background = Brushes.AliceBlue;
            pastMedicalRecordsLabel.Background = Brushes.AliceBlue;
            pastMedicalRecords.Background = Brushes.White;
            patientInfo.Background = Brushes.AliceBlue;
            scheduleHeader.Background = Brushes.AliceBlue;
            appointmentsPanel.Background = Brushes.AliceBlue;
            operationsPanel.Background = Brushes.AliceBlue;
            medicineSearchHeader.Background = Brushes.AliceBlue;
            unapprovedMedicineHeader.Background = Brushes.AliceBlue;
            reportHeader.Background = Brushes.AliceBlue;
            reportTextBlock.Background = Brushes.White;
        }

        private void tabButton_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "calendarTabButton":
                    loadActivities();
                    break;
                case "accountTabButton":
                    loadNotifications();
                    break;
                case "patientsTabButton":
                    break;
                case "medicineTabButton":
                    break;
            }
            renderTabs(sender);
            string buttonName = ((Button)sender).Name;
            string gridName = buttonToGrid(buttonName);
            displayTab(gridName);
        }
        private void loadActivities()
        {
            Appointments.Clear();
            foreach (Appointment ap in _appointmentController.GetActivityByDoctor(LoggedInDoctor))
            {
                Appointments.Add(ap);
            }
            Operations.Clear();
            foreach (Operation op in _operationController.GetActivityByDoctor(LoggedInDoctor))
            {
                Operations.Add(op);
            }
        }
        private void loadNotifications()
        {
            Notifications.Clear();
            foreach(Notification notification in _notificationController.GetNotificationsForUser(LoggedInDoctor))
            {
                Notifications.Add(notification);
            }
        }

        private void renderTabs(object sender)
        {
            foreach (System.Windows.Controls.ListViewItem item in menuList.Items)
            {
                item.Background = System.Windows.Media.Brushes.Transparent;
            }

            Button tabButton = (Button)sender;
            Grid parent = (Grid)tabButton.Parent;
            ListViewItem grandParent = (ListViewItem)parent.Parent;
            grandParent.Background = System.Windows.Media.Brushes.CornflowerBlue;
        }

        private string buttonToGrid(string buttonName)
        {
            StringBuilder gridName = new StringBuilder(buttonName.Substring(0, buttonName.Length - 6));
            gridName.Append("Grid");
            return gridName.ToString();

        }

        private void displayTab(string tab)
        {
            hideOtherTabs(tab);
        }

        private void hideOtherTabs(string tab)
        {
            foreach (UIElement grid in mainGrid.Children)
            {
                if (!((Grid)grid).Name.Equals(tab))
                {
                    grid.Visibility = Visibility.Hidden;
                }
                else
                {
                    grid.Visibility = Visibility.Visible;
                }
            }
        }

        private void displayPatientsList(object sender, RoutedEventArgs e)
        {
            patientMedicalRecord.Visibility = Visibility.Hidden;
            patientsList.Visibility = Visibility.Visible;
        }

        private void displayMedicalRecords()
        {
            patientMedicalRecord.Visibility = Visibility.Visible;
            patientsList.Visibility = Visibility.Hidden;
        }

        private void openPatient(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    SelectedPatient = (Patient)row.Item;
                    SelectedPatientMedicalRecords = new ObservableCollection<MedicalRecord>(_medicalRecordController.GetMedicalRecordByPatient(SelectedPatient));
                    addMedicalRecords();
                    displayMedicalRecords();
                    break;
                }
        }
        private  void addMedicalRecords()
        {
            StringBuilder sb = new StringBuilder();
            foreach(MedicalRecord mr in SelectedPatientMedicalRecords.Reverse())
            {
                sb.Append(mr.ToString());
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
            }
            pastMedicalRecords.Text = sb.ToString();
        }
        private void openUnapprovedMedicine(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    Medicine medicine = (Medicine)row.Item;
                    MedicineReviewDialog dialog = new MedicineReviewDialog(medicine);
                    dialog.ShowDialog();
                    break;
                }
        }

        private void displayAccountSettings(object sender, RoutedEventArgs e)
        {
            accountInfo.Visibility = Visibility.Hidden;
            accountSettings.Visibility = Visibility.Visible;
        }

        private void displayAccountInfo(object sender, RoutedEventArgs e)
        {
            accountSettings.Visibility = Visibility.Hidden;
            accountInfo.Visibility = Visibility.Visible;
            resetAccountSettings();
        }

        private void resetAccountSettings()
        {
            nameSettingTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            surnameSettingTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            emailSettingTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            telephoneSettingTextBox.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
            newPassPasswordBox.Clear();
            repeatPassPasswordBox.Clear();
            emailSettingTextBox.ClearValue(Border.BorderBrushProperty);
            telephoneSettingTextBox.ClearValue(Border.BorderBrushProperty);
            if (LoggedInDoctor.Theme == Theme.Light)
            {
                themeComboBox.SelectedIndex = 0;
            } else if(LoggedInDoctor.Theme == Theme.Dark)
            {
                themeComboBox.SelectedIndex = 1;
            }
        }

        private void openMedicineDetails(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    Medicine medicine = (Medicine)row.Item;
                    MedicineDetailsDialog dialog = new MedicineDetailsDialog(medicine);
                    dialog.ShowDialog();
                    break;
                }
        }

        private void openStationaryTreatmentDialog(object sender, RoutedEventArgs e)
        {
            if (!_hospitalStayController.isHospitalized(SelectedPatient))
            {
                HospitalizePatientDialog dialog = new HospitalizePatientDialog(SelectedPatient);
                dialog.ShowDialog();
            }
            else
            {
                HospitalStayDialog dialog = new HospitalStayDialog(SelectedPatient);
                dialog.ShowDialog();
            }
        }

        private void openAppointmentDialog(object sender, RoutedEventArgs e)
        {
            AppointmentDialog dialog = new AppointmentDialog(SelectedPatient);
            dialog.ShowDialog();
        }

        private void openOperationDialog(object sender, RoutedEventArgs e)
        {
            OperationDialog dialog = new OperationDialog(SelectedPatient);
            dialog.ShowDialog();
        }

        private void openVacationRequestDialog(object sender, RoutedEventArgs e)
        {
            VacationRequestDialog dialog = new VacationRequestDialog();
            dialog.ShowDialog();
        }

        private void openSpecialistRefferalDialog(object sender, RoutedEventArgs e)
        {
            SpecialistReferralDialog dialog = new SpecialistReferralDialog(SelectedPatient);
            dialog.ShowDialog();
        }

        private void openComboBox(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                comboBox.IsDropDownOpen = true;
            }
        }

        private void openCalendar(object sender, KeyEventArgs e)
        {
            DatePicker picker = (DatePicker)sender;
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                picker.IsDropDownOpen = true;
            }
        }

        private void activityViewSlecetionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (monthlyView.IsSelected)
            {
                activityViewPicker.IsEnabled = true;
                activityViewPicker.Format = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
                activityViewPicker.FormatString = "MMMM yyyy";
                activityViewPicker.Minimum = System.DateTime.Today;
            }
            else if (dailyView.IsSelected)
            {
                activityViewPicker.IsEnabled = true;
                activityViewPicker.Format = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
                activityViewPicker.FormatString = "dd MMMM yyyy";
                activityViewPicker.Minimum = System.DateTime.Today;
            } else
            {
                activityViewPicker.IsEnabled = false;
            }
        }

        private void openNotification(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    Notification notification = (Notification)row.Item;
                    NotificationDialog dialog = new NotificationDialog(notification);
                    dialog.ShowDialog();
                    break;
                }
        }

        private void openAppointment(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    Appointment appointment = (Appointment)row.Item;
                    OpenAppointment dialog = new OpenAppointment(appointment);
                    dialog.ShowDialog();
                    break;
                }
        }

        private void deleteAppointment(object sender, RoutedEventArgs e)
        {
            string titleSrb = "Brisanje pregleda";
            string titleEng = "Delete appointment";
            string textSrb = "Da li ste sigurni da želite da obrišete pregled?";
            string textEng = "Are you sure you want to delete this appointment?";
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    Appointment appointment = (Appointment)row.Item;
                    MessageBoxResult messageBoxResult;
                    if ((Application.Current.MainWindow as MainWindow).Lang == Backend.Model.DoctorModel.Language.English)
                    {
                        messageBoxResult = MessageBox.Show(textEng, titleEng, MessageBoxButton.OKCancel);
                    } else
                    {
                        messageBoxResult = MessageBox.Show(textSrb, titleSrb, MessageBoxButton.OKCancel);
                    }
                    if(messageBoxResult == MessageBoxResult.OK)
                    {
                        Appointments.Remove(appointment);
                        _appointmentController.Delete(appointment);
                    }
                    break;
                }
        }

        private void openOperation(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    Operation operation = (Operation)row.Item;
                    OpenOperation dialog = new OpenOperation(operation);
                    dialog.ShowDialog();
                    break;
                }
        }

        private void selectPatientForReport(object sender, RoutedEventArgs e)
        {
            patientForReport.SelectedItem = SelectedPatient;
            reportTabButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void logOut(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void changeTheme(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if(comboBox.SelectedIndex == 0)
            {
                setLightTheme();
            } else if(comboBox.SelectedIndex == 1)
            {
                setDarkTheme();
            }
        }

        private void passChanged(object sender, RoutedEventArgs e)
        {
            if(newPassPasswordBox.Password == repeatPassPasswordBox.Password && newPassPasswordBox.Password.Length >= MIN_PASS_LENGTH)
            {
                changePassButton.IsEnabled = true;
            } else
            {
                changePassButton.IsEnabled = false;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void settingsChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            if(box.Text == "")
            {
                saveChangesButton.IsEnabled = false;
            } else
            {
                saveChangesButton.IsEnabled = true;
            }
        }

        private void saveSettings(object sender, RoutedEventArgs e)
        {
            string name = nameSettingTextBox.Text;
            string lastName = surnameSettingTextBox.Text;
            string email = emailSettingTextBox.Text;
            string phone = telephoneSettingTextBox.Text;

            string emailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            Regex emailRegex = new Regex(emailPattern, RegexOptions.IgnoreCase);
            if (!emailRegex.IsMatch(email))
            {
                emailSettingTextBox.BorderBrush = Brushes.Red;
                return;
            } else
            {
                emailSettingTextBox.ClearValue(Border.BorderBrushProperty);
            }

            string phonePattern = @"^\+?[0-9]{6,20}$";
            Regex phoneRegex = new Regex(phonePattern);
            if (!phoneRegex.IsMatch(phone))
            {
                telephoneSettingTextBox.BorderBrush = Brushes.Red;
                return;
            } else
            {
                telephoneSettingTextBox.ClearValue(Border.BorderBrushProperty);
            }

            LoggedInDoctor.Name = name;
            LoggedInDoctor.Surname = lastName;
            LoggedInDoctor.Email = email;
            LoggedInDoctor.Telephone = phone;
            _userController.Set(LoggedInDoctor);
            LoggedInDoctor = (Doctor)_userController.Get(LoggedInDoctor.Id);
            if (themeComboBox.SelectedIndex == 0)
            {
                LoggedInDoctor.Theme = Theme.Light;
                (Application.Current.MainWindow as MainWindow).Theme = Theme.Light;
            }
            else
            {
                LoggedInDoctor.Theme = Theme.Dark;
                (Application.Current.MainWindow as MainWindow).Theme = Theme.Dark;
            }
        }

        private void showReportRecords(object sender, RoutedEventArgs e)
        {
            DateTime startDate = recordStartDate.SelectedDate.Value.Date;
            DateTime endDate = recordEndDate.SelectedDate.Value.Date;

            Patient patient = (Patient)patientForReport.SelectedItem;

            StringBuilder report = new StringBuilder();
            
            foreach(MedicalRecord record in _medicalRecordController.GetMedicalRecordByPatient(patient))
            {
                if(record.ExamDate.Date >= startDate && record.ExamDate.Date <= endDate)
                {
                    report.Append(record.ToString());
                }
            }

            reportText.Text = report.ToString();
        }

        private void reportDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (recordStartDate.SelectedDate.HasValue && recordEndDate.SelectedDate.HasValue)
            {
                showReport.IsEnabled = true;
            } else
            {
                showReport.IsEnabled = false;
            }
        }

        private void deleteNotification(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    Notification notification = (Notification)row.Item;
                    Notifications.Remove(notification);
                    _notificationController.Delete(notification);
                    break;
                }
        }

        private void deleteOperation(object sender, RoutedEventArgs e)
        {
            string titleSrb = "Brisanje operacije";
            string titleEng = "Delete operation";
            string textSrb = "Da li ste sigurni da želite da obrišete operaciju?";
            string textEng = "Are you sure you want to delete this operation?";
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                { 
                    var row = (DataGridRow)vis;
                    Operation operation = (Operation)row.Item;
                    MessageBoxResult messageBoxResult;
                    if ((Application.Current.MainWindow as MainWindow).Lang == Backend.Model.DoctorModel.Language.English)
                    {
                        messageBoxResult = MessageBox.Show(textEng, titleEng, MessageBoxButton.OKCancel);
                    }
                    else
                    {
                        messageBoxResult = MessageBox.Show(textSrb, titleSrb, MessageBoxButton.OKCancel);
                    }
                    if (messageBoxResult == MessageBoxResult.OK)
                    {
                        Operations.Remove(operation);
                        _operationController.Delete(operation);
                    }
                    break;
                }
        }

        private void filterActivities(object sender, RoutedEventArgs e)
        {
            if(activityPeriodSelection.SelectedIndex == 0)
            {
                ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>(_appointmentController.GetActivityByDoctor(LoggedInDoctor));
                Appointments.Clear();
                foreach(Appointment app in appointments)
                {
                    Appointments.Add(app);
                }
                ObservableCollection<Operation> operations = new ObservableCollection<Operation>(_operationController.GetActivityByDoctor(LoggedInDoctor));
                Operations.Clear();
                foreach (Operation op in operations)
                {
                    Operations.Add(op);
                }
            }
            if(activityPeriodSelection.SelectedIndex == 1)
            {
                if (activityViewPicker.Value.HasValue)
                {
                    DateTime date = activityViewPicker.Value.Value;
                    ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
                    foreach(Appointment app in _appointmentController.GetActivityByDoctor(LoggedInDoctor))
                    {
                        if(app.DateAndTime.Year == date.Year && app.DateAndTime.Month == date.Month) { appointments.Add(app); }
                    }
                    Appointments.Clear();
                    foreach(Appointment app in appointments) { Appointments.Add(app); }
                    ObservableCollection<Operation> operations = new ObservableCollection<Operation>();
                    foreach (Operation op in _operationController.GetActivityByDoctor(LoggedInDoctor))
                    {
                        if (op.DateAndTime.Year == date.Year && op.DateAndTime.Month == date.Month) { operations.Add(op); }
                    }
                    Operations.Clear();
                    foreach (Operation op in operations) { Operations.Add(op); }
                }

            }
            if(activityPeriodSelection.SelectedIndex == 2)
            {
                if (activityViewPicker.Value.HasValue)
                {
                    DateTime date = activityViewPicker.Value.Value;
                    ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
                    foreach (Appointment app in _appointmentController.GetActivityByDoctor(LoggedInDoctor))
                    {
                        if (app.DateAndTime.Year == date.Year && app.DateAndTime.Month == date.Month && app.DateAndTime.Day == date.Day) { appointments.Add(app); }
                    }
                    Appointments.Clear();
                    foreach (Appointment app in appointments) { Appointments.Add(app); }
                    ObservableCollection<Operation> operations = new ObservableCollection<Operation>();
                    foreach (Operation op in _operationController.GetActivityByDoctor(LoggedInDoctor))
                    {
                        if (op.DateAndTime.Year == date.Year && op.DateAndTime.Month == date.Month && op.DateAndTime.Day == date.Day) { operations.Add(op); }
                    }
                    Operations.Clear();
                    foreach (Operation op in operations) { Operations.Add(op); }
                }
            }
        }

        private void changePassword(object sender, RoutedEventArgs e)
        {
            string pass = newPassPasswordBox.Password;
            LoggedInDoctor.Password = pass;
            _userController.Set(LoggedInDoctor);
        }

        private void filterPatients(object sender, RoutedEventArgs e)
        {
            if (searchPatientsQuery.Text.Equals(string.Empty))
            {
                Patients.Clear();
                foreach(Patient patient in _userController.GetAllPatients())
                {
                    Patients.Add(patient);
                }
                return;
            } else
            {
                Patients.Clear();
                foreach(Patient patient in _userController.GetAllPatients())
                {
                    if(patient.Name.Contains(searchPatientsQuery.Text) || patient.Surname.Contains(searchPatientsQuery.Text))
                    {
                        Patients.Add(patient);
                    }
                }
            }
            

        }

        private void filterMedicine(object sender, RoutedEventArgs e)
        {
            if (searchMedicineQuery.Text.Equals(string.Empty))
            {
                ApprovedMedicine.Clear();
                foreach (Medicine medicine in _medicineController.GetAll())
                {
                    ApprovedMedicine.Add(medicine);
                }
                return;
            } else
            {
                ApprovedMedicine.Clear();
                foreach (Medicine medicine in _medicineController.GetAll())
                {
                    if (medicine.Name.Contains(searchMedicineQuery.Text))
                    {
                        ApprovedMedicine.Add(medicine);
                    }
                }
            }
        }
    }
}
