using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DoctorModel;
using Repository.HospitalRepository;

namespace ProjekatBolnica.Backend.Repository.HospitalRepository
{
    public interface ISpecialtyRepository : IRepository<Specialty, long>
    {
    }
}
