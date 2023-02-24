/***********************************************************************
 * Module:  LanguageRepository.cs
 * Purpose: Definition of the Class Repository.UserRepository.LanguageRepository
 ***********************************************************************/

using Model.UserModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using Repository.HospitalRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.UserRepository
{
   public class LanguageRepository : CSVRepository<Language, long>, ILanguageRepository, IEagerCSVRepository<Language, long>
    {
        private const string ENTITY_NAME = "Language";
        private readonly IEagerCSVRepository<Language, long> _repository;
        private String Path;


        public LanguageRepository(ICSVStream<Language> stream, ISequencer<long> sequencer,
           IEagerCSVRepository<Language, long> repository) : base(ENTITY_NAME, stream, sequencer)
        {
            _repository = repository;
        }


        public new IEnumerable<Language> Find(Func<Language, bool> predicate) => GetAllEager().Where(predicate);
        

        public Language GetEager(long id) => Get(id);
        

        public IEnumerable<Language> GetAllEager() => GetAll();      
   
   }
}