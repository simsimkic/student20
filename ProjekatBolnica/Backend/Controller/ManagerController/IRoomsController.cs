/***********************************************************************
 * Module:  IRoomsController.cs
 * Purpose: Definition of the Interface Controller.ManagerController.IRoomsController
 ***********************************************************************/

using Controller.HospitalController;
using Model.ManagerModel;
using Model.UtilityModel;
using System.Collections.Generic;

namespace Controller.ManagerController
{
    public interface IRoomsController : IController<Room, long>
    {
        void AnnounceAction(Notification n);
        List<Room> GetAllControlRooms();
        List<Room> GetAllOperationRooms();
        List<Room> GetAllRoomsForLaying();
    }
}