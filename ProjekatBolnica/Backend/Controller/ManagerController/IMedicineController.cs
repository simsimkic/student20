/***********************************************************************
 * Module:  IMedicineController.cs
 * Purpose: Definition of the Interface Controller.ManagerController.IMedicineController
 ***********************************************************************/

using Controller.HospitalController;
using Model.ManagerModel;
using ProjekatBolnica.Backend.Model.UserModel;
using System.Collections.Generic;

namespace Controller.ManagerController
{
    public interface IMedicineController : IController<Medicine, long>
    {
        void SendApprovalRequest(Feedback message);
        IEnumerable<Medicine> GetAllUnapproved();
        void DeleteUnapproved(Medicine medicine);
    }
}