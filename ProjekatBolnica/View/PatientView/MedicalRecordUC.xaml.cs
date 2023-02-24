using Model.ManagerModel;
using Model.PatientModel;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ProjekatBolnica.View.PatientView
{
    public partial class MedicalRecordUC : UserControl
    {
        private String _perscription;
        private String _doctor;
        private DateTime _examDate;
        private String _examReport;
        private String _typeOfRecord;

        private MedicalRecord medicalRecord;


        public MedicalRecordUC()
        {

            InitializeComponent();
        }

        public void LoadRecorda(MedicalRecord m)
        {
            medicalRecord = m;
            Perscription = FormatPerscription();  
            Doctor = FormatDoctor();
            ExamDate = medicalRecord.ExamDate;
            ExamReport = medicalRecord.ExamReport;
            TypeOfRecorda = FormatTypeOfRecord();

            doctorLbl.Content = Doctor;
            perscriptionTxt.Text = Perscription;
            dateLbl.Content = ExamDate;
            reportTxt.Text = ExamReport;
            typeLbl.Content = TypeOfRecorda;

        }

        private string FormatTypeOfRecord()
        {
            return medicalRecord.TypeOfRecord.ToString();
        }

        private string FormatDoctor()
        {
            String format = "";
            format = medicalRecord.Doctor.Name + " " + medicalRecord.Doctor.Surname;
            return format;
        }

        private String FormatPerscription()
        {
            String format = "";
            foreach (Medicine medicine in medicalRecord.Perscription.Medicine)
            {
                format += "Medicine: " + medicine.Name + " MethodOfUse: " + medicine.MethodOfUse + "\n";
            }


            return format;
        }

        public String Perscription
        {
            get { return _perscription; }
            set
            {
                if (_perscription != value)
                {
                    _perscription = value;
                    OnPropertyChanged();
                }

            }
        }

        public String Doctor
        {
            get { return _doctor; }
            set
            {
                if (_doctor != value)
                {
                    _doctor = value;
                    OnPropertyChanged();
                }

            }
        }

        public DateTime ExamDate
        {
            get { return _examDate; }
            set
            {
                if (_examDate != value)
                {
                    _examDate = value;
                    OnPropertyChanged();
                }

            }
        }

        private String ExamReport
        {
            get { return _examReport; }
            set
            {
                if (_examReport != value)
                {
                    _examReport = value;
                    OnPropertyChanged();
                }

            }
        }

        private String TypeOfRecorda
        {
            get { return _typeOfRecord; }
            set
            {
                if (_typeOfRecord != value)
                {
                    _typeOfRecord = value;
                    OnPropertyChanged();
                }

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            
        }
    }
}
