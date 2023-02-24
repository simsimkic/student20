/***********************************************************************
 * Module:  WorkHoursForDoctorController.cs
 * Purpose: Definition of the Class Controller.ManagerController.WorkHoursForDoctorController
 ***********************************************************************/

using Model;
using Model.ManagerModel;
using Service.ManagerService;
using System;
using System.Collections.Generic;

namespace Controller.ManagerController
{
    public class WorkHoursForDoctorController : IWorkHoursForDoctorController
    {
        private readonly IWorkHoursForDoctorService _service;

        public WorkHoursForDoctorController(IWorkHoursForDoctorService service)
        {
            _service = service;
        }

        public void Delete(WorkHoursForDoctor workHours) => _service.Delete(workHours);

        public WorkHoursForDoctor Get(long id) => _service.Get(id);

        public IEnumerable<WorkHoursForDoctor> GetAll() => _service.GetAll();

        public void New(WorkHoursForDoctor workHours) => _service.New(workHours);

        public void Set(WorkHoursForDoctor workHours) => _service.Set(workHours);

        public int CalculateNumberOfDoctorsOnDuty(int dayInWeek)
            => _service.CalculateNumberOfDoctorsOnDuty(dayInWeek);

        public int CalculateNumberOfDoctorsOnVacation(int dayInWeek)
            => _service.CalculateNumberOfDoctorsOnVacation(dayInWeek);

        public List<double> CalculateOvertimeWorkInPercantage(List<int> overtimeAnnualWork, List<int> overtimeWeeklyWork)
            => _service.CalculateOvertimeWorkInPercantage(overtimeAnnualWork, overtimeWeeklyWork);

        public bool OccupationOfDoctors(DateTime date, Doctor doctor)
            => _service.OccupationOfDoctors(date, doctor);
    }
}