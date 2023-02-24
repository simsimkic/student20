/***********************************************************************
 * Module:  Bed.cs
 * Purpose: Definition of the Class Model.ManagerModel.Bed
 ***********************************************************************/

using Model.UtilityModel;

namespace Model.ManagerModel
{
    public class Bed : Resource
    {
        public TimePeriod LayingInBed { get; set; }

        public Bed(TimePeriod layingInBed)
        {
            LayingInBed = layingInBed;
        }

        public Bed(long id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Name + "_" + Id;
        }
    }
}