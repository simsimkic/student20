/***********************************************************************
 * Module:  ApprovalOfMedicineService.cs
 * Purpose: Definition of the Class Service.HospitalServices.ApprovalOfMedicineService
 ***********************************************************************/

using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Service.UserService;
using Repository.ManagerRepository;
using Repository.UserRepository;
using System;
using System.Linq;
using System.Windows;

namespace Service.HospitalServices
{
   public class ApprovalOfMedicineService : IApprovalOfMedicineService
   {
        private readonly INotificationRepository _notificationRepository;
        public ApprovalOfMedicineService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public void ApproveMedicine(Notification notification)
        {
            string message = "Medicine is approved. " + notification.Description;
            notification.Description = message;
            _notificationRepository.New(notification);
        }
      
         public Medicine SearchMedicine(String query)
        {
         // TODO: implement
         return null;
        }
      
        public void DisapproveMedicine(Notification notification)
        {
            string message = "Medicine is not approved. " + notification.Description;
            notification.Description = message;
            _notificationRepository.New(notification);
        }
   }
}