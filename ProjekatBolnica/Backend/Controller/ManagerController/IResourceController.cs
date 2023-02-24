/***********************************************************************
 * Module:  IResourceController.cs
 * Purpose: Definition of the Interface Controller.ManagerController.IResourceController
 ***********************************************************************/

using System.Collections.Generic;
using Controller.HospitalController;
using Model.ManagerModel;

namespace Controller.ManagerController
{
    public interface IResourceController : IController<Resource, long>
    {
        void IncreaseQuantity(long id, int quantity);
        void DecreaseQuantity(long id, int quantity);
        int GetAmountOfResource(long id);
        int CountNumberOfBedsInRoom(Room room);
        List<Bed> GetBedsInRoom(Room room);
    }
}