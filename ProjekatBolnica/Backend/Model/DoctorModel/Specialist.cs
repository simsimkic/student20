/***********************************************************************
 * Module:  Specialist.cs
 * Purpose: Definition of the Class Doctor.Specialist
 ***********************************************************************/

using System;
using ProjekatBolnica.Backend.Repository;

namespace Model.DoctorModel
{
    public class Specialist : Doctor
    {
        public Specialist() { }
        public Specialist(long id)
        {
            Id = id;
        }
        public Specialty Specialty { get; set; }
    }
}