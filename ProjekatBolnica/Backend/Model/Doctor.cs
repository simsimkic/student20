/***********************************************************************
 * Module:  Doctor.cs
 * Purpose: Definition of the Class Doctor
 ***********************************************************************/

using Model.DoctorModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using System;

namespace Model
{
    public class Doctor : User
    {
        public Doctor() {}
        public Doctor(long id)
        {
            Id = id;
        }
        public Language Language { get; set; }
        public Theme Theme { get; set; }
    }
}