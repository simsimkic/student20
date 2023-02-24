/***********************************************************************
 * Module:  IActivity.cs
 * Purpose: Definition of the Interface Repository.HospitalRepository.IActivity
 ***********************************************************************/

using Model;
using Model.ManagerModel;
using ProjekatBolnica.Backend.Repository;
using System;
using System.Collections.Generic;

namespace Repository.HospitalRepository
{
   public interface IActivity<E, ID>
        where E : IIdentifiable<ID>
        where ID : IComparable
    {
      List<E> GetActivityByDoctor(Doctor doctor);
      List<E> GetActivityByDate(DateTime date);
      List<E> GetActivityByPatient(Patient patient);
      List<E> GetActivityByRoom(Room room);
   }
}