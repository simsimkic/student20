/***********************************************************************
 * Module:  SurveyService.cs
 * Purpose: Definition of the Class Service.SurveyServices.SurveyService
 ***********************************************************************/

using Model.PatientModel;
using Repository.PatientRepository;
using Service.HospitalServices;
using System;
using System.Collections.Generic;

namespace Service.PatientServices
{
   public class SurveyService : IService<Survey, long>
    {
        private readonly SurveyRepository _repository;

        public SurveyService(SurveyRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Survey survey) => _repository.Delete(survey);

        public Survey Get(long id) => _repository.Get(id);

        public IEnumerable<Survey> GetAll() => _repository.GetAll();

        public void New(Survey survey) => _repository.New(survey);

        public void Set(Survey survey) => _repository.Set(survey);

        public QuestionsAndAnswers AddQuestionAndAnswerToSurvey(string question)
        {
            throw new NotImplementedException();
        }

        public Survey ChangeSurveryDescription(string description, Survey survey)
        {
            throw new NotImplementedException();
        }

        public Survey ChangeSurveryName(string newName, Survey survey)
        {
            throw new NotImplementedException();
        }

        public Survey Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}