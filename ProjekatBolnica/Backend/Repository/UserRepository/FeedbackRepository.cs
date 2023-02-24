/***********************************************************************
 * Module:  Class6.cs
 * Purpose: Definition of the Class Repository.PatientRepository.Class6
 ***********************************************************************/

using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.UserModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using System;
using System.Collections.Generic;

namespace Repository.UserRepository
{
    public class FeedbackRepository : CSVRepository<Feedback, long>,
        IFeedbackRepository,
        IEagerCSVRepository<Feedback, long>
    {

        private const string ENTITY_NAME = "Feedback";

        public FeedbackRepository(ICSVStream<Feedback> stream, ISequencer<long> sequencer) : base(ENTITY_NAME, stream, sequencer) { }
    

        public IEnumerable<Feedback> GetAllEager() => GetAll();


        public Feedback GetEager(long id) => Get(id);

    }
}