using Controller.HospitalController;
using Model.PatientModel;
using Service.HospitalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatBolnica.Backend.Controller.PatientController
{
    class QuestionsAndAnwsersController : IController<QuestionsAndAnswers, long>
    {
        private readonly IService<QuestionsAndAnswers, long> _service;
        public QuestionsAndAnwsersController(IService<QuestionsAndAnswers, long> service)
        {
            _service = service;
        }



        public void Delete(QuestionsAndAnswers questionsAndAnswers) => _service.Delete(questionsAndAnswers);

        public QuestionsAndAnswers Get(long id) => _service.Get(id);

        public IEnumerable<QuestionsAndAnswers> GetAll() => _service.GetAll();

        public void New(QuestionsAndAnswers questionsAndAnswers) => _service.New(questionsAndAnswers);

        public void Set(QuestionsAndAnswers questionsAndAnswers) => _service.Set(questionsAndAnswers);
    }
}
