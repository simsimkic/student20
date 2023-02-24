/***********************************************************************
 * Module:  Survey.cs
 * Purpose: Definition of the Class Model.PatientModel.Survey
 ***********************************************************************/

using ProjekatBolnica.Backend.Repository;
using System;
using System.Collections.Generic;
using System.Data.Odbc;

namespace Model.PatientModel
{
   public class Survey : IIdentifiable<long>
    {

        public long Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public List<QuestionsAndAnswers> QuestionsAndAnswers { get; set; }


        public Survey()
        {

        }
        public Survey(long id, String name, String description, List<QuestionsAndAnswers> questionsAndAnswers)
        {
            Id = id; 
            Name = name;
            Description = description;
            QuestionsAndAnswers = questionsAndAnswers;
        }

        public Survey(String name, String description, List<QuestionsAndAnswers> questionsAndAnswers)
        {
            Name = name;
            Description = description;
            QuestionsAndAnswers = questionsAndAnswers;
        }

        public long GetId() => Id;

        public void SetId(long id) => Id = id;
    }
}