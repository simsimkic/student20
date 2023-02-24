using Model.PatientModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using Repository.HospitalRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatBolnica.Backend.Repository.PatientRepository
{
    class QuestionsAndAnswersRepository : CSVRepository<QuestionsAndAnswers, long>,
        IRepository<QuestionsAndAnswers, long>,
        IEagerCSVRepository<QuestionsAndAnswers, long>
    {

        private const string ENTITY_NAME = "QuestionsAndAnswers";

        public QuestionsAndAnswersRepository(ICSVStream<QuestionsAndAnswers> stream, ISequencer<long> sequencer
          ) : base(ENTITY_NAME, stream, sequencer)
        {

        }

        public new IEnumerable<QuestionsAndAnswers> Find(Func<QuestionsAndAnswers, bool> predicate)
        => GetAllEager().Where(predicate);

        public IEnumerable<QuestionsAndAnswers> GetAllEager()
        {
            var questionAndAnswers = GetAll();
            return questionAndAnswers;
        }

        public QuestionsAndAnswers GetEager(long id)
        {
            var questionAndAnswers = Get(id);
            return questionAndAnswers;
        }
    }
}
