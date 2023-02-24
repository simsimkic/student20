using Model.PatientModel;
using ProjekatBolnica.Backend.Repository.PatientRepository;
using Service.HospitalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatBolnica.Backend.Service.PatientServices
{
    class QuestionsAndAnswersService : IService<QuestionsAndAnswers, long>
    {
        private readonly QuestionsAndAnswersRepository _repository;

        public QuestionsAndAnswersService(QuestionsAndAnswersRepository repository)
        {
            _repository = repository;
            // _service = service;
        }

        public void Delete(QuestionsAndAnswers questionsAndAnswers) => _repository.Delete(questionsAndAnswers);

        public QuestionsAndAnswers Get(long id) => _repository.Get(id);

        public IEnumerable<QuestionsAndAnswers> GetAll() => _repository.GetAll();

        public void New(QuestionsAndAnswers questionsAndAnswers) => _repository.New(questionsAndAnswers);

        public void Set(QuestionsAndAnswers questionsAndAnswers) => _repository.Set(questionsAndAnswers);

    }
}
