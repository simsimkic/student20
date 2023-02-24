/***********************************************************************
 * Module:  Operation.cs
 * Purpose: Definition of the Class Doctor.Operation
 ***********************************************************************/

using Model.ManagerModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.Backend.Repository;
using System;

namespace Model.DoctorModel
{
   public class Operation :  Activity, IIdentifiable<long>
    {
        public Operation() { }
        public Specialist Specialist { get; set; }

        internal string ToDescription()
        {
            return "Patient: " + this.Patient.Name + " " + this.Patient.Surname + " " + this.Patient.PersonalIDnumber
                + " - Doctor: " + this.Specialist.Name + " " + this.Specialist.Surname
                + " - Date: " + this.DateAndTime.ToString()
                + " - Room: " + this.Room.Id;
        }


    }
}