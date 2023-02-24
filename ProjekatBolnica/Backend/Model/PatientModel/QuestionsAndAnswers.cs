
using ProjekatBolnica.Backend.Repository;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;

namespace Model.PatientModel
{
   public class QuestionsAndAnswers : IIdentifiable<long>
    {
        public long Id { get; set; }
        public String Question { get; set; }
        public List<String> PossibleAnswers { get; set; }
        public String SelectedAnswer { get; set; }



        public QuestionsAndAnswers(long id, String question, List<String> possibleAnswers, String selectedAnswer)
        {
            Id = id;
            Question = question;
            PossibleAnswers = possibleAnswers;
            SelectedAnswer = selectedAnswer;
    }

        public QuestionsAndAnswers(String question, List<String> possibleAnswers, String selectedAnswer)
        {
            Question = question;
            PossibleAnswers = possibleAnswers;
            SelectedAnswer = selectedAnswer;
        }

        public QuestionsAndAnswers(long id)
        {
            Id = id;
        }

        public QuestionsAndAnswers()
        {
        }

        public long GetId() => Id;

        public void SetId(long id) => Id = id;
    }
}