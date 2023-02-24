using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ManagerModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Repository;

namespace ProjekatBolnica.Backend.Model.DoctorModel
{
    public class HospitalStay : IIdentifiable<long>
    {
        public HospitalStay() { }
        public Patient Patient { get; set; }
        public Bed Bed { get; set; }
        public TimePeriod Duration { get; set; }
        public long Id { get; set; }
        public long GetId()
        {
            return Id;
        }

        public void SetId(long id)
        {
            Id = id;
        }
    }
}
