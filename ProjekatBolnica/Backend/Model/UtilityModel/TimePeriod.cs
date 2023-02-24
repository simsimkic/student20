/***********************************************************************
 * Module:  TimePeriod.cs
 * Purpose: Definition of the Struct Model.UtilityModel.TimePeriod
 ***********************************************************************/

using System;

namespace Model.UtilityModel
{
    public struct TimePeriod
    {
        public static string DATE_FORMAT = "dd.MM.yyyy.";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public override string ToString()
        {
            return StartTime.ToString(DATE_FORMAT) + " - " + EndTime.ToString(DATE_FORMAT);
        }
    }
}