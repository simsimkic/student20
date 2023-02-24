/***********************************************************************
 * Module:  IApprovalOfMedicineService.cs
 * Purpose: Definition of the Interface Service.HospitalServices.IApprovalOfMedicineService
 ***********************************************************************/

using Model.ManagerModel;
using Model.UtilityModel;
using System;

namespace Service.HospitalServices
{
   public interface IApprovalOfMedicineService
   {
        void ApproveMedicine(Notification notification);
        void DisapproveMedicine(Notification notification);
        Medicine SearchMedicine(String query);
   }
}