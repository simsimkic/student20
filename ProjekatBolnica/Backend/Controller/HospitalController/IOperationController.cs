/***********************************************************************
 * Module:  IOperationController.cs
 * Purpose: Definition of the Interface Controller.HospitalController.IOperationController
 ***********************************************************************/

using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using System;
using System.Collections.Generic;

namespace Controller.HospitalController
{
   public interface IOperationController : IController<Operation, long>
    {
        int CountOperationsInRoom(Model.ManagerModel.Room room, Model.UtilityModel.TimePeriod timePeriod);
        List<Operation> GetActivityByDoctor(Doctor doctor);
        List<Operation> GetActivityByDate(DateTime date);
        List<Operation> GetActivityByPatient(Patient patient);
        List<Operation> GetActivityByRoom(Room room);
        List<DateTime> GetAvailableTermsForRoomAndDoctorAndDate(Doctor doctor, DateTime term, Room room);
    }
}