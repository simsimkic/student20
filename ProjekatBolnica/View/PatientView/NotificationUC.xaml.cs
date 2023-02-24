using Model.UtilityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// <summary>
    /// Interaction logic for NotificationUC.xaml
    /// </summary>
    public partial class NotificationUC : UserControl
    {
        public NotificationUC()
        {
            InitializeComponent();
            DataContext = this;
        }

        private String _notification;
        private DateTime _date;


        public String NotificationText
        {
            get { return _notification; }
            set { if (_notification!=value)
                        _notification = value;
                OnPropertyChanged();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
