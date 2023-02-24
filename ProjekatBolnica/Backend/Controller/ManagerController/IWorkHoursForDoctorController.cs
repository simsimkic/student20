/***********************************************************************
 * Module:  IWorkHoursForDoctorController.cs
 * Purpose: Definition of the Interface Controller.ManagerController.IWorkHoursForDoctorController
 ***********************************************************************/

using Controller.HospitalController;
using Model;
using Model.ManagerModel;
using System;
using System.Collections.Generic;

namespace Controller.ManagerController
{
    public interface IWorkHoursForDoctorController : IController<WorkHoursForDoctor, long>
    {
        int CalculateNumberOfDoctorsOnDuty(int dayInWeek);
        int CalculateNumberOfDoctorsOnVacation(int dayInWeek);
        List<double> CalculateOvertimeWorkInPercantage(List<int> overtimeAnnualWork, List<int> overtimeWeeklyWork);
        bool OccupationOfDoctors(DateTime date, Doctor doctor);
    }
}