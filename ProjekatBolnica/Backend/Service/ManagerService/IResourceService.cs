/***********************************************************************
 * Module:  IResourceService.cs
 * Purpose: Definition of the Interface Service.ManagerService.IResourceService
 ***********************************************************************/

using System.Collections.Generic;
using Model.ManagerModel;
using Service.HospitalServices;

namespace Service.ManagerService
{
    public interface IResourceService : IService<Resource, long>
    {
        void IncreaseQuantity(long id, int quantity);
        void DecreaseQuantity(long id, int quantity);
        int GetAmountOfResource(long id);
        int CountNumberOfBedsInRoom(Room room);
        List<Bed> GetBedsInRoom(Room room);
    }
}