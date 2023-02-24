/***********************************************************************
 * Module:  IService.cs
 * Purpose: Definition of the Interface Service.HospitalServices.IService
 ***********************************************************************/

using System.Collections.Generic;

namespace Service.HospitalServices
{
    public interface IService<E, ID> where E : class
    {
        void Set(E obj);
        E Get(ID id);
        IEnumerable<E> GetAll();
        void New(E obj);
        void Delete(E obj);
    }
}