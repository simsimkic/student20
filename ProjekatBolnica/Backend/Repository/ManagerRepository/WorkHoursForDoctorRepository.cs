/***********************************************************************
 * Module:  WorkHoursForDoctorRepository.cs
 * Purpose: Definition of the Class Repository.ManagerRepository.WorkHoursForDoctorRepository
 ***********************************************************************/

using Model.ManagerModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.ManagerRepository
{
    public class WorkHoursForDoctorRepository : CSVRepository<WorkHoursForDoctor, long>,
        IWorkHoursForDoctorRepository,
        IEagerCSVRepository<WorkHoursForDoctor, long>
    {
        private const String ENTITY_NAME = "WorkHoursForDoctor";

        public WorkHoursForDoctorRepository(ICSVStream<WorkHoursForDoctor> stream, ISequencer<long> sequencer)
            : base(ENTITY_NAME, stream, sequencer)
        {
        }

        public new IEnumerable<WorkHoursForDoctor> Find(Func<WorkHoursForDoctor, bool> predicate)
             => GetAllEager().Where(predicate);

        public new void New(WorkHoursForDoctor workHoursForDoctor) => base.New(workHoursForDoctor);

        public IEnumerable<WorkHoursForDoctor> GetAllEager() => GetAll();

        public WorkHoursForDoctor GetEager(long id) => Get(id);
    }
}