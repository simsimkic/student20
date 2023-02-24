/***********************************************************************
 * Module:  Perscription.cs
 * Purpose: Definition of the Class Doctor.Perscription
 ***********************************************************************/

using System;
using System.Collections.Generic;
using Model.ManagerModel;
using Model.UtilityModel;
using Syncfusion.XPS;

namespace Model.DoctorModel
{
   public class Perscription
   {
        public List<Medicine> Medicine { get; set; }

        public long _token { get; set; }

        public Perscription() { }

        public Perscription(long token)
        {
            _token = token;
        }
      
        public TimePeriod PeriodOfValidity;
   
   }
}