/***********************************************************************
 * Module:  IMedicineService.cs
 * Purpose: Definition of the Interface Service.ManagerService.IMedicineService
 ***********************************************************************/

using Model.ManagerModel;
using ProjekatBolnica.Backend.Model.UserModel;
using Service.HospitalServices;
using System.Collections.Generic;

namespace Service.ManagerService
{
    public interface IMedicineService : IService<Medicine, long>
    {
        void SendApprovalRequest(Feedback message);

        IEnumerable<Medicine> GetAllUnapproved();

        void DeleteUnapproved(Medicine medicine);
    }
}