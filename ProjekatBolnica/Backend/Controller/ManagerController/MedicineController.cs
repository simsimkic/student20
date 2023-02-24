/***********************************************************************
 * Module:  MedicineController.cs
 * Purpose: Definition of the Class Controller.ManagerController.MedicineController
 ***********************************************************************/

using Model.ManagerModel;
using ProjekatBolnica.Backend.Model.UserModel;
using Service.ManagerService;
using System.Collections.Generic;

namespace Controller.ManagerController
{
    public class MedicineController : IMedicineController
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public void Delete(Medicine medicine) => _medicineService.Delete(medicine);

        public Medicine Get(long id) => _medicineService.Get(id);

        public IEnumerable<Medicine> GetAll() => _medicineService.GetAll();

        public void New(Medicine medicine) => _medicineService.New(medicine);

        public void Set(Medicine medicine) => _medicineService.Set(medicine);

        public void SendApprovalRequest(Feedback message) => _medicineService.SendApprovalRequest(message);

        public IEnumerable<Medicine> GetAllUnapproved() => _medicineService.GetAllUnapproved();

        public void DeleteUnapproved(Medicine medicine) => _medicineService.DeleteUnapproved(medicine);
    }
}