/***********************************************************************
 * Module:  ILanguageRepository.cs
 * Purpose: Definition of the Interface Repository.UserRepository.ILanguageRepository
 ***********************************************************************/

using Model.UserModel;
using Repository.HospitalRepository;
using System;

namespace Repository.UserRepository
{
   public interface ILanguageRepository : IRepository<Language, long>
   {
   }
}