/***********************************************************************
 * Module:  WorkHoursForDoctor.cs
 * Purpose: Definition of the Class Model.ManagerModel.WorkHoursForDoctor
 ***********************************************************************/

using Model.UtilityModel;
using ProjekatBolnica.Backend.Repository;
using System.Collections.Generic;

namespace Model.ManagerModel
{
    public class WorkHoursForDoctor : Doctor, IIdentifiable<long>
    {
        public Doctor Doctor { get; set; }

        public List<TimePeriod> Shift { get; set; }

        public int DaysForVacation { get; set; }

        public int OvertimeAnnualWork { get; set; }

        public int OvertimeWeeklyWork { get; set; }

        public WorkHoursForDoctor(Doctor doctor, List<TimePeriod> shift, int daysForVacation, int overtimeAnnualWork, int overtimeWeeklyWork)
        {
            Doctor = doctor;
            Shift = shift;
            DaysForVacation = daysForVacation;
            OvertimeAnnualWork = overtimeAnnualWork;
            OvertimeWeeklyWork = overtimeWeeklyWork;
        }
    }
}