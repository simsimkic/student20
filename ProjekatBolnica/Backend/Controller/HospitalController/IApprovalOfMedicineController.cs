/***********************************************************************
 * Module:  IApprovalOfMedicineController.cs
 * Purpose: Definition of the Interface Controller.HospitalController.IApprovalOfMedicineController
 ***********************************************************************/

using Model.ManagerModel;
using Model.UtilityModel;
using System;

namespace Controller.HospitalController
{
   public interface IApprovalOfMedicineController
   {
        void ApproveMedicine(Notification notification);
        void DisapproveMedicine(Notification notification);
        Medicine SearchMedicine(String query);
    }
}