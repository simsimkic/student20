/***********************************************************************
 * Module:  SurveyController.cs
 * Purpose: Definition of the Class Controller.PatientController.SurveyController
 ***********************************************************************/

using Controller.HospitalController;
using Model.PatientModel;
using Service.HospitalServices;
using Service.PatientServices;
using System;
using System.Collections.Generic;

namespace Controller.PatientController
{
   public class SurveyController : IController<Survey, long>
    {
        private readonly IService<Survey, long> _service;


        public SurveyController(IService<Survey, long> service)
        {
            _service = service;
        }

        public void Delete(Survey survey) => _service.Delete(survey);

        public Survey Get(long id) => _service.Get(id);

        public IEnumerable<Survey> GetAll() => _service.GetAll();

        public void New(Survey survey) => _service.New(survey);

        public void Set(Survey survey) => _service.Set(survey);

    }
}