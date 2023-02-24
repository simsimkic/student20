/***********************************************************************
 * Module:  ApprovalOfMedicineController.cs
 * Purpose: Definition of the Class Controller.HospitalController.ApprovalOfMedicineController
 ***********************************************************************/

using Model.ManagerModel;
using Model.UtilityModel;
using Service.HospitalServices;
using System;

namespace Controller.HospitalController
{
   public class ApprovalOfMedicineController : IApprovalOfMedicineController
   {
        private readonly IApprovalOfMedicineService _service;
        public ApprovalOfMedicineController(IApprovalOfMedicineService service)
        {
            _service = service;
        }

        public void ApproveMedicine(Notification notification) => _service.ApproveMedicine(notification);

        public void DisapproveMedicine(Notification notification) => _service.DisapproveMedicine(notification);

        public Medicine SearchMedicine(string query)
        {
            throw new NotImplementedException();
        }
    }
}