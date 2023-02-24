using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DoctorModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;

namespace ProjekatBolnica.Backend.Repository.HospitalRepository
{
    public class SpecialtyRepository : 
        CSVRepository<Specialty, long>,
        ISpecialtyRepository
    {
        private const String ENTITY_NAME = "Specialty";
        public SpecialtyRepository(ICSVStream<Specialty> stream, ISequencer<long> sequencer) : base(ENTITY_NAME, stream, sequencer) { }
    }
}
