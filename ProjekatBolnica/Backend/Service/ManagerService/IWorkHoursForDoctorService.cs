/***********************************************************************
 * Module:  IWorkHoursForDoctorService.cs
 * Purpose: Definition of the Interface Service.ManagerService.IWorkHoursForDoctorService
 ***********************************************************************/

using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using Service.HospitalServices;
using System;
using System.Collections.Generic;

namespace Service.ManagerService
{
    public interface IWorkHoursForDoctorService : IService<WorkHoursForDoctor, long>
    {
        int CalculateNumberOfDoctorsOnDuty(int dayInWeek);
        int CalculateNumberOfDoctorsOnVacation(int dayInWeek);
        List<double> CalculateOvertimeWorkInPercantage(List<int> overtimeAnnualWork, List<int> overtimeWeeklyWork);
        bool OccupationOfDoctors(DateTime date, Doctor doctor);
        TimePeriod GetDoctorsShift(Doctor doctor, DateTime date);
    }
}