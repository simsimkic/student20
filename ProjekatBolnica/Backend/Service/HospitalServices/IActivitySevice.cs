/***********************************************************************
 * Module:  IActivitySevice.cs
 * Purpose: Definition of the Interface Service.HospitalServices.IActivitySevice
 ***********************************************************************/

using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using System;
using System.Collections.Generic;

namespace Service.HospitalServices
{
   public interface IActivitySevice<E, ID> where E : class /*: IService<Object, long>*/
    {
      Boolean IsValidAppointment(E entity);
      Boolean IsAvailableDoctor(Doctor doctor, DateTime term);
      Boolean IsAvailableTerm(DateTime term, List<E> obj);
      Boolean IsAvailableRoom(Room room, DateTime term);
      List<E> GetActivityByDoctor(Doctor doctor);
      List<E> GetActivityByDate(DateTime date);
      List<E> GetActivityByPatient(Patient patient);
      List<E> GetActivityByRoom(Room room);
    }
}