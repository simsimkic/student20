/***********************************************************************
 * Module:  IAppointmentRepository.cs
 * Purpose: Definition of the Interface Repository.HospitalRepository.IAppointmentRepository
 ***********************************************************************/

using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using ProjekatBolnica.Backend.Repository.CSV;
using Service.HospitalServices;
using Syncfusion.XPS;
using System;
using System.Collections.Generic;

namespace Repository.HospitalRepository
{
   public interface IAppointmentRepository : IActivity<Appointment, long>, IRepository<Appointment, long>, IEagerCSVRepository<Appointment, long>
   {
   }
}