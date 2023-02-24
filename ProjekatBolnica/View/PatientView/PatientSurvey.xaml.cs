using Controller.HospitalController;
using Controller.PatientController;
using Model;
using Model.PatientModel;
using ProjekatBolnica.PatientView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ProjekatBolnica.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientSurvey.xaml
    /// </summary>
    public partial class PatientSurvey : Window
    {
        public ObservableCollection<UserControl> Data { get; set; }


        Patient patient;
        private readonly IController<QuestionsAndAnswers, long> _questionsAndAnswersController;
        List<QuestionsAndAnswers> questionsAndAnswers = new List<QuestionsAndAnswers>();
        public PatientSurvey(Patient p)
        {
            InitializeComponent();
            DataContext = this;

            Data = new ObservableCollection<UserControl>();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Bar.Visibility = Visibility.Collapsed;
            patient = p;
            NotificationPanel.Children.Add(new NotificationView(patient));
            var app = Application.Current as App;
            _questionsAndAnswersController = app.QuestionsAndAnswersController;
            //survayPanel.Children.Add(new SurvayQuestions());
            //List<QuestionsAndAnswers> questionsAnds = new List<QuestionsAndAnswers>();
            //questionsAnds.AddRange(_questionsAndAnswersController.GetAll().ToList());
            List<String> answers = _questionsAndAnswersController.Get(1).PossibleAnswers.ToList();
            //LoadQuestionsAndAnswers();
            LoadQuestion();
            LoadAnswers();
            answersComboBox.ItemsSource = answers;
            answersComboBox1.ItemsSource = answers;
            answersComboBox2.ItemsSource = answers;
            answersComboBox3.ItemsSource = answers;

        }

        private void LoadQuestion()
        {
            QuestionText = _questionsAndAnswersController.Get(1).Question;
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

        private void LoadAnswers() {
            ObservableCollection<String> AnswersPom = new ObservableCollection<string>();
            foreach (String str in _questionsAndAnswersController.Get(1).PossibleAnswers)
                AnswersPom.Add(str);

            Answers = AnswersPom;



        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //private void LoadQuestionsAndAnswers()
        //{
        //    questionsAndAnswers.ForEach(qna =>
        //    DataQNA.Add(new SurvayQuestions() {
        //        QuestionText = qna.Question,
        //        Answers = LoadAnswers(qna.PossibleAnswers)
        //    }));
        //}

        //private ObservableCollection<string> LoadAnswers(List<string> possibleAnswers)
        //{
        //    ObservableCollection<string> Answers = new ObservableCollection<string>();
        //    possibleAnswers.ForEach(str => Answers.Add(str));
        //    return Answers;
        //}


        private void btnBarClk(object sender, RoutedEventArgs e)
        {

            if (Bar.Visibility == Visibility.Visible)
                Bar.Visibility = Visibility.Collapsed;
            else
                Bar.Visibility = Visibility.Visible;

        }

        private void btnAppointment(object sender, RoutedEventArgs e)
        {
            MakeAnAppointment appointment = new MakeAnAppointment(patient);
            appointment.Show();
            this.Close();

        }

        private void btnProfile(object sender, RoutedEventArgs e)
        {
            PatientProfile pp = new PatientProfile(patient);

            pp.Show();
            this.Close();
        }

        private void btnSingOut(object sender, RoutedEventArgs e)
        {
            PatientLogin login = new PatientLogin();
            login.Show();
            this.Close();

        }

        private void btnEditAppointments(object sender, RoutedEventArgs e)
        {
            AppointmentsList al = new AppointmentsList(patient);
            al.Show();
            this.Close();

        }


        private void btnRecords(object sender, RoutedEventArgs e)
        {
            PatientRecords pr = new PatientRecords(patient);
            pr.Show();
            this.Close();

        }

        private void btnSurvey(object sender, RoutedEventArgs e)
        {

            PatientSurvey ps = new PatientSurvey(patient);
            ps.Show();
            this.Close();
        }


        private void notificationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!(NotificationPanel.Visibility == Visibility.Visible))
            {

                NotificationPanel.Visibility = Visibility.Visible;
            }
            else
                NotificationPanel.Visibility = Visibility.Collapsed;
        }

        private void btnFeedback_Click(object sender, RoutedEventArgs e)
        {
            feedbackUC.Visibility = Visibility.Visible;
            feedbackPanel.Visibility = Visibility.Collapsed;
        }

        private void moreBtn_Click(object sender, RoutedEventArgs e)
        {
            if (feedbackPanel.Visibility != Visibility.Visible)
                feedbackPanel.Visibility = Visibility.Visible;
            else
                feedbackPanel.Visibility = Visibility.Collapsed;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (answersComboBox.SelectedItem != null && answersComboBox1.SelectedItem != null && answersComboBox2.SelectedItem != null && answersComboBox3.SelectedItem != null)
            {
                QuestionsAndAnswers q = _questionsAndAnswersController.Get(1);
                q.SelectedAnswer = answersComboBox1.SelectedItem.ToString();
                _questionsAndAnswersController.Set(q);

            }
            else
                MessageBox.Show("Answer all question before submition");
        }
    }

}
