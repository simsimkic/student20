/***********************************************************************
 * Module:  Specialty.cs
 * Purpose: Definition of the Class Doctor.Specialty
 ***********************************************************************/

using System;
using ProjekatBolnica.Backend.Repository;

namespace Model.DoctorModel
{
    public class Specialty : IIdentifiable<long>
    {
        public Specialty() { }
        public Specialty(long id)
        {
            Id = id;
        }
        public string SpecialtyName { get; set; }
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