using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using System;
using System.Collections.Generic;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.ManagerConverter
{
    class WorkHoursForDoctorCSVConverter : ICSVConverter<WorkHoursForDoctor>
    {
        private readonly String _delimiter;
        private readonly String _datetimeFormat;

        public WorkHoursForDoctorCSVConverter(String delimiter, String datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
        }

        public WorkHoursForDoctor ConvertCSVFormatToEntity(String entityCSVFormat)
        {
            String[] tokens = entityCSVFormat.Split(_delimiter.ToCharArray());

            Doctor dr = new Doctor(long.Parse(tokens[0]));

            List<TimePeriod> shift = new List<TimePeriod>();

            TimePeriod duration1 = new TimePeriod();
            duration1.StartTime = DateTime.Parse(tokens[1]);
            duration1.EndTime = DateTime.Parse(tokens[2]);
            TimePeriod duration2 = new TimePeriod();
            duration2.StartTime = DateTime.Parse(tokens[3]);
            duration2.EndTime = DateTime.Parse(tokens[4]);
            TimePeriod duration3 = new TimePeriod();
            duration3.StartTime = DateTime.Parse(tokens[5]);
            duration3.EndTime = DateTime.Parse(tokens[6]);
            TimePeriod duration4 = new TimePeriod();
            duration4.StartTime = DateTime.Parse(tokens[7]);
            duration4.EndTime = DateTime.Parse(tokens[8]);
            TimePeriod duration5 = new TimePeriod();
            duration5.StartTime = DateTime.Parse(tokens[9]);
            duration5.EndTime = DateTime.Parse(tokens[10]);
            TimePeriod duration6 = new TimePeriod();
            duration6.StartTime = DateTime.Parse(tokens[11]);
            duration6.EndTime = DateTime.Parse(tokens[12]);
            TimePeriod duration7 = new TimePeriod();
            duration7.StartTime = DateTime.Parse(tokens[13]);
            duration7.EndTime = DateTime.Parse(tokens[14]);

            shift.Add(duration1);
            shift.Add(duration2);
            shift.Add(duration3);
            shift.Add(duration4);
            shift.Add(duration5);
            shift.Add(duration6);
            shift.Add(duration7);

            return new WorkHoursForDoctor(dr,
                 shift, Int32.Parse(tokens[15]),
                 Int32.Parse(tokens[16]),
                 Int32.Parse(tokens[17]));
        }

        public string ConvertEntityToCSVFormat(WorkHoursForDoctor entity)
            => String.Join(_delimiter,
                   entity.Doctor.GetId(),
                   entity.Shift[0].StartTime.ToString(_datetimeFormat),
                   entity.Shift[0].EndTime.ToString(_datetimeFormat),
                   entity.Shift[1].StartTime.ToString(_datetimeFormat),
                   entity.Shift[1].EndTime.ToString(_datetimeFormat),
                   entity.Shift[2].StartTime.ToString(_datetimeFormat),
                   entity.Shift[2].EndTime.ToString(_datetimeFormat),
                   entity.Shift[3].StartTime.ToString(_datetimeFormat),
                   entity.Shift[3].EndTime.ToString(_datetimeFormat),
                   entity.Shift[4].StartTime.ToString(_datetimeFormat),
                   entity.Shift[4].EndTime.ToString(_datetimeFormat),
                   entity.Shift[5].StartTime.ToString(_datetimeFormat),
                   entity.Shift[5].EndTime.ToString(_datetimeFormat),
                   entity.Shift[6].StartTime.ToString(_datetimeFormat),
                   entity.Shift[6].EndTime.ToString(_datetimeFormat),
                   entity.DaysForVacation,
                   entity.OvertimeAnnualWork,
                   entity.OvertimeWeeklyWork);
    }
}
