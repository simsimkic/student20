/***********************************************************************
 * Module:  OperationController.cs
 * Purpose: Definition of the Class Controller.HospitalController.OperationController
 ***********************************************************************/

using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.UtilityModel;
using Service.HospitalServices;
using System;
using System.Collections.Generic;

namespace Controller.HospitalController
{
   public class OperationController : IOperationController
   {

        private readonly IOperationService _service;

        public OperationController(IOperationService service)
        {
            _service = service;
        }
        public int CountOperationsInRoom(Room room, TimePeriod timePeriod) => _service.CountOperationsInRoom(room, timePeriod);
        public void Set(Operation operation) => _service.Set(operation);
        public Operation Get(long id) => _service.Get(id);
        IEnumerable<Operation> IController<Operation, long>.GetAll() => _service.GetAll();
        public void New(Operation operation) => _service.New(operation);
        public void Delete(Operation operation) => _service.Delete(operation);
        public List<Operation> GetActivityByDoctor(Doctor doctor) => _service.GetActivityByDoctor(doctor);
        public List<Operation> GetActivityByDate(DateTime date) => _service.GetActivityByDate(date);
        public List<Operation> GetActivityByPatient(Patient patient) => _service.GetActivityByPatient(patient);
        public List<Operation> GetActivityByRoom(Room room) => _service.GetActivityByRoom(room);
        public List<DateTime> GetAvailableTermsForRoomAndDoctorAndDate(Doctor doctor, DateTime term, Room room) => _service.GetAvailableTermsForRoomAndDoctorAndDate(doctor, term, room);
    }
}