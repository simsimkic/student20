/***********************************************************************
 * Module:  LanguageService.cs
 * Purpose: Definition of the Class Service.UserService.LanguageService
 ***********************************************************************/

using Model.UserModel;
using Repository.UserRepository;
using Service.HospitalServices;
using System;
using System.Collections.Generic;

namespace Service.UserService
{
   public class LanguageService : IService<Language, long>
   {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }


        public void Set(Language language) => _languageRepository.Set(language);


        public Language Get(long id) => _languageRepository.Get(id);


        public IEnumerable<Language> GetAll() => _languageRepository.GetAll();


        public void New(Language language) => _languageRepository.New(language);


        public void Delete(Language language) => _languageRepository.Delete(language);




    }
}