/***********************************************************************
 * Module:  INotificationRepository.cs
 * Purpose: Definition of the Interface Repository.UserRepository.INotificationRepository
 ***********************************************************************/

using Model;
using Model.UserModel;
using Model.UtilityModel;
using Repository.HospitalRepository;
using Syncfusion.XPS;
using System;
using System.Collections.Generic;

namespace Repository.UserRepository
{
   public interface INotificationRepository : IRepository<Notification, long>
   {
        List<Notification> GetNotificationsForUser(User user);
   }
}