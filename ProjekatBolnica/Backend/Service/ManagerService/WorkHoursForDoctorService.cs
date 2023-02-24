/***********************************************************************
 * Module:  WorkHoursForDoctorService.cs
 * Purpose: Definition of the Class Service.ManagerService.WorkHoursForDoctorService
 ***********************************************************************/

using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using Repository.ManagerRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ManagerService
{
    public class WorkHoursForDoctorService : IWorkHoursForDoctorService
    {
        private readonly IWorkHoursForDoctorRepository _repository;

        public WorkHoursForDoctorService(IWorkHoursForDoctorRepository repository)
        {
            _repository = repository;
        }

        public void Delete(WorkHoursForDoctor workHours) => _repository.Delete(workHours);

        public WorkHoursForDoctor Get(long id) => _repository.Get(id);

        public IEnumerable<WorkHoursForDoctor> GetAll() => _repository.GetAll();

        public void New(WorkHoursForDoctor workHours) => _repository.New(workHours);

        public void Set(WorkHoursForDoctor workHours) => _repository.Set(workHours);

        public TimePeriod GetDoctorsShift(Doctor doctor, DateTime date)
        {
            TimePeriod shift = new TimePeriod();
            List<WorkHoursForDoctor> shifts = _repository.GetAll().Where(x => x.Doctor.Id == doctor.Id).ToList();
            foreach (WorkHoursForDoctor workHours in shifts)
            {
                foreach (TimePeriod timePeriod in workHours.Shift)
                {
                    if (timePeriod.StartTime.Date.Equals(date.Date))
                    {
                        return shift = timePeriod;
                    }
                }
            }
            return shift;
        }

        public bool OccupationOfDoctors(DateTime date, Doctor doctor)
        {
            TimePeriod shift = new TimePeriod();
            shift = GetDoctorsShift(doctor, date);

            if (shift.StartTime.ToString().Equals(shift.EndTime.ToString()))
                return false;

            return true;
        }

        public int CalculateNumberOfDoctorsOnDuty(int dayInWeek)
        {
            int onDuty = 0;
            for (int i = 0; i < GetAll().Count(); i++)
                if (GetAll().ToList()[i].Shift[dayInWeek].StartTime.ToString().Equals(GetAll().ToList()[i].Shift[dayInWeek].EndTime.ToString()))
                    onDuty++;
            return onDuty;
        }

        public int CalculateNumberOfDoctorsOnVacation(int dayInWeek)
        {
            return GetAll().Count() - CalculateNumberOfDoctorsOnDuty(dayInWeek);
        }

        public List<double> CalculateOvertimeWorkInPercantage(List<int> overtimeAnnualWork, List<int> overtimeWeeklyWork)
        {
            double allAnnual = (double)AddAllAnnualWork(overtimeAnnualWork);
            double allWeekly = (double)AddAllWeeklyWork(overtimeWeeklyWork);

            List<double> allPerc = new List<double>();
            for (int i = 0; i < overtimeAnnualWork.Count; i++)
                allPerc.Add(((double)overtimeAnnualWork[i] / allAnnual + (double)overtimeWeeklyWork[i] / allWeekly) / 2);

            return allPerc;
        }

        private int AddAllAnnualWork(List<int> overtimeAnnualWork)
        {
            int allAnnual = 0;
            for (int i = 0; i < overtimeAnnualWork.Count; i++)
                allAnnual += overtimeAnnualWork[i];
            return allAnnual;
        }

        private int AddAllWeeklyWork(List<int> overtimeWeeklyWork)
        {
            int allWeekly = 0;
            for (int i = 0; i < overtimeWeeklyWork.Count; i++)
                allWeekly += overtimeWeeklyWork[i];
            return allWeekly;
        }

    }
}