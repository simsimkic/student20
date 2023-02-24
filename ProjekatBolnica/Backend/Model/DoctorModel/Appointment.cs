/***********************************************************************
 * Module:  Appointment.cs
 * Purpose: Definition of the Class Doctor.Appointment
 ***********************************************************************/

using System;
using Model.ManagerModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.Backend.Repository;

namespace Model.DoctorModel
{
    public class Appointment : Activity
    {
        public Doctor Doctor { get; set; }
        public Appointment() { }

        public Appointment(long id, Patient patient, Doctor doctor, Room room, DateTime dateAndTime, TimeSpan duration)
            : base(id, patient, room, dateAndTime, duration)
        {
            this.Doctor = doctor;
        }

        public Appointment(Patient patient, Doctor doctor, DateTime dateAndTime)
    : base(patient, dateAndTime)
        {
            this.Doctor = doctor;
        }

        internal string ToDescription()
        {
            return "Patient: " + this.Patient.Name + " " + this.Patient.Surname + " " + this.Patient.PersonalIDnumber
                + " - Doctor: " + this.Doctor.Name + " " + this.Doctor.Surname 
                + " - Date: " + this.DateAndTime.ToString()
                + " - Room: " + this.Room.Id;
        }
    }
}