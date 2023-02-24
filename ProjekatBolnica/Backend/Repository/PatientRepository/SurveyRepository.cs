using Model.PatientModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.PatientRepository;
using ProjekatBolnica.Backend.Repository.Sequencer;
using Repository.HospitalRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.PatientRepository
{
    public class SurveyRepository : CSVRepository<Survey, long>,
        IRepository<Survey, long>,
        IEagerCSVRepository<Survey, long>
    {
        private const string ENTITY_NAME = "Survey";
        private readonly IEagerCSVRepository<QuestionsAndAnswers, long> _repository;

        public SurveyRepository(ICSVStream<Survey> stream, ISequencer<long> sequencer,
           IEagerCSVRepository<QuestionsAndAnswers, long> repository) : base(ENTITY_NAME, stream, sequencer)
        {
            _repository = repository;
        }

        public new IEnumerable<Survey> Find(Func<Survey, bool> predicate)
        => GetAllEager().Where(predicate);

        public Survey GetEager(long id)
        {
            Survey questionsAndAnswers = new Survey();
            var survey = Get(id);

            foreach (QuestionsAndAnswers qna in survey.QuestionsAndAnswers)
                questionsAndAnswers.QuestionsAndAnswers.Add(_repository.GetEager(qna.Id));
            survey.QuestionsAndAnswers = questionsAndAnswers.QuestionsAndAnswers;
            return survey;

        }

        public IEnumerable<Survey> GetAllEager()
        {
            List<Survey> surveys = new List<Survey>();
            foreach (Survey survey in GetAll())
            {
                surveys.Add(GetEager(survey.Id));
            }
            return surveys;
        }



    }
}