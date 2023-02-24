/***********************************************************************
 * Module:  IFeedbackRepository.cs
 * Purpose: Definition of the Interface Repository.UserRepository.IFeedbackRepository
 ***********************************************************************/

using ProjekatBolnica.Backend.Model.UserModel;
using Repository.HospitalRepository;
using System;

namespace Repository.UserRepository
{
   public interface IFeedbackRepository : IRepository<Feedback, long>
   {
   }
}