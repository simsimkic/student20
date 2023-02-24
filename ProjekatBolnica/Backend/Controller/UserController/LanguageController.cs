/***********************************************************************
 * Module:  LanguageController.cs
 * Purpose: Definition of the Class Controller.UserController.LanguageController
 ***********************************************************************/

using Controller.HospitalController;
using Model.UserModel;
using Service.HospitalServices;
using Service.UserService;
using System;
using System.Collections.Generic;

namespace Controller.UserController
{
   public class LanguageController : IController<Language, long>
   {
        private readonly IService<Language, long> _service;

        public LanguageController(IService<Language, long> service)
        {
            _service = service;
        }


        public void Set(Language language) => _service.Set(language);


        public Language Get(long id) => _service.Get(id);
        

        public IEnumerable<Language> GetAll() => _service.GetAll();


        public void New(Language language) => _service.New(language);


        public void Delete(Language language) => _service.Delete(language);



    }
}