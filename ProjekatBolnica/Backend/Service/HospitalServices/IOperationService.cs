/***********************************************************************
 * Module:  IOperationService.cs
 * Purpose: Definition of the Interface Service.HospitalServices.IOperationService
 ***********************************************************************/

using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.UtilityModel;
using System;
using System.Collections.Generic;

namespace Service.HospitalServices
{
   public interface IOperationService : IActivitySevice<Operation, long>, IService<Operation, long>
    {
        int CountOperationsInRoom(Room room, TimePeriod timePeriod);
        List<DateTime> GetAvailableTermsForRoomAndDoctorAndDate(Doctor doctor, DateTime term, Room room);
    }
}