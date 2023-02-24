using System;
using System.ComponentModel;

namespace ProjekatBolnica.View.PatientView.Data
{
    public class DoctorsNames : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private String _doctor;
        private long _id;


        public String DoctorName
        {
            get { return _doctor; }
            set
            {
                if (_doctor != value)
                {
                    _doctor = value;
                    RaisePropertyChanged("Doctor");
                }
            }
        }

        public long ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged("Doctor");
                }
            }
        }

    }
}
