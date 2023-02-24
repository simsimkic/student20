using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SurvayQuestions.xaml
    /// </summary>
    public partial class SurvayQuestions : UserControl
    {
        public SurvayQuestions()
        {
            InitializeComponent();
            
            Answers = new ObservableCollection<string>();
        }

        private String _question;


        public String QuestionText
        {
            get { return _question; }
            set
            {
                if (_question != value)
                    _question = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<String> Answers
        {
            get;
            set;
        }




        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
